using Microsoft.EntityFrameworkCore;
using NoteTagr.Api.Notes.Application.Internal.CommandServices;
using NoteTagr.Api.Notes.Application.Internal.QueryServices;
using NoteTagr.Api.Notes.Domain.Repositories;
using NoteTagr.Api.Notes.Domain.Services;
using NoteTagr.Api.Notes.Infrastructure.Persistence.EFC.Repositories;
using NoteTagr.Api.Shared.Domain.Repositories;
using NoteTagr.Api.Shared.Persistence.EFC.Configuration;
using NoteTagr.Api.Shared.Persistence.EFC.Repositories;

var builder = WebApplication.CreateBuilder(args);

//apply route
builder.Services.AddControllers();

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
builder.Services.AddSwaggerGen();

//shared bounded context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Notes bounded context
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<INoteCommandService, NoteCommandService>();
builder.Services.AddScoped<INoteQueryService, NoteQueryService>();


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.MapControllers();

app.Run();