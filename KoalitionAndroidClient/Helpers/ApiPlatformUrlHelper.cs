namespace KoalitionAndroidClient.Helpers
{
    public static class ApiPlatformUrlHelper
    {
        public static string GetPlatformApiUrl()
        {
#if __ANDROID__
        return "http://10.0.2.2:5127";
#else
        return "http://localhost:5127";
#endif
        }
    }
}
