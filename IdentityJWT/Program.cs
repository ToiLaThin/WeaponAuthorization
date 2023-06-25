using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeaponAuthorization.Data;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var connString = builder.Configuration.GetConnectionString("MyConnStr");
builder.Services.AddDbContext<WeaponContext>(options =>
    options.UseSqlServer(connString, action => action.MigrationsHistoryTable("_MigrationsHistory", "Heroes"))
);
builder.Services.AddDbContext<HeroIdentityContext>(options =>
    options.UseSqlServer(connString, action => action.MigrationsHistoryTable("_MigrationsHistory", "Identity"))
);
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<HeroIdentityContext>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
