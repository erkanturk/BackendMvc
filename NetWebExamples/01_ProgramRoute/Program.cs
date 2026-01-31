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
    pattern: "{controller=Home}/{action=Index}/{id?}"
 );
app.MapControllerRoute(
    name: "about",
    pattern: "hakkimizda",
    defaults: new { controller = "Home", action = "About" }
    );
app.MapControllerRoute(
    name: "blogDetails",//Bu route 'a verilen isim (isteðe baðlý ama faydalý yapý)
    pattern: "blog/details/{id}",//Eþleþmesi gereken url deðeri
    defaults: new { controller = "Blog", action = "Details" },//bu url deðeri eþleþirse çalýþacak controller ve action yapýsý
    constraints: new { id=@"\d+"}//id parametresi sadece sayýlardan (0-9) oluþmalý
    );

app.Run();
