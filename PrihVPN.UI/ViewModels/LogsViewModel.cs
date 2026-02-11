using CommunityToolkit.Mvvm.ComponentModel;
using PrihVPN.Infrastructure.Network;

namespace PrihVPN.UI.ViewModels;

public partial class LogsViewModel : ObservableObject
{
    [ObservableProperty] private string _logContent = "";

    public void UpdateFromBuffer(LogBuffer buffer)
    {
        LogContent = buffer.GetFullLog();
    }
}
