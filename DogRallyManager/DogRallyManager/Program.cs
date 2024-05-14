using DogRallyManager.DbContexts;
using DogRallyManager.Entities;
using DogRallyManager.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// This line is added to compare changes in a pull request for testing purposes.

// The connection string is to be found in the appsettings.Development.json file
builder.Services.AddDbContext<DogRallyDbContext>(
    dbContextOptions => dbContextOptions.UseSqlite(
        builder.Configuration.GetConnectionString("DogRallySQLiteConnectionString")));

// Adding AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Adding dataservice
builder.Services.AddTransient<IDataService, DemoDataService>();
// Adding messagecreation service
builder.Services.AddTransient<IMessageCreationService, MessageCreationService>();

// Sets up identity on our custom classes that extends IdentityUser and IdentityRole classes. 
// So far these classes have just been made to make the class names more app-specific-appropriate.
builder.Services.AddIdentity<RallyUser, RallyUserRole>().AddEntityFrameworkStores<DogRallyDbContext>();

// Any request made for a resource that the client is not authorized for it directed to the login-page.
builder.Services.ConfigureApplicationCookie(
    config => config.LoginPath = "/account");

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

// @@@[FYT 3]
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();