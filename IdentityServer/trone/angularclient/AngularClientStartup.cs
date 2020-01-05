using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Authentication;
using IdentityModel;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using angularclient.Models;
using angularclient.DbAccess;
using Microsoft.Extensions.Hosting;
using angularclient.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Antiforgery;

namespace angularclient
{
    public class AngularClientStartup
    {
        public AngularClientStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("SqlServerConnectionString")
                .Replace("=\\", "=" + System.Environment.MachineName + "\\");

            services.AddDbContext<TechRezDbContext>(options =>
            {
                options.UseSqlServer(connectionString, o => o.MigrationsAssembly("angualarclient"));
            });

            services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = "Dynamic";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.SignInScheme = "Cookies";

                options.Authority = "http://localhost:5000";
                options.RequireHttpsMetadata = false;

                options.ClientId = "angularclient";
                options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
                options.ResponseType = "code id_token";

                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true; //option is important if we want to retreive claims

                    options.Scope.Add("offline_access");
                options.Scope.Add("profile");
                options.Scope.Add("email");
                options.Scope.Add("openid");
                options.Scope.Add("complementary_profile");

                options.ClaimActions.MapJsonKey("favcolor", "favcolor");
                options.ClaimActions.MapJsonKey(JwtClaimTypes.BirthDate, JwtClaimTypes.BirthDate);
                options.ClaimActions.MapJsonKey(JwtClaimTypes.Gender, JwtClaimTypes.Gender);
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "http://localhost:5000";
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                    //options.Audience = "api1";

                    //options.Audience = "http://10.0.2.2:5000/resources";
                    options.TokenValidationParameters.ValidAudiences = new List<string>()
                {
                        "http://10.0.2.2:5000/resources",
                        "http://localhost:5000/resources"
                };

                    //token validation options
                    options.TokenValidationParameters.ValidateLifetime = true;
                options.TokenValidationParameters.ClockSkew = TimeSpan.Zero;

                options.TokenValidationParameters.ValidateAudience = true;
                options.TokenValidationParameters.RequireExpirationTime = true;
                options.TokenValidationParameters.ValidateIssuerSigningKey = true;
            })
            .AddPolicyScheme("Dynamic", "Dynamic", options =>
            {
                options.ForwardDefaultSelector = ctx =>
                {
                    try
                    {
                        string authHeader = ctx.Request.Headers["Authorization"];
                        return authHeader.Split(' ')[0];
                    }
                    catch (Exception)
                    {
                        return "Cookies";
                    }

                };
            });

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-XSRF-TOKEN";
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithOrigins("http://localhost:5001");
                });
            });

            services.AddScoped<ProductRepository>();
            services.AddScoped<FeedRepository>();
            services.AddSignalR();


            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");

                endpoints.MapHub<FeedHub>("/feedhub");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}

