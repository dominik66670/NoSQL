using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NoSQL.Data;

namespace NoSQL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<NoSQLContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("NoSQLContext") ?? throw new InvalidOperationException("Connection string 'NoSQLContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<NoSQLContext>();

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("Redis");
            });
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<NoSQLContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Czas wyga�ni�cia sesji
                options.Cookie.HttpOnly = true; // Tylko serwer mo�e modyfikowa� ciasteczko
                options.Cookie.IsEssential = true; // Wymagane dla GDPR
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
