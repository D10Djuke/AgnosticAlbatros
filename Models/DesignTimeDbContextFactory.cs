using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DeliContext>
    {
        public DeliContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var builder = new DbContextOptionsBuilder<DeliContext>();

            var connection = "Server=DESKTOP-IL0S50Q\\SQLEXPRESS;Database=DeliCore;Trusted_Connection=True;MultipleActiveResultSets=true";
            builder.UseSqlServer(connection);

            return new DeliContext(builder.Options);
        }
    }

    //public class DesignTimeDbContextFactory { 
        
    //}
}
