using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Reflection;
using IWshRuntimeLibrary;

class AppConsts
{
    // App Consts
    public static Version appVersion = Assembly.GetExecutingAssembly().GetName().Version;
    public static string appStartupShortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\Ato Assist.lnk";

    // Server Consts
    public static Uri uri = new Uri("https://ato-assist-server.onrender.com");
    public static HttpClient client = new HttpClient();

    public static string ServerResponse(string response) => JsonConvert.DeserializeObject<Dictionary<string, string>>(response)["message"];

    // File Consts
    public static string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/ato-assist";
    public static string jsonFileName = $"{appDataPath}/appSettings.json";
    public static string qrImgFileName = $"{appDataPath}/QR.png";
    public static void CreateDataFolder()
    {
        if(!Directory.Exists(appDataPath))
            Directory.CreateDirectory(appDataPath);
    }

    public static bool InternetConnected = NetworkInterface.GetIsNetworkAvailable();

    public static void CreateStartupShortcut(Action  loadingElement)
    {
        WshShell shell = new WshShell();
        IWshShortcut shortcut = shell.CreateShortcut(AppConsts.appStartupShortcutPath);
        shortcut.Description = "Ato Assist";
        shortcut.TargetPath = Assembly.GetExecutingAssembly().Location;
        shortcut.Save();
    }
}