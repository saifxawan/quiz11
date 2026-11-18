using ECommerceApp.Infrastructure.Api;
using ECommerceApp.Infrastructure.Security;
using ECommerceApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace ECommerceApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<ITokenStore, SecureTokenStore>();
        builder.Services.AddTransient<AuthHeaderHandler>();
        builder.Services.AddHttpClient<IStoreApiClient, StoreApiClient>(client =>
        {
            client.BaseAddress = new Uri(AppConfig.ApiBaseUrl);
            client.Timeout = TimeSpan.FromSeconds(20);
        }).AddHttpMessageHandler<AuthHeaderHandler>();
        builder.Services.AddAuthorizationCore();
        builder.Services.AddSingleton<JwtAuthenticationStateProvider>();
        builder.Services.AddSingleton<AuthenticationStateProvider>(sp => sp.GetRequiredService<JwtAuthenticationStateProvider>());
        builder.Services.AddSingleton<IAuthService, AuthService>();

        return builder.Build();
    }
}
