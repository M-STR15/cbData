using cbData.BE.DB.DataContext;
using cbData.Components;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

#if DEBUG
var connectionString = builder.Configuration.GetConnectionString("cbDataDb-local");
#else
var connectionString = builder.Configuration.GetConnectionString("cbDataDb");
#endif

builder.Services.AddDbContextFactory<CbDataDbContext>(options => options.UseSqlServer(connectionString)
			.EnableSensitiveDataLogging()
			.LogTo(Console.WriteLine), ServiceLifetime.Singleton);




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
