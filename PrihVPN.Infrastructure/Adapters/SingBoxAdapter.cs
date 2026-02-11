using System.Diagnostics;
using PrihVPN.Core.Interfaces;

namespace PrihVPN.Infrastructure.Adapters;

public class SingBoxAdapter : ICoreAdapter
{
    private Process? _process;
    public CoreState State { get; private set; } = CoreState.Stopped;
    public event EventHandler<string>? LogReceived;

    public async Task StartAsync(string configPath)
    {
        _process = new Process {
            StartInfo = new ProcessStartInfo {
                FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "sing-box.exe"),
                Arguments = $"run -c \"{configPath}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };
        _process.OutputDataReceived += (s, e) => { if(e.Data != null) LogReceived?.Invoke(this, e.Data); };
        _process.Start();
        _process.BeginOutputReadLine();
        State = CoreState.Running;
    }

    public async Task StopAsync() { _process?.Kill(); State = CoreState.Stopped; return Task.CompletedTask; }
}
