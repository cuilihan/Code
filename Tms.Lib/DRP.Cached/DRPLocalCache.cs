using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRP.Cached
{
    /// <summary>
    /// 本地静态缓存类，使Dictionary存储缓存数据（无过期时间）
    /// 创建人：胡怀清
    /// 日期：2014-9-4
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class DRPLocalCache<T>
    {
        #region << 属性 >>

        /// <summary>
        /// 缓存数据集合
        /// 创建人：胡怀清
        /// 日期：2014-9-4
        /// </summary>
        private Dictionary<string, T> _CacheDatas;

        /// <summary>
        /// 缓存项数量
        /// 创建人：胡怀清
        /// 日期：2014-9-4
        /// </summary>
        public int Count
        {
            get
            {
                return this._CacheDatas.Count;
            }
        }

        #endregion

        #region << 构造函数 >>

        /// <summary>
        /// 构造函数
        /// 创建人：胡怀清
        /// 日期：2014-9-4
        /// </summary>
        public DRPLocalCache()
        {
            this._CacheDatas = new Dictionary<string, T>();
        }

        #endregion

        #region << 公共方法 >>

        /// <summary>
        /// 插入缓存数据，重复则覆盖
        /// 创建人：胡怀清
        /// 日期：2014-9-4
        /// </summary>
        /// <param name="key">数据的键</param>
        /// <param name="value">要缓存的数据</param>
        public void Insert(string key, T value)
        {
            if (String.IsNullOrEmpty(key)
                || value == null)
                return;

            lock (this._CacheDatas)
            {
                this._CacheDatas[key] = value;
            }
        }

        /// <summary>
        /// 移除缓存：若缓存同步方法实现，则移除缓存的同时也执行缓存同步方法
        /// 创建人：胡怀清
        /// 日期：2014-9-4
        /// </summary>
        /// <param name="key">数据的键</param>
        public void Remove(string key)
        {
            if (String.IsNullOrEmpty(key))
                return;

            lock (this._CacheDatas)
            {
                this._CacheDatas.Remove(key);
            }
        }

        /// <summary>
        /// 获取缓存信息
        /// 创建人：胡怀清
        /// 日期：2014-9-4
        /// </summary>
        /// <param name="key">数据的键</param>
        /// <returns>返回缓存的数据</returns>
        public T Get(string key)
        {
            // 键值为空直接返回默认
            if (String.IsNullOrEmpty(key))
                return default(T);

            T item;
            bool isGet = this._CacheDatas.TryGetValue(key, out item);
            if (isGet)
            {
                return item;
            }

            return default(T);
        }

        /// <summary>
        /// 判断当前缓存中是否存在该键的缓存项
        /// 创建人：胡怀清
        /// 日期：2014-9-4
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns>返回是否存在</returns>
        public bool Exists(string key)
        {
            if (String.IsNullOrEmpty(key))
                return false;

            T item;
            bool isGet = this._CacheDatas.TryGetValue(key, out item);
            if (isGet)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 清空所有缓存
        /// 创建人：胡怀清
        /// 日期：2014-9-4
        /// </summary>
        public void Clear()
        {
            lock (this._CacheDatas)
            {
                this._CacheDatas.Clear();
            }
        }

        #endregion

    }
}
