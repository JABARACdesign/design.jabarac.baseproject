using UnityEngine;

namespace JABARACdesign.Base.Presentation.Manager
{
    /// <summary>
    /// SafeAreaのマネージャインターフェース。
    /// </summary>
    public interface ISafeAreaManager
    {
        float MarginTop { get; }
        
        float MarginBottom { get; }
        
        float MarginLeft { get; }
        
        float MarginRight { get; }
        
        Vector2 AnchorMin { get; }
        
        Vector2 AnchorMax { get; }
    }
}