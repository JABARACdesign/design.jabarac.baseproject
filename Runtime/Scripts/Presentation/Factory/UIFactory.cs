using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Application.ScriptableObject;
using JABARACdesign.Base.Application.UI;
using JABARACdesign.Base.Presentation.Helper;
using JABARACdesign.Base.Presentation.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace JABARACdesign.Base.Presentation.Factory
{
    /// <summary>
    /// UIの生成を行うファクトリクラス。
    /// </summary>
    public abstract class UIFactory : IUIFactory
    {
        #region Private Fields
        
        private readonly Dictionary<IBaseUIView, LifetimeScope> _scopeDictionary = new();
        
        private IPresenterFactory _presenterFactory;
        
        private IViewFactory _viewFactory;
        
        #endregion
        
        /// <summary>
        /// コンストラクタ。MonoBehaviourのコンストラクタはInjectを用いて代用する。
        /// </summary>
        /// <param name="viewFactory">Viewのファクトリクラス。</param>
        /// <param name="presenterFactory">Presenterのファクトリクラス。</param>
        [Inject]
        public void Constructor(
            IViewFactory viewFactory,
            IPresenterFactory presenterFactory)
        {
            _viewFactory = viewFactory;
            _presenterFactory = presenterFactory;
        }

        /// <summary>
        /// Model, View, Presenterで構成されたUIの生成を行う。
        /// </summary>
        /// <typeparam name="TModel">Modelの型</typeparam>
        /// <typeparam name="TView">Viewの型</typeparam>
        /// <typeparam name="TPresenter">Presenterの型</typeparam>
        /// <typeparam name="TEnum">ラベルの型</typeparam>
        /// <param name="parentLifetimeScope">親のライフタイムスコープ</param>
        /// <param name="parentTransform">生成先のトランスフォーム</param>
        /// <param name="assetSettings">アセット設定</param>
        /// <param name="label">UIのラベル</param>
        /// <param name="data">UIの生成において初期化に用いるデータ</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>ViewとPresenterのタプル</returns>
        public async UniTask<(TView view, TPresenter presenter)> CreateUIAsync<TModel, TView, TPresenter, TEnum>(
            LifetimeScope parentLifetimeScope,  
            Transform parentTransform,
            IAssetSettings<TEnum> assetSettings,
            TEnum label,
            BaseUIData data,
            CancellationToken cancellationToken)
            where TModel : class, IBaseUIModel
            where TView : class, IDIBaseUIView
            where TPresenter : IBaseUIPresenter<IBaseUIModel, IDIBaseUIView>
            where TEnum : Enum
        {
            // アセット参照を作成する
            var assetReference = assetSettings.GetAssetReference(label: label);
            
            // ビューを生成する
            var view = await InstantiateViewAsync<TView>(
                parentTransform: parentTransform,
                label: label,
                assetReference: assetReference,
                cancellationToken: cancellationToken);
            
            // LifetimeScopeを生成する
            var scope = CreateLifetimeScope<TModel, TView, TPresenter>(
                parentLifetimeScope,
                view: view,
                parentTransform: parentTransform);
            var presenter = _presenterFactory.CreatePresenter<TModel, TView, TPresenter>(scope: scope);
            
            await presenter.InitializeAsyncBase(data: data, cancellationToken: cancellationToken);
            
            // 辞書に登録する
            _scopeDictionary.Add(key: view, value: scope);
            
            return (view, presenter);
        }

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
        public async UniTask<TView> CreateUIAsync<TView,TEnum>(
            Transform parentTransform,
            IAssetSettings<TEnum> assetSettings,
            TEnum label,
            BaseUIData data,
            CancellationToken cancellationToken)
            where TView : INonDIBaseUIView
            where TEnum : Enum
        {
            var assetReference = assetSettings.GetAssetReference(label: label);
            
            // ビューを生成する
            var view = await InstantiateViewAsync<TView>(
                parentTransform: parentTransform,
                label: label,
                assetReference: assetReference,
                cancellationToken: cancellationToken);
            
            // 初期化処理を実行する
            await view.InitializeViewAsyncBase(
                initData: data,
                cancellationToken: cancellationToken);
            
            return view;
        }
        
        
        /// <summary>
        /// Viewのインスタンスを生成する。
        /// </summary>
        /// <typeparam name="TView">Viewの型</typeparam>
        /// <param name="parentTransform">生成先のトランスフォーム</param>
        /// <param name="label">アセットのラベル</param>
        /// <param name="assetReference">アセットリファレンス</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns></returns>
        private async UniTask<TView> InstantiateViewAsync<TView>(
            Transform parentTransform,
            Enum label,
            AssetReference assetReference,
            CancellationToken cancellationToken = default)
            where TView : IBaseUIView
        {
            var view = await _viewFactory.CreateViewAsync<TView>(
                label: label,
                assetReference: assetReference,
                cancellationToken: cancellationToken);
            view.Transform.SetParent(parent: parentTransform, worldPositionStays: false);
            
            return view;
        }

        /// <summary>
        /// Model,View,PresenterのLifetimeScopeを動的に生成する。
        /// </summary>
        /// <typeparam name="TModel">Modelの型</typeparam>
        /// <typeparam name="TView">Viewの型</typeparam>
        /// <typeparam name="TPresenter">Presenterの型</typeparam>
        /// <param name="parentLifetimeScope">親のライフタイムスコープ</param>
        /// <param name="view">生成したView</param>
        /// <param name="parentTransform">LifetimeScopeの生成先のトランスフォーム</param>
        /// <returns>LifetimeScope</returns>
        private LifetimeScope CreateLifetimeScope<TModel, TView, TPresenter>(
            LifetimeScope parentLifetimeScope, 
            TView view,
            Transform parentTransform)
            where TModel : IBaseUIModel
            where TView : IDIBaseUIView
            where TPresenter : IBaseUIPresenter<IBaseUIModel, IDIBaseUIView>
        {
            var scope = VContainerHelper.CreateLifetimeScope<TModel, TView, TPresenter>(
                view: view,
                parentLifetimeScope: parentLifetimeScope);
            scope.transform.SetParent(p: parentTransform);
            return scope;
        }

        /// <summary>
        /// UIアセットをプリロードする。
        /// </summary>
        /// <param name="assetSettings">アセット設定</param>
        /// <param name="labels">UIラベルのリスト</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        public async UniTask PreloadUIAssetsAsync<TEnum>(
            IAssetSettings<TEnum> assetSettings,
            List<TEnum> labels,
            CancellationToken cancellationToken)
            where TEnum : Enum
        {
            foreach (var label in labels)
            {
                var assetReference = assetSettings.GetAssetReference(label: label);
                await _viewFactory.PreloadViewAsync<IBaseUIView>(
                    label: label,
                    assetReference: assetReference,
                    cancellationToken: cancellationToken);
            }
        }
        
        /// <summary>
        /// UIを破棄する。
        /// </summary>
        /// <param name="view">破棄する対象のView</param>
        public void DisposeUI(IBaseUIView view)
        {
            if (_scopeDictionary.TryGetValue(key: view, value: out var scope))
            {
                // スコープを破棄する際に、VContainerに登録されているModel, View, Presenterなども破棄される(Disposeが呼ばれる)
                scope.Dispose();
                _scopeDictionary.Remove(key: view);
            }
            else
            {
                // ビューのDispose処理を実行する
                view.Dispose();
            }
        }
    }
}