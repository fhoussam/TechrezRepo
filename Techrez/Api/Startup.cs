using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Api.CustomMiddlewares;
using Dal;
using Dal.Models;
using Api.CustomFilters;
using Swashbuckle.AspNetCore.Swagger;
using Api.DataServices;

namespace Api
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
            services.AddScoped<IDalService, DalService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddDbContext<TechrezDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));
            //services.AddMemoryCache(); -< to be used later
            services.AddSwaggerGen(options => options.SwaggerDoc("v1", new Info() { Title = "techrezapi", Description = "Techrez Core API" }));
            services.AddCors();
            services.AddMvc(options => {
                //accept header is ignored by default by the browser, that's he way to give him some respect
                options.RespectBrowserAcceptHeader = true;
                //generic filter for all controllers
                options.Filters.Add(typeof(ValidateModelState));
            }
            )
            .AddXmlSerializerFormatters()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCustomeExcptionHandler();
            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Core API"));
        }
    }
}
