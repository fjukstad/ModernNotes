using System;
using System.IO;
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
using ModernNotes.Models;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;


namespace ModernNotes {

    ///<summary>Startup</summary>
    public class Startup {

        ///<summary>Startup</summary>
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        ///<summary> App config </summary> 
        public IConfiguration Configuration { get; }

        ///<summary>Configure services</summary>
        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<NoteContext>(opt => opt.UseInMemoryDatabase("Notes"));
            services.AddMvc();
            services.AddSwaggerGen( c => {
                c.SwaggerDoc("v1", new Info { Title = "Modern Notes API",
                    Version = "v1" });
                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "docs.xml");
                c.IncludeXmlComments(filePath);
            });

        }
        ///<summary>Config HTTP request pipeline</summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment
        env) {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Modern Notes API V1");
            });

        }
    }
}
