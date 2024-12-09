using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace JABARACdesign.Base.Infrastructure.Resource
{
    /// <summary>
    /// Addressable Assetsシステムを抽象化するインターフェース。
    /// このインターフェースを通じて、アセットやゲームオブジェクトの非同期ロードとインスタンス化を行う。
    /// </summary>
    public interface IAddressablesAdapter
    {
        /// <summary>
        /// 指定されたアセット参照をもとにアセットを非同期でロードする。
        /// </summary>
        /// <typeparam name="T">ロードするアセットの型。</typeparam>
        /// <param name="assetReference">アセットリファレンス。</param>
        /// <returns>非同期操作ハンドル。</returns>
        AsyncOperationHandle<T> LoadAssetAsync<T>(AssetReference assetReference);
    }
    
    /// <summary>
    /// Addressable Assetsシステムに対する具体的なアダプター実装。
    /// IAddressablesAdapterインターフェースを通じて、アセットのロードやGameObjectのインスタンス化を行う。
    /// </summary>
    public class AddressablesAdapter : IAddressablesAdapter
    {
        /// <summary>
        /// 指定されたアセット参照をもとにアセットを非同期でロードする。
        /// </summary>
        /// <typeparam name="T">ロードするアセットの型。</typeparam>
        /// <param name="assetReference">アセットリファレンス。</param>
        /// <returns>非同期操作ハンドル。</returns>
        public AsyncOperationHandle<T> LoadAssetAsync<T>(AssetReference assetReference)
        {
            return assetReference.LoadAssetAsync<T>();
        }
    }
}