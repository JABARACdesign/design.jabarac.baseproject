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
        /// <param name="userId">ユーザーID</param>
        /// <param name="identifier">識別子</param>
        /// <param name="extensionType">拡張子タイプ</param>
        /// <param name="fileType">ファイルタイプ</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>APIレスポンス</returns>
        public UniTask<IAPIResponse> UploadFileAsync(
            string userId,
            string identifier,
            StorageDefinition.ExtensionType extensionType,
            StorageDefinition.FileType fileType,
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// ファイルをダウンロードする。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="identifier">識別子</param>
        /// <param name="extensionType">拡張子タイプ</param>
        /// <param name="fileType">ファイルタイプ</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>APIレスポンス(ローカルのパス)</returns>
        public UniTask<IAPIResponse<string>> DownloadFileAsync(
            string userId,
            string identifier,
            StorageDefinition.ExtensionType extensionType,
            StorageDefinition.FileType fileType,
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// ファイルの存在チェックを行う。
        /// </summary>
        /// <param name="identifier">識別子</param>
        /// <param name="extensionType">拡張子タイプ</param>
        /// <param name="fileType">ファイルタイプ</param>
        /// <returns>APIレスポンス(ファイルの有無)</returns>
        public UniTask<IAPIResponse<bool>> FileExistsAsync(
            string identifier,
            StorageDefinition.ExtensionType extensionType,
            StorageDefinition.FileType fileType);
    }
}