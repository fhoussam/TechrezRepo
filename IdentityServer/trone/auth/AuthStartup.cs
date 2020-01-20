// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using auth.Data;
using auth.Models;
using auth.Services;
using auth.Utils;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace auth
{
    public class AuthStartup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public AuthStartup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            //services.AddDataProtection().UseCryptographicAlgorithms
            //(
            //    new AuthenticatedEncryptorConfiguration()
            //    {
            //        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
            //        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
            //    }
            //);

            if (Environment.IsDevelopment()) 
                services.AddControllersWithViews().AddRazorRuntimeCompilation(); 
            else 
                services.AddControllersWithViews(); 

            // configures IIS out-of-proc settings (see https://github.com/aspnet/AspNetCore/issues/14882)
            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "AUTHHX-XSRF-TOKEN";
                options.Cookie.Name = "AUTHCX-XSRF-TOKEN";
            });

            // configures IIS in-proc settings
            services.Configure<IISServerOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });


            //little warning, System.Environment is different from locql property IHostingEnvironment Environment
            string connectionString = Configuration.GetConnectionString("SqlServerConnectionString").Replace("=\\", "=" + System.Environment.MachineName + "\\");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            
            var builder = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<ApplicationUser>()
                ;

            builder.AddDeveloperSigningCredential();

            //// not recommended for production - you need to store your key material somewhere secure
            //if (Environment.IsDevelopment())
            //{
            //    builder.AddDeveloperSigningCredential();
            //}
            //else
            //{
            //    X509Store store = new X509Store(StoreLocation.LocalMachine);
            //    store.Open(OpenFlags.ReadOnly);
            //    X509Certificate2Collection cers = store.Certificates.Find(X509FindType.FindByThumbprint, "586621a84b4c187893858e8643d953b9254145d2", false);
            //    if (cers.Count > 0)
            //    {
            //        builder.AddSigningCredential(cers[0]);
            //        X509SecurityKey privateKey = new X509SecurityKey(cers[0]);
            //        var cert = new SigningCredentials(privateKey, SecurityAlgorithms.RsaSha256Signature);
            //        builder.AddSigningCredential(cert);
            //    };
            //    store.Close();
            //}

            //to have control on some ASP idenitity auth cookie 
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            //    options.Cookie.Name = "YourAppCookieName";
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            //    options.LoginPath = "/Identity/Account/Login";
            //    // ReturnUrlParameter requires 
            //    //using Microsoft.AspNetCore.Authentication.Cookies;
            //    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            //    options.SlidingExpiration = true;
            //});

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    // register your IdentityServer with Google at https://console.developers.google.com
                    // enable the Google+ API
                    // set the redirect URI to http://localhost:5000/signin-google
                    options.ClientId = "67483393858-9br5jsr4be05dmbcdtmjkv217b2cog8p.apps.googleusercontent.com";
                    options.ClientSecret = "K5QBNVzsIahSBrfKAeWm8w8b";
                });

            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);
            services.AddTransient<IProfileService, ProfileService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
            //.AllowAnyOrigin()
                .WithOrigins
                (
                    "http://localhost:5003"
                    ,"https://localhost:44301"
                    //,"http://localhost:8100"
                    //,"http://localhost"
                    //,"ioniclient://ioniclient.trone"
                )
            );

            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}