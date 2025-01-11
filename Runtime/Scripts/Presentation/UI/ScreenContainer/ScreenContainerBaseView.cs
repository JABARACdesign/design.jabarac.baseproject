using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using JABARACdesign.Base.Application.ScriptableObject;
using JABARACdesign.Base.Domain.Entity.Helper;
using JABARACdesign.Base.Presentation.UI.Screen;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace JABARACdesign.Base.Presentation.UI.ScreenContainer
{
    /// <summary>
    /// スクリーンコンテナの基底インターフェース
    /// </summary>
    public interface IScreenContainerBaseView : IDIBaseUIView
    {
        /// <summary>
        /// スクリーンを生成する
        /// </summary>
        /// <param name="parentLifetimeScope">親のライフタイムスコープ</param>
        /// <param name="assetSettings">アセット設定</param>
        /// <param name="label">UIラベル</param>
        /// <param name="data">初期化データ</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <typeparam name="TScreenModel">スクリーンのモデルの型</typeparam>
        /// <typeparam name="TScreenView">スクリーンのビューの型</typeparam>
        /// <typeparam name="TScreenPresenter">スクリーンのプレゼンターの型</typeparam>
        /// <typeparam name="TScreenData">スクリーンのデータの型</typeparam>
        /// <typeparam name="TEnum">UIラベルの型</typeparam>
        /// <returns>IScreenBasePresenter</returns>
        UniTask<(TScreenView view, TScreenPresenter presenter)> CreateScreenAsync<
            TScreenModel,
            TScreenView,
            TScreenPresenter,
            TScreenData,
            TEnum>(
            LifetimeScope parentLifetimeScope,
            IAssetSettings<TEnum> assetSettings,
            TEnum label,
            TScreenData data,
            CancellationToken cancellationToken)
            where TScreenModel : class, IScreenBaseModel
            where TScreenView : class, IScreenBaseView
            where TScreenPresenter : IScreenBasePresenter<IScreenBaseModel, IScreenBaseView>
            where TScreenData : ScreenBaseData
            where TEnum : Enum;

        /// <summary>
        /// スクリーン遷移アニメーションを再生する
        /// </summary>
        /// <param name="currentScreen">現在表示中のスクリーン</param>
        /// <param name="nextScreen">次に表示するスクリーン</param>
        /// <param name="isForward">前進方向へのアニメーションか</param>
        /// <param name="transitionType">遷移アニメーションの種類</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        UniTask PlayScreenTransitionAnimationAsync(
            IScreenBaseView currentScreen,
            IScreenBaseView nextScreen,
            bool isForward,
            ScreenContainerBaseData.ScreenTransitionType transitionType,
            CancellationToken cancellationToken);

        /// <summary>
        /// スクリーンコンテナを閉じる。
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン</param>
        UniTask CloseAsync(CancellationToken cancellationToken);
    }
    
    /// <summary>
    /// スクリーンコンテナの基底ビュークラス
    /// </summary>
    /// <typeparam name="TData">初期化データの型</typeparam>
    public abstract class ScreenContainerBaseView<TData> : DIBaseUIView<TData>, IScreenContainerBaseView
        where TData : ScreenContainerBaseData
    {

        
        private const float FADE_ANIMATION_DURATION = 0.3f;
        
        [SerializeField]
        protected CanvasGroup _canvasGroup;
        
        [SerializeField]
        protected RectTransform _screenContainerTransform;
        
        public RectTransform ScreenContainerTransform => _screenContainerTransform;
        
        private ScreenContainerBaseData.AnimationType _animationType;
        
        public override async UniTask InitializeViewAsync(CancellationToken cancellationToken = default)
        {
            await base.InitializeViewAsync(cancellationToken: cancellationToken);
            
            _animationType = _data.Animation;
        }
        
        public override async UniTask PlayShowAnimationAsync(CancellationToken cancellationToken)
        {
            await base.PlayShowAnimationAsync(cancellationToken: cancellationToken);
            
            switch (_animationType)
            {
                case ScreenContainerBaseData.AnimationType.None:
                    break;
                case ScreenContainerBaseData.AnimationType.Fade:
                    _canvasGroup.alpha = 0;
                    await PlayFadeInAnimationAsync(cancellationToken: cancellationToken);
                    break;
            }
        }
        
        public override UniTask PlayHideAnimationAsync(CancellationToken cancellationToken)
        {
            switch (_animationType)
            {
                case ScreenContainerBaseData.AnimationType.None:
                    break;
                case ScreenContainerBaseData.AnimationType.Fade:
                    return PlayFadeOutAnimationAsync(cancellationToken: cancellationToken);
            }
            
            return UniTask.CompletedTask;
        }
        
        /// <summary>
        /// フェードインアニメーションを再生する。
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン</param>
        private async UniTask PlayFadeInAnimationAsync(CancellationToken cancellationToken)
        {
            await _canvasGroup.DOFade(endValue: 1, duration: FADE_ANIMATION_DURATION)
                .SetEase(ease: Ease.InOutCubic)
                .WithCancellation(cancellationToken: cancellationToken);
        }
        
        /// <summary>
        /// フェードアウトアニメーションを再生する。
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン</param>
        private async UniTask PlayFadeOutAnimationAsync(CancellationToken cancellationToken)
        {
            await _canvasGroup.DOFade(endValue: 0, duration: FADE_ANIMATION_DURATION)
                .SetEase(ease: Ease.InOutCubic)
                .WithCancellation(cancellationToken: cancellationToken);
        }

        /// <summary>
        /// スクリーンを生成する
        /// </summary>
        /// <param name="parentLifetimeScope">親のライフタイムスコープ</param>
        /// <param name="assetSettings">アセット設定</param>
        /// <param name="label">UIラベル</param>
        /// <param name="data">初期化データ</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <typeparam name="TScreenModel">スクリーンのモデルの型</typeparam>
        /// <typeparam name="TScreenView">スクリーンのビューの型</typeparam>
        /// <typeparam name="TScreenPresenter">スクリーンのプレゼンターの型</typeparam>
        /// <typeparam name="TScreenData">スクリーンのデータの型</typeparam>
        /// <typeparam name="TEnum">UIラベルの型</typeparam>
        /// <returns>IScreenBasePresenter</returns>
        public async UniTask<(TScreenView view, TScreenPresenter presenter)> CreateScreenAsync<
            TScreenModel,
            TScreenView,
            TScreenPresenter,
            TScreenData,
            TEnum>(
            LifetimeScope parentLifetimeScope,
            IAssetSettings<TEnum> assetSettings,
            TEnum label,
            TScreenData data,
            CancellationToken cancellationToken)
            where TScreenModel : class, IScreenBaseModel
            where TScreenView : class, IScreenBaseView
            where TScreenPresenter : IScreenBasePresenter<IScreenBaseModel, IScreenBaseView>
            where TScreenData : ScreenBaseData
            where TEnum : Enum
        {
            return await UIFactory.CreateUIAsync<
                TScreenModel,
                TScreenView,
                TScreenPresenter,
                TEnum>(
                parentLifetimeScope: parentLifetimeScope,
                parentTransform: _screenContainerTransform,
                assetSettings: assetSettings,
                label: label,
                data: data,
                cancellationToken: cancellationToken);
        }
        
        /// <summary>
        /// スクリーン遷移アニメーションを再生する
        /// </summary>
        /// <param name="currentScreen">現在表示中のスクリーン</param>
        /// <param name="nextScreen">次に表示するスクリーン</param>
        /// <param name="isForward">前進方向へのアニメーションか</param>
        /// <param name="transitionType">遷移アニメーションの種類</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        public async UniTask PlayScreenTransitionAnimationAsync(
            IScreenBaseView currentScreen,
            IScreenBaseView nextScreen,
            bool isForward,
            ScreenContainerBaseData.ScreenTransitionType transitionType,
            CancellationToken cancellationToken)
        {
            // 現在のスクリーンが無い場合(初回表示)は、 nextScreen をそのまま表示する
            if (currentScreen == null)
            {
                if (nextScreen != null) return;
        
                LogHelper.Error(message: "新たに表示するスクリーンが存在しません。");
                return;
            }

            // currentScreenとnextScreenの間でpush/popアニメーションを行う
            switch (transitionType)
            {
                case ScreenContainerBaseData.ScreenTransitionType.None:
                    break;
            
                case ScreenContainerBaseData.ScreenTransitionType.Fade:
                    await PlayTransitionFadeAnimationAsync(
                        currentScreen,
                        nextScreen,
                        cancellationToken);
                    break;
            
                case ScreenContainerBaseData.ScreenTransitionType.Slide:
                    await PlayTransitionSlideAnimationAsync(
                        currentScreen,
                        nextScreen,
                        isForward,
                        cancellationToken);
                    break;
                
                default:
                    break;
            }
        }
        
        /// <summary>
        /// フェードアニメーションを再生する
        /// </summary>
        /// <param name="currentScreen">現在のスクリーン</param>
        /// <param name="nextScreen">次のスクリーン</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        private async UniTask PlayTransitionFadeAnimationAsync(
            IScreenBaseView currentScreen,
            IScreenBaseView nextScreen,
            CancellationToken cancellationToken)
        {
            var fadeOutTask = currentScreen.PlayFadeAnimationAsync(
                startAlpha: 1,
                endAlpha: 0,
                cancellationToken: cancellationToken);

            var fadeInTask = nextScreen.PlayFadeAnimationAsync(
                startAlpha: 0,
                endAlpha: 1,
                cancellationToken: cancellationToken);

            await UniTask.WhenAll(fadeOutTask, fadeInTask);
        }
        
        /// <summary>
        /// スライドアニメーションを再生する
        /// </summary>
        /// <param name="currentScreen">現在のスクリーン</param>
        /// <param name="nextScreen">次のスクリーン</param>
        /// <param name="isForward">前方向の遷移か</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        private async UniTask PlayTransitionSlideAnimationAsync(
            IScreenBaseView currentScreen,
            IScreenBaseView nextScreen,
            bool isForward,
            CancellationToken cancellationToken)
        {
            var screenWidth = RectTransform.rect.width;
    
            if (isForward)
            {
                // Forward: currentスクリーンを左へ、nextスクリーンを右から左へスライドイン
                var currentScreenAnimationTask = currentScreen.PlaySlideAnimationAsync(
                    isForward: true,
                    startPositionX: 0,
                    screenWidth: screenWidth,
                    cancellationToken: cancellationToken);

                var nextScreenAnimationTask = nextScreen.PlaySlideAnimationAsync(
                    isForward: true,
                    startPositionX: screenWidth,
                    screenWidth: screenWidth,
                    cancellationToken: cancellationToken);

                await UniTask.WhenAll(currentScreenAnimationTask, nextScreenAnimationTask);
            }
            else
            {
                // Backward: currentスクリーンを右へ、previousスクリーンを左から右へスライドイン
                var previousScreenAnimationTask = nextScreen.PlaySlideAnimationAsync(
                    isForward: false,
                    startPositionX: -screenWidth,
                    screenWidth: screenWidth,
                    cancellationToken: cancellationToken);

                var currentScreenAnimationTask = currentScreen.PlaySlideAnimationAsync(
                    isForward: false,
                    startPositionX: 0,
                    screenWidth: screenWidth,
                    cancellationToken: cancellationToken);

                await UniTask.WhenAll(previousScreenAnimationTask, currentScreenAnimationTask);
            }
        }
        
        /// <summary>
        /// スクリーンコンテナを閉じる。
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン</param>
        public async UniTask CloseAsync(CancellationToken cancellationToken)
        {
            await PlayHideAnimationAsync(cancellationToken: cancellationToken);
            UIFactory.DisposeUI(view: this);
        }
    }
}