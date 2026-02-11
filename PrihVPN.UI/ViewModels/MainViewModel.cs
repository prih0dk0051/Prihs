using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PrihVPN.Core.Models;
using PrihVPN.Infrastructure.Adapters;
using PrihVPN.Infrastructure.ConfigGenerators;
using PrihVPN.Infrastructure.Network;

namespace PrihVPN.UI.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly XrayAdapter _adapter = new();
    private readonly WindowsProxyController _proxyController = new();
    private readonly XrayConfigBuilder _configBuilder = new();

    [ObservableProperty] private string _status = "Disconnected";

    [RelayCommand]
    public async Task Connect(ServerProfile profile)
    {
        Status = "Connecting...";
        
        // 1. Генерируем конфиг
        string configJson = _configBuilder.Build(profile);
        string configPath = Path.Combine(AppContext.BaseDirectory, "config.json");
        await File.WriteAllTextAsync(configPath, configJson);

        // 2. Запускаем Xray
        await _adapter.StartAsync(configPath);

        // 3. Включаем прокси в Windows
        _proxyController.SetProxy(true, "127.0.0.1:10809");
        
        Status = "Connected";
    }

    [RelayCommand]
    public async Task Disconnect()
    {
        await _adapter.StopAsync();
        _proxyController.SetProxy(false);
        Status = "Disconnected";
    }
}
