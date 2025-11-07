var builder = WebApplication.CreateBuilder(args);

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
    name: "adminList",
    pattern: "Admin/Student/List",
    defaults: new { controller = "Student", action = "Index" }
);

app.MapControllerRoute(
    name: "adminAdd",
    pattern: "Admin/Student/Add",
    defaults: new { controller = "Student", action = "Create" }
);
app.MapControllerRoute(
    name: "admin",
    pattern: "Admin/Student/{action}",
    defaults: new { controller = "Student" });  

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
