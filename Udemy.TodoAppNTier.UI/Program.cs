using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Udemy.TodoAppNTier.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {



            //CreateHostBuilder(args).Build().Run();
            Log.Logger = new LoggerConfiguration()

                .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
           .ReadFrom.Configuration(new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build())
           .Enrich.FromLogContext()
           //.WriteTo.MSSqlServer(
           // connectionString: "YourConnectionString",
           // tableName: "CustomLogTable", // Replace with your desired table name
           // autoCreateSqlTable: false, // Set to true if table doesn't exist
           // columnOptions: new ColumnOptions
           // {
           //     TimeStamp = { ColumnName = "Timestamp" },
           //     Message = { ColumnName = "Message" },
           //     Exception = { ColumnName = "Exception" },

           //     // Ekstra kolonlar ekleme
           //     AdditionalColumns = new List<SqlColumn>
           //         {
           //             new SqlColumn("Username", SqlDbType.NVarChar) { DataLength = 100 }, // Kullanıcı adı
           //             new SqlColumn("OrderId", SqlDbType.Int) // Sipariş ID
           //         }
           // }


           .CreateLogger();

            //try
            //{
            //    //Log.Information("Uygulama başlatılıyor");
            CreateHostBuilder(args).Build().Run();
            //}
            //catch (Exception ex)
            //{
            //    Log.Fatal(ex, "Uygulama başlatılamadı");
            //}
            //finally
            //{
            //    Log.CloseAndFlush();
            //}


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
