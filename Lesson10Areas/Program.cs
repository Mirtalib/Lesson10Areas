using Lesson10Areas.Data;
using Lesson10Areas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EcommerceDbContext>(options => 
        options.UseSqlServer(
            builder.Configuration
            .GetConnectionString("default"))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Transient);

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<EcommerceDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoint =>
{
    endpoint.MapAreaControllerRoute(
        name: "foradminarea",
        areaName: "Admin",
        pattern: "foradmin/{controller=Home}/{action=Index}"
        );
    //endpoint.MapControllerRoute(
    //    name: "foradmin",
    //    pattern: "{area}/{controller=Home}/{action=Index}"
    //    );
    endpoint.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
