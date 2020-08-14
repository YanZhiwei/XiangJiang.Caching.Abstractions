using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace XiangJiang.Caching.Abstractions
{
    /// <summary>
    ///     缓存辅助类
    /// </summary>
    public static class CacheHelper
    {
        /// <summary>
        ///     获取缓存
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="cacheManager">ICacheProvider</param>
        /// <param name="key">键</param>
        /// <param name="keySelector">条件委托</param>
        /// <returns>缓存</returns>
        public static T Get<T>(this ICacheProvider cacheManager, string key, Func<T> keySelector)
        {
            return Get(cacheManager, key, 60, keySelector);
        }


        /// <summary>
        ///     获取缓存
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="cacheManager">ICacheProvider</param>
        /// <param name="key">键</param>
        /// <param name="cacheTimeMinute">缓存时间，单位分钟</param>
        /// <param name="keySelector">条件委托</param>
        /// <returns>缓存</returns>
        public static T Get<T>(this ICacheProvider cacheManager, string key, uint cacheTimeMinute, Func<T> keySelector)
        {
            if (cacheManager.IsSet(key)) 
                return cacheManager.Get<T>(key);

            var result = keySelector();
            if (cacheTimeMinute > 0)
                cacheManager.Set(key, result, cacheTimeMinute);

            return result;
        }

        /// <summary>
        ///     根据正则表达式移除缓存
        /// </summary>
        /// <param name="cacheManager">ICacheProvider</param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="keys">要移除的缓存key</param>
        public static void RemoveByPattern(this ICacheProvider cacheManager, string pattern, IEnumerable<string> keys)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (var key in keys.Where(p => regex.IsMatch(p.ToString())).ToList()) cacheManager.Remove(key);
        }
    }
}