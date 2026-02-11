using System.Diagnostics;
using PrihVPN.Core.Interfaces;

namespace PrihVPN.Infrastructure.Adapters;

public class XrayAdapter
{
    private Process? _process;

    public async Task StartAsync(string configPath)
    {
        await Task.Run(() => {
            _process = new Process();
            _process.StartInfo.FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "xray.exe");
            _process.StartInfo.Arguments = $"-config \"{configPath}\"";
            _process.StartInfo.CreateNoWindow = true;
            _process.StartInfo.UseShellExecute = false;
            _process.Start();
        });
    }

    public async Task StopAsync()
    {
        await Task.Run(() => {
            if (_process != null && !_process.HasExited)
            {
                _process.Kill(true);
            }
        });
    }
}
