using Microsoft.EntityFrameworkCore;
using Pokedex.Data;
using Pokedex.Repository;
using Pokedex.Repository.Interfaces;
using Pokedex.Services;
using Pokedex.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Configura o contexto do banco de dados
builder.Services.AddDbContext<PokemonContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("PokemonConnection")));

// Registra o repositório e o serviço
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
builder.Services.AddScoped<IPokemonServices, PokemonService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
