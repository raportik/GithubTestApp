using GithubTestApp.Models;
using GithubTestApp.Pages;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//builder.WebHost.UseUrls("http://*:1313");
builder.Services.AddDbContext<TodoContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHangfire(configuration =>
    configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                 .UseSimpleAssemblyNameTypeSerializer()
                 .UseRecommendedSerializerSettings()
                 .UsePostgreSqlStorage(builder.Configuration.GetConnectionString("DefaultConnection"), new PostgreSqlStorageOptions
                 {
                     InvisibilityTimeout = TimeSpan.FromMinutes(5),
                     QueuePollInterval = TimeSpan.FromSeconds(15),
                     DistributedLockTimeout = TimeSpan.FromMinutes(10)
                 }));

builder.Services.AddHangfireServer();

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
app.UseHangfireDashboard();

RecurringJob.AddOrUpdate("midnightJob", () => BackgorundService.Test() , Cron.Daily(23, 30)); // Her gün gece 00:00

app.UseAuthorization();

app.MapRazorPages();

app.Run();
