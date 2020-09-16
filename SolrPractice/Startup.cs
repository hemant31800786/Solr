using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SolrNet;
using SolrPractice.core;
using SolrPractice.core.Domian;
using SolrPractice.Models;
using SolrPractice.Persistance;

namespace SolrPractice
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
            services.AddControllersWithViews();
            //  services.AddSolrNet("http://192.168.212.146:8983/solr");
            //   services.AddSolrNet<PhotoSearch>("http://192.168.212.146:8983/solr/PhotoServiceSearchPhoto");


            services.AddSolrNet("http://localhost:8983/solr");
            services.AddSolrNet<PhotoSearch>("http://localhost:8983/solr/PhotoSearch");

            services.AddScoped<ISolrIndexService<PhotoSearch>, SolrIndexService<PhotoSearch, ISolrOperations<PhotoSearch>>>();



            services.AddSolrNet<SolrPostModel>($"http://localhost:8983/solr/my_posts");
            services.AddScoped<ISolrIndexService<SolrPostModel>, SolrIndexService<SolrPostModel, ISolrOperations<SolrPostModel>>>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
