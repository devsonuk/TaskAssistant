using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using TaskAssistant.Api.Authentication;
using TaskAssistant.Api.Data;
using TaskAssistant.Api.Extensions.ServiceCollection;
using TaskAssistant.Api.Services;
using TaskAssistant.Api.Services.Interfaces;
using TaskAssistant.Domain.Configuration;
using TaskAssistant.Repository;
using TaskAssistant.Repository.Interfaces;

namespace TaskAssistant.Api
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["AppSettings:ConnectionStrings:TaskAssistantAuth"]));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddScheme<AuthenticationSchemeOptions, CustomAuthenticationHandler>(JwtBearerDefaults.AuthenticationScheme, null);

            // configure AppSettings object using appsettings.json

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // configure Custom Options like Auth, Email etc.
            services.ConfigureCustomOptions(Configuration.GetSection("AppSettings"));

            services.AddScoped<IPendingTaskRepository, PendingTaskRepository>();

            services.AddScoped<IPendingTaskService, PendingTaskService>();

            

            DapperSetup.SetUpDapperExtensions();

            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo { Title = "Task Assistant API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Assitant API v1");
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
