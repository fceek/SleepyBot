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

        public static bool TestDBConnection()
        {
            string key = "persistTest";
            if ((int) Db.StringGet(key) == 123) return true;
            return false;
        }
    }
}
