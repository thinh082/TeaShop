using TeaShop.Data;
using TeaShop.Services;
using Microsoft.EntityFrameworkCore.SqlServer;
using TeaShop.Responsitory;
using Microsoft.EntityFrameworkCore;
using TeaShop.Respository;
using ProjectTMDT.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
// Add services to the container.
builder.Services.AddControllersWithViews();
//
builder.Services.AddScoped<UserRespository>();
//
builder.Services.AddScoped<PasswordServices>();
//Add MyDbContext
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnectionString")));
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
//Add Img Sv
builder.Services.AddScoped<ImageServices>();
// Add Email
builder.Services.AddScoped<EmailServices>();
//
builder.Services.AddScoped<ProductRepository>();
//
builder.Services.AddScoped<CartRespostory>();
// 
builder.Services.AddScoped<CheckOutRespository>();
// 
builder.Services.AddScoped<OrderRepository>();
// 
builder.Services.AddScoped<PaymentRepository>();
// Thêm dòng này để dùng Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); // thời gian timeout session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
var app = builder.Build();
// Thêm middleware sử dụng Session
app.UseSession();
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
    name: "admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.Run();
