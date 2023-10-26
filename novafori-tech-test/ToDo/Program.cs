using ToDo.BusinessLogic;
using ToDo.BusinessLogic.Interfaces;
using ToDo.Repositories;
using ToDo.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

// Add memory cache to the container

builder.Services.AddMemoryCache();

builder.Services.AddSingleton<IUserTaskLogic, UserTaskLogic>();
builder.Services.AddSingleton<IUserTaskRepository, UserTaskRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();