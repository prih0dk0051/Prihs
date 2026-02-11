using LiteDB;
using PrihVPN.Core.Models;

namespace PrihVPN.Infrastructure.Persistence;

public class LiteDbRepository
{
    private readonly string _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.db");

    public void SaveServer(ServerProfile profile)
    {
        using var db = new LiteDatabase(_dbPath);
        db.GetCollection<ServerProfile>("servers").Upsert(profile);
    }

    public List<ServerProfile> GetAll()
    {
        using var db = new LiteDatabase(_dbPath);
        return db.GetCollection<ServerProfile>("servers").FindAll().ToList();
    }
}
