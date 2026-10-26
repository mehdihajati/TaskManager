using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using TaskManager.Application.Common.Interfaces;

namespace TaskManager.Infrastructure.Caching;

public class RedisCacheService : ICacheService
{
    private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(5);
    private readonly IDistributedCache _cache;

    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var json = await _cache.GetStringAsync(key, cancellationToken);
        return json is null ? default : JsonSerializer.Deserialize<T>(json);
    }
    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        return _cache.RemoveAsync(key, cancellationToken);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        var options = new DistributedCacheEntryOptions
        {

            AbsoluteExpirationRelativeToNow = expiration ?? DefaultExpiration
        };
        await _cache.SetStringAsync(key, JsonSerializer.Serialize(value), options, cancellationToken);
    }
}