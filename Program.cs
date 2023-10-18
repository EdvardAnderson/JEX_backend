using Autofac;
using JEX_backend;
using Microsoft.EntityFrameworkCore;


public class Program
{

    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }


    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel()
                        .UseUrls("https://localhost:7066")
                        .UseStartup<Startup>();
                });
    }
}