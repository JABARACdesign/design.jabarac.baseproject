using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Application.ScriptableObject;
using JABARACdesign.Base.Application.UI;
using JABARACdesign.Base.Presentation.UI;
using UnityEngine;

namespace JABARACdesign.Base.Presentation.Factory
{
    /// <summary>
    /// UIの生成を行うファクトリクラスのインターフェース。
    /// </summary>
    public interface IUIFactory
    {
        /// <summary>
        /// Model, View, Presenterで構成されたUIの生成を行う。
        /// </summary>
        /// <typeparam name="TModel">Modelの型</typeparam>
        /// <typeparam name="TView">Viewの型</typeparam>
        /// <typeparam name="TPresenter">Presenterの型</typeparam>
        /// <typeparam name="TEnum">ラベルの型</typeparam>
        /// <param name="parentTransform">生成先のトランスフォーム</param>
        /// <param name="assetSettings">アセット設定</param>
        /// <param name="label">UIのラベル</param>
        /// <param name="data">UIの生成において初期化に用いるデータ</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>ViewとPresenterのタプル</returns>
        UniTask<(TView view, TPresenter presenter)> CreateUIAsync<TModel, TView, TPresenter, TEnum>(
            Transform parentTransform,
            IAssetSettings<TEnum> assetSettings,
            TEnum label,
            BaseUIData data,
            CancellationToken cancellationToken)
            where TModel : class, IBaseUIModel
            where TView : class, IDIBaseUIView
            where TPresenter : IBaseUIPresenter
            where TEnum : Enum;

        /// <summary>
        /// Viewのみで構成されたUIの生成を行う。
        /// </summary>
        /// <typeparam name="TView">Viewの型</typeparam>
        /// <typeparam name="TEnum">ラベルの型</typeparam>
        /// <param name="parentTransform">生成先のトランスフォーム</param>
        /// <param name="assetSettings">アセット設定</param>
        /// <param name="label">UIのラベル</param>
        /// <param name="data">UIの生成において初期化に用いるデータ</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>UniTask(View)</returns>
        UniTask<TView> CreateUIAsync<TView, TEnum>(
            Transform parentTransform,
            IAssetSettings<TEnum> assetSettings,
            TEnum label,
            BaseUIData data,
            CancellationToken cancellationToken)
            where TView : INonDIBaseUIView
            where TEnum : Enum;
        
        /// <summary>
        /// UIを破棄する。
        /// </summary>
        /// <param name="view">破棄する対象のView</param>
        void DisposeUI(IBaseUIView view);

        /// <summary>
        /// UIアセットをプリロードする。
        /// </summary>
        /// <param name="assetSettings">アセット設定</param>
        /// <param name="labels">UIラベルのリスト</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        UniTask PreloadUIAssetsAsync<TEnum>(
            IAssetSettings<TEnum> assetSettings,
            List<TEnum> labels,
            CancellationToken cancellationToken)
            where TEnum : Enum;
    }
}