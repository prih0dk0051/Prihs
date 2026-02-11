using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PrihVPN.UI.ViewModels;
using PrihVPN.Infrastructure.Persistence;

namespace PrihVPN.UI;

public partial class App : Application
{
    public static IServiceProvider? Services { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var serviceCollection = new ServiceCollection();
        
        // Регистрация слоев
        serviceCollection.AddSingleton<LiteDbRepository>();
        serviceCollection.AddSingleton<MainViewModel>();

        Services = serviceCollection.BuildServiceProvider();

        var mainWindow = new MainWindow { DataContext = Services.GetRequiredService<MainViewModel>() };
        mainWindow.Show();
    }
}
