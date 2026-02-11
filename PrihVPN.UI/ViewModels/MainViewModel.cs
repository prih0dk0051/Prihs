using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PrihVPN.Core.Models;
using PrihVPN.Infrastructure.Adapters;
using PrihVPN.Infrastructure.ConfigGenerators;
using PrihVPN.Infrastructure.Network;
using PrihVPN.Infrastructure.Persistence;
using PrihVPN.Infrastructure.Parsers;

namespace PrihVPN.UI.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly XrayAdapter _adapter = new();
    private readonly WindowsProxyController _proxyController = new();
    private readonly XrayConfigBuilder _configBuilder = new();
    private readonly LiteDbRepository _repository;

    [ObservableProperty] private string _status = "Ready";
    [ObservableProperty] private ObservableCollection<ServerProfile> _servers;
    [ObservableProperty] private ServerProfile? _selectedServer;

    public MainViewModel(LiteDbRepository repository)
    {
        _repository = repository;
        _servers = new ObservableCollection<ServerProfile>(_repository.GetAll());
    }

    [RelayCommand]
    public async Task Connect()
    {
        if (SelectedServer == null) return;
        
        Status = "Connecting...";
        var configJson = _configBuilder.Build(SelectedServer);
        var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
        await File.WriteAllTextAsync(configPath, configJson);

        await _adapter.StartAsync(configPath);
        _proxyController.SetProxy(true);
        Status = $"Connected to {SelectedServer.Name}";
    }

    [RelayCommand]
    public async Task Disconnect()
    {
        await _adapter.StopAsync();
        _proxyController.SetProxy(false);
        Status = "Disconnected";
    }

    [RelayCommand]
    public void ImportFromClipboard()
    {
        var text = System.Windows.Clipboard.GetText();
        var newServers = SubscriptionParser.Parse(text);
        foreach (var s in newServers)
        {
            _repository.SaveServer(s);
            Servers.Add(s);
        }
        Status = $"Imported {newServers.Count} servers";
    }
}
