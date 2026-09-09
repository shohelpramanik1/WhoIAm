using StackExchange.Redis;
using System.Text.Json;
using WhoIAm.Application.Interfaces;

namespace WhoIAm.Infrastructure.Services;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _db;
    private const string CacheKeyPrefix = "whoiam:";

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _db.StringGetAsync(CacheKeyPrefix + key);
        if (!value.HasValue)
            return default;

        return JsonSerializer.Deserialize<T>(value.ToString());
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        var json = JsonSerializer.Serialize(value);
        await _db.StringSetAsync(CacheKeyPrefix + key, json, expiration);
    }

    public async Task RemoveAsync(string key)
    {
        await _db.KeyDeleteAsync(CacheKeyPrefix + key);
    }

    public async Task RemoveByPatternAsync(string pattern)
    {
        var server = _db.Multiplexer.GetServer(_db.Multiplexer.GetEndPoints().First());
        var keys = server.Keys(pattern: CacheKeyPrefix + pattern);
        foreach (var key in keys)
        {
            await _db.KeyDeleteAsync(key);
        }
    }
}
