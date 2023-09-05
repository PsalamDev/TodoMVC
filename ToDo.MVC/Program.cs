
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using ToDo.MVC.Models;
using static System.Formats.Asn1.AsnWriter;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDbContext>(options => options.
 UseInMemoryDatabase("Todos"));

//Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<TodoDbContext>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DbContext>(options => options.UseInMemoryDatabase(connectionString));

//IServiceProvider provider;
//    TodoDbSeeder.Seed(provider.CreateScope());


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    TodoDbSeeder.Seed(services);
}

//Configure the HTTP request pipeline.





if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();