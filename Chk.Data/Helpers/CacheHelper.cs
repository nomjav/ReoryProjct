using System;
using System.Web;
using System.Web.Caching;

namespace AcademyLockSmith.Data.Helpers
{
    public static class CacheHelper
    {
        /// <summary>
        ///     Insert value into the cache using
        ///     appropriate name/value pairs
        /// </summary>
        public static void Add<T>(T value, string key, int cacheMinutes = 15)
        {
            try
            {
                // NOTE: Apply expiration parameters as you see fit.
                // I typically pull from configuration file.

                // In this example, I want an absolute
                // timeout so changes will always be reflected
                // at that time. Hence, the NoSlidingExpiration.
                HttpRuntime.Cache.Insert(
                key,
                value,
                null,
                DateTime.Now.AddMinutes(cacheMinutes),
                Cache.NoSlidingExpiration,
                CacheItemPriority.High,
                null
                );
            }
            #pragma warning disable 
            catch (Exception exception)
            {
                //Logger.Logger.LogException(exception.Message, exception);
            }
        }

        /// <summary>
        ///     Remove item from cache
        /// </summary>
        public static void Remove(string key)
        {
            if (Exists(key))
                HttpRuntime.Cache.Remove(key);
        }

        /// <summary>
        ///     Check for item in cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        /// <returns></returns>
        public static bool Exists(string key)
        {
            return HttpRuntime.Cache[key] != null;
        }

        /// <summary>
        ///     Retrieve cached item
        /// </summary>
        public static bool Get<T>(string key, out T value)
        {
            try
            {
                if (!Exists(key))
                {
                    value = default(T);
                    return false;
                }

                value = (T) HttpRuntime.Cache[key];
            }
            catch (Exception exception)
            {
                //Logger.Logger.LogException(exception.Message, exception);
                value = default(T);
                return false;
            }

            return true;
        }

        public static T Get<T>(string key)
        {
            try
            {
                if (!Exists(key))
                {
                    return default(T);
                }

                return (T) HttpRuntime.Cache[key];
            }
            catch (Exception exception)
            {
                //Logger.Logger.LogException(exception.Message, exception);
                return default(T);
            }
        }
             
            //custom functions
        public static T GetValueByKey<T>(string key)
        {
            T value;
            try
            {
                if (!Exists(key))
                {
                    value = default(T);
                    return value;
                }

                value = (T)HttpRuntime.Cache[key];
            } 
            catch (Exception exception)
            {
                //Logger.Logger.LogException(exception.Message, exception);
                value = default(T);
                return value;
            }

            return value;
        }

    }
}