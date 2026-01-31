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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "products",//Bu route 'a verilen isim (isteðe baðlý ama faydalý yapý)
    pattern: "products/details/{id?}",//Eþleþmesi gereken url deðeri id deðeri boþ da gelebilir
    defaults: new { controller = "Home", action = "Details" }//bu url deðeri eþleþirse çalýþacak controller ve action yapýsý
  
    );

app.Run();
