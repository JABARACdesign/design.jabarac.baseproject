using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.UI;
using UnityEngine;

namespace JABARACdesign.Base.Application.Interface
{
    /// <summary>
    /// UIのViewのインターフェイス。
    /// </summary>
    public interface IBaseUIView : IDisposable
    {
        bool IsInitialized { get; }
        
        Transform Transform { get; }
        
        UniTask InitializeViewAsyncBase(
            BaseUIData initData,
            CancellationToken cancellationToken);
        
        UniTask UpdateDataAsync(BaseUIData data, CancellationToken cancellationToken);
        
        void UpdateViewOnEditor(BaseUIData data);
        
        UniTask PlayShowAnimationAsync(CancellationToken cancellationToken);
        
        UniTask PlayHideAnimationAsync(CancellationToken cancellationToken);
        
        void SetActive(bool isActive);
        
        UniTask CreateErrorDialogAsync(string message, CancellationToken cancellationToken);
        
        Component Component { get; }
    }
}