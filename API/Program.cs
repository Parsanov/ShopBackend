using Application.Repository.Category;
using Application.Repository.Shoes;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IXMLCategoriesParser, XMLCategoriesParser>();
builder.Services.AddScoped<IDBCategoryRepository, DBCategoryRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IDBShoesRepository, DBShoesRepository>();
builder.Services.AddScoped<IShoesRepository, ShoesRepository>();
builder.Services.AddScoped<IXMLShoesParser, XMLShoesParser>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
