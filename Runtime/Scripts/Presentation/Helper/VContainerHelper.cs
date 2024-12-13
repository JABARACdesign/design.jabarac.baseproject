using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Presentation.UI;
using VContainer;
using VContainer.Unity;

namespace JABARACdesign.Base.Presentation.Helper
{
    /// <summary>
    /// VContainerのUtilityクラス。
    /// </summary>
    public static class VContainerHelper
    {
        /// <summary>
        /// Model,View,Presenterを持つLifetimeScopeを生成する。
        /// </summary>
        /// <typeparam name="TModel">Modelの型</typeparam>
        /// <typeparam name="TView">Viewの型</typeparam>
        /// <typeparam name="TPresenter">Presenterの型</typeparam>
        /// <param name="view">LifetimeScopeに登録するViewのインスタンス</param>
        /// <param name="parentLifetimeScope">親のLifetimeScope</param>
        /// <returns>LifetimeScope</returns>
        public static LifetimeScope CreateLifetimeScope<TModel, TView, TPresenter>(
            TView view,
            LifetimeScope parentLifetimeScope)
            where TModel : IBaseUIModel
            where TView : IDIBaseUIView
            where TPresenter : IBaseUIPresenter<IBaseUIModel, IDIBaseUIView>
        {
            var lifetimeScope = parentLifetimeScope.CreateChild(
                (IContainerBuilder builder) =>
                {
                    builder.Register<TModel>(lifetime: Lifetime.Scoped).AsImplementedInterfaces();
                    builder.Register<TPresenter>(lifetime: Lifetime.Scoped);
                    builder.RegisterComponent(component: view).AsImplementedInterfaces().AsSelf();
                });
            
            return lifetimeScope;
        }
        
        /// <summary>
        /// View,Presenterを持つLifetimeScopeを生成する。
        /// </summary>
        /// <typeparam name="TView">Viewの型</typeparam>
        /// <typeparam name="TPresenter">Presenterの型</typeparam>
        /// <param name="view">LifetimeScopeに登録するViewのインスタンス</param>
        /// <param name="parentLifetimeScope">親のLifetimeScope</param>
        /// <returns>LifetimeScope</returns>
        public static LifetimeScope CreateLifetimeScope<TView, TPresenter>(
            TView view,
            LifetimeScope parentLifetimeScope)
            where TView : IBaseUIView
            where TPresenter : class
        {
            var lifetimeScope = parentLifetimeScope.CreateChild(
                (IContainerBuilder builder) =>
                {
                    builder.Register<TPresenter>(lifetime: Lifetime.Scoped);
                    builder.RegisterComponent(component: view).AsImplementedInterfaces();
                });
            
            return lifetimeScope;
        }
    }
}