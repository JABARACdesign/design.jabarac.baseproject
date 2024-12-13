using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using JABARACdesign.Base.Presentation.Extension;
using JABARACdesign.Base.Presentation.UI;
using UnityEngine;

namespace JabaracDesign.Voick.Presentation.UI.Screen
{
    /// <summary>
    /// スクリーンの基底ビュークラスのインターフェース。
    /// </summary>
    public interface IScreenBaseView : IDIBaseUIView
    {
        public UniTask PlaySlideAnimationAsync(
            bool isForward,
            float startPositionX,
            float screenWidth,
            CancellationToken cancellationToken);
    }
    
    /// <summary>
    /// スクリーンの基底ビュークラス。
    /// </summary>
    public abstract class ScreenBaseView<TData> : DIBaseUIView<TData>, IScreenBaseView
        where TData : ScreenBaseData
    {
        private const float SLIDE_ANIMATION_DURATION = 0.3f;
        
        [SerializeField]
        private RectTransform _rectTransform;
        
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
    }
}