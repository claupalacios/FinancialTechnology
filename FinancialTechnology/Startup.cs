using FinancialTechnology.Helpers;
using FinancialTechnology.Models;
using FinancialTechnology.Repositories;
using FinancialTechnology.Repositories.Interfaces;
using FinancialTechnology.Services;
using FinancialTechnology.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text.Json.Serialization;

namespace FinancialTechnology
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("GlobalProcessing")));

            services.AddControllers()
                       .AddJsonOptions(options =>
                           options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "User Manager Service V1",
                });
            });

            //Repository Contracts
            services.AddScoped<IAccountRepository, AccountRepository>();

            //Services Contracts
            services.AddScoped<IAccountService, AccountService>();

            //Mapper profile
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfiles(new AutoMapper.Profile[] {
                        new MappingProfile()
                 });
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"));
        }
    }
}
