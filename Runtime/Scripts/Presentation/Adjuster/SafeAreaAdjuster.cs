using JABARACdesign.Base.Domain.Entity.Helper;
using JABARACdesign.Base.Presentation.Manager;
using UnityEngine;
using VContainer;

namespace JABARACdesign.Base.Presentation.Adjuster
{
    /// <summary>
    /// SafeAreaに合わせた調整を行うクラス。
    /// 基本的にシーン上に配置されたオブジェクトにアタッチして利用する。
    /// 利用するには、DIでこのアジャスターがアタッチされたオブジェクトについて、Injectを実行する必要がある。
    /// </summary>
    public class SafeAreaAdjuster : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _rectTransform;
        
        private ISafeAreaManager _safeAreaManager;
        
        /// <summary>
        /// コンストラクタ(DI)。
        /// </summary>
        /// <param name="safeAreaManager">セーフエリアマネージャ</param>
        [Inject]
        public void Constructor(ISafeAreaManager safeAreaManager)
        {
            _safeAreaManager = safeAreaManager;
            Adjust();
        }
        
        /// <summary>
        /// セーフエリアに合わせた調整を行う。
        /// </summary>
        private void Adjust()
        {
            if (_rectTransform == null)
            {
                LogHelper.Warning(message: "RectTransform is not assigned to SafeAreaAdjuster");
                return;
            }
            
            _rectTransform.anchorMin = _safeAreaManager.AnchorMin;
            _rectTransform.anchorMax = _safeAreaManager.AnchorMax;
        }
    }
}