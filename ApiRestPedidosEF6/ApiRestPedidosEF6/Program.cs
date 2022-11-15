using ApiRestPedidosEF6.Interfaces;
using ApiRestPedidosEF6.Models;
using ApiRestPedidosEF6.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<Contexto>(Options=>
 {
     Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
 });


   
string con = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IClienteRep, ClienteRep>();
builder.Services.AddScoped<IItemRep, ItemRep>();
builder.Services.AddScoped<IPedidoRep, PedidoRep>();
builder.Services.AddScoped<IItemPedidoRep, ItemPedidoRep>();

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
