namespace BestUnitPriceApp;

public static class Constants
{
    #if DEBUG
    public static string RestUrl = "https://bup-core.azurewebsites.net/{0}";

    // public static string RestUrl = DeviceInfo.Platform == DevicePlatform.Android
    //          ? "http://10.0.2.2:5000/{0}" 
    //        : "http://localhost:5000/{0}";
    #else 
    public static string RestUrl = "https://bup-core.azurewebsites.net/{0}";
    #endif
    
}