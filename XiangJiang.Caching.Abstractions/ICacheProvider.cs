using System.Threading.Tasks;

namespace XiangJiang.Caching.Abstractions
{
    /// <summary>
    ///     缓存提供者接口
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        ///     根据Key获取缓存
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>缓存</returns>
        T Get<T>(string key);

        Task<T> GetAsync<T>(string key);

        /// <summary>
        ///     设置缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="data">值</param>
        /// <param name="cacheTimeMinute">过期时间，单位分钟</param>
        void Set(string key, object data, uint cacheTimeMinute);

        Task SetAsync(string key, object data, uint cacheTimeMinute);

        /// <summary>
        ///     是否设置缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>
        ///     <c>true</c> if the specified key is set; otherwise, <c>false</c>.
        /// </returns>
        bool IsSet(string key);

        Task<bool> IsSetAsync(string key);

        /// <summary>
        ///     移除缓存
        /// </summary>
        /// <param name="key">键</param>
        void Remove(string key);

        Task RemoveAsync(string key);

        /// <summary>
        ///     根据正则表达式移除缓存
        /// </summary>
        /// <param name="pattern">移除缓存</param>
        void RemoveByPattern(string pattern);

        Task RemoveByPatternAsync(string pattern);
    }
}