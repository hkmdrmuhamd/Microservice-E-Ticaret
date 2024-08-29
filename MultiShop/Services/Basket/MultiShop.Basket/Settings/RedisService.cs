using StackExchange.Redis;

namespace MultiShop.Basket.Settings
{
    public class RedisService
    {
        public string _host { get; set; }
        public int _port { get; set; }
        private ConnectionMultiplexer _connectionMultiplexer; // ConnectionMultiplexer, Redis'e yapılan bağlantıları yöneten sınıftır
        public RedisService(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public void Connect() => _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}"); // Redis'e bağlanma işlemi
        
        public IDatabase GetDb(int db = 0) => _connectionMultiplexer.GetDatabase(db);// Bu işlem Redis'e bağlı 0 numaralı veritabanına erişim sağlamak için kullanılır.
    }
}
