using AppleStore.ApplicationLayer.Interfaces;
using AppleStore.ApplicationLayer.Services;
using AppleStore.DataAccess;
using AppleStore.DataAccess.Interfaces;
using AppleStore.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AppleStore API", Version = "v1" });
    c.OperationFilter<FileUploadOperationFilter>();
});


builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartItemServices, CartItemService>();



builder.Services.AddDbContext<AppleStoreDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

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




public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.OperationId == "UploadImage")
        {
            operation.Parameters.Clear();
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "image",
                In = ParameterLocation.Query,
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "file"
                }
            });
        }
    }
}