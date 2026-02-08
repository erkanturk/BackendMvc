using _11_AutoMapper.Dto;
using _11_AutoMapper.MappingProfile;
using _11_AutoMapper.Models;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddAutoMapper(typeof(UserProfile)); // AutoMapper'ý UserProfile sýnýfýný kullanarak yapýlandýrýyoruz 13.0.1 versiyonunda bu þekilde kullanýlýr. 12.0 versiyonunda ise aþaðýdaki gibi yapýlandýrýlýrdý.
builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<User, UserDto>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
