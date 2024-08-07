using JEX_backend;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.AddDebug();
                // Voeg eventuele andere logproviders toe die je wilt gebruiken
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseKestrel().UseUrls("https://localhost:7066/").UseStartup<Startup>();
            });
    }
}
