using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace ToDoApp
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
            services.AddDbContext<TodoDbContext>(opt => opt.UseSqlServer(Configuration
                     .GetConnectionString("TodoDbContext")));

            services.AddMvc().AddJsonOptions(opts =>
            {
                // Force Camel Case to JSON
                opts.SerializerSettings.ContractResolver = new DefaultContractResolver();
            }).AddViewLocalization().AddDataAnnotationsLocalization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(option =>
            {
                option.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials();
            });

            app.UseMvc();
        }
    }
}
