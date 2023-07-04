using Capstone.DataAccess.Data;
using Capstone.DataAccess.Repository;
using Capstone.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Capstone.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Adding entity framework to project for sql server
// ApplicationDbContext created in the Data folder
// Pass the connection string to the sql server from appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
// Add razor page services for all the identity pages which are not MVC
builder.Services.AddRazorPages();
// Add Repository/UnitOfWork to the service builder
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Add email services, not used at the moment, just to avoid exception
builder.Services.AddScoped<IEmailSender, EmailSender>();

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
// Manually added authen and author.
app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

// Add ability for app to route to razor pages
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Client}/{controller=Home}/{action=Index}/{id?}");

app.Run();
