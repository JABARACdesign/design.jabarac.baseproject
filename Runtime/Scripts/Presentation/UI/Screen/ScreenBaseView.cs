using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using JABARACdesign.Base.Presentation.Extension;
using UnityEngine;

namespace JABARACdesign.Base.Presentation.UI.Screen
{
    /// <summary>
    /// スクリーンの基底ビュークラスのインターフェース。
    /// </summary>
    public interface IScreenBaseView : IDIBaseUIView
    {
        /// <summary>
        /// スライドアニメーションを再生する
        /// </summary>
        /// <param name="isForward">前進方向へのアニメーションか</param>
        /// <param name="startPositionX">アニメーションの開始X座標</param>
        /// <param name="screenWidth">スクリーンの幅</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        UniTask PlaySlideAnimationAsync(
            bool isForward,
            float startPositionX,
            float screenWidth,
            CancellationToken cancellationToken);
        
        /// <summary>
        /// フェードアニメーションを再生する
        /// </summary>
        /// <param name="startAlpha">開始時のアルファ</param>
        /// <param name="endAlpha">終了時のアルファ</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        UniTask PlayFadeAnimationAsync(
            float startAlpha,
            float endAlpha,
            CancellationToken cancellationToken);
    }
    
    /// <summary>
    /// スクリーンの基底ビュークラス。
    /// </summary>
    public abstract class ScreenBaseView<TData> : DIBaseUIView<TData>, IScreenBaseView
        where TData : ScreenBaseData
    {
        private const float SLIDE_ANIMATION_DURATION = 0.3f;
        
        private const float FADE_ANIMATION_DURATION = 0.3f;
        
        [SerializeField]
        private RectTransform _rectTransform;
        
        [SerializeField]
        private CanvasGroup _canvasGroup;
        
        /// <summary>
        /// スライドアニメーションを再生する
        /// </summary>
        /// <param name="isForward">前進方向へのアニメーションか</param>
        /// <param name="startPositionX">アニメーションの開始X座標</param>
        /// <param name="screenWidth">スクリーンの幅</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        public async UniTask PlaySlideAnimationAsync(
            bool isForward,
            float startPositionX,
            float screenWidth,
            CancellationToken cancellationToken)
        {
            _rectTransform.SetAnchoredPositionX(x: startPositionX);
            
            var endPositionX = isForward
                ? startPositionX - screenWidth
                : startPositionX + screenWidth;
            
            await _rectTransform
                .DOAnchorPosX(endValue: endPositionX, duration: SLIDE_ANIMATION_DURATION)
                .SetEase(ease: Ease.OutCubic)
                .WithCancellation(cancellationToken: cancellationToken);
        }
        
        /// <summary>
        /// フェードアニメーションを再生する
        /// </summary>
        /// <param name="startAlpha">開始時のアルファ</param>
        /// <param name="endAlpha">終了時のアルファ</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        public async UniTask PlayFadeAnimationAsync(
            float startAlpha,
            float endAlpha,
            CancellationToken cancellationToken)
        {
            _canvasGroup.alpha = startAlpha;
        
            await _canvasGroup.DOFade(endValue: endAlpha, duration: FADE_ANIMATION_DURATION)
                .SetEase(ease: Ease.InOutCubic)
                .WithCancellation(cancellationToken: cancellationToken);
        }
    }
}