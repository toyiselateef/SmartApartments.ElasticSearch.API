using Application.Interface.Services;
using Application.Middlewares;
using Domain.Entities;
using Implementation.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SmartApartment.API.Helper;
using SmartApartment.Application.Interface.Services;

namespace SmartApartment.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

       
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartApartmentSearch.API", Version = "v1" });

                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                ////... and tell Swagger to use those XML comments.
                //c.IncludeXmlComments(xmlPath);
            });

            services.AddElasticSearch(Configuration);
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IUploadServices, UploadServices>();
            services.AddTransient<ExceptionHandlingMiddleware>();

            services.Configure<ElasticSettings>(Configuration.GetSection("Elastic"));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder
                        .WithOrigins("*")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });

            //services.AddAuthentication(options =>
            //{
            //    //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            //});
            ////.AddJwtBearer(options =>
            ////{
            ////    options.Authority = Configuration["Auth0:Domain"];
            ////    options.Audience = Configuration["Auth0:ApiIdentifier"];
            ////});

            services.AddAuthorization();
        }

      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartApartment.API v1"));
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();
          

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
