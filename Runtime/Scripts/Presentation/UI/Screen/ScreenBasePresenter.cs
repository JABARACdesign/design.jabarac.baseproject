using System.Threading;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Manager;

namespace JABARACdesign.Base.Presentation.UI.Screen
{
    /// <summary>
    /// スクリーンの基底プレゼンタークラスのインターフェース。
    /// </summary>
    public interface IScreenBasePresenter<out TModel, out TView> : IBaseUIPresenter<TModel, TView>
        where TModel : IScreenBaseModel
        where TView : IScreenBaseView
    {
        public string GetScreenTitle();
        
        UniTask PlaySlideAnimationAsync(
            bool isForward,
            float startPositionX,
            float screenWidth,
            CancellationToken cancellationToken);
    }
    
    /// <summary>
    /// スクリーンの基底プレゼンタークラス。
    /// </summary>
    public abstract class ScreenBasePresenter<TModel, TView>
        : BaseUIPresenter<TModel, TView>, IScreenBasePresenter<TModel, TView>
        where TModel : IScreenBaseModel
        where TView : IScreenBaseView
    {
        protected ScreenBasePresenter(TModel model, TView view, IMstDataManager mstDataManager) : base(
            model: model,
            view: view,
            mstDataManager: mstDataManager)
        {
        }
        
        public TModel Model { get; }
        public TView View { get; }
        
        /// <summary>
        /// スクリーンタイトルを取得する。
        /// </summary>
        /// <returns>スクリーンタイトル</returns>
        public string GetScreenTitle()
        {
            return Model.ScreenTitle;
        }
        
        /// <summary>
        /// スライドアニメーションを再生する。
        /// </summary>
        /// <param name="isForward">前方へのスライドかどうか</param>
        /// <param name="startPositionX">開始X座標</param>
        /// <param name="screenWidth">スクリーン幅</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>UniTask</returns>
        public UniTask PlaySlideAnimationAsync(
            bool isForward,
            float startPositionX,
            float screenWidth,
            CancellationToken cancellationToken)
        {
            return View.PlaySlideAnimationAsync(
                isForward: isForward,
                startPositionX: startPositionX,
                screenWidth: screenWidth,
                cancellationToken: cancellationToken);
        }


    }
}