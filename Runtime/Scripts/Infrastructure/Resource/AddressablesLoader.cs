using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Domain.Entity.Helper;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace JABARACdesign.Base.Infrastructure.Resource
{
    /// <summary>
    /// IAddressablesLoaderインターフェースの実装クラスで、Addressable Assetsのロードとインスタンス化を管理します。
    /// </summary>
    public class AddressablesLoader : IAddressablesLoader
    {
        private readonly IAddressablesAdapter _addressablesAdapter;
        
        /// <summary>
        /// AddressablesLoaderクラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="addressablesAdapter">Addressable Assets操作のためのアダプター。</param>
        public AddressablesLoader(IAddressablesAdapter addressablesAdapter)
        {
            _addressablesAdapter = addressablesAdapter;
        }
        
        /// <summary>
        /// 指定されたアセットを解放します。
        /// </summary>
        /// <typeparam name="T">解放するアセットの型。</typeparam>
        /// <param name="asset">解放するアセット。</param>
        public void ReleaseAsset<T>(T asset)
            where T : IBaseUIView
        {
            Addressables.Release(obj: asset);
        }
        
        /// <summary>
        /// 指定されたアドレスのアセットを非同期にプリロードします。
        /// </summary>
        /// <param name="label">アセットのラベル</param>
        /// <param name="assetReference">アセットリファレンス</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        public async UniTask PreloadAssetsAsync<TView>(
            Enum label,
            AssetReference assetReference,
            CancellationToken cancellationToken = default)
            where TView : IBaseUIView
        {
            if (assetReference.Asset != null)
            {
                LogHelper.Warning(message: "アセットはすでに取得済みです。");
                return;
            }
            
            await PerformAddressableOperationAsync(
                () => _addressablesAdapter.LoadAssetAsync<GameObject>(assetReference: assetReference)
                    .WithCancellation(cancellationToken: cancellationToken),
                identifier: label.ToString());
        }
        
        /// <summary>
        /// 指定されたアドレスのアセットを非同期にインスタンス化し、特定のコンポーネントを取得します。
        /// </summary>
        /// <typeparam name="T">取得するコンポーネントの型。</typeparam>
        /// <param name="label">アセットのラベル。</param>
        /// <param name="assetReference">アセットリファレンス。</param>
        /// <param name="cancellationToken">操作をキャンセルするためのトークン。</param>
        /// <returns>指定された型のコンポーネント。</returns>
        public async UniTask<T> InstantiateAsync<T>(
            Enum label,
            AssetReference assetReference,
            CancellationToken cancellationToken = default)
        {
            var asset = assetReference.Asset as GameObject;
            
            // アセットがすでに取得済みの場合はそのままインスタンス化する
            if (asset != null)
            {
                return Object.Instantiate(original: asset).GetComponent<T>();
            }
            
            var loadedAsset = await PerformAddressableOperationAsync(
                () => _addressablesAdapter.LoadAssetAsync<GameObject>(assetReference: assetReference)
                    .WithCancellation(cancellationToken: cancellationToken),
                identifier: label.ToString());
            
            return InstantiateAndGetComponent<T>(assetObject: loadedAsset);
        }
        
        /// <summary>
        /// 指定されたアドレスのアセットを非同期にロードします。
        /// </summary>
        /// <typeparam name="T">ロードするアセットの型。</typeparam>
        /// <param name="label">アセットのラベル。</param>
        /// <param name="assetReference">アセットリファレンス。</param>
        /// <param name="cancellationToken">操作をキャンセルするためのトークン。</param>
        /// <returns>ロードされたアセットのインスタンス。</returns>
        public async UniTask<T> LoadAssetAsync<T>(
            Enum label,
            AssetReference assetReference,
            CancellationToken cancellationToken = default)
        {
            var asset = assetReference.Asset;
            
            // アセットがすでに取得済みの場合はそのまま返す
            if (asset != null)
            {
                if (asset is T castedAsset)
                {
                    return castedAsset;
                }
            }
            
            var newAsset = await PerformAddressableOperationAsync(
                () => _addressablesAdapter.LoadAssetAsync<T>(assetReference: assetReference)
                    .WithCancellation(cancellationToken: cancellationToken),
                identifier: label.ToString());
            
            return newAsset;
        }
        
        private T InstantiateAndGetComponent<T>(GameObject assetObject)
        {
            var instance = Object.Instantiate(original: assetObject);
            return instance.GetComponent<T>();
        }
        
        /// <summary>
        /// 指定されたアドレスのアセットを非同期にロードし、AsyncOperationHandleを返します。
        /// </summary>
        /// <typeparam name="T">ロードするアセットの型。</typeparam>
        /// <param name="operationFunc">Addressableの非同期動作のハンドル取得処理。</param>
        /// <param name="identifier">ロードするアセットのアドレス。</param>
        /// <returns>非同期操作のハンドル。操作が成功すれば結果を含むハンドル、失敗またはキャンセルされた場合は無効なハンドル。</returns>
        /// <exception cref="OperationCanceledException">非同期操作がキャンセルされた場合にスローされます。</exception>
        /// <exception cref="Exception">非同期操作中に例外が発生した場合にスローされます。</exception>
        private static async UniTask<T> PerformAddressableOperationAsync<T>(
            Func<UniTask<T>> operationFunc,
            string identifier)
        {
            try
            {
                var asset = await operationFunc();
                
                if (asset != null)
                {
                    return asset;
                }
            }
            catch (OperationCanceledException)
            {
                LogHelper.Warning(message: $"非同期操作がキャンセルされました: {identifier}");
            }
            catch (Exception ex)
            {
                LogHelper.Error(message: $"非同期操作で例外が発生しました: {ex.Message}");
            }
            
            LogHelper.Error(message: $"非同期操作が成功しませんでした: {identifier}");
            return default;
        }
    }
}