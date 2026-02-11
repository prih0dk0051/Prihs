using System.Collections.Concurrent;

namespace PrihVPN.Infrastructure.Network;

public class LogBuffer
{
    private readonly ConcurrentQueue<string> _logs = new();
    private const int MaxLines = 100;

    public void Add(string line)
    {
        _logs.Enqueue($"[{DateTime.Now:HH:mm:ss}] {line}");
        while (_logs.Count > MaxLines) _logs.TryDequeue(out _);
    }

    public string GetFullLog() => string.Join(Environment.NewLine, _logs);
    public void Clear() => _logs.Clear();
}
