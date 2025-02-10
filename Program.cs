using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Models;
using MyApp.Repositories;
using MyApp.ViewModels;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddEnvironmentVariables();


var connectionString = builder.Configuration["ConnectionStrings:DefaultConnectionString"];

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("❌ Connection string not found in environment variables.");
}


builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<MyAppContext>(options =>
    options.UseSqlServer(connectionString));


// Configurarea Identity, setarea regulilor de securitate
builder.Services.AddIdentity<Users, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;  // configureaza regulile de securitate
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MyAppContext>()
    .AddDefaultTokenProviders();


builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ITemeRepository, TemeRepository>();


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await InitializeRolesAndAdmin(services);
}

async Task InitializeRolesAndAdmin(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var accountRepository = serviceProvider.GetRequiredService<IAccountRepository>();

    
    var roles = new[] { "Profesor" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    
    string email = "admin@admin.com";
    string password = "Test1234";

    var existingUser = await accountRepository.FindByEmailAsync(email);
    if (existingUser == null)
    {
        var registerModel = new RegisterViewModel
        {
            Name = "Profesor",
            Email = email,
            Password = password
        };

        var result = await accountRepository.RegisterAsync(registerModel);
        if (result.Succeeded)
        {
            var user = await accountRepository.FindByEmailAsync(email);
            await accountRepository.AddUserToRoleAsync(user, "Profesor");
        }
    }
}

app.Run();
