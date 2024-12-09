using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Domain.Entity;
using JABARACdesign.Base.Domain.Entity.Helper;

namespace JABARACdesign.Base.Application.Manager
{
    public abstract class DomainDataManagerBase : ManagerBase, IDomainDataManagerBase
    {
        private readonly Dictionary<Type, object> _dataStores = new();
        
        /// <summary>
        /// 指定されたデータを取得してデータストアに登録する。
        /// </summary>
        /// <typeparam name="TDomainData">データのエンティティの型</typeparam>
        /// <typeparam name="TKey">データのキーの型</typeparam>
        /// <param name="getDataFunc">データを取得する関数</param>
        /// <param name="getKeyFunc">エンティティからキーを取得する関数</param>
        protected async UniTask<List<TDomainData>> RegisterDataListAsync<TDomainData, TKey>(
            Func<UniTask<IAPIResponse<List<TDomainData>>>> getDataFunc,
            Func<TDomainData, TKey> getKeyFunc)
            where TDomainData : IDomainData
        {
            // データを取得する
            var response = await getDataFunc();
            var handledResponse = await HandleAPIResponseAsync(response: response);
            var entityList = handledResponse ?? new List<TDomainData>();
            
            // データストアに追加する
            AddDataStore(
                dataList: entityList,
                getKeyFunc: getKeyFunc);
            
            return entityList;
        }
        
        /// <summary>
        /// データストアを追加する
        /// </summary>
        /// <param name="dataList">データのリスト</param>
        /// <param name="getKeyFunc">キーを取得する関数</param>
        /// <typeparam name="TDomainData">データの型</typeparam>
        /// <typeparam name="TKey">ストアのキーの型</typeparam>
        private void AddDataStore<TDomainData, TKey>(
            List<TDomainData> dataList,
            Func<TDomainData, TKey> getKeyFunc)
            where TDomainData : IDomainData
        {
            var store = new DomainDataStore<TDomainData, TKey>(domainDataList: dataList, getKeyFunc: getKeyFunc);
            _dataStores.TryAdd(key: typeof(TDomainData), value: store);
            
            LogHelper.Log(message: $"データストアに{typeof(TDomainData).Name}を追加しました。");
        }
        
        /// <summary>
        /// 指定されたキーに対応するデータを取得する。
        /// </summary>
        /// <param name="key">キー</param>
        /// <typeparam name="TDomainData">データの型</typeparam>
        /// <typeparam name="TKey">キーの型</typeparam>
        /// <returns>データ</returns>
        public TDomainData GetData<TDomainData, TKey>(TKey key)
            where TDomainData : IDomainData
        {
            if (!_dataStores.TryGetValue(key: typeof(TDomainData), value: out var store))
            {
                return default;
            }
            
            var dataStore = (DomainDataStore<TDomainData, TKey>)store;
            return dataStore.GetByKey(key: key);
        }
        
        /// <summary>
        /// 指定されたデータのリストを取得する。
        /// </summary>
        /// <typeparam name="TDomainData">データの型</typeparam>
        /// <returns>データのリスト</returns>
        public List<TDomainData> GetAllData<TDomainData>()
            where TDomainData : IDomainData
        {
            if (!_dataStores.TryGetValue(key: typeof(TDomainData), value: out var store))
            {
                return default;
            }
            
            var dataStore = (DomainDataStore<TDomainData, string>)store;
            return dataStore.GetAll();
        }
        
        /// <summary>
        /// データを追加する。
        /// </summary>
        /// <param name="data">追加するデータ</param>
        /// <typeparam name="TDomainData">データの型</typeparam>
        /// <typeparam name="TKey">キーの型</typeparam>
        public virtual void AddData<TDomainData, TKey>(TDomainData data)
            where TDomainData : IDomainData
        {
            if (!_dataStores.TryGetValue(key: typeof(TDomainData), value: out var store))
            {
                return;
            }
            
            var dataStore = (DomainDataStore<TDomainData, TKey>)store;
            dataStore.Add(data: data);
        }
        
        /// <summary>
        /// データのリストを追加する。
        /// </summary>
        /// <param name="dataList">データのリスト</param>
        /// <typeparam name="TDomainData">データの型</typeparam>
        /// <typeparam name="TKey">キーの型</typeparam>
        public virtual void AddDataList<TDomainData, TKey>(List<TDomainData> dataList)
            where TDomainData : IDomainData
        {
            if (!_dataStores.TryGetValue(key: typeof(TDomainData), value: out var store))
            {
                return;
            }
            
            var dataStore = (DomainDataStore<TDomainData, TKey>)store;
            dataStore.AddRange(dataList: dataList);
        }
    }
}