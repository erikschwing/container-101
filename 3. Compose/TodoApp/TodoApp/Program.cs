using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TodoApp.Database;

internal class Program
{
    private static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        string connectionString = builder.Configuration.GetConnectionString("default") ?? "";

        builder.Services.AddDbContext<TodoContext>(options =>
            options.UseNpgsql(connectionString));

        builder.Services.AddRazorPages();

        var app = builder.Build();

        app.UseRouting();
        app.UseStaticFiles();

        app.MapRazorPages();

        // Auto-load the migration
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<TodoContext>();
            db.Database.Migrate();
        }

        app.Run();
    }
}