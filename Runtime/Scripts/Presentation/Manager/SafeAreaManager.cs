using UnityEngine;

namespace JABARACdesign.Base.Presentation.Manager
{
    /// <summary>
    /// SafeAreaのマネージャ。
    /// </summary>
    public class SafeAreaManager : ISafeAreaManager
    {
        public float MarginTop { get; private set; }
        
        public float MarginBottom { get; private set; }
        
        public float MarginLeft { get; private set; }
        
        public float MarginRight { get; private set; }
        
        public Vector2 AnchorMin { get; private set; }
        
        public Vector2 AnchorMax { get; private set; }
        
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public SafeAreaManager()
        {
            SetUp();
        }
        
        /// <summary>
        /// セットアップ処理。
        /// </summary>
        private void SetUp()
        {
            var safeArea = Screen.safeArea;
            MarginTop = Screen.height - safeArea.height - safeArea.y;
            MarginBottom = safeArea.y;
            MarginLeft = safeArea.x;
            MarginRight = Screen.width - safeArea.width - safeArea.x;
            
            var anchorMin = safeArea.position;
            var anchorMax = safeArea.position + safeArea.size;
            
            AnchorMin = new Vector2(
                x: anchorMin.x /= Screen.width,
                y: anchorMin.y /= Screen.height
            );
            
            AnchorMax = new Vector2(
                x: anchorMax.x /= Screen.width,
                y: anchorMax.y /= Screen.height
            );
        }
    }
}