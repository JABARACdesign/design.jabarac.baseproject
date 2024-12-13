using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Manager;
using JABARACdesign.Base.Application.ScriptableObject;
using JABARACdesign.Base.Domain.Entity.Helper;
using JABARACdesign.Base.Presentation.UI.Screen;
using R3;
using UnityEngine;

namespace JABARACdesign.Base.Presentation.UI.ScreenContainer
{
    /// <summary>
    /// スクリーンコンテナの基底プレゼンタークラス
    /// </summary>
    /// <typeparam name="TModel">モデルの型</typeparam>
    /// <typeparam name="TView">ビューの型</typeparam>
    public abstract class ScreenContainerBasePresenter<TModel, TView>
        : BaseUIPresenter<TModel, TView>
        where TModel : IScreenContainerBaseModel
        where TView : IScreenContainerBaseView
    {
        private readonly Stack<IScreenBasePresenter<IScreenBaseModel, IScreenBaseView>> _screenStack = new();
        
        private IScreenBasePresenter<IScreenBaseModel, IScreenBaseView> _currentScreenPresenter;
        
        protected ScreenContainerBasePresenter(TModel model, TView view, IMstDataManager mstDataManager) : base(
            model: model,
            view: view,
            mstDataManager: mstDataManager)
        {
        }
        
        /// <summary>
        /// 初期化処理。
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン</param>
        protected override async UniTask InitializeAsync(CancellationToken cancellationToken)
        {
            SetUpViewEvent();
            await PushInitialScreenAsync(cancellationToken: cancellationToken);
        }
        
        /// <summary>
        /// ビューのイベントを設定。
        /// </summary>
        private void SetUpViewEvent()
        {
            View.OnScreenBackButtonClicked
                .Subscribe(_ => PopScreenAsync(cancellationToken: CancellationToken).Forget())
                .AddTo(Disposables);
        }
        
        /// <summary>
        /// 初期ページを生成する。
        /// </summary>
        /// <param name="cancellationToken"></param>
        private async UniTask PushInitialScreenAsync(CancellationToken cancellationToken)
        {
            var (screenView, screenPresenter) = await CreateInitialScreenAsync(cancellationToken: cancellationToken);
            
            _screenStack.Push(screenPresenter);
            
            // 初期スクリーン表示（現在のスクリーンは存在しないためnullを渡す）
            await View.TransitionToScreenAsync(
                currentScreen: null,
                nextScreen: screenView,
                isForward: true,
                cancellationToken: cancellationToken
            );
        }
        
        /// <summary>
        /// 初期ページを表示。
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン</param>
        protected abstract UniTask<(IScreenBaseView, IScreenBasePresenter<IScreenBaseModel, IScreenBaseView>)> CreateInitialScreenAsync(
            CancellationToken cancellationToken);
        
        
        /// <summary>
        /// スクリーンをプッシュする。
        /// </summary>
        /// <typeparam name="TScreenModel">スクリーンのモデルの型</typeparam>
        /// <typeparam name="TScreenView">スクリーンのビューの型</typeparam>
        /// <typeparam name="TScreenPresenter">スクリーンのプレゼンターの型</typeparam>
        /// <typeparam name="TScreenData">スクリーンのデータの型</typeparam>
        /// <typeparam name="TEnum">Enumの型</typeparam>
        /// <param name="assetSettings">アセット設定</param>
        /// <param name="label">ラベル</param>
        /// <param name="data">初期化データ</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>プッシュされたスクリーンのプレゼンター</returns>
        public async UniTask PushScreenAsync<
            TScreenModel,
            TScreenView, 
            TScreenPresenter,
            TScreenData,
            TEnum>(
            IAssetSettings<TEnum> assetSettings,
            TEnum label,
            TScreenData data,
            CancellationToken cancellationToken)
            where TScreenModel : class, IScreenBaseModel
            where TScreenView : MonoBehaviour, IScreenBaseView
            where TScreenPresenter : IScreenBasePresenter<IScreenBaseModel, IScreenBaseView>
            where TScreenData : ScreenBaseData
            where TEnum : Enum
        {
            var (nextView, nextPresenter) =
                await View.CreateScreenAsync<
                    TScreenModel, 
                    TScreenView,
                    TScreenPresenter,
                    TScreenData,
                    TEnum>(
                    assetSettings,
                    label: label,
                    data: data,
                    cancellationToken: cancellationToken);
            
            var currentPresenter = _screenStack.Count > 0 
                ? _screenStack.Peek() 
                : null;
            _screenStack.Push(nextPresenter);
            
            var currentView = currentPresenter?.View;
            
            await View.TransitionToScreenAsync(
                currentScreen: currentView,
                nextScreen: nextView,
                isForward: true,
                cancellationToken: cancellationToken
            );
        }
        
        /// <summary>
        /// スクリーンコンテナからスクリーンをポップする。
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン</param>
        public async UniTask PopScreenAsync(CancellationToken cancellationToken)
        {
            if (_screenStack.Count <= 1)
            {
                LogHelper.Warning("最後のスクリーンのため、ポップできません。");
                return;
            }
            
            var currentPresenter = _screenStack.Pop();
            var previousPresenter = _screenStack.Peek();

            var currentView = currentPresenter.View;
            var previousView = previousPresenter.View;
            
            // スクリーン遷移アニメーション
            await View.TransitionToScreenAsync(
                currentScreen: currentView,
                nextScreen: previousView,
                isForward: false,
                cancellationToken: cancellationToken
            );

            // Pop後、不要になったスクリーンを破棄
            currentView.DisposeUI();
        }
    }
}