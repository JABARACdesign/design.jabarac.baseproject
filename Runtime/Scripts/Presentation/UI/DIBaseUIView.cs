using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Application.UI;
using JABARACdesign.Base.Presentation.Factory;
using VContainer;

namespace JABARACdesign.Base.Presentation.UI
{
    /// <summary>
    /// DIを利用するUIのViewクラス。
    /// </summary>
    /// <typeparam name="TData">データの型</typeparam>
    public class DIBaseUIView<TData> : BaseUIView<TData>, IDIBaseUIView
        where TData : BaseUIData
    {
        public IUIFactory UIFactory { get; private set; }
        
        public IAssetFactory AssetFactory { get; private set; }
        
        /// <summary>
        /// コンストラクタ(DI)。
        /// </summary>
        /// <param name="uiFactory">UIファクトリ</param>
        /// <param name="assetFactory">アセットファクトリ</param>
        [Inject]
        public void Constructor(
            IUIFactory uiFactory,
            IAssetFactory assetFactory)
        {
            UIFactory = uiFactory;
            AssetFactory = assetFactory;
        }
        
        protected override void ApplyView()
        {
        }
        
        protected override void ApplyViewOnEditor()
        {
        }
        
        protected override void SetupViewEvent()
        {
        }
        
        /// <summary>
        /// UIFactoryからUIを破棄する。
        /// </summary>
        public void DisposeUI()
        {
            UIFactory.DisposeUI(view: this);
        }
    }
}