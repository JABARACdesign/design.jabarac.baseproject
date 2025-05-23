﻿using System;
using System.IO;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Domain.Definition;
using JABARACdesign.Base.Domain.Entity.API;
using JABARACdesign.Base.Domain.Entity.Helper;
using JABARACdesign.Base.Domain.Helper;
using JABARACdesign.Base.Infrastructure.Network.API;
using JABARACdesign.Base.Infrastructure.PathProvider;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System.Collections.Generic;

namespace JABARACdesign.Base.Infrastructure.Network.Client.LocalData
{
    /// <summary>
    /// ローカルストレージのAPIクライアントクラス。
    /// </summary>
    public class LocalStorageApiClient : ILocalStorageApiClient
    {
        private const int DEFAULT_TEXTURE_SIZE = 2;
        
        private readonly ILocalPathProvider _localPathProvider;
        
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="localPathProvider">ローカルパスプロパイダ</param>
        public LocalStorageApiClient(ILocalPathProvider localPathProvider)
        {
            _localPathProvider = localPathProvider;
        }
        
        /// <summary>
        /// 対象のパスが存在するかどうかを確認し、存在しない場合はディレクトリを作成する。
        /// </summary>
        /// <param name="path">対象のパス</param>
        private void EnsureDirectoryExists(string path)
        {
            var directoryPath = Path.GetDirectoryName(path: path);
            if (Directory.Exists(path: directoryPath)) return;
            
            Directory.CreateDirectory(path: directoryPath);
            LogHelper.Info(message: $"Created directory: {directoryPath}");
        }
        
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
            StorageDefinition.FileType fileType)
        {
            var localFilePath = _localPathProvider.GetFilePath(
                userId: userId,
                identifier: identifier,
                extensionType: extensionType,
                fileType: fileType);
            
            EnsureDirectoryExists(path: localFilePath);
            
            return File.Exists(path: localFilePath);
        }
        
