var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache(); // Session'ý kullanabilmek için bir cache mekanizmasýna ihtiyacýmýz var.
                                              // Bu örnekte, bellek içi bir cache kullanýyoruz.
                                              // Bu, uygulamanýn belleðinde oturum verilerini saklar.
                                              // Ancak, bu yöntem daðýtýk senaryolarda (örneðin, birden fazla sunucu arasýnda) uygun deðildir.
                                              // Daðýtýk senaryolar için Redis veya SQL Server
                                              // gibi daha saðlam bir cache mekanizmasý kullanmanýz gerekebilir.
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session'ýn 30 dakika boyunca boþta kalmasý durumunda sona ermesini saðlar.
    options.Cookie.HttpOnly = true; // Session cookie'sinin JavaScript tarafýndan eriþilmesini engeller.
    options.Cookie.IsEssential = true; // GDPR uyumluluðu için gerekli olabilir.
});
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
app.UseSession(); // Session'ý kullanabilmek için bu middleware'i eklememiz gerekiyor. Session'ý kullanabilmek için ayrýca builder.Services.AddSession() ile session servislerini de eklememiz gerekiyor. Bu kod snippet'inde bu kýsým eksik görünüyor, bu yüzden eklememiz gerekiyor. Aþaðýdaki gibi ekleyebiliriz:
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
