var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

// map controllers that use attribute routing – often not necessary
app.MapControllers(); 

// most specific route - 5 required segments
app.MapControllerRoute(
    name: "paging_and_sorting",
    pattern: "{controller}/{action}/{id}/page{num}/sort-by-{sortby}");

// less specific route - 4 required segments
app.MapControllerRoute(
    name: "paging",
    pattern: "{controller}/{action}/{id}/page{num}");

// least specific route - 0 required segments
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapDefaultControllerRoute();   // another way to configure default route

app.Run();
