using BestBankApp.Repository;
using BestBankApp.Repository.Context;
using BestBankApp.Services.Helpers;
using BestBankApp.Services.Implementation;
using BestBankApp.Services.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BestBankApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddControllersWithViews();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IClientsService, ClientsService>();
            services.AddTransient<ICreditsService, CreditsService>();
            services.AddAutoMapper(x => x.AddProfile(new MappingEntity()));
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BestBankAppConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Clients/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Clients}/{action=Index}/{id?}");
            });
        }
    }
}
