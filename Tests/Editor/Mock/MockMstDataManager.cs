using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Manager;
using JABARACdesign.Base.Domain.Entity;

namespace JABARACdesign.Base.Tests.Mock
{
    /// <summary>
    /// テスト用のモックマスターデータマネージャ。
    /// 任意のデータを登録して取得できる。
    /// </summary>
    public class MockMstDataManager : IMstDataManager
    {
        private readonly Dictionary<(Type, object), object> _dataMap = new();
        private readonly Dictionary<Type, object> _allDataMap = new();
        
        public UniTask SetUpAsync()
        {
            return UniTask.CompletedTask;
        }
        
        /// <summary>
        /// テスト用のデータを登録する。
        /// </summary>
        /// <typeparam name="TDomainData">データの型</typeparam>
        /// <typeparam name="TKey">キーの型</typeparam>
        /// <param name="key">キー</param>
        /// <param name="data">データ</param>
        /// <returns>メソッドチェーン用にthisを返す</returns>
        public MockMstDataManager WithData<TDomainData, TKey>(TKey key, TDomainData data)
            where TDomainData : IDomainData
        {
            _dataMap[(typeof(TDomainData), key)] = data;
            return this;
        }
        
        /// <summary>
        /// テスト用のデータリストを登録する。
        /// </summary>
        /// <typeparam name="TDomainData">データの型</typeparam>
        /// <param name="dataList">データリスト</param>
        /// <returns>メソッドチェーン用にthisを返す</returns>
        public MockMstDataManager WithDataList<TDomainData>(List<TDomainData> dataList)
            where TDomainData : IDomainData
        {
            _allDataMap[typeof(TDomainData)] = dataList;
            return this;
        }
        
        public TDomainData GetData<TDomainData, TKey>(TKey key)
            where TDomainData : IDomainData
        {
            var compositeKey = (typeof(TDomainData), (object)key);
            return _dataMap.TryGetValue(key: compositeKey, value: out var data)
                ? (TDomainData)data
                : default;
        }
        
        public List<TDomainData> GetAllData<TDomainData>()
            where TDomainData : IDomainData
        {
            return _allDataMap.TryGetValue(key: typeof(TDomainData), value: out var data)
                ? (List<TDomainData>)data
                : new List<TDomainData>();
        }
        
        public void AddData<TDomainData, TKey>(TDomainData data)
            where TDomainData : IDomainData
        {
        }
        
        public void AddDataList<TDomainData, TKey>(List<TDomainData> dataList)
            where TDomainData : IDomainData
        {
        }
    }
}
