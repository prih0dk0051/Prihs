namespace PrihVPN.Core.Interfaces;

public enum CoreState { Stopped, Starting, Running, Error }

public interface ICoreAdapter
{
    CoreState State { get; }
    Task StartAsync(string configPath);
    Task StopAsync();
    event EventHandler<string> LogReceived;
}
