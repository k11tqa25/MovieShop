using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieShopAPI", Version = "v1" });
            });

            services.AddControllersWithViews();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<ICastService, CastService>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFavoriteService, FavoriteService>();
            services.AddScoped<IMovieRepository, MovieRepository>();    
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<ICastRepository, CastRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();

            // This will inject HttpContext to IHttpContextAccessor 
            services.AddHttpContextAccessor();

            //Inject connection string to DbContext
            services.AddDbContext<MovieShopDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("MovieShopDbConnection"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieShopAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Enable ajax call from other origin
            app.UseCors(builder => {
                builder.WithOrigins(Configuration.GetValue<string>("MovieShopSpaUrl")).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            });


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
