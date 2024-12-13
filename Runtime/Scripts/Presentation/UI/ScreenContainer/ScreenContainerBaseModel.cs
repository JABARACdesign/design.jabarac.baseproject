using JABARACdesign.Base.Application.Manager;

namespace JABARACdesign.Base.Presentation.UI.ScreenContainer
{
    /// <summary>
    /// スクリーンコンテナの基底インターフェース。
    /// </summary>
    public interface IScreenContainerBaseModel : IBaseUIModel
    {
    }
    
    /// <summary>
    /// スクリーンコンテナの基底モデルクラス。
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public abstract class ScreenContainerBaseModel<TData> : BaseUIModel<TData>, IScreenContainerBaseModel
        where TData : ScreenContainerBaseData
    {
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="mstDataManager">マスタデータマネージャ</param>
        protected ScreenContainerBaseModel(IMstDataManager mstDataManager) : base(mstDataManager: mstDataManager)
        {
        }
    }
}