using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Repository.BusinessRepository;
using BusinessLogic.UnitOfWork;
using BusinesssLogic.Data;
using CoffeeCorner.Helpers;
using Domains.Interfaces.IBusinessRepository;
using Domains.Interfaces.IUnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Domains.Models;

namespace CoffeeCorner
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

      
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoffeeCorner", Version = "v1" });
            });


            //DbContext Config
            services.AddDbContext<DataStoreContext>(x => 
                              x.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));


            //identity
            services.AddIdentity<ApplicationUser, IdentityRole>(
                opt =>
                {
                    opt.Password.RequiredLength = 8;
                    opt.User.RequireUniqueEmail = true;
                }).AddEntityFrameworkStores<DataStoreContext>();


            services.AddAuthentication();
            services.AddAuthorization();

         
            //redis config
            services.AddSingleton<IConnectionMultiplexer>(config=> {
                return ConnectionMultiplexer.Connect(ConfigurationOptions.
                              Parse(_configuration.GetConnectionString("Redis"), true));

            });


            // UnitOfWork Config
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //ShoppingCart Repository service
            services.AddScoped<IShopppingCartRepository, ShoppingCartRepository>();


            //session Config
            //services.AddSession(opt=> {
            //    opt.Cookie.Name = ".CoffeeCornerCart.Session";
            //    opt.IdleTimeout = TimeSpan.FromSeconds(60);
            //    opt.Cookie.IsEssential = true;
            //});

            //services.AddHttpContextAccessor();
            //services.AddDistributedMemoryCache();


            //AutoMapper Config
            services.AddAutoMapper(typeof(AppMappingProfile));

            //CORS policy
            services.AddCors(opt => {
                opt.AddPolicy("CorsPolicy", builder =>
                       builder.AllowAnyMethod().AllowAnyHeader().WithOrigins("https://localhost:4200"));
            });



        }

     
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoffeeCorner v1"));
            }

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

           // app.UseSession();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
