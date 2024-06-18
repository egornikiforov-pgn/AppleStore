using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using AppleStore.DataAccess;

namespace AppleStore.Console1
{
class Program
{
    static void Main(string[] args)
    {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppleStoreDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseNpgsql(connectionString);
            Console.WriteLine(connectionString);
            
            using var dbContext = new AppleStoreDbContext(optionsBuilder.Options);


            Console.WriteLine("DbContext успешно создан и настроен.");

    }
}
}
