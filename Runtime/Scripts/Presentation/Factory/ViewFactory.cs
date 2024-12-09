using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;
using UnityEngine.AddressableAssets;
using VContainer;

namespace JABARACdesign.Base.Presentation.Factory
{
    /// <summary>
    /// ビューの生成を担当するファクトリクラス。
    /// </summary>
    public class ViewFactory : IViewFactory
    {
        private readonly IAddressablesLoader _addressablesLoader;
        
        /// <summary>
        /// コンストラクタ。
        /// IAddressablesLoaderはVContainerによって自動的に注入されます。
        /// </summary>
        /// <param name="addressablesLoader">アドレッサブルローダー</param>
        [Inject]
        public ViewFactory(IAddressablesLoader addressablesLoader)
        {
            _addressablesLoader = addressablesLoader;
        }
        
        /// <summary>
        /// ビューを生成する
        /// </summary>
        /// <typeparam name="TView">Viewの型</typeparam>
        /// <param name="label">アセットのラベル</param>
        /// <param name="assetReference">アセットリファレンス</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>View(UniTask)</returns>
        public async UniTask<TView> CreateViewAsync<TView>(
            Enum label,
            AssetReference assetReference,
            CancellationToken cancellationToken = default)
            where TView : IBaseUIView
        {
            // アセットリファレンスをもとにビューを生成する
            return await _addressablesLoader.InstantiateAsync<TView>(
                label: label,
                assetReference: assetReference,
                cancellationToken: cancellationToken);
        }
        
        /// <summary>
        /// ビューをプリロードする。
        /// </summary>
        /// <typeparam name="TView">Viewの型</typeparam>
        /// <param name="label">アセットのラベル</param>
        /// <param name="assetReference">アセットリファレンス</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        public async UniTask PreloadViewAsync<TView>(
            Enum label,
            AssetReference assetReference,
            CancellationToken cancellationToken = default)
            where TView : IBaseUIView
        {
            await _addressablesLoader.PreloadAssetsAsync<TView>(
                label: label,
                assetReference: assetReference,
                cancellationToken: cancellationToken);
        }
    }
}