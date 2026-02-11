using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PrihVPN.UI.ViewModels;
using PrihVPN.Infrastructure.Persistence;
using PrihVPN.Infrastructure.Network;

namespace PrihVPN.UI;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var services = new ServiceCollection();
        
        // Регистрация инфраструктуры
        services.AddSingleton<LiteDbRepository>();
        services.AddSingleton<WindowsProxyController>();
        
        // Регистрация UI
        services.AddSingleton<MainViewModel>();

        var provider = services.BuildServiceProvider();
        var mainWindow = new MainWindow { DataContext = provider.GetRequiredService<MainViewModel>() };
        mainWindow.Show();
    }
}
