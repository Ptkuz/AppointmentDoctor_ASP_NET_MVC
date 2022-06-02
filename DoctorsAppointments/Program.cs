using Microsoft.EntityFrameworkCore;
using DoctorsAppointments.Models;
using DoctorsAppointments.Models.DataBase;
using DoctorsAppointments;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;


var builder = WebApplication.CreateBuilder(args);
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection), ServiceLifetime.Scoped);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Account/login");
        options.AccessDeniedPath = new PathString("/Account/Login");
    });
    
builder.Services.AddMvc();
var app = builder.Build();


app.UseHttpsRedirection();
app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();    // аутентификация
app.UseAuthorization();     // авторизация

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"
    );

app.MapControllerRoute(
    name: "Account",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");



app.Run();
