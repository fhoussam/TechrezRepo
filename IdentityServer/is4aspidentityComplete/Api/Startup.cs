using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    //options.ApiName = "api1";
                    options.Audience = "http://localhost:5000/resources";
                });

            services.AddCors();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseMvc();
        }
    }
}
