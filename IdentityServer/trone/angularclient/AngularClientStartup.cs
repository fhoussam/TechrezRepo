using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

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
                options.GetClaimsFromUserInfoEndpoint = true;

                options.Scope.Add("offline_access");
                options.Scope.Add("profile");
                options.Scope.Add("email");
                options.Scope.Add("openid");
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "http://localhost:5000";
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                //options.Audience = "api1";
                options.Audience = "http://localhost:5000/resources";
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
            })
            ;

            services.AddCors();

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "ANGHX-XSRF-TOKEN";
                options.Cookie.Name = "ANGCX-XSRF-TOKEN";
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(new CustomAntiForgeryAttributeAttribute());
            })
            //why are we using this ?
            //.SetCompatibilityVersion(CompatibilityVersion.Version_2_0)
            ;

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
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
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseAuthentication();

            app.UseCors(o =>
            {
                o.AllowAnyHeader();
                o.AllowAnyMethod();
                o.AllowAnyOrigin();
                o.AllowCredentials();
            });

            app.MapWhen(x => x.Request.Path.Value.StartsWith("/api"), builder =>
            {
                app.UseMvcWithDefaultRoute();
            });

            app.MapWhen(x => !x.Request.Path.Value.StartsWith("/api"), builder =>
            {
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
            });;
        }
    }
}
