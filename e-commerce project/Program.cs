using e_commerce_project.Controllers;
using e_commerce_project.Modles;
using e_commerce_project.Repository;
using e_commerce_project.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
       .AddNewtonsoftJson();
builder.Services.AddDbContext<sql_e_commerce_DB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IProducts, Sql_Products__Repository>();
builder.Services.AddScoped<ICart, Sql_Cart_Repository>();
builder.Services.AddScoped<IWishlist, Sql_Wishlist_Repository>();
builder.Services.AddIdentity<Users, IdentityRole>()
       .AddEntityFrameworkStores<sql_e_commerce_DB>();
       

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await RoleSeeder.SeedRolesAsync(roleManager);
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
