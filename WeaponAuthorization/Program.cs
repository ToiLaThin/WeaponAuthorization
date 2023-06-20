using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeaponAuthorization.Data;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("MyConnStr");
builder.Services.AddDbContext<WeaponContext>(options =>
    options.UseSqlServer(connString, action => action.MigrationsHistoryTable("_MigrationsHistory", "Heroes"))
);
builder.Services.AddDbContext<HeroIdentityContext>(options =>
    options.UseSqlServer(connString, action => action.MigrationsHistoryTable("_MigrationsHistory", "Identity"))
);

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<HeroIdentityContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication(); //required
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
