using JABARACdesign.Base.Application.UI;
using UnityEngine;

namespace JABARACdesign.Base.Presentation.UI.ScreenContainer
{
    /// <summary>
    /// スクリーンコンテナの基底データクラス
    /// </summary>
    public abstract class ScreenContainerBaseData : BaseUIData
    {
        /// <summary>
        /// コンテナ自身のアニメーションのタイプ
        /// </summary>
        public enum AnimationType
        {
            None,
            Fade,
        }
        
        /// <summary>
        /// スクリーンのトランジションタイプ
        /// </summary>
        public enum ScreenTransitionType
        {
            None,
            Fade,
            Slide,
        }
        
        [SerializeField]
        private AnimationType _animationType;
        
        public AnimationType Animation => _animationType;
        
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="animationType">アニメーションタイプ</param>
        protected ScreenContainerBaseData(AnimationType animationType)
        {
            _animationType = animationType;
        }
    }
}