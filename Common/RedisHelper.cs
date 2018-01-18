using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Common
{
    /// <summary>
    /// Redis辅助类
    /// </summary>
    public class RedisHelper
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private static string ConnStr = ConfigurationManager.AppSettings["rpath"];
        

        /// <summary>
        /// 锁
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// 连接对象
        /// </summary>
        private volatile IConnectionMultiplexer _connection;

        private IDatabase _db;

        public RedisHelper()
        {
            _connection = ConnectionMultiplexer.Connect(ConnStr);
            _db = GetDataBase();
        }

        protected IConnectionMultiplexer GetConnection()
        {
            if (_connection != null && _connection.IsConnected)
            {
                return _connection;
            }

            lock (_lock)
            {
                if (_connection != null && _connection.IsConnected)
                {
                    return _connection;
                }

                if (_connection != null)
                {
                    _connection.Dispose();
                }

                _connection = ConnectionMultiplexer.Connect(ConnStr);
            }

            return _connection;
        }

        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public IDatabase GetDataBase(int? db = null)
        {
            return GetConnection().GetDatabase(db ?? -1);
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTime"></param>
        public virtual void Set(string key, object data, int cacheTime)
        {
            if (_db == null)
            {
                return;
            }

            var entryBytes = Serialize(data);
            var expiresIn = TimeSpan.FromMinutes(cacheTime);
            _db.StringSet(key, entryBytes, expiresIn);
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual T Get<T>(string key)
        {
            var value = _db.StringGet(key);
            if (!value.HasValue)
            {
                return default(T);
            }

            var result = Deserialize<T>(value);
            return result;
        }

        public bool IsSet(string key)
        {
            return _db.KeyExists(key);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private byte[] Serialize(object data)
        {
            var json = JsonConvert.SerializeObject(data);
            return Encoding.UTF8.GetBytes(json);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializeObject"></param>
        /// <returns></returns>
        protected virtual T Deserialize<T>(byte[] serializeObject)
        {
            if (serializeObject == null)
            {
                return default(T);
            }
            var json = Encoding.UTF8.GetString(serializeObject);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
