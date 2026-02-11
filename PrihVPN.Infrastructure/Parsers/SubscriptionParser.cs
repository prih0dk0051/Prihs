using System.Text;
using PrihVPN.Core.Models;

namespace PrihVPN.Infrastructure.Parsers;

public static class SubscriptionParser
{
    public static List<ServerProfile> Parse(string rawData)
    {
        var profiles = new List<ServerProfile>();
        string decoded = rawData;

        // Если это Base64 (стандарт для v2ray подписок)
        try {
            byte[] data = Convert.FromBase64String(rawData);
            decoded = Encoding.UTF8.GetString(data);
        } catch { /* Значит это обычный текст со ссылками */ }

        var lines = decoded.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            if (line.StartsWith("vless://")) 
                profiles.Add(VlessParser.Parse(line));
            // Здесь можно добавить вызовы для других протоколов (ss://, trojan://)
        }
        return profiles;
    }
}
