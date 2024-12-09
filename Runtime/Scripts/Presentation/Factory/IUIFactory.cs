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
        UniTask<(TView view, TPresenter presenter)> CreateUIAsync<TModel, TView, TPresenter, TEnum>(
            Transform parentTransform,
            IAssetSettings<TEnum> assetSettings,
            TEnum label,
            BaseUIData data,
            CancellationToken cancellationToken)
            where TModel : class, IBaseUIModel
            where TView : class, IDIBaseUIView
            where TPresenter : IBaseUIPresenter
            where TEnum : Enum, IUILabel;

        UniTask<TView> CreateUIAsync<TView, TEnum>(
            Transform parentTransform,
            IAssetSettings<TEnum> assetSettings,
            TEnum label,
            BaseUIData data,
            CancellationToken cancellationToken)
            where TView : INonDIBaseUIView
            where TEnum : Enum, IUILabel;
        
        void DisposeUI(IBaseUIView view);

        UniTask PreloadUIAssetsAsync<TEnum>(
            IAssetSettings<TEnum> assetSettings,
            List<TEnum> labels,
            CancellationToken cancellationToken)
            where TEnum : Enum, IUILabel;
    }
}