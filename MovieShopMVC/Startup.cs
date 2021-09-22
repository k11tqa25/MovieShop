using ApplicationCore.ServiceInterfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using MovieShopMVC.Infrastructure;

namespace MovieShopMVC
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
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<ICastService, CastService>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            services.AddScoped<ICastRepository, CastRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddMemoryCache();

            // This will inject HttpContext to IHttpContextAccessor 
            services.AddHttpContextAccessor();

            //Inject connection string to DbContext
            services.AddDbContext<MovieShopDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("MovieShopDbConnection"));
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "MovieShopAuth";
                    options.ExpireTimeSpan = TimeSpan.FromHours(2);
                    options.LoginPath = "/Account/Login";  // if the cookie auth fails, go to this url
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseMovieShopExceptionMiddleware();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //Auth for cookie (before Authorization)
            app.UseAuthentication(); 

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
