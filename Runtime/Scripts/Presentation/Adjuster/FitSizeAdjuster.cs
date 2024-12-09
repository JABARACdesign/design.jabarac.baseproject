using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Presentation.Extension;
using R3;
using UnityEngine;

namespace JABARACdesign.Base.Presentation.Adjuster
{
    /// <summary>
    /// 対象に合わせてサイズを調整するクラス。
    /// </summary>
    public class FitSizeAdjuster : MonoBehaviour
    {
        [SerializeField]
        private bool _isFitHeight;
        
        [SerializeField]
        private bool _isFitWidth;
        
        [SerializeField]
        private bool _isFixAspectRatio;
        
        [SerializeField]
        private RectTransform _parentRectTransform;
        
        [SerializeField]
        private RectTransform _targetRectTransform;
        
        private Vector2 _parentSize = default;
        
        public void Awake()
        {
            // RectTransformが確定した際の処理を登録
            Observable.EveryValueChanged(_parentRectTransform, x => x.sizeDelta)
                .Subscribe(OnRectTransformDimensionsChanged)
                .AddTo(destroyCancellationToken);
        }
        
        /// <summary>
        /// RectTransformのサイズが変更された際の処理。
        /// </summary>
        /// <param name="size">サイズ</param>
        private void OnRectTransformDimensionsChanged(Vector2 size)
        {
            _parentSize = _parentRectTransform.rect.size;
            AdjustSize();
        }
        
        /// <summary>
        /// サイズを調整する。
        /// </summary>
        private void AdjustSize()
        {
            if (!_isFitWidth && !_isFitHeight)
            {
                return;
            }
            
            var targetSize = GetTargetSize();
            
            if (_isFixAspectRatio)
            {
                targetSize = AdjustSizeWithAspectRatio(targetSize: targetSize);
            }
            
            _targetRectTransform.SetSize(x: targetSize.x, y: targetSize.y);
        }
        
        /// <summary>
        /// 目標のサイズを取得する。
        /// </summary>
        /// <returns>目標のサイズ</returns>
        private Vector2 GetTargetSize()
        {
            return new Vector2(
                x: _isFitWidth ? _parentSize.x : _targetRectTransform.sizeDelta.x,
                y: _isFitHeight ? _parentSize.y : _targetRectTransform.sizeDelta.y
            );
        }
        
        /// <summary>
        /// アスペクト比を維持してサイズを調整する。
        /// </summary>
        /// <param name="targetSize">目標のサイズ</param>
        /// <returns>調整後のサイズ</returns>
        private Vector2 AdjustSizeWithAspectRatio(Vector2 targetSize)
        {
            var aspectRatio = _targetRectTransform.sizeDelta.x / _targetRectTransform.sizeDelta.y;
            
            if (_isFitWidth != _isFitHeight)
            {
                targetSize = _isFitWidth
                    ? new Vector2(x: targetSize.x, y: targetSize.x / aspectRatio)
                    : new Vector2(x: targetSize.y * aspectRatio, y: targetSize.y);
            }
            else
            {
                var parentAspectRatio = _parentSize.x / _parentSize.y;
                targetSize = aspectRatio > parentAspectRatio
                    ? new Vector2(x: _parentSize.x, y: _parentSize.x / aspectRatio)
                    : new Vector2(x: _parentSize.y * aspectRatio, y: _parentSize.y);
            }
            
            return targetSize;
        }
        
        /// <summary>
        /// エディタ上でサイズを調整する。
        /// </summary>
        public void AdjustSizeOnEditor()
        {
            _parentSize = _parentRectTransform.rect.size;
            AdjustSize();
        }
    }
}