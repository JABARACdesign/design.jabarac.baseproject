using JABARACdesign.Base.Presentation.Extension;
using JABARACdesign.Base.Presentation.Manager;
using UnityEngine;
using VContainer;

namespace JABARACdesign.Base.Presentation.Adjuster
{
    /// <summary>
    /// スクリムサイズ調整インターフェース。
    /// </summary>
    public interface IScrimSizeAdjuster
    {
        void Adjust(RectTransform rectTransform);
    }
    
    /// <summary>
    /// スクリムサイズ調整クラス。
    /// </summary>
    public class ScrimSizeAdjuster : IScrimSizeAdjuster
    {
        private ISafeAreaManager _safeAreaManager;
        
        /// <summary>
        /// コンストラクタ(DI)。
        /// </summary>
        /// <param name="safeAreaManager">セーフエリアマネージャ</param>
        [Inject]
        public void Constructor(ISafeAreaManager safeAreaManager)
        {
            _safeAreaManager = safeAreaManager;
        }
        
        /// <summary>
        /// セーフエリアに合わせた調整を行う。
        /// </summary>
        /// <param name="rectTransform">調整対象のRectTransform</param>
        public void Adjust(RectTransform rectTransform)
        {
            rectTransform.SetOffsetTop(top: -_safeAreaManager.MarginTop);
            rectTransform.SetOffsetBottom(bottom: -_safeAreaManager.MarginBottom);
            rectTransform.SetOffsetLeft(left: -_safeAreaManager.MarginLeft);
            rectTransform.SetOffsetRight(right: -_safeAreaManager.MarginRight);
        }
    }
}