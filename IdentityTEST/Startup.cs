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
using Microsoft.AspNetCore.Http;


namespace CoreERP.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private string apiurl;
        private string printAPIurl;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            apiurl = Configuration["api"];
            printAPIurl = Configuration["print"];

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

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpClient<IClientService, ClientService>(
                client => { client.BaseAddress = new Uri(apiurl); });

            services.AddHttpClient<IAreaService, AreaService>(
                 area => { area.BaseAddress = new Uri(apiurl); });

            services.AddHttpClient<INeighborhoodService, NeighborhoodService>(
                 neighborhood => { neighborhood.BaseAddress = new Uri(apiurl); });

            services.AddHttpClient<ICityService, CityService>(
                 cities => { cities.BaseAddress = new Uri(apiurl); });

            services.AddHttpClient<IPositionService, PositionService>(
                 position => { position.BaseAddress = new Uri(apiurl); });

            services.AddHttpClient<INationalityService, NationalityService>(
                nationality => { nationality.BaseAddress = new Uri(apiurl); });

            services.AddHttpClient<IClientTypeService, ClientTypeService>(
                clientType => { clientType.BaseAddress = new Uri(apiurl); });

            services.AddHttpClient<ICountryService, CountryService>(
                country => { country.BaseAddress = new Uri(apiurl); });

            services.AddHttpClient<ICurrencyService, CurrencyService>(
                currency => { currency.BaseAddress = new Uri(apiurl); });

            services.AddHttpClient<ICivilStatusService, CivilStatusService>(
               civilStatus => { civilStatus.BaseAddress = new Uri(apiurl); });

            services.AddHttpClient<IBrandService, BrandService>(
                brand => { brand.BaseAddress = new Uri(apiurl); });

            services.AddHttpClient<IOriginService, OriginService>(
             origin => { origin.BaseAddress = new Uri(apiurl); });

            services.AddHttpClient<IVendorService, VendorService>(
                vendor => { vendor.BaseAddress = new Uri(apiurl); });

            services.AddHttpClient<IProductService, ProductService>(
               product => { product.BaseAddress = new Uri(apiurl); });

            services.AddHttpClient<IStoreService, StoreService>(
               store => { store.BaseAddress = new Uri(apiurl); });

            services.AddHttpClient<IStockService, StockService>(
               stock => { stock.BaseAddress = new Uri(apiurl); });

            services.AddHttpClient<IBudgetService, BudgetService>(
              budget => { budget.BaseAddress = new Uri(apiurl); });

            services.AddHttpClient<IEmployeeService, EmployeeService>(
            employee => { employee.BaseAddress = new Uri(apiurl); });

            services.AddHttpClient<IBudgetDetailsService, BudgetDetailsService>(
            budgetDetails => { budgetDetails.BaseAddress = new Uri(apiurl); });

            services.AddHttpClient<IDiscountService, DiscountService>(
            discount => { discount.BaseAddress = new Uri(apiurl); });

            services.AddHttpClient<IConfigurationService, ConfigurationService>(
            configuration => { configuration.BaseAddress = new Uri(apiurl); });

            services.AddHttpClient<IInvoiceTypeService, InvoiceTypeService>(
            invoiceType => { invoiceType.BaseAddress = new Uri(apiurl); });

            services.AddHttpClient<IPrintService, PrintService>(
            printService => { printService.BaseAddress = new Uri(printAPIurl); }
            );

            services.AddHttpClient<IPurchaseService, PurchaseService>(
            purchaseService => { purchaseService.BaseAddress = new Uri(apiurl); }
            );

            services.AddHttpClient<IPurchaseDetailsService, PurchaseDetailsService>(
            purchaseDetailsService => { purchaseDetailsService.BaseAddress = new Uri(apiurl); }
            );

            services.AddHttpClient<IQuotaService, QuotaService>(
            quotaDetailsService => { quotaDetailsService.BaseAddress = new Uri(apiurl); }
            );

            services.AddHttpClient<ISaleService, SaleService>(
                saleDetailsService => { saleDetailsService.BaseAddress = new Uri(apiurl); }
            );

            services.AddHttpClient<IPaymentMethodService, PaymentMethodService>(
                paymentMethodService => { paymentMethodService.BaseAddress = new Uri(apiurl); }
            );

            services.AddHttpClient<ICollectionService, CollectionService>(
                collectionService => { collectionService.BaseAddress = new Uri(apiurl); }
            );

            services.AddHttpClient<IAccountTypeService, AccountTypeService>(
                accountTypeService => { accountTypeService.BaseAddress = new Uri(apiurl); }
            );

            services.AddHttpClient<IBankService, BankService>(
            bankService => { bankService.BaseAddress = new Uri(apiurl); }
            );

            services.AddHttpClient<IAccountService, AccountService>(
            accountService => { accountService.BaseAddress = new Uri(apiurl); }
            );


            services.AddHttpClient<IAccountDetailsService, AccountDetailsService>(
            accountDetailsService => { accountDetailsService.BaseAddress = new Uri(apiurl); }
            );

            services.AddHttpClient<IDiscountLimitService, DiscountLimitService>(
            dicsountLimitService => { dicsountLimitService.BaseAddress = new Uri(apiurl); }
            );

            services.AddHttpClient<ILittleBoxService, LittleBoxService>(
           littleBoxService => { littleBoxService.BaseAddress = new Uri(apiurl); }
           );

            services.AddHttpClient<ILittleBoxDetailsService, LittleBoxDetailsService>(
            littleBoxDetailsService => { littleBoxDetailsService.BaseAddress = new Uri(apiurl); }
            );

            services.AddHttpClient<IPurchaseOrderService, PurchaseOrderService>(
            purchaseOrderBoxDetailsService => { purchaseOrderBoxDetailsService.BaseAddress = new Uri(apiurl); }
            );

            services.AddHttpClient<ITransferService, TransferService>(
           transferService => { transferService.BaseAddress = new Uri(apiurl); }
           );


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzQ3MDkwQDMxMzgyZTMzMmUzMEdDejEyQ1Z6Z0FUYk1JSG9WSk1GNFp5MWQ2bVNveUh6SWVwb0lCR01aMkU9"); // Synfusion (v18.3.0.*)
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDg4NzE3QDMxMzkyZTMyMmUzMFA1VHpFanZxK2ZGR1AzOTJlSjNlM2lQOGxNMlNvSGI3Q2hVTlowSHpHL009");
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
