using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Domain.Definition;

namespace JABARACdesign.Base.Infrastructure.Network.Client
{
    /// <summary>
    /// クラウドストレージのクライアントインターフェース。
    /// </summary>
    public interface ICloudStorageClient
    {
        /// <summary>
        /// ファイルをアップロードする。
        /// </summary>
        /// <typeparam name="TEnum">拡張子タイプの列挙型</typeparam>
        /// <param name="identifier">識別子</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>APIレスポンス</returns>
        UniTask<IAPIResponse> UploadFileAsync<TEnum>(
            TEnum identifier,
            CancellationToken cancellationToken = default) 
            where TEnum  : struct, Enum;

        /// <summary>
        /// ファイルをダウンロードする。
        /// </summary>
        /// <param name="identifier">識別子</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>APIレスポンス(ローカルのパス)</returns>
        UniTask<IAPIResponse<string>> DownloadFileAsync<TEnum>(
            TEnum identifier,
            CancellationToken cancellationToken = default)
            where TEnum : struct, Enum;

        /// <summary>
        /// ファイルの存在チェックを行う。
        /// </summary>
        /// <param name="identifier">識別子</param>
        /// <returns>APIレスポンス(ファイルの有無)</returns>
        UniTask<IAPIResponse<bool>> FileExistsAsync<TEnum>(TEnum identifier)
            where TEnum : struct, Enum;
    }
}