using AccessShieldFeedback.Data;
using AccessShieldFeedback.Services;
using Microsoft.EntityFrameworkCore;

namespace AccessShieldFeedback
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add PostgreSQL database linker to entities.
            builder.Services.AddEntityFrameworkNpgsql().AddDbContext<FeedbackContext>(opt =>
                opt.UseNpgsql(builder.Configuration.GetConnectionString("AccessControlFeedback")));

            builder.Services.AddTransient<IFeedbackService, FeedbackService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
