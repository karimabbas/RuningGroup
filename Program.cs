using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using SocilaMediaProject.Data;
using SocilaMediaProject.helper;
using SocilaMediaProject.Interfaces;
using SocilaMediaProject.Models;
using SocilaMediaProject.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MyDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConn"), builder => builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));
});

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<MyDBContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<IClubRepository, ClubRepository>();
builder.Services.AddScoped<IRaceRepository, RaceRepository>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILocationService, LocatioinService>();
builder.Services.AddTransient<ISendGridEmail, SendGridEmail>();

// builder.Configuration.GetValue<string>("mySendGridKey");
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration.GetSection("SendGrid"));

builder.Services.AddAuthentication()
   .AddGoogle(options =>
   {
       options.ClientId = "681518767098-l1qncoe8j81nas6vpdv3hfc3k5fa90cs.apps.googleusercontent.com";
       options.ClientSecret = "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";
   })
   .AddFacebook(options =>
   {
       options.AppId = builder.Configuration.GetValue<string>("FaceBook_AppId");
       options.AppSecret = builder.Configuration.GetValue<string>("FaceBook_AppSecret");
   });

builder.Services.AddAuthorization(option => option.AddPolicy("ClubAccess", policy => policy.RequireRole("Admin")));

builder.Services.AddMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.Name = "MyCookie";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddRazorPages(options => options.Conventions.AuthorizePage("/Privacy", "ClubAccess"));


// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseStatusCodePagesWithRedirects("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// To Seed data:
// SeedRoles.SeedRolesAsync(app);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
