using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimionatoChefBusiness;
using SimionatoChefDAO;
using Microsoft.AspNetCore.Mvc.Cors;

namespace SimionatoChefAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddTransient<IAPIBusiness, APIBusiness>();
            services.AddTransient<IClienteBusiness, ClienteBusiness>();
            services.AddTransient<IDashboardBusiness, DashboardBusiness>();
            services.AddTransient<IProdutoBusiness, ProdutoBusiness>();
            services.AddTransient<IUsuarioBusiness, UsuarioBusiness>();
            services.AddTransient<IVendaBusiness, VendaBusiness>();

            services.AddTransient<IAPIDAO, APIDAO>();
            services.AddTransient<IClienteDAO, ClienteDAO>();
            services.AddTransient<IProdutoDAO, ProdutoDAO>();
            services.AddTransient<IUsuarioDAO, UsuarioDAO>();
            services.AddTransient<IVendaDAO, VendaDAO>();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseCors("MyPolicy");

            app.UseMvc();
        }
    }
}
