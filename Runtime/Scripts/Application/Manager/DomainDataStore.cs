using System;
using System.Collections.Generic;
using System.Linq;
using JABARACdesign.Base.Domain.Entity;

namespace JABARACdesign.Base.Application.Manager
{
    /// <summary>
    /// データのストアクラス。データのキャッシュや取得を担う。
    /// </summary>
    /// <typeparam name="TDomainData">データの型</typeparam>
    /// <typeparam name="TKey">キーの型</typeparam>
    public class DomainDataStore<TDomainData, TKey>
        where TDomainData : IDomainData
    {
        private readonly Dictionary<TKey, TDomainData> _dataMap = new();
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="domainDataList">ドメインデータのリスト</param>
        /// <param name="getKeyFunc">キーを取得する関数</param>
        public DomainDataStore(List<TDomainData> domainDataList, Func<TDomainData, TKey> getKeyFunc)
        {
            CreateDomainDataMap(domainDataList: domainDataList, getKeyFunc: getKeyFunc);
        }
        
        /// <summary>
        /// ドメインデータのマップを作成する
        /// </summary>
        /// <param name="domainDataList">ドメインデータのリスト</param>
        /// <param name="getKeyFunc">キーを取得する関数</param>
        private void CreateDomainDataMap(List<TDomainData> domainDataList, Func<TDomainData, TKey> getKeyFunc)
        {
            foreach (var data in domainDataList)
            {
                _dataMap.TryAdd(key: getKeyFunc(arg: data), value: data);
            }
        }
        
        /// <summary>
        /// 主キーを指定してデータを取得する
        /// </summary>
        /// <param name="key">マスタデータの主キー</param>
        /// <returns>マスタデータ</returns>
        public TDomainData GetByKey(TKey key)
        {
            _dataMap.TryGetValue(key: key, value: out var data);
            
            return data;
        }
        
        /// <summary>
        /// 全てのマスタデータを取得する
        /// </summary>
        /// <returns>マスタデータのリスト</returns>
        public List<TDomainData> GetAll()
        {
            return typeof(ISortable).IsAssignableFrom(c: typeof(TDomainData))
                ? _dataMap.Values.OrderBy(x => ((ISortable)x).SortOrder).ToList()
                : _dataMap.Values.ToList();
        }
        
        /// <summary>
        /// データを追加する。
        /// </summary>
        /// <param name="data">追加するデータ</param>
        public void Add(TDomainData data)
        {
            _dataMap.TryAdd(key: default, value: data);
        }
        
        /// <summary>
        /// データリストを追加する。
        /// </summary>
        /// <param name="dataList">データリスト</param>
        public void AddRange(List<TDomainData> dataList)
        {
            foreach (var data in dataList)
            {
                Add(data: data);
            }
        }
    }
}