using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using System;
using System.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.Entity;
using System.Data.Common;

using DoAnFramework.src.Configurations;
using DoAnFramework.Migrations;


namespace DoAnFramework
{
    public class Startup
    {
        public void TestConnection()
        {
            using (var db = new DoAnFramework.src.Models.DbContext())
            {
                string conn = db.Database.Connection.ConnectionString;
                Console.WriteLine(conn);
            }
        }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<DoAnFramework.src.Models.DbContext>(_ => new DoAnFramework.src.Models.DbContext(Configuration.GetConnectionString("Judger")));
            services.AddControllers();
            services.AddRazorPages();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });
            });
           

           /*services.AddScoped<DoAnFramework.src.Models.DbContext>();*/
           Database.SetInitializer(new MigrateDatabaseToLatestVersion<src.Models.DbContext, Configuration>());

            EfBulkInsertGlimpseProviderRegistry.Execute();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                TestConnection();
                Console.WriteLine("Run dev");
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyProject.Api v1"));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
