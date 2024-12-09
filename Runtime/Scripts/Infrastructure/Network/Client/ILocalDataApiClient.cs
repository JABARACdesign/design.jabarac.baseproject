using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;

namespace JABARACdesign.Base.Infrastructure.Network.Client
{
    /// <summary>
    /// ローカルデータAPIクライアント
    /// </summary>
    public interface ILocalDataApiClient
    {
        /// <summary>
        /// 指定した型に対応する単一のデータを非同期で取得する
        /// </summary>
        /// <typeparam name="TDomainDataDto">取得するデータの型</typeparam>
        /// <returns>APIレスポンス。成功時はデータを含み、失敗時はエラーメッセージを含む</returns>
        public UniTask<IAPIResponse<TDomainDataDto>> GetSingleAsync<TDomainDataDto>();

        /// <summary>
        /// 指定した型に対応するデータのリストを非同期で取得する
        /// </summary>
        /// <typeparam name="TDomainDataDto">取得するデータの型</typeparam>
        /// <returns>APIレスポンス。成功時はデータリストを含み、失敗時はエラーメッセージを含む</returns>
        public UniTask<IAPIResponse<List<TDomainDataDto>>> GetListAsync<TDomainDataDto>();
        
        /// <summary>
        /// 指定した型に対応する単一のデータを非同期で保存する
        /// </summary>
        /// <typeparam name="TDomainDataDto">保存するデータの型</typeparam>
        /// <param name="data">保存するデータ</param>
        /// <returns>APIレスポンス。成功時は成功状態を、失敗時はエラーメッセージを含む</returns>
        public UniTask<IAPIResponse> SaveSingleAsync<TDomainDataDto>(TDomainDataDto data);
        
        /// <summary>
        /// 指定した型に対応するデータのリストを非同期で保存する
        /// </summary>
        /// <typeparam name="TDomainDataDto">保存するデータの型</typeparam>
        /// <param name="dataList">保存するデータのリスト</param>
        /// <returns>APIレスポンス。成功時は成功状態を、失敗時はエラーメッセージを含む</returns>
        public UniTask<IAPIResponse> SaveListAsync<TDomainDataDto>(List<TDomainDataDto> dataList);
    }
}