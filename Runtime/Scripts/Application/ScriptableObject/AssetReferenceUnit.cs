using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace JABARACdesign.Base.Application.ScriptableObject
{
    /// <summary>
    /// ラベルとアセットの参照を保持するクラス。
    /// </summary>
    /// <typeparam name="TEnum">ラベルの型</typeparam>
    [Serializable]
    public class AssetReferenceUnit<TEnum>
        where TEnum : Enum
    {
        [SerializeField] 
        private TEnum _label;
        
        [SerializeField] 
        private AssetReference _assetReference;
        
        public TEnum Label => _label;
        public AssetReference AssetReference => _assetReference;
    }
}