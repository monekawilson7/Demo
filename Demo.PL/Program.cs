using Demo.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDepartmentService , DepartmentService>();
builder.Services.AddScoped<IDepartmentRepository , DepartmentRepository>();
//builder.Services.AddScoped<CompanyDBContext>(provider => 
//{
//    var builder = new DbContextOptionsBuilder<CompanyDBContext>();
//    builder.UseSqlServer("");
//    return new CompanyDBContext(builder.Options);
//});


builder.Services.AddDbContext<CompanyDBContext>(options => 
{
    //var _ = builder.Configuration["ConnectionString:DefaultConnection"];
    //var _ = builder.Configuration.GetSection("DefaultConnection")["ConnectionString:DefaultConnection"];
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseSqlServer(connectionString);
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

//app.UseRouting();

//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
