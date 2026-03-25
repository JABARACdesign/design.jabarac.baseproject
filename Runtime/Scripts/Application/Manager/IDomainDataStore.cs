using System.Collections.Generic;
using JABARACdesign.Base.Domain.Entity;

namespace JABARACdesign.Base.Application.Manager
{
    /// <summary>
    /// DomainDataStoreの非ジェネリックインターフェース。
    /// TKeyの型に依存せずにデータを取得するために使用する。
    /// </summary>
    /// <typeparam name="TDomainData">データの型</typeparam>
    public interface IDomainDataStore<TDomainData>
        where TDomainData : IDomainData
    {
        /// <summary>
        /// 全てのデータを取得する。
        /// </summary>
        /// <returns>データのリスト</returns>
        List<TDomainData> GetAll();
    }
}
