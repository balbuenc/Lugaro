using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CoreERP.UI.Areas.Identity;
using CoreERP.UI.Data;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using CoreERP.UI.Interfaces;
using CoreERP.UI.Services;
using Microsoft.Extensions.Options;
using Syncfusion.Blazor;

namespace CoreERP.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("AuthenticationDB")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddBlazorise(options =>
                  {
                      options.ChangeTextOnKeyPress = true; // optional
                  })
                  .AddBootstrapProviders()
                  .AddFontAwesomeIcons();

            services.AddRazorPages();
            services.AddServerSideBlazor().AddCircuitOptions(options => options.DetailedErrors = true);
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddSyncfusionBlazor();


            services.AddHttpClient<IClientService, ClientService>(
                client => { client.BaseAddress = new Uri("https://localhost:44342"); });

            services.AddHttpClient<IAreaService, AreaService>(
                 area => { area.BaseAddress = new Uri("https://localhost:44342"); });

            services.AddHttpClient<INeighborhoodService, NeighborhoodService>(
                 neighborhood => { neighborhood.BaseAddress = new Uri("https://localhost:44342"); });

            services.AddHttpClient<ICityService, CityService>(
                 cities => { cities.BaseAddress = new Uri("https://localhost:44342"); });

            services.AddHttpClient<IPositionService, PositionService>(
                 position => { position.BaseAddress = new Uri("https://localhost:44342"); });

            services.AddHttpClient<INationalityService, NationalityService>(
                nationality => { nationality.BaseAddress = new Uri("https://localhost:44342"); });

            services.AddHttpClient<IClientTypeService, ClientTypeService>(
                clientType => { clientType.BaseAddress = new Uri("https://localhost:44342"); });

            services.AddHttpClient<ICountryService, CountryService>(
                country => { country.BaseAddress = new Uri("https://localhost:44342"); });

            services.AddHttpClient<ICurrencyService, CurrencyService>(
                currency => { currency.BaseAddress = new Uri("https://localhost:44342"); });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzQ3MDkwQDMxMzgyZTMzMmUzMEdDejEyQ1Z6Z0FUYk1JSG9WSk1GNFp5MWQ2bVNveUh6SWVwb0lCR01aMkU9");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.ApplicationServices
                 .UseBootstrapProviders()
                 .UseFontAwesomeIcons();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
