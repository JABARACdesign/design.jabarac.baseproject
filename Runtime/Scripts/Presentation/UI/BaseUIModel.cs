using System;
using System.Threading;
using JABARACdesign.Base.Application.Manager;
using JABARACdesign.Base.Application.UI;
using JABARACdesign.Base.Domain.Entity.Helper;
using R3;

namespace JABARACdesign.Base.Presentation.UI
{
    /// <summary>
    /// UIのモデルのインターフェイス。
    /// </summary>
    public interface IBaseUIModel : IDisposable
    {
        bool IsInitialized { get; }
        void InitializeModel();
        
        void InitializeModelBase(BaseUIData initData);
    }
    
    /// <summary>
    /// UIのモデルの基底クラス。
    /// </summary>
    public abstract class BaseUIModel<TData> : IBaseUIModel
        where TData : BaseUIData
    {
        protected IMstDataManager MstDataManager;
        
        private CancellationTokenSource _cancellationTokenSource;
        
        private readonly CompositeDisposable _disposables = new();
        
        private bool _isInitialized;
        
        /// <summary>
        /// 初期化データ
        /// </summary>
        protected TData Data;
        
        /// <summary>
        /// キャンセルトークン
        /// </summary>
        protected CancellationToken CancellationToken
        {
            get
            {
                if (_cancellationTokenSource == null || _cancellationTokenSource.IsCancellationRequested)
                {
                    _cancellationTokenSource = new CancellationTokenSource();
                }
                
                return _cancellationTokenSource.Token;
            }
        }
        
        /// <summary>
        /// Dispose処理をまとめて行うためのCompositeDisposable
        /// </summary>
        protected CompositeDisposable Disposables => _disposables;
        
        /// <summary>
        /// 初期化済みかどうか
        /// </summary>
        public bool IsInitialized => _isInitialized;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BaseUIModel(IMstDataManager mstDataManager)
        {
            MstDataManager = mstDataManager;
            _cancellationTokenSource = new CancellationTokenSource();
        }
        
        public virtual void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
            _disposables.Dispose();
        }
        
        /// <summary>
        /// キャンセルトークンをリセットする
        /// </summary>
        private void ResetCancellationTokenSource()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
        }
        
        /// <summary>
        /// Modelの初期化処理のベース
        /// </summary>
        /// <param name="initData">初期化データ</param>
        /// <returns>UniTask</returns>
        public virtual void InitializeModelBase(BaseUIData initData)
        {
            if (_isInitialized)　return;
            
            ResetCancellationTokenSource();
            
            // 初期化データのキャスト
            if (initData is not TData castedData)
            {
                LogHelper.Error(message: "キャストに失敗しました : " + initData.GetType().Name);
                return;
            }
            
            // 初期化データのキャッシュ
            Data = castedData;
            
            // モデルの初期化処理
            InitializeModel();
            
            _isInitialized = true;
        }
        
        /// <summary>
        /// モデルの初期化処理
        /// </summary>
        /// <returns>UniTask</returns>
        public virtual void InitializeModel()
        {
            SetUpProperties();
        }
        
        /// <summary>
        /// プロパティの初期化処理
        /// </summary>
        protected abstract void SetUpProperties();
        
        /// <summary>
        /// UniRxの監視をCompositeDisposableに追加する。
        /// </summary>
        /// <param name="disposable">IDisposable</param>
        protected void AddDisposable(IDisposable disposable)
        {
            _disposables.Add(item: disposable);
        }
    }
}