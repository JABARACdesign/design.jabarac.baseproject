using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;
using UnityEngine.AddressableAssets;

namespace JABARACdesign.Base.Presentation.Factory
{
    /// <summary>
    /// ビューの生成を担当するファクトリのインターフェース。
    /// </summary>
    public interface IViewFactory
    {
        /// <summary>
        /// ビューを生成する
        /// </summary>
        /// <typeparam name="TView">Viewの型。</typeparam>
        /// <param name="label">アセットのラベル。</param>
        /// <param name="assetReference">アセットリファレンス</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>View(UniTask)</returns>
        UniTask<TView> CreateViewAsync<TView>(
            Enum label,
            AssetReference assetReference,
            CancellationToken cancellationToken = default)
            where TView : IBaseUIView;
        
        /// <summary>
        /// ビューをプリロードする。
        /// </summary>
        /// <typeparam name="TView">Viewの型</typeparam>
        /// <param name="label">アセットのラベル</param>
        /// <param name="assetReference">アセットリファレンス</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        public UniTask PreloadViewAsync<TView>(
            Enum label,
            AssetReference assetReference,
            CancellationToken cancellationToken = default)
            where TView : IBaseUIView;
    }
}