using Capstone.DataAccess.Data;
using Capstone.DataAccess.Repository;
using Capstone.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Capstone.Utility;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Adding entity framework to project for sql server
// ApplicationDbContext created in the Data folder
// Pass the connection string to the sql server from appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Stripe connection settings
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));



builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Re-route default error pages from Identity.
// Default path does not contain Identity Area in URL
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

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
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

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
