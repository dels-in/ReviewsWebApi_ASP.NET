using StackExchange.Redis;

namespace Review.Domain.Services;

public class ConnectionHelper
{
    static ConnectionHelper()
    {
        _lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(ConfigurationManager.AppSetting["RedisURL"]));
    }

    private static Lazy<ConnectionMultiplexer> _lazyConnection;
    public static ConnectionMultiplexer Connection => _lazyConnection.Value;
}