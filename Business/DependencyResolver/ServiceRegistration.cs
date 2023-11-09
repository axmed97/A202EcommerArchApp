using AutoMapper;
using Business.Abstract;
using Business.AutoMapper;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.SQLServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Business.DependencyResolver
{
    public static class ServiceRegistration
    {
        public static void Run(this IServiceCollection services)
        {
            // AddScoped, AddSingleton, AddTransient
            services.AddScoped<AppDbContext>();

            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDAL, EFCategoryDAL>();

            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IProductDAL, EFProductDAL>();

            services.AddScoped<IPictureService, PictureManager>();
            services.AddScoped<IPictureDAL, EFPictureDAL>();

            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<IOrderDAL, EFOrderDAL>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            });


            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
