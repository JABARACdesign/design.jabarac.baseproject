using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Presentation.UI;
using VContainer.Unity;

namespace JABARACdesign.Base.Presentation.Factory
{
    /// <summary>
    /// Presenterを生成するファクトリーのインターフェース。
    /// </summary>
    public interface IPresenterFactory
    {
        /// <summary>
        /// Model,Viewを持つプレゼンターを生成する。
        /// </summary>
        /// <typeparam name="TPresenter">Presenterの型</typeparam>
        /// <typeparam name="TModel">Modelの型</typeparam>
        /// <typeparam name="TView">Viewの型</typeparam>
        /// <param name="scope">ライフタイムスコープ</param>
        /// <returns>Presenter</returns>
        TPresenter CreatePresenter<TModel, TView, TPresenter>(LifetimeScope scope)
            where TModel : IBaseUIModel
            where TView : IBaseUIView
            where TPresenter : IBaseUIPresenter;
    }
}