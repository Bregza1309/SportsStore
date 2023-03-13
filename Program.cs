using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add database context
builder.Services.AddDbContext<StoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SportsStoreConnection"));
});
//add Identity Database
builder.Services.AddDbContext<StoreIdentityContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<StoreIdentityContext>();
//Add Store data services
builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();
    

//add support for razor pages
builder.Services.AddRazorPages();

//add support for sessions
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();


//add cart service
builder.Services.AddScoped(sp => SessionCart.GetCart(sp));
//add OrderRepository service
builder.Services.AddScoped<IOrderRepository,EFOrderRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//add blazor server side
builder.Services.AddServerSideBlazor();

//build web app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute("catpage","{category}/Page{productPage:int}",new {Controller = "Home",action = "Index",productPage = 1});
app.MapControllerRoute("page", "Page{productPage:int}", new { Controller = "Home", action = "Index", productPage = 1 });
app.MapControllerRoute("category","{category}", new {Controller = "Home", action = "Index" , productPage = 1});
app.MapControllerRoute("pagination",
    "Products/Page{productPage}",new {Controller = "Home",Action = "Index",productPage = 1});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/admin/{*catchall}" ,"/Admin/Index");
SeedData.EnsurePopulated(app);
IdentitySeedData.EnsurePopulated(app);
app.Run();
