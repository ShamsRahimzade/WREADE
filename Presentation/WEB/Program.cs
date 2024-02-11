using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
using Wreade.Application;
using Wreade.Persistence.ServiceRegistration;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
	  name: "areas",
	  pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
	);
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
