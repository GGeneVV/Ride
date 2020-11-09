using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RidePal.Data;
using RidePal.Data.Seeder;
using RidePal.Models;
using RidePal.Services;
using RidePal.Services.Contracts;

namespace RidePal.Web
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


            services.AddDbContext<AppDbContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews()
              .AddRazorRuntimeCompilation();

            services.AddIdentity<User, Role>(option => option.SignIn.RequireConfirmedAccount = false)
              .AddEntityFrameworkStores<AppDbContext>()
               .AddDefaultTokenProviders();



            services.AddAutoMapper(typeof(Startup));

            services.AddRazorPages();


            services.AddScoped<IMapper, Mapper>();

            services.AddScoped<IAlbumService, AlbumService>();

            services.AddScoped<IArtistService, ArtistService>();

            services.AddScoped<IGenreService, GenreService>();

            services.AddScoped<IPlaylistService, PlaylistService>();

            services.AddScoped<ITrackService, TrackService>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

                endpoints.MapRazorPages();
            });

            
            // TODO: Toggle seeder
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
            //await context.SeedDbAsync();
            //context.SeedRoles();

            //using var serviceScopeRole = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            //var contextThird = serviceScopeRole.ServiceProvider.GetRequiredService<AppDbContext>();
            //contextThird.SeedRoles();

     

            //using var serviceScopeAdmin = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            //var contextSecond = serviceScopeAdmin.ServiceProvider.GetRequiredService<AppDbContext>();
            //contextSecond.SeedUsersRoleAdmin();




        }
    }
}