        /// <summary>
        /// ローカルからテクスチャをロードする。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="identifier">識別子</param>
        /// <param name="extensionType">拡張子</param>
        /// <param name="fileType">取得するファイルのタイプ</param>
        /// <returns>APIレスポンス(Texture2D)</returns>
        public async UniTask<IAPIResponse<Texture2D>> LoadTextureAsync(
            string userId,
            string identifier,
            StorageDefinition.ExtensionType extensionType,
            StorageDefinition.FileType fileType)
        {
            var localFilePath = _localPathProvider.GetFilePath(
                userId: userId,
                identifier: identifier,
                extensionType: extensionType,
                fileType: fileType);
            
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
        /// <param name="userId">ユーザーID</param>
        /// <param name="identifier">識別子</param>
        /// <param name="extensionType">拡張子</param>
        /// <param name="fileType">取得するファイルのタイプ</param>
        /// <returns>APIレスポンス(AudioClip)</returns>
        public async UniTask<IAPIResponse<AudioClip>> LoadAudioAsync(
            string userId,
            string identifier,
            StorageDefinition.ExtensionType extensionType,
            StorageDefinition.FileType fileType)
        {
            var localFilePath = _localPathProvider.GetFilePath(
                userId: userId,
                identifier: identifier,
                extensionType: extensionType,
                fileType: fileType);
            
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
                var audioType = StorageDefinition.GetAudioType(extensionType: extensionType);
                using var www = UnityWebRequestMultimedia
                    .GetAudioClip(uri: "file://" + localFilePath, audioType: audioType);
                await www.SendWebRequest();
                
                if (www.result != UnityWebRequest.Result.Success)
                {
                    return new APIResponse<AudioClip>(
                        status: APIStatus.Code.Error,
                        data: null,
                        errorMessage: $"音声ファイルの読み込みに失敗しました: {www.error}");
                }
                
                var clip = DownloadHandlerAudioClip.GetContent(www: www);
                
                if (clip == null)
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
        /// <typeparam name="T">JSONデータの型</typeparam>
        /// <param name="userId">ユーザーID</param>
        /// <param name="identifier">識別子</param>
        /// <param name="extensionType">拡張子</param>
        /// <param name="fileType">取得するファイルのタイプ</param>
        /// <returns>APIレスポンス(T型のデータ)</returns>
        public async UniTask<IAPIResponse<T>> LoadJsonAsync<T>(
            string userId,
            string identifier,
            StorageDefinition.ExtensionType extensionType,
            StorageDefinition.FileType fileType)
        {
            var localFilePath = _localPathProvider.GetFilePath(
                userId: userId,
                identifier: identifier,
                extensionType: extensionType,
                fileType: fileType);
    
            LogHelper.Debug(message: $"LoadJsonAsync: {localFilePath}");
    
            EnsureDirectoryExists(path: localFilePath);
    
            if (!File.Exists(path: localFilePath))
            {
                return new APIResponse<T>(
                    status: APIStatus.Code.Error,
                    data: default,
                    errorMessage: "ファイルが見つかりませんでした。");
            }
    
            try
            {
                var jsonText = await File.ReadAllTextAsync(path: localFilePath);
                var data = JsonUtility.FromJson<T>(json: jsonText);
        
                if (data == null)
                {
                    return new APIResponse<T>(
                        status: APIStatus.Code.Error,
                        data: default,
                        errorMessage: "JSONデータの変換に失敗しました。");
                }
        
                return new APIResponse<T>(
                    status: APIStatus.Code.Success,
                    data: data,
                    errorMessage: null);
            }
            catch (Exception e)
            {
                return new APIResponse<T>(
                    status: APIStatus.Code.Error,
                    data: default,
                    errorMessage: $"JSONファイルの読み込み中にエラーが発生しました: {e.Message}");
            }
        }
        
        /// <summary>
        /// ローカルからJSON配列データをロードしてリストとして取得する。
        /// </summary>
        /// <typeparam name="T">JSONデータの型</typeparam>
        /// <param name="userId">ユーザーID</param>
        /// <param name="identifier">識別子</param>
        /// <param name="extensionType">拡張子</param>
        /// <param name="fileType">取得するファイルのタイプ</param>
        /// <returns>APIレスポンス(T型のリスト)</returns>
        public async UniTask<IAPIResponse<List<T>>> LoadJsonListAsync<T>(
            string userId,
            string identifier,
            StorageDefinition.ExtensionType extensionType,
            StorageDefinition.FileType fileType)
        {
            var localFilePath = _localPathProvider.GetFilePath(
                userId: userId,
                identifier: identifier,
                extensionType: extensionType,
                fileType: fileType);
            
            LogHelper.Debug(message: $"LoadJsonListAsync: {localFilePath}");
            
            EnsureDirectoryExists(path: localFilePath);
            
            if (!File.Exists(path: localFilePath))
            {
                return new APIResponse<List<T>>(
                    status: APIStatus.Code.Error,
                    data: null,
                    errorMessage: "ファイルが見つかりませんでした。");
            }
            
            try
            {
                var jsonText = await File.ReadAllTextAsync(path: localFilePath);
                var dataList = JsonHelper.FromJsonList<T>(json: jsonText);
                
                if (dataList == null)
                {
                    return new APIResponse<List<T>>(
                        status: APIStatus.Code.Error,
                        data: null,
                        errorMessage: "JSONデータの変換に失敗しました。");
                }
                
                return new APIResponse<List<T>>(
                    status: APIStatus.Code.Success,
                    data: dataList,
                    errorMessage: null);
            }
            catch (Exception e)
            {
                return new APIResponse<List<T>>(
                    status: APIStatus.Code.Error,
                    data: null,
                    errorMessage: $"JSONファイルの読み込み中にエラーが発生しました: {e.Message}");
            }
        }

        /// <summary>
        /// ローカルからJSON配列データをロードして配列として取得する。
        /// </summary>
        /// <typeparam name="T">JSONデータの型</typeparam>
        /// <param name="userId">ユーザーID</param>
        /// <param name="identifier">識別子</param>
        /// <param name="extensionType">拡張子</param>
        /// <param name="fileType">取得するファイルのタイプ</param>
        /// <returns>APIレスポンス(T型の配列)</returns>
        public async UniTask<IAPIResponse<T[]>> LoadJsonArrayAsync<T>(
            string userId,
            string identifier,
            StorageDefinition.ExtensionType extensionType,
            StorageDefinition.FileType fileType)
        {
            var localFilePath = _localPathProvider.GetFilePath(
                userId: userId,
                identifier: identifier,
                extensionType: extensionType,
                fileType: fileType);
            
            LogHelper.Debug(message: $"LoadJsonArrayAsync: {localFilePath}");
            
            EnsureDirectoryExists(path: localFilePath);
            
            if (!File.Exists(path: localFilePath))
            {
                return new APIResponse<T[]>(
                    status: APIStatus.Code.Error,
                    data: null,
                    errorMessage: "ファイルが見つかりませんでした。");
            }
            
            try
            {
                var jsonText = await File.ReadAllTextAsync(path: localFilePath);
                var dataArray = JsonHelper.FromJsonArray<T>(json: jsonText);
                
                if (dataArray == null)
                {
                    return new APIResponse<T[]>(
                        status: APIStatus.Code.Error,
                        data: null,
                        errorMessage: "JSONデータの変換に失敗しました。");
                }
                
                return new APIResponse<T[]>(
                    status: APIStatus.Code.Success,
                    data: dataArray,
                    errorMessage: null);
            }
            catch (Exception e)
            {
                return new APIResponse<T[]>(
                    status: APIStatus.Code.Error,
                    data: null,
                    errorMessage: $"JSONファイルの読み込み中にエラーが発生しました: {e.Message}");
            }
        }

    }
}