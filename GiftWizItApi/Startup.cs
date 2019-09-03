using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using GiftWizItApi.Models;
using Microsoft.EntityFrameworkCore;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Implementations;
using AutoMapper;
using GiftWizItApi.Services;
using GiftWizItApi.SignalR.Hubs;

namespace GiftWizItApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string GWAllowSpecificOrigins = "_gwAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddAuthentication(AzureADB2CDefaults.BearerAuthenticationScheme)
                .AddAzureADB2CBearer(options => Configuration.Bind("AzureAdB2C", options));

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddMvc()
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                )
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSignalR();
            services.AddHttpContextAccessor();

            services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddSingleton<IGiftWizItWebSettings>(Configuration.GetSection("GiftWizItWebSettings").Get<GiftWizItWebSettings>());
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUserService, UserService>();
            services.AddCors(options =>
            {
                options.AddPolicy(GWAllowSpecificOrigins, builder =>
                {
                    builder.WithOrigins("https://localhost:44347",
                                        "https://www.giftwizit.com",
                                        "chrome-extension://adofnoobbeoahcnapncpcndebfcfdcbi",
                                        "chrome-extension://ojbmfjenijdkdndemkbkongfaendkgic").AllowAnyMethod();
                    builder.WithHeaders("Authorization", "Content-Type", "Cache-Control");
                });
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IGiftListRepository, GiftListRepository>();
            services.AddScoped<IWishListRepository, WishListRepository>();
            services.AddScoped<IItemsRepository, ItemsRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors(GWAllowSpecificOrigins);
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationsHub>("/notifHub");
            });
            app.UseMvc();
        }
    }
}
