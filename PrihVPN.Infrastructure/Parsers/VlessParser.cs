using System.Web;
using PrihVPN.Core.Models;

namespace PrihVPN.Infrastructure.Parsers;

public static class VlessParser
{
    public static ServerProfile Parse(string url)
    {
        var uri = new Uri(url);
        var query = HttpUtility.ParseQueryString(uri.Query);
        
        return new ServerProfile
        {
            Type = ProxyType.Vless,
            Name = uri.Fragment.TrimStart('#') ?? "Imported VLESS",
            Address = uri.Host,
            Port = uri.Port,
            UUID = uri.UserInfo,
            Sni = query["sni"],
            PublicKey = query["pbk"],
            ShortId = query["sid"],
            Fingerprint = query["fp"] ?? "chrome",
            Flow = query["flow"]
        };
    }
}
