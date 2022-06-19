using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtualShopping.API.SignalR;
using VirtualShopping.BLL.Implement;
using VirtualShopping.BLL.Interface;
using VirtualShopping.DAL.Implement.Data;
using VirtualShopping.DAL.Interface;

namespace API.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config){
            services.AddDbContext<DataContext>(options => {
                options.UseSqlite(config.GetConnectionString("DbConnection"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IShopService, ShopService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<ICartServices, CartServices>();
            services.AddSingleton<GroupsTracker>();


            return services;
        }
    }
}