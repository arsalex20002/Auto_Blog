using Auto_Blog;
using Auto_Blog.DAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionstring));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.InitializeContext();
builder.Services.InitializeRepositories();
builder.Services.InitializeServices();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/User/Login");
        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/User/Login");
    });


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);//спросить

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
    pattern: "{controller=Post}/{action=GetPosts}");
app.MapControllerRoute(
    name: "SportCarRoute",
    pattern: "{TypeCar}",
    defaults: new { controller = "Post", action = "GetPosts" }
);
app.MapControllerRoute(
    name: "MyProfile",
    pattern: "Myprofile/{username}",
    defaults: new { controller = "User", action = "Account" });

app.Run();
