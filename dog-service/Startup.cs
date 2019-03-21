using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using data_library.DataContexts.AuditTrail;
using data_library.DataContexts.Canine;
using data_library.Interfaces;
using data_library.Models;
using DogService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace dog_service
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
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient<IAuditService<Dog>, AuditService.Services.AuditService<Dog>>();
            services.AddTransient<IDataService<Dog>, DogService.Services.DogService>();
            services.AddDbContext<DogContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("trojan-demo"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure()
                    ));
            services.AddTransient<IDogContext, DogContext>();
            services.AddDbContext<AuditContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("trojan-audit"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure()
                    ));
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
            app.UseMvc();
        }
    }
}
