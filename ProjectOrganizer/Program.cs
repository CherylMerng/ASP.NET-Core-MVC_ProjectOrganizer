using ProjectOrganizer.Interfaces;
using ProjectOrganizer.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 3. To use interface -> dependency injection
// Register services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProject, ProjectRepository>();
builder.Services.AddScoped<IModel, ModelRepository>();
builder.Services.AddScoped<IProperty, PropertyRepository>();

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

// routing - controllerName/controllerMethod
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Project}/{action=Dashboard}/{id?}");

app.Run();
