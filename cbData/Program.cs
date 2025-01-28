using cbData.BE.BusinessLogic.Controllers;
using cbData.BE.DB.DataContext;
using cbData.BE.DB.Services;
using cbData.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

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


builder.Services.AddScoped<ProductDbService>();

builder.Services.AddScoped<ProductController>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

	options.IncludeXmlComments(xmlPath);
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "cbData",
		Version = "v1",
		Description = ""
	});
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}


app.UseSwagger();
app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
	c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});

app.UseStaticFiles();
app.UseAntiforgery();

app.UseHttpsRedirection();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();


app.Run();
