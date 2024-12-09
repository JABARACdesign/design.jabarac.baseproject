using JABARACdesign.Base.Application.UI;

namespace JABARACdesign.Base.Presentation.UI
{
    /// <summary>
    /// DIを利用しないUIのViewクラス。
    /// </summary>
    /// <typeparam name="TData">データの型</typeparam>
    public class NonDIBaseUIView<TData> : BaseUIView<TData>, INonDIBaseUIView
        where TData : BaseUIData
    
    {
        protected override void ApplyView()
        {
        }
        
        protected override void ApplyViewOnEditor()
        {
        }
        
        protected override void SetupViewEvent()
        {
        }
    }
}