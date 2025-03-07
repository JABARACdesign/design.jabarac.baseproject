using System.Threading;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Application.UI;
using JABARACdesign.Base.Domain.Entity.Helper;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace JABARACdesign.Base.Presentation.UI
{
    /// <summary>
    /// UIのViewの基底クラス。
    /// </summary>
    /// <typeparam name="TData">データの型</typeparam>
    public abstract class BaseUIView<TData> : BaseUIViewCore, IBaseUIView
        where TData : BaseUIData
    {
        [FormerlySerializedAs("_data")]
        [Header(header: "設定項目")]
        
        [SerializeField]
        protected TData Data;
        
        private CancellationToken _cancellationToken = default;
        
        private bool _isInitialized;
        
        protected CancellationToken CancellationToken => _cancellationToken;
        
        public Transform Transform => transform;
        
        public RectTransform RectTransform => transform as RectTransform;
        
        public bool IsInitialized => _isInitialized;
        
        /// <summary>
        /// Viewの初期化処理のベース
        /// </summary>
        /// <param name="initData">初期化データ</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>UniTask</returns>
        public virtual async UniTask InitializeViewAsyncBase(
            BaseUIData initData,
            CancellationToken cancellationToken)
        {
            if (_isInitialized) return;
            
            // オブジェクト破棄時のキャンセルトークンを設定
            _cancellationToken = this.GetCancellationTokenOnDestroy();
            
            // 初期化データのキャスト
            if (initData is not TData castedData)
            {
                LogHelper.Error(message: "キャストに失敗しました : " + initData.GetType().Name);
                return;
            }
            
            // 初期化データの反映
            Data = castedData;
            
            // 初期化処理
            await InitializeViewAsync(cancellationToken: _cancellationToken);
            
            // 処理は1回のみ実施する
            _isInitialized = true;
        }
        
        /// <summary>
        /// サブクラスにおけるViewの初期化処理。
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns></returns>
        public virtual async UniTask InitializeViewAsync(CancellationToken cancellationToken = default)
        {
            // Viewの見た目の反映
            ApplyView();
            
            // Viewの見た目の反映(非同期)
            await ApplyViewAsync(cancellationToken: cancellationToken);
            
            // Viewの見た目の反映後の処理
            OnAfterApplyView();
            
            // イベントの設定
            SetupViewEvent();
            
            PlayShowAnimationAsync(cancellationToken: cancellationToken).Forget();
        }
        
        protected abstract void ApplyView();
        
        /// <summary>
        /// Viewの見た目の反映処理。
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン</param>
        protected virtual async UniTask ApplyViewAsync(CancellationToken cancellationToken)
        {
            await UniTask.CompletedTask;
        }
        
        /// <summary>
        /// コンポーネントの設定を見た目に反映する。
        /// この処理は、エディタのOnValidateで呼び出される。
        /// </summary>
        protected abstract void ApplyViewOnEditor();
        
        /// <summary>
        /// Viewの見た目の反映処理後の処理。
        /// </summary>
        protected virtual void OnAfterApplyView()
        {
        }
        
        /// <summary>
        /// Viewのイベント設定処理。
        /// </summary>
        protected abstract void SetupViewEvent();
        
        /// <summary>
        /// 表示アニメーションを再生する。
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>UniTask</returns>
        public virtual UniTask PlayShowAnimationAsync(CancellationToken cancellationToken)
        {
            // 表示する
            SetActive(isActive: true);
            return UniTask.CompletedTask;
        }
        
        /// <summary>
        /// 非表示アニメーションを再生する。
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>UniTask</returns>
        public virtual UniTask PlayHideAnimationAsync(CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }
        
        /// <summary>
        /// アクティブ状態をセットする
        /// </summary>
        /// <param name="isActive">アクティブ状態</param>
        public void SetActive(bool isActive)
        {
            this.gameObject.SetActive(value: isActive);
        }
        
        /// <summary>
        /// データを適用する
        /// </summary>
        /// <param name="data">データ</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        public async UniTask UpdateDataAsync(
            BaseUIData data,
            CancellationToken cancellationToken)
        {
            if (data is not TData castedData) return;
            Data = castedData;
            
            await InitializeViewAsync(cancellationToken: cancellationToken);
        }
        
        /// <summary>
        /// データを適用する(エディタ用)
        /// </summary>
        /// <param name="data">データ</param>
        public void UpdateViewOnEditor(BaseUIData data)
        {
            if (data is not TData castedData) return;
            Data = castedData;
            ApplyViewOnEditor();
        }
        
        /// <summary>
        /// エラーダイアログを表示する。
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        public async UniTask CreateErrorDialogAsync(string message, CancellationToken cancellationToken)
        {
            LogHelper.Error(message: message);
            await UniTask.CompletedTask;
        }
        
        /// <summary>
        /// このビューのコンポーネントを取得する。
        /// </summary>
        public Component Component => this;
        
        /// <summary>
        /// Dispose処理。
        /// </summary>
        public virtual void Dispose()
        {
            if (this != null && this.gameObject != null)
            {
                Destroy(obj: this.gameObject);
            }
        }
        
#if UNITY_EDITOR
        
        protected void OnValidate()
        {
            if (this.gameObject == null) return;
            
            // プレイモードが開始された、または開始されようとしている場合は何もしない
            if (EditorApplication.isPlayingOrWillChangePlaymode) return;
            
            if (EditorApplication.isCompiling) return;
            
            ApplyViewOnEditor();
        }
        
#endif
    }
}