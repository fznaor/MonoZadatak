using Autofac;
using Autofac.Extensions.DependencyInjection;
using Project.MVC;
using Project.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new DIModule());
        builder.RegisterModule(new AutoMapperModule());
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Makes}/{action=Index}/{id?}");

app.Run();
