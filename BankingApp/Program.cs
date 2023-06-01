using BankingApp.Data;
using BankingApp.Models;
using BankingApp.Resources;
using BankingApp.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
	.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
	// enables immediate logout, after updating the user's stat.
	options.ValidationInterval = TimeSpan.Zero;
});

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("apiLayer", c =>
{
	c.BaseAddress = new Uri("https://api.apilayer.com/");
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

const string defaultCulture = "en-US";
var supportedCultures = new[]
{
	new CultureInfo(defaultCulture),
	new CultureInfo("fr-CA"),
	new CultureInfo("es-Es")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	options.DefaultRequestCulture = new RequestCulture(defaultCulture);
	options.SupportedCultures = supportedCultures;
	options.SupportedUICultures = supportedCultures;
});

builder.Services.AddMvc()
				.AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
				.AddDataAnnotationsLocalization();


//builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddTransient<ISharedViewLocalizer, SharedViewLocalizer>();

builder.WebHost.UseSentry(o =>
{
	o.Dsn = "https://23d5817eaf9d43dfbfbef53d9c6a2441@o474576.ingest.sentry.io/4504937432350720";
	// When configuring for the first time, to see what the SDK is doing:
	o.Debug = true;
	// Set TracesSampleRate to 1.0 to capture 100% of transactions for performance monitoring.
	// We recommend adjusting this value in production.
	o.TracesSampleRate = 1.0;
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();

	using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
	{
		var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
		context.Database.Migrate();
	}
}

app.UseSentryTracing();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(
		   Path.Combine(builder.Environment.ContentRootPath, "MyStaticFiles")),
	RequestPath = "/StaticFiles"
});


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
