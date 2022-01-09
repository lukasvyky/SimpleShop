using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Database;
using Stripe;
using System;
using System.Linq;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe")["SecretKey"];

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Cart";
    options.Cookie.MaxAge = TimeSpan.FromMinutes(20);
});
builder.Services.AddMvc(options => options.EnableEndpointRouting = false)
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AuthorizeFolder("/Admin");
        options.Conventions.AuthorizePage("/Admin/ConfigureUsers", "Admin");
    });

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddApplicationServices();

SetupAuthenticationAndAuthor(builder);

var app = builder.Build();

SeedDb(app);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseAuthentication();
app.UseMvcWithDefaultRoute();

app.Run();

void SeedDb(WebApplication webApplication)
{
    try
    {
        using var scope = webApplication.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.EnsureCreated();

        if (!dbContext.Users.Any())
        {
            var adminUser = new IdentityUser()
            {
                UserName = "Admin"
            };

            var managerUser = new IdentityUser()
            {
                UserName = "Manager"
            };

            userManager.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
            userManager.CreateAsync(managerUser, "password").GetAwaiter().GetResult();

            var adminClaim = new Claim("Role", "Admin");
            var managerClaim = new Claim("Role", "Manager");

            userManager.AddClaimAsync(adminUser, adminClaim).GetAwaiter().GetResult(); ;
            userManager.AddClaimAsync(managerUser, managerClaim).GetAwaiter().GetResult(); ;
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}

void SetupAuthenticationAndAuthor(WebApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>();

    webApplicationBuilder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Accounts/Login");

    webApplicationBuilder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("Admin", policy => policy.RequireClaim("Role", "Admin"));
        options.AddPolicy("Manager", policy =>
            policy.RequireAssertion(context =>
                context.User.HasClaim("Role", "Manager") ||
                context.User.HasClaim("Role", "Admin")
            ));
    });
}