﻿using System.Threading;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Domain.Interface;
using JABARACdesign.Base.Infrastructure.Dto.API;
using JABARACdesign.Base.Infrastructure.Network.API;

namespace JABARACdesign.Base.Infrastructure.Network.Client
{
    /// <summary>
    /// Function（サーバーレス関数）APIクライアントのインターフェース。
    /// </summary>
    public interface IFunctionApiClient
    {
        /// <summary>
        /// データを取得するFunction呼び出し
        /// </summary>
        /// <typeparam name="TResponseData">レスポンスデータの型</typeparam>
        /// <param name="request">APIリクエスト</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>APIレスポンス</returns>
        UniTask<IAPIResponse<TResponseData>> SendAsync<TResponseData>(
            IAPIRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// データ付きリクエストを送信するFunction呼び出し
        /// </summary>
        /// <typeparam name="TDto">リクエストDTOの型</typeparam>
        /// <typeparam name="TResponseData">レスポンスデータの型</typeparam>
        /// <param name="request">DTO付きAPIリクエスト</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>APIレスポンス</returns>
        UniTask<IAPIResponse<TResponseData>> SendAsync<TDto, TResponseData>(
            IApiRequest<TDto> request,
            CancellationToken cancellationToken = default)
            where TDto : IQueryParamConvertible;
            
        /// <summary>
        /// レスポンスデータ不要のFunction呼び出し
        /// </summary>
        /// <param name="request">APIリクエスト</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>APIレスポンス</returns>
        UniTask<IAPIResponse> SendAsync(
            IAPIRequest request,
            CancellationToken cancellationToken = default);
            
        /// <summary>
        /// レスポンスデータ不要のDTO付きFunction呼び出し
        /// </summary>
        /// <typeparam name="TDto">リクエストDTOの型</typeparam>
        /// <param name="request">DTO付きAPIリクエスト</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>APIレスポンス</returns>
        UniTask<IAPIResponse> SendAsync<TDto>(
            IApiRequest<TDto> request,
            CancellationToken cancellationToken = default)
            where TDto : IQueryParamConvertible;
    }
}