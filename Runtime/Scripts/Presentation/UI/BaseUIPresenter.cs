using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Application.Manager;
using JABARACdesign.Base.Application.UI;
using JABARACdesign.Base.Domain.Entity.API;
using R3;
using VContainer;

namespace JABARACdesign.Base.Presentation.UI
{
    /// <summary>
    /// UIプレゼンターのインターフェイス。
    /// </summary>
    public interface IBaseUIPresenter : IDisposable
    {
        public UniTask InitializeAsyncBase(
            BaseUIData data,
            CancellationToken cancellationToken);
        
        public void AddDisposable(IDisposable disposable);
        
        public UniTask<TData> HandleAPIResponseAsync<TData>(
            IAPIResponse<TData> response,
            CancellationToken cancellationToken)
            where TData : class;
        
        public void DisposeUI();
        
        public CancellationToken CancellationToken { get; }
        
        public CompositeDisposable Disposables { get; }
    }
    
    /// <summary>
    /// モデルとビューを持つUIプレゼンターの基底クラス。
    /// </summary>
    public abstract class BaseUIPresenter<TModel, TView> : IBaseUIPresenter
        where TModel : IBaseUIModel
        where TView : IDIBaseUIView
    {
        protected readonly TModel _model;
        
        protected readonly TView _view;
        
        private CancellationTokenSource _cancellationTokenSource = new();
        
        public CancellationToken CancellationToken => _cancellationTokenSource.Token;
        
        private readonly CompositeDisposable _disposables = new();
        
        private BaseUIData _initData;
        
        public CompositeDisposable Disposables => _disposables;
        
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
        
        public TModel Model => _model;
        
        public TView View => _view;
        
        /// <summary>
        /// 初期化処理。
        /// </summary>
        /// <param name="data">初期化データ</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        public async UniTask InitializeAsyncBase(BaseUIData data, CancellationToken cancellationToken)
        {
            _initData = data;
            _model.InitializeModelBase(initData: _initData);
            await _view.InitializeViewAsyncBase(initData: _initData, cancellationToken: cancellationToken);
            
            await InitializeAsync(cancellationToken: cancellationToken);
            
            SetupViewEvent();
            SetupModelEvent();
        }
        
        /// <summary>
        /// UIFactoryからUIを破棄する。
        /// </summary>
        public void DisposeUI()
        {
            _view.DisposeUI();
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
                case APIStatus.Code.Success:
                    return response.Data;
                
                case APIStatus.Code.Error:
                    var message = response.ErrorMessage;
                    await _view.CreateErrorDialogAsync(message: message, cancellationToken: cancellationToken);
                    return null;
                
                case APIStatus.Code.Maintenance:
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