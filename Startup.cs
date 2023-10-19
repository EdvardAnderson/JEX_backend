using Microsoft.EntityFrameworkCore;
namespace JEX_backend;
public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<JEXDbContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("JEXDbContext"));
        });

        
        services.AddScoped<ICompanyService, CompanyService>();

         services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.WithOrigins("http://localhost:4200") // Angular endpoint allowed
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
    });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JEX_backend v1"));
        }
        else
        {
            app.UseHsts();
            //app.UseHttpsRedirection();
        }

        app.UseHttpsRedirection();
        // Other middleware configuration...
        app.UseCors();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(ep =>
        {
            ep.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

                ep.MapControllerRoute(
                    name:"",
                    pattern:"",
                    defaults: new { controller = "", action=""}

                );
        });
    }
}