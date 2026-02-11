namespace PrihVPN.Infrastructure.ConfigGenerators;

public static class RoutingBuilder
{
    public static object BuildDefaultRouting()
    {
        return new
        {
            domainStrategy = "IPIfNonMatch",
            rules = new[]
            {
                // Пример: блокировка рекламы
                new { type = "field", outbondTag = "block", domain = new[] { "geosite:category-ads-all" } },
                // Пример: прямой доступ для локальных ресурсов
                new { type = "field", outboundTag = "direct", domain = new[] { "geosite:cn", "geosite:ru" } },
                // Всё остальное в прокси
                new { type = "field", outboundTag = "proxy", network = "tcp,udp" }
            }
        };
    }
}
