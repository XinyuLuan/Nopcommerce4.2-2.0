using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
namespace Nop.Data
{
    public class NopDesignTimeObjectContextFactory : IDesignTimeDbContextFactory<NopObjectContext>
    {
        public NopObjectContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<NopObjectContext>();

            string basePath = WebContentDirectoryFinder.CalculateContentRootFolder();
            if (string.IsNullOrWhiteSpace(basePath))
            {
                basePath = AppDomain.CurrentDomain.BaseDirectory;
            }
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(basePath, "App_Data"))
                .AddJsonFile("dataSettings.json")
                .Build();
            builder.UseSqlServer("Server=WINDEV2005EVAL;uid=sa;pwd=db12345;database=Nop4.2database");
            return new NopObjectContext(builder.Options);
        }
    }
}