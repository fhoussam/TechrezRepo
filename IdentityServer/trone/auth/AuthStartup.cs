using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.DataProtection;

namespace auth
{
    public class AuthStartup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }
        public ILoggerFactory LoggerFactory { get; }

        public AuthStartup(IConfiguration configuration, IHostingEnvironment environment, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            Environment = environment;
            LoggerFactory = loggerFactory;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddDataProtection().UseCryptographicAlgorithms
            (
                    new AuthenticatedEncryptorConfiguration()
                    {
                        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                    }
            );

            string connectionString = Configuration.GetConnectionString("SqlServerConnectionString");
            Console.WriteLine("Connection string : " + connectionString);

            services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseSqlServer(connectionString, o => o.MigrationsAssembly("auth"));
            });

            services.AddIdentity<IdentityUser, IdentityRole >(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddDefaultUI()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.AddMvc()
                //we should know why we this line is for 
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1)
                ;

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "AUTHHX-XSRF-TOKEN";
                options.Cookie.Name = "AUTHCX-XSRF-TOKEN";
            });

            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.UserInteraction.LoginUrl = "~/Identity/Account/Login";
            })
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<IdentityUser>()
                ;

            if (Environment.IsDevelopment())
            //if(false)
            {
                Console.WriteLine("Loading dev credentials");
                builder.AddDeveloperSigningCredential();
                Console.WriteLine("dev credentials loaded");
            }
            else
            {
                Console.WriteLine("Loading prod credentials");
                X509Store store = new X509Store(StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection cers = store.Certificates.Find(X509FindType.FindByThumbprint, "586621a84b4c187893858e8643d953b9254145d2", false);
                if (cers.Count > 0)
                {
                    builder.AddSigningCredential(cers[0]);
                    X509SecurityKey privateKey = new X509SecurityKey(cers[0]);
                    var cert = new SigningCredentials(privateKey, SecurityAlgorithms.RsaSha256Signature);
                    builder.AddSigningCredential(cert);
                    Console.WriteLine("prod credentials loaded");
                };
                store.Close();
            }

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    // register your IdentityServer with Google at https://console.developers.google.com
                    // enable the Google+ API
                    // set the redirect URI to http://localhost:5000/signin-google
                    options.ClientId = "copy client ID from Google here";
                    options.ClientSecret = "copy client secret from Google here";
                });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                string path = context.Request.Path;
                if (!path.ToLower().Contains("js") && !path.ToLower().Contains("css") && !path.ToLower().Contains("png"))
                {

                }
                await next.Invoke();
            });

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:5003", "http://localhost:5001"));

            app.UseCors();

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
        }
    }
}