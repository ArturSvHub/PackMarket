using Blazorise;
using Blazorise.Icons.Material;
using Blazorise.Material;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders;

using PackMarket.Areas.Identity;
using PackMarket.Data;
using PackMarket.Data.Models;
using PackMarket.Services;

using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");
var postgresConnection = builder.Configuration.GetConnectionString("PostgresConnection");
var postgres2Connection = builder.Configuration.GetConnectionString("Postgres2Connection");
var postgresDefaultConnection = builder.Configuration.GetConnectionString("PostgresDefaultConnection");
//builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
//    options.UseSqlServer(SqlConnectionString));
builder.Services.AddDbContext<ApplicationDbContext>(opts =>
    opts.UseNpgsql(postgresConnection));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddCors(x => x.AddPolicy("External", policy => policy.WithOrigins("https://jsonip.com")));
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
})
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddHubOptions(options =>
{
    options.ClientTimeoutInterval = TimeSpan.FromSeconds(60);
    options.HandshakeTimeout = TimeSpan.FromSeconds(30);
});
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
builder.Services.AddScoped<RepositoryService>();
builder.Services.AddScoped<BlazorAppContext>();
builder.Services.AddHostedService<TimedHostedCartService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();

var app = builder.Build();
app.UseCors("External");
//if (app.Environment.IsDevelopment())
//{
//    app.UseMigrationsEndPoint();
//}
//else
//{
//    app.UseExceptionHandler("/Error");
//}
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
