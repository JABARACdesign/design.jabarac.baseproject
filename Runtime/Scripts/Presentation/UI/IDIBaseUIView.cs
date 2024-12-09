using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Presentation.Factory;

namespace JABARACdesign.Base.Presentation.UI
{
    /// <summary>
    /// DIを利用するUIのViewのインターフェイス。
    /// </summary>
    public interface IDIBaseUIView : IBaseUIView
    {
        IUIFactory UIFactory { get; }
        
        IAssetFactory AssetFactory { get; }
        
        void DisposeUI();
    }
}