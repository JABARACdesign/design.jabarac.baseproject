using System;
using System.IO;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Domain.Entity.API;
using JABARACdesign.Base.Domain.Entity.Helper;
using JABARACdesign.Base.Domain.Helper;
using JABARACdesign.Base.Infrastructure.Network.API;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

namespace JABARACdesign.Base.Infrastructure.Network.Client.LocalData
{
    /// <summary>
    /// ローカルストレージのAPIクライアントクラス。
    /// </summary>
    public class LocalStorageApiClient : ILocalStorageApiClient
    {
        private const int DEFAULT_TEXTURE_SIZE = 2;
        
        private readonly IPathProvider _pathProvider;
        
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="pathProvider">ローカルパスプロパイダ</param>
        public LocalStorageApiClient(IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }
        
        /// <summary>
        /// 対象のパスが存在するかどうかを確認し、存在しない場合はディレクトリを作成する。
        /// </summary>
        /// <param name="path">対象のパス</param>
        private static void EnsureDirectoryExists(string path)
        {
            var directoryPath = Path.GetDirectoryName(path: path);
            if (Directory.Exists(path: directoryPath)) return;

            if (directoryPath == null) return;
            
            Directory.CreateDirectory(path: directoryPath);
            LogHelper.Info(message: $"Created directory: {directoryPath}");
        }
        
        /// <summary>
        /// ローカルファイルが存在するかどうかを返す。
        /// </summary>
        /// <param name="identifier">識別子</param>
        /// <returns>ローカルファイルが存在するかどうか</returns>
        public bool IsLocalFileExists<TEnum>(TEnum identifier)
        where TEnum : struct, Enum
        {
            var localFilePath = _pathProvider.GetPath(identifier: identifier);
            
            EnsureDirectoryExists(path: localFilePath);
            
            return File.Exists(path: localFilePath);
        }
        
        /// <summary>
        /// ローカルからテクスチャをロードする。
        /// </summary>
        /// <param name="identifier">識別子</param>
        /// <returns>APIレスポンス(Texture2D)</returns>
        public async UniTask<IAPIResponse<Texture2D>> LoadTextureAsync<TEnum>(TEnum identifier)
            where TEnum : struct, Enum
        {
            var localFilePath = _pathProvider.GetPath(identifier: identifier);
            
            LogHelper.Debug(message: $"LoadTextureAsync: {localFilePath}");
            
            EnsureDirectoryExists(path: localFilePath);
            
            if (!File.Exists(path: localFilePath))
            {
                return new APIResponse<Texture2D>(
                    status: APIStatus.Code.Error,
                    data: null,
                    errorMessage: "ファイルが見つかりませんでした。");
            }
            
            try
            {
                var fileData = await File.ReadAllBytesAsync(path: localFilePath);
                var texture = new Texture2D(
                    width: DEFAULT_TEXTURE_SIZE,
                    height: DEFAULT_TEXTURE_SIZE,
                    textureFormat: TextureFormat.RGBA32,
                    mipChain: false);
                
                if (!texture.LoadImage(data: fileData))
                {
                    return new APIResponse<Texture2D>(
                        status: APIStatus.Code.Error,
                        data: null,
                        errorMessage: "テクスチャの読み込みに失敗しました。");
                }
                
                return new APIResponse<Texture2D>(status: APIStatus.Code.Success, data: texture, errorMessage: null);
            }
            catch (Exception e)
            {
                return new APIResponse<Texture2D>(
                    status: APIStatus.Code.Error,
                    data: null,
                    errorMessage: $"ファイルの読み込み中にエラーが発生しました: {e.Message}");
            }
        }
        
        /// <summary>
        /// ローカルから音声をロードする。
        /// </summary>
        /// <param name="identifier">識別子</param>
        /// <param name="audioType">音声のタイプ</param>
        /// <returns>APIレスポンス(AudioClip)</returns>
        public async UniTask<IAPIResponse<AudioClip>> LoadAudioAsync<TEnum>(
            TEnum identifier,
            AudioType audioType)
        where TEnum : struct, Enum
        {
            var localFilePath = _pathProvider.GetPath(identifier: identifier);
            
            EnsureDirectoryExists(path: localFilePath);
            
            if (!File.Exists(path: localFilePath))
            {
                return new APIResponse<AudioClip>(
                    status: APIStatus.Code.Error,
                    data: null,
                    errorMessage: "ファイルが見つかりませんでした。");
            }
            
            try
            {
                using var www = UnityWebRequestMultimedia.GetAudioClip(
                    "file://" + localFilePath,
                    audioType);
                await www.SendWebRequest();
                
                if (www.result != UnityWebRequest.Result.Success)
                {
                    return new APIResponse<AudioClip>(
                        status: APIStatus.Code.Error,
                        data: null,
                        errorMessage: $"音声ファイルの読み込みに失敗しました: {www.error}");
                }
                
                var clip = DownloadHandlerAudioClip.GetContent(www: www);
                
                if (!clip)
                {
                    return new APIResponse<AudioClip>(
                        status: APIStatus.Code.Error,
                        data: null,
                        errorMessage: "AudioClipの作成に失敗しました。");
                }
                
                return new APIResponse<AudioClip>(
                    status: APIStatus.Code.Success,
                    data: clip,
                    errorMessage: null);
            }
            catch (Exception e)
            {
                return new APIResponse<AudioClip>(
                    status: APIStatus.Code.Error,
                    data: null,
                    errorMessage: $"ファイルの読み込み中にエラーが発生しました: {e.Message}");
            }
        }
        
        /// <summary>
        /// ローカルからJSONデータをロードする。
        /// </summary>
        /// <typeparam name="TData">JSONデータの型</typeparam>
        /// <typeparam name="TEnum">識別子の列挙型</typeparam>
        /// <returns>APIレスポンス(T型のデータ)</returns>
        public async UniTask<IAPIResponse<TData>> LoadJsonAsync<TData,TEnum>(TEnum identifier)
            where TEnum : struct, Enum
        {
            var localFilePath = _pathProvider.GetPath(identifier: identifier);
    
            LogHelper.Debug(message: $"LoadJsonAsync: {localFilePath}");
    
            EnsureDirectoryExists(path: localFilePath);
    
            if (!File.Exists(path: localFilePath))
            {
                return new APIResponse<TData>(
                    status: APIStatus.Code.Error,
                    data: default,
                    errorMessage: "ファイルが見つかりませんでした。");
            }
    
            try
            {
                var jsonText = await File.ReadAllTextAsync(path: localFilePath);
                var data = JsonUtility.FromJson<TData>(json: jsonText);
        
                if (data == null)
                {
                    return new APIResponse<TData>(
                        status: APIStatus.Code.Error,
                        data: default,
                        errorMessage: "JSONデータの変換に失敗しました。");
                }
        
                return new APIResponse<TData>(
                    status: APIStatus.Code.Success,
                    data: data,
                    errorMessage: null);
            }
            catch (Exception e)
            {
                return new APIResponse<TData>(
                    status: APIStatus.Code.Error,
                    data: default,
                    errorMessage: $"JSONファイルの読み込み中にエラーが発生しました: {e.Message}");
            }
        }
        
        /// <summary>
        /// ローカルからJSON配列データをロードしてリストとして取得する。
        /// </summary>
        /// <typeparam name="TData">JSONデータの型</typeparam>
        /// <typeparam name="TEnum">識別子の列挙型</typeparam>
        /// <returns>APIレスポンス(T型のリスト)</returns>
        public async UniTask<IAPIResponse<List<TData>>> LoadJsonListAsync<TData,TEnum>(TEnum identifier)
            where TEnum : struct, Enum
        {
            var localFilePath = _pathProvider.GetPath(identifier: identifier);
            
            LogHelper.Debug(message: $"LoadJsonListAsync: {localFilePath}");
            
            EnsureDirectoryExists(path: localFilePath);
            
            if (!File.Exists(path: localFilePath))
            {
                return new APIResponse<List<TData>>(
                    status: APIStatus.Code.Error,
                    data: null,
                    errorMessage: "ファイルが見つかりませんでした。");
            }
            
            try
            {
                var jsonText = await File.ReadAllTextAsync(path: localFilePath);
                var dataList = JsonHelper.FromJsonList<TData>(json: jsonText);
                
                if (dataList == null)
                {
                    return new APIResponse<List<TData>>(
                        status: APIStatus.Code.Error,
                        data: null,
                        errorMessage: "JSONデータの変換に失敗しました。");
                }
                
                return new APIResponse<List<TData>>(
                    status: APIStatus.Code.Success,
                    data: dataList,
                    errorMessage: null);
            }
            catch (Exception e)
            {
                return new APIResponse<List<TData>>(
                    status: APIStatus.Code.Error,
                    data: null,
                    errorMessage: $"JSONファイルの読み込み中にエラーが発生しました: {e.Message}");
            }
        }
    }
}