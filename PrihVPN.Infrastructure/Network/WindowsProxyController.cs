using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace PrihVPN.Infrastructure.Network;

public class WindowsProxyController
{
    [DllImport("wininet.dll")]
    public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

    public void SetProxy(bool enabled)
    {
        const string keyPath = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings";
        using var key = Registry.CurrentUser.OpenSubKey(keyPath, true);
        
        key?.SetValue("ProxyEnable", enabled ? 1 : 0);
        key?.SetValue("ProxyServer", "127.0.0.1:10809");

        // Уведомляем Windows об изменениях
        InternetSetOption(IntPtr.Zero, 39, IntPtr.Zero, 0); // INTERNET_OPTION_SETTINGS_CHANGED
        InternetSetOption(IntPtr.Zero, 37, IntPtr.Zero, 0); // INTERNET_OPTION_REFRESH
    }
}
