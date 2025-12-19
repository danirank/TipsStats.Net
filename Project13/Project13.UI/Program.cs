using Project13.UI.Components;
using Project13.UI.Services;

namespace Project13.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddHttpClient("CouponApi", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7055/");
            });

            // API 2 – FootballStats-API
            builder.Services.AddHttpClient("FootballApi", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7173/");
            });

            builder.Services.AddScoped<CouponApiService>();
            builder.Services.AddScoped<FootballStatsApiService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
