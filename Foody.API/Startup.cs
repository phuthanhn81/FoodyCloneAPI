using Foody.Data.EF;
using Foody.Data.Repositories;
using Foody.Service.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Foody.API
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
            #region Connection
            services.AddDbContext<FoodyContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Foody")));
            #endregion

            #region Repository
            services.AddTransient<IPlacesRepository, PlacesRepository>();
            services.AddTransient<IPlaceDishesRepository, PlaceDishesRepository>();
            services.AddTransient<IOrdersRepository, OrdersRepository>();
            services.AddTransient<IOrderDetailsRepository, OrderDetailsRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<ITokenRepository, TokenRepository>();
            #endregion

            #region Service
            services.AddTransient<IPlacesService, PlacesService>();
            services.AddTransient<IPlaceDishesService, PlaceDishesService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<ILoginService, LoginService>();
            #endregion

            services.AddControllersWithViews();

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Title Swagger", Version = "v1" });
            });
            #endregion

            #region Authorization
            string signingKey = Configuration.GetValue<string>("Token:Key");
            byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration.GetValue<string>("Token:Issuer"),
                    ValidateAudience = true,
                    ValidAudience = Configuration.GetValue<string>("Token:Issuer"),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = System.TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                };
            });
            #endregion
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Swagger");
            });
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
