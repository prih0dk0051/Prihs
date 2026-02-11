namespace PrihVPN.Core.Models;

public enum ProxyType { Vless, Vmess, Shadowsocks, Trojan, WireGuard, Hysteria2, Tuic }

public class ServerProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "New Server";
    public string Address { get; set; } = "";
    public int Port { get; set; }
    public ProxyType Type { get; set; }
    
    // Для VLESS REALITY
    public string? UUID { get; set; }
    public string? PublicKey { get; set; }
    public string? ShortId { get; set; }
    public string? Sni { get; set; }
    public string? Fingerprint { get; set; } = "chrome";
    public string? Flow { get; set; } = "xtls-rprx-vision";
    
    // Для WireGuard
    public string? PrivateKey { get; set; }
    public string? LocalAddress { get; set; }
}
