using JABARACdesign.Base.Application.Manager;
using JABARACdesign.Base.Presentation.UI;

namespace JabaracDesign.Voick.Presentation.UI.Screen
{
    public interface IScreenBaseModel : IBaseUIModel
    {
        string ScreenTitle { get; }
    }
    
    public abstract class ScreenBaseModel<TData> : BaseUIModel<TData>, IScreenBaseModel
        where TData : ScreenBaseData
    {
        private string _screenTitle;
        
        public string ScreenTitle => _screenTitle;
        
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="mstDataManager">マスタデータマネージャ</param>
        protected ScreenBaseModel(IMstDataManager mstDataManager) : base(mstDataManager: mstDataManager)
        {
        }
        
        protected override void SetUpProperties()
        {
            _screenTitle = Data.ScreenTitle;
        }
    }
}