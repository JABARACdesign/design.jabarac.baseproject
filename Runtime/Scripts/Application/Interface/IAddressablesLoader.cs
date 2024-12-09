using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace JABARACdesign.Base.Application.Interface
{
    /// <summary>
    /// Addressable Assetsに関する非同期ロードおよびインスタンス化処理を担当するインターフェース。
    /// </summary>
    public interface IAddressablesLoader
    {
        /// <summary>
        /// 指定されたアドレスのアセットを非同期にインスタンス化し、特定のコンポーネントを取得します。
        /// </summary>
        /// <typeparam name="T">取得するコンポーネントの型。</typeparam>
        /// <param name="label">アセットのラベル。</param>
        /// <param name="assetReference">アセットリファレンス。</param>
        /// <param name="cancellationToken">操作をキャンセルするためのトークン。</param>
        /// <returns>GameObjectと指定された型のコンポーネント。</returns>
        UniTask<T> InstantiateAsync<T>(
            Enum label,
            AssetReference assetReference,
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 指定されたアドレスのアセットを非同期にロードします。
        /// </summary>
        /// <param name="label">アセットのラベル。</param>
        /// <param name="assetReference">アセットリファレンス。</param>
        /// <param name="cancellationToken">操作をキャンセルするためのトークン。</param>
        /// <returns>ロードされたアセット。</returns>
        UniTask<T> LoadAssetAsync<T>(
            Enum label,
            AssetReference assetReference,
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 指定されたアセットを解放します。
        /// </summary>
        /// <typeparam name="T">解放するアセットの型。</typeparam>
        /// <param name="asset">解放するアセット。</param>
        void ReleaseAsset<T>(T asset)
            where T : IBaseUIView;
        
        /// <summary>
        /// 指定されたアドレスのアセットを非同期にプリロードします。
        /// </summary>
        /// <param name="label">アセットのラベル</param>
        /// <param name="assetReference">アセットリファレンス</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        public UniTask PreloadAssetsAsync<TView>(
            Enum label,
            AssetReference assetReference,
            CancellationToken cancellationToken = default)
            where TView : IBaseUIView;
    }
}