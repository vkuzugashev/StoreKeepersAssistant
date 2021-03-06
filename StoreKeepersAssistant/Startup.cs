using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoreKeepersAssistant.Models;
using StoreKeepersAssistant.Repositories;
using StoreKeepersAssistant.Services;
using System.Diagnostics;

namespace StoreKeepersAssistant
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<StorageContext>(options =>
            {
                options.UseSqlServer(connection);
                options.LogTo(o => Debug.WriteLine(o));
            }/*, ServiceLifetime.Transient*/);

            services.AddSingleton<ICustomDbContextFactory<StorageContext>, CustomDbContextFactory<StorageContext>>();

            services.AddControllersWithViews();

            //Repositories
            services.AddScoped<IStorageRepository, StorageRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IInvoiceItemRepository, InvoiceItemRepository>();

            //Services
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IInvoiceItemService, InvoiceItemService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IItemService, ItemService>();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
