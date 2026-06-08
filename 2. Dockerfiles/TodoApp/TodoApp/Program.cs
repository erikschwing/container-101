internal class Program
{
    private static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();

        var app = builder.Build();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapRazorPages();

        app.Logger.LogInformation("ContentRoot={ContentRoot}", app.Environment.ContentRootPath);
        app.Logger.LogInformation("WebRoot={WebRoot}", app.Environment.WebRootPath);

        app.Run();

    }
}