using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromXml("NLog.config").GetCurrentClassLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
builder.Host.UseNLog();


builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPropertiesAndHeaders | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponseStatusCode;
});

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    await next();
});

app.MapRazorPages();

app.Run();
