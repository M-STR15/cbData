using cbData.BE.BusinessLogic.Services;
using cbData.BE.DB.DataContext;
using cbData.Components;
using cbData.Services;
using cbData.Shared.Services;
using cbData.Shared.Stories;
using cbData.Stories;
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

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClient("ApiClient", (sp, client) =>
{
	var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
	var request = httpContextAccessor.HttpContext?.Request;

	var baseAddress = request != null ? $"{request.Scheme}://{request.Host.Value}" : "https://localhost:7246/";
	client.BaseAddress = new Uri(baseAddress);
});

builder.Services.AddDbContextFactory<CbDataDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

builder.Services.AddSingleton<PathsStory>();
builder.Services.AddSingleton<IEventLogService, EventLogService>();
builder.Services.AddCbDataBeBusinessLogicService();

builder.Services.AddSingleton<CJsonService>();
builder.Services.AddSingleton<ProductStory>();

builder.Services.AddControllers();
//.AddApplicationPart(typeof(ProductController).Assembly);

builder.Services.AddHostedService<TimedHostedService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

	var beXmlPath = Path.Combine(AppContext.BaseDirectory, "cbData.BE.BusinessLogic.xml");

	options.IncludeXmlComments(xmlPath);
	options.IncludeXmlComments(beXmlPath);

	options.EnableAnnotations();
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "cbData",
		Version = "v1",
		Description = ""
	});
});

var app = builder.Build();

app.UseRouting();

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
	c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
});

app.UseStaticFiles();
app.UseAntiforgery();

app.UseHttpsRedirection();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.MapControllers();

app.Run();
