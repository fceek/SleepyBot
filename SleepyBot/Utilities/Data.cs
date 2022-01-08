using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace SleepyBot.Utilities
{
    
    public static class Data
    {
        //本地缓存
        private static Dictionary<string, List<string>>? rest_Cache;

        private static IDatabase? db;

        private static IDatabase Db
        {
            get
            {
                if (db == null)
                {
                    db = Connect();
                }

                return db;
            }
        }

        private static IDatabase Connect()
        {
            ConfigurationOptions conf = new ConfigurationOptions
            {
                EndPoints = {Secret.REDIS_ENDPOINT}
            };
            conf.Password = Secret.REDIS_AUTHKEY;
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(conf,Console.Out);
            return redis.GetDatabase();
        }

        public static string GetBlogLink(long id)
        {
            string key = "Blog:" + id.ToString();
            string value = Db.StringGet(key);
            Console.WriteLine($"Read from Redis {key}:{value}");
            return value;
        }

        public static bool SetBlogLink(long id, string addr)
        {
            string key = "Blog:" + id.ToString();
            return Db.StringSet(key, addr);
        }

        private static void InitRestaurantCache(string cacheKey)
        {
            rest_Cache ??= new Dictionary<string, List<string>>();

            if (!rest_Cache.ContainsKey(cacheKey))
            {
                if (!TryCacheRestaurantData(cacheKey))
                {
                    rest_Cache.Add(cacheKey, new List<string>());
                }
            }
        } 

        public static string GetRandomRestaurant(long id)
        {
            string cacheKey = id.ToString();

            InitRestaurantCache(cacheKey);

            if (rest_Cache[cacheKey].Count >= 1)
            {
                return $"就去吃{rest_Cache[cacheKey].GetRandom()}";
            }

            return "没得吃！没得吃！";
        }

        public static string GetAllRestaurant(long id)
        {
            string cacheKey = id.ToString();

            InitRestaurantCache(cacheKey);

            string response = string.Empty;
            if (rest_Cache[cacheKey].Count >= 1)
            {
                int order = 0;
                foreach (string place in rest_Cache[cacheKey])
                {
                    response += $"{++order}. {place}\n";
                }
            }
            else
            {
                response = "没得吃！没得吃！";
            }

            return response;
        }

        public static string AddRestaurant(long id, string rawPlace)
        {
            string cacheKey = id.ToString();

            InitRestaurantCache(cacheKey);

            if (rest_Cache[cacheKey].Contains(rawPlace))
            {
                return "地点已存在";
            }
            rest_Cache[cacheKey].Add(rawPlace);
            bool success = SubmitRestaurantData(cacheKey);
            if (success)
            {
                return "地点已提交";
            }
            rest_Cache[cacheKey].Remove(rawPlace);
            return "地点提交失败";
        }

        private static bool SubmitRestaurantData(string key)
        {
            string value = String.Empty;
            foreach (var place in rest_Cache[key])
            {
                value += (place + "|");
            }

            value = value.Substring(0, value.Length - 1);
            string dbKey = "Restaurant:" + key;
            return Db.StringSet(dbKey, value);
        }

        private static bool TryCacheRestaurantData(string key)
        {
            string dbKey = "Restaurant:" + key;
            if (Db.KeyExists(dbKey))
            {
                string value = db.StringGet(dbKey);
                if (string.IsNullOrEmpty(value)) return false;
                rest_Cache.Add(key, value.Split('|').ToList());
                rest_Cache[key].RemoveAll(s => string.IsNullOrEmpty(s));
                return true;
            }
            return false;
        }

        public static bool TestDBConnection()
        {
            string key = "persistTest";
            if ((int) Db.StringGet(key) == 123) return true;
            return false;
        }
    }
}
