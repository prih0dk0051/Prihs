using PrihVPN.Core.Models;
using System.Text.Json;

namespace PrihVPN.Infrastructure.ConfigGenerators;

public class XrayConfigBuilder
{
    public string Build(ServerProfile profile, int localPort = 10809)
    {
        var config = new
        {
            log = new { loglevel = "warning" },
            inbounds = new[] {
                new {
                    port = localPort,
                    protocol = "http", // Для системного прокси Windows
                    settings = new { }
                }
            },
            outbounds = new[] {
                new {
                    protocol = "vless",
                    settings = new {
                        vnext = new[] {
                            new {
                                address = profile.Address,
                                port = profile.Port,
                                users = new[] {
                                    new {
                                        id = profile.UUID,
                                        encryption = "none",
                                        flow = profile.Flow
                                    }
                                }
                            }
                        }
                    },
                    streamSettings = new {
                        network = "tcp",
                        security = "reality",
                        realitySettings = new {
                            show = false,
                            fingerprint = profile.Fingerprint,
                            serverName = profile.Sni,
                            publicKey = profile.PublicKey,
                            shortId = profile.ShortId
                        }
                    }
                },
                new { protocol = "freedom", tag = "direct" }
            }
        };

        return JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
    }
}
