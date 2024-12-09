using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Presentation.UI;
using VContainer;
using VContainer.Unity;

namespace JABARACdesign.Base.Presentation.Factory
{
    /// <summary>
    /// Presenterを生成するファクトリーのクラス。
    /// </summary>
    public class PresenterFactory : IPresenterFactory
    {
        /// <summary>
        /// Model,Viewを持つプレゼンターを生成する。
        /// </summary>
        /// <typeparam name="TPresenter">Presenterの型</typeparam>
        /// <typeparam name="TModel">Modelの型</typeparam>
        /// <typeparam name="TView">Viewの型</typeparam>
        /// <param name="scope">ライフタイムスコープ</param>
        /// <returns>Presenter</returns>
        public TPresenter CreatePresenter<TModel, TView, TPresenter>(LifetimeScope scope)
            where TModel : IBaseUIModel
            where TView : IBaseUIView
            where TPresenter : IBaseUIPresenter
        {
            return ResolvePresenter<TPresenter>(scope: scope);
        }
        
        /// <summary>
        /// Presenterを解決して生成する。
        /// </summary>
        /// <typeparam name="TPresenter">Presenterの型</typeparam>
        /// <param name="scope">解決するLifetimeScope</param>
        /// <returns>Presenter</returns>
        private TPresenter ResolvePresenter<TPresenter>(LifetimeScope scope)
        {
            var resolver = scope.Container;
            var presenter = resolver.Resolve<TPresenter>();
            return presenter;
        }
    }
}