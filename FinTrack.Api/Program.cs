using FinTrack.Api.Data;
using FinTrack.Api.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("A connection string 'DefaultConnection' não foi configurada.");

var databasePath = connectionString.Split("Data Source=").Last().Split(';')[0];
var databaseDirectory = Path.GetDirectoryName(databasePath);
if (!string.IsNullOrWhiteSpace(databaseDirectory))
{
    Directory.CreateDirectory(databaseDirectory);
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<TransacaoService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

// Swagger fica habilitado também no container para facilitar a avaliação do desafio.
// Em produção, restrinja ou desabilite a documentação conforme a política da organização.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

// Aplica migrations pendentes na inicialização. A migration InitialCreate deve existir
// (gerada pelo comando documentado no README) antes de executar a aplicação.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.Run();

public partial class Program { }
