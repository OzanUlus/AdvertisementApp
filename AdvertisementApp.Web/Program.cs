using AdvertisementApp.Business.Extension;
using AdvertisementApp.Web.Models;
using AdvertisementApp.Web.ValidationRules;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddBusinesExtension(builder.Configuration.GetConnectionString("SqlCon")!,Assembly.GetExecutingAssembly());
builder.Services.AddTransient<IValidator<UserCreateModel>,UserCreateModelValidator>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.Cookie.Name = "AdvertisementApp";
        opt.Cookie.HttpOnly = true;
        opt.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
        opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        opt.ExpireTimeSpan = TimeSpan.FromDays(14);
        opt.LoginPath = new PathString("/Account/SignIn");
        opt.LogoutPath = new PathString("/Account/LogOut");
        opt.AccessDeniedPath = new PathString("/Account/AccessDenied");
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
