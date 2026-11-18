namespace ECommerceApp;

public static class AppConfig
{
    public static string ApiBaseUrl =>
#if ANDROID
        "http://10.0.2.2:5080/";
#else
        "http://localhost:5080/";
#endif
}
