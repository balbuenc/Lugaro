using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreERP.Data;
using CoreERP.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace CoreERP.API
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
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IAreaRepository, AreaRepository>();
            services.AddScoped<INeighborhoodRepository, NeighborhoodRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IChildrenRepository, ChildrenRepository>();
            services.AddScoped<IBudgetRepository, BudgetRepository>();
            services.AddScoped<IBudgetDetailRepository, BudgetDetailRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<INationalityRepository, NationalityRepository>();
            services.AddScoped<IClientTypeRepository, ClientTypeRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<ICivilStatusRepository, CivilStatusRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IVendorRepository, VendorRepository>();
            services.AddScoped<IOriginRepository, OriginRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            services.AddScoped<IInvoiceTypeRepository, InvoiceTypeRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IPurchaseDetailsRepository, PurchaseDetailsRepository>();
            services.AddScoped<IQuotaRepository, QuotaRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddScoped<ICollectionRepository, CollectionRepository>();
            services.AddScoped<IAccountTypeRepository, AccountTypeRepository>();
            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountDetailRepository, AccountDetailRepository>();
            services.AddScoped<IDiscountLimitRepository, DiscountLimitRepository>();
            services.AddScoped<ILittleBoxRepository, LittleBoxRepository>();
            services.AddScoped<ILittleBoxDetailRepository, LittleBoxDetailRepository>();
            services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
            services.AddScoped<ITransferRepository, TransferRepository>();
            services.AddScoped<IGeneralPurchaseRepository, GeneralPurchaseRepository>();
            services.AddScoped<IGeneralPurchaseDetailsRepository, GeneralPurchaseDetailsRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            services.AddControllers();

            var sqlConnectionConfiguration = new SqlConfiguration(Configuration.GetConnectionString("ERPConnectionString"));
            services.AddSingleton(sqlConnectionConfiguration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
