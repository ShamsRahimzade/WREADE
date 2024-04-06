
using Stripe;
using WEB.Middlewares;
using Wreade.Application;
using Wreade.Persistence.ServiceRegistration;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();

StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
app.UseAuthorization();
//app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
	  name: "areas",
	  pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
	);
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=AppUser}/{action=register}/{id?}");

app.Run();
