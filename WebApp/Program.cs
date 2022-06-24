using Microsoft.EntityFrameworkCore;
using UnitOfWork.Interfaces;
using UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

var sqlString = builder.Configuration.GetConnectionString("MsSqlConnection");

builder.Services.AddDbContext<Domain.ApplicationDbContext>(options => options.UseSqlServer(sqlString));

builder.Services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();

builder.Services.AddAutoMapper(
    typeof(AutoMapperRepositoryEntity), 
    typeof(AutoMapperServiceEntity));

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

app.Run();
