using BookStore.Data;
using BookStore.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddControllersWithViews();
            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer("Server=.; Database = BookStore; Integrated Security = True"));
#if DEBUG  
            services.AddRazorPages().AddRazorRuntimeCompilation();
#endif
            services.AddScoped<BookRepository, BookRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
