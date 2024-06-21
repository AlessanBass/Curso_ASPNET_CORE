using System.Text.Json.Serialization;
using APICatalogo.Context;
using APICatalogo.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions
        .ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Obtendo a string de conexão
var mysqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
//Adicionando no containner
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseMySql(mysqlConnection, 
    ServerVersion.AutoDetect(mysqlConnection)));

/* Precisamos registrar os serviços */
builder.Services.AddScoped<ICategoriaRepository,CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository,ProdutoRepository>();
/* AddScoped não permite tipos genercios etão faremos o seguite: */
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));

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
