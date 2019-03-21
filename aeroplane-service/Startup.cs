using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AeroplaneService.Services;
using AuditService.Services;
using data_library.DataContexts.Transport;
using data_library.Interfaces;
using data_library.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace aeroplane_service
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
            services.AddTransient<IAuditService<Aeroplane>, AuditService.Services.AuditService<Aeroplane>>();
            services.AddTransient<IDataService<Aeroplane>, AeroplaneService.Services.AeroplaneService>();
            services.AddDbContext<AeroplaneContext>(
                options => options.UseSqlServer(
                    "Server=tcp:trojan-demo.database.windows.net,1433;Initial Catalog=aeroplanes;Persist Security Info=False;User ID=dba;Password=***;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
                    sqlOptions => sqlOptions.EnableRetryOnFailure()
                    ));
            services.AddTransient<IAeroplaneContext, AeroplaneContext>();
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
