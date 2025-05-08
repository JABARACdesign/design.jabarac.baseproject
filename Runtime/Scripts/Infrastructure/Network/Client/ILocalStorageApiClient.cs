using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Domain.Definition;
using UnityEngine;
using System.Collections.Generic;

namespace JABARACdesign.Base.Infrastructure.Network.Client
{
    /// <summary>
    /// ローカルストレージのAPIクライアントインターフェース。
    /// </summary>
    public interface ILocalStorageApiClient
    {
        /// <summary>
        /// ローカルファイルが存在するかどうかを返す。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="identifier">識別子</param>
        /// <param name="extensionType">拡張子</param>
        /// <param name="fileType">ファイルのタイプ</param>
        /// <returns>ローカルファイルが存在するかどうか</returns>
        public bool IsLocalFileExists(
            string userId,
            string identifier,
            StorageDefinition.ExtensionType extensionType,
            StorageDefinition.FileType fileType);
        
        /// <summary>
        /// ローカルからテクスチャをロードする。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="identifier">識別子</param>
        /// <param name="extensionType">拡張子</param>
        /// <param name="fileType">取得するファイルのタイプ</param>
        /// <returns>APIレスポンス(Texture2D)</returns>
        public UniTask<IAPIResponse<Texture2D>> LoadTextureAsync(
            string userId,
            string identifier,
            StorageDefinition.ExtensionType extensionType,
            StorageDefinition.FileType fileType);
        
        /// <summary>
        /// ローカルから音声をロードする。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="identifier">識別子</param>
        /// <param name="extensionType">拡張子</param>
        /// <param name="fileType">取得するファイルのタイプ</param>
        /// <returns>APIレスポンス(AudioClip)</returns>
        public UniTask<IAPIResponse<AudioClip>> LoadAudioAsync(
            string userId,
            string identifier,
            StorageDefinition.ExtensionType extensionType,
            StorageDefinition.FileType fileType);

        /// <summary>
        /// ローカルからJSON配列データをロードしてリストとして取得する。
        /// </summary>
        /// <typeparam name="T">JSONデータの型</typeparam>
        /// <param name="userId">ユーザーID</param>
        /// <param name="identifier">識別子</param>
        /// <param name="extensionType">拡張子</param>
        /// <param name="fileType">取得するファイルのタイプ</param>
        /// <returns>APIレスポンス(T型のリスト)</returns>
        UniTask<IAPIResponse<List<T>>> LoadJsonListAsync<T>(
            string userId,
            string identifier,
            StorageDefinition.ExtensionType extensionType,
            StorageDefinition.FileType fileType);

        /// <summary>
        /// ローカルからJSON配列データをロードして配列として取得する。
        /// </summary>
        /// <typeparam name="T">JSONデータの型</typeparam>
        /// <param name="userId">ユーザーID</param>
        /// <param name="identifier">識別子</param>
        /// <param name="extensionType">拡張子</param>
        /// <param name="fileType">取得するファイルのタイプ</param>
        /// <returns>APIレスポンス(T型の配列)</returns>
        UniTask<IAPIResponse<T[]>> LoadJsonArrayAsync<T>(
            string userId,
            string identifier,
            StorageDefinition.ExtensionType extensionType,
            StorageDefinition.FileType fileType);
    }
}