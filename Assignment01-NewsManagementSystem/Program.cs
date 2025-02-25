using Assignment01_NewsManagementSystem.Controllers;
using Assignment01_NewsManagementSystem.Models;
using Assignment01_NewsManagementSystem.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Assignment01_NewsManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ✅ Add Authentication and Authorization
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Home/Login";  // Redirect if not authenticated
                    options.AccessDeniedPath = "/Home/AccessDenied"; // Redirect if unauthorized
                    options.LogoutPath = "/Home/Logout"; // Logout path
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Set session expiration
                });

            builder.Services.AddAuthorization();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Register DbContext (Scoped by default)
            builder.Services.AddDbContext<FunewsManagementContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyDb")));

            builder.Services.AddScoped<INewsArticleRepository, NewsArticleRepository>();
            builder.Services.AddScoped<ISystemAccountRepository, SystemAccountRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

            var context = builder.Services.BuildServiceProvider().GetRequiredService<FunewsManagementContext>();
            var newsRepo = builder.Services.BuildServiceProvider().GetRequiredService<INewsArticleRepository>();
            var accountRepo = builder.Services.BuildServiceProvider().GetRequiredService<ISystemAccountRepository>();
            var categoryRepo = builder.Services.BuildServiceProvider().GetRequiredService<ICategoryRepository>();

            // Initialize Singleton
            WebManager.Initialize(context, newsRepo, accountRepo, categoryRepo);

            // Register as Singleton
            builder.Services.AddSingleton(WebManager.Instance());


            //Session
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            // Load DefaultAdminAccount configuration
            builder.Services.Configure<DefaultAdminAccount>(
                builder.Configuration.GetSection("DefaultAdminAccount")
            );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Seed admin account
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var adminConfig = services.GetRequiredService<IOptions<DefaultAdminAccount>>().Value;

            if (!context.SystemAccounts.Any(account => account.AccountEmail.Equals(adminConfig.Email)))
            {
                context.SystemAccounts.Add(new SystemAccount
                {
                    AccountEmail = adminConfig.Email,
                    AccountPassword = adminConfig.Password,
                    AccountRole = 0,
                    AccountName = "Admin"
                });
                context.SaveChanges();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
