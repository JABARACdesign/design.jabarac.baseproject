using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Application.Manager;
using JABARACdesign.Base.Application.UI;
using JABARACdesign.Base.Domain.Definition;
using R3;
using VContainer;

namespace JABARACdesign.Base.Presentation.UI
{
    /// <summary>
    /// UIプレゼンターのインターフェイス。
    /// </summary>
    public interface IBaseUIPresenter<out TModel, out TView> : IDisposable
        where TModel : IBaseUIModel
        where TView : IDIBaseUIView
    {
        TModel Model { get; }
        
        TView View { get; }
        
        UniTask InitializeAsyncBase(
            BaseUIData data,
            CancellationToken cancellationToken);
        
        void AddDisposable(IDisposable disposable);
        
        UniTask<TData> HandleAPIResponseAsync<TData>(
            IAPIResponse<TData> response,
            CancellationToken cancellationToken)
            where TData : class;
        
        void DisposeUI();
        
        CancellationToken CancellationToken { get; }
        
        CompositeDisposable Disposables { get; }
    }
    
    /// <summary>
    /// モデルとビューを持つUIプレゼンターの基底クラス。
    /// </summary>
    public abstract class BaseUIPresenter<TModel, TView> : IBaseUIPresenter<TModel, TView>
        where TModel : IBaseUIModel
        where TView : IDIBaseUIView
    {
        
        public TModel Model => _model;
        
        public TView View => _view;
        
        public CancellationToken CancellationToken => _cancellationTokenSource.Token;
        
        public CompositeDisposable Disposables => _disposables;
        
        private readonly TModel _model;
        
        private readonly TView _view;
        
        private CancellationTokenSource _cancellationTokenSource = new();
        
        private readonly CompositeDisposable _disposables = new();
        
        private BaseUIData _initData;
        protected IMstDataManager MstDataManager { get; }
        
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="view">View</param>
        /// <param name="mstDataManager">マスタデータマネージャ</param>
        [Inject]
        protected BaseUIPresenter(
            TModel model,
            TView view,
            IMstDataManager mstDataManager)
        {
            _view = view;
            _model = model;
            MstDataManager = mstDataManager;
        }
        
        /// <summary>
        /// 初期化処理。
        /// </summary>
        /// <param name="data">初期化データ</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        public async UniTask InitializeAsyncBase(BaseUIData data, CancellationToken cancellationToken)
        {
            _initData = data;
            Model.InitializeModelBase(initData: _initData);
            await View.InitializeViewAsyncBase(initData: _initData, cancellationToken: cancellationToken);
            
            await InitializeAsync(cancellationToken: cancellationToken);
            
            SetupViewEvent();
            SetupModelEvent();
        }
        
        /// <summary>
        /// UIFactoryからUIを破棄する。
        /// </summary>
        public void DisposeUI()
        {
            View.DisposeUI();
        }
        
        public virtual void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _disposables.Dispose();
        }
        
        /// <summary>
        /// ビューのイベント監視を設定する。
        /// </summary>
        protected virtual void SetupViewEvent()
        {
        }
        
        /// <summary>
        /// モデルのイベント監視を設定する。
        /// </summary>
        protected virtual void SetupModelEvent()
        {
        }
        
        protected abstract UniTask InitializeAsync(CancellationToken cancellationToken);
        
        /// <summary>
        /// APIのレスポンスを処理する。
        /// </summary>
        /// <param name="response">APIレスポンス</param>
        /// <typeparam name="TData">レスポンスのデータ型</typeparam>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>レスポンスのデータ</returns>
        public async UniTask<TData> HandleAPIResponseAsync<TData>(
            IAPIResponse<TData> response,
            CancellationToken cancellationToken)
            where TData : class
        {
            switch (response.Status)
            {
                case APIDefinition.Code.Success:
                    return response.Data;
                
                case APIDefinition.Code.Error:
                    var message = response.ErrorMessage;
                    await View.CreateErrorDialogAsync(message: message, cancellationToken: cancellationToken);
                    return null;
                
                case APIDefinition.Code.Maintenance:
                default:
                    return null;
            }
        }
        
        /// <summary>
        /// UniRxの監視をCompositeDisposableに追加する。
        /// </summary>
        /// <param name="disposable">IDisposable</param>
        public void AddDisposable(IDisposable disposable)
        {
            _disposables.Add(item: disposable);
        }
    }
}