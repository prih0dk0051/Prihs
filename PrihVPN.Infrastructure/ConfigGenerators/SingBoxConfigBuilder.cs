using PrihVPN.Core.Models;
using System.Text.Json;

namespace PrihVPN.Infrastructure.ConfigGenerators;

public class SingBoxConfigBuilder
{
    public string Build(ServerProfile profile, int localPort = 10809)
    {
        var config = new
        {
            inbounds = new[] {
                new { type = "socks", listen = "127.0.0.1", listen_port = localPort + 1 },
                new { type = "http", listen = "127.0.0.1", listen_port = localPort }
            },
            outbounds = new object[] {
                new {
                    type = profile.Type.ToString().ToLower(),
                    tag = "proxy",
                    server = profile.Address,
                    server_port = profile.Port,
                    // Добавь специфичные поля для WG/Hysteria здесь
                },
                new { type = "direct", tag = "direct" }
            }
        };
        return JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
    }
}
