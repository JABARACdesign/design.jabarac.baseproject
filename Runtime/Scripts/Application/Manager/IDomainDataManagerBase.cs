using System.Collections.Generic;
using JABARACdesign.Base.Domain.Entity;

namespace JABARACdesign.Base.Application.Manager
{
    public interface IDomainDataManagerBase
    {
        /// <summary>
        /// 指定されたキーに対応するデータを取得する。
        /// </summary>
        /// <param name="key">キー</param>
        /// <typeparam name="TDomainData">データの型</typeparam>
        /// <typeparam name="TKey">キーの型</typeparam>
        /// <returns>データ</returns>
        public TDomainData GetData<TDomainData, TKey>(TKey key)
            where TDomainData : IDomainData;
        
        /// <summary>
        /// 指定されたデータのリストを取得する。
        /// </summary>
        /// <typeparam name="TDomainData">データの型</typeparam>
        /// <returns>データのリスト</returns>
        public List<TDomainData> GetAllData<TDomainData>()
            where TDomainData : IDomainData;
        
        /// <summary>
        /// データを追加する。
        /// </summary>
        /// <param name="data">追加するデータ</param>
        /// <typeparam name="TDomainData">データの型</typeparam>
        /// <typeparam name="TKey">キーの型</typeparam>
        public void AddData<TDomainData, TKey>(TDomainData data)
            where TDomainData : IDomainData;
        
        /// <summary>
        /// データのリストを追加する。
        /// </summary>
        /// <param name="dataList">データのリスト</param>
        /// <typeparam name="TDomainData">データの型</typeparam>
        /// <typeparam name="TKey">キーの型</typeparam>
        public void AddDataList<TDomainData, TKey>(List<TDomainData> dataList)
            where TDomainData : IDomainData;
    }
}