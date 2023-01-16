using Blazorise;
using Blazorise.Icons.Material;
using Blazorise.Material;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders;

using PackMarket.Areas.Identity;
using PackMarket.Data;
using PackMarket.Data.Models;
using PackMarket.Services;

using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var SqlConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var postgresConnectionString = builder.Configuration.GetConnectionString("PostgresConnection");
//builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
//    options.UseSqlServer(SqlConnectionString));
builder.Services.AddDbContext<ApplicationDbContext>(opts =>
    opts.UseNpgsql(postgresConnectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddIdentity<User, IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
})
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazorise(opts =>
{ opts.Immediate = true; })
    .AddMaterialProviders()
    .AddMaterialIcons();
builder.Services.Configure<WebEncoderOptions>(options =>
{
    options.TextEncoderSettings = new System
    .Text.Encodings.Web
    .TextEncoderSettings(System.Text.Unicode.UnicodeRanges.All);
});
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<PackMarket.Data.Models.User>>();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.KnownProxies.Add(IPAddress.Parse("127.0.0.1"));
});
builder.Services.AddScoped<DataCrudService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
}
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor |
    ForwardedHeaders.XForwardedProto
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
