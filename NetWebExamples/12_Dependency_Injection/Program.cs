using _12_Dependency_Injection.Services;

var builder = WebApplication.CreateBuilder(args);

/* 
     IMyService i ve MyService i container a ekliyoruz.
    .NET CORE da Dependency Injection da servislerin ömrünü 
    (lifecycle) belirtmek için üç ana yöntem kullanýlýr. AddTransient, AddScoped, AddSingelton
    1. AddTransient
    Her istek için yeni bir nesne oluþturulur. Yani bir servis her kullandýldýðýnda yeni bir instance oluþturulur.
    Bu durum maliyetlidir. Performans açýsýndan sýkýntýlar yaratabilir
    2. AddScoped
    Her istek için yeni bir nesne oluþturulur. 
Ayný istek içinde ayný servis nesnesi kullanýlýr. Ancak farklý isteklerde farklý nesneler oluþturulur.
    3. AddSingelton
    Uygulama baþladýðýnda bir kez oluþturlan ve uygulama yaþam döngüsü boyunca ayný
kalan tek bir nesne oluþuturulur ve kullanýlýr. Performans açýsýndan en verimlisidir. Çünkü nesne bir kez oluþturulur.

 
 */
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ScopedRandomNumberService>();
builder.Services.AddTransient<TransientRandomNumberService>();
builder.Services.AddSingleton<SingletonRandomNumberService>();

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

app.Run();
