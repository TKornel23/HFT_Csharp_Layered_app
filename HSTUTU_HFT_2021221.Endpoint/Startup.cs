using HSTUTU_HFT_2021221.Data;
using HSTUTU_HFT_2021221.Logic;
using HSTUTU_HFT_2021221.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace HSTUTU_HFT_2021221.Endpoint
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();//.AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            services.AddTransient<IBlogLogic, BlogLogic>();
            services.AddTransient<IPostLogic, PostLogic>();
            services.AddTransient<ITagLogic, TagLogic>();
            services.AddTransient<IBlogRepository, BlogRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddDbContext<BlogDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
