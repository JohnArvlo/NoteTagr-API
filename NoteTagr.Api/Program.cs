using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NoteTagr.Api.IAM.Application.ACL.Services;
using NoteTagr.Api.IAM.Application.Internal.CommandServices;
using NoteTagr.Api.IAM.Application.Internal.OutboundServices;
using NoteTagr.Api.IAM.Application.Internal.QueryServices;
using NoteTagr.Api.IAM.Domain.Repositories;
using NoteTagr.Api.IAM.Domain.Services;
using NoteTagr.Api.IAM.Infrastructure.Hashing;
using NoteTagr.Api.IAM.Infrastructure.Persistence;
using NoteTagr.Api.IAM.Infrastructure.Tokens.JWT.Configuration;
using NoteTagr.Api.IAM.Infrastructure.Tokens.JWT.Services;
using NoteTagr.Api.IAM.Interfaces.ACL;
using NoteTagr.Api.Notes.Application.Internal.CommandServices;
using NoteTagr.Api.Notes.Application.Internal.QueryServices;
using NoteTagr.Api.Notes.Domain.Repositories;
using NoteTagr.Api.Notes.Domain.Services;
using NoteTagr.Api.Notes.Infrastructure.Persistence.EFC.Repositories;
using NoteTagr.Api.Shared.Domain.Repositories;
using NoteTagr.Api.Shared.Persistence.EFC.Configuration;
using NoteTagr.Api.Shared.Persistence.EFC.Repositories;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

//apply route
builder.Services.AddControllers();

//Add CORS Policy (to all controllers)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => 
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
});


// Add database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString is null)
    throw new Exception("Connection string is null");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString);
});

// OpenAPI/Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
    options.EnableAnnotations();
});

//shared bounded context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Notes bounded context
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<INoteCommandService, NoteCommandService>();
builder.Services.AddScoped<INoteQueryService, NoteQueryService>();

builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ITagCommandService, TagCommandService>();
builder.Services.AddScoped<ITagQueryService, TagQueryService>();

// Profiles Bounded Context Dependency Injection Configuration
/*
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();
*/

// TokenSettings Configuration
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                builder.Configuration["TokenSettings:Secret"]!)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });



var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    //create and drop
    // context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();