using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace JABARACdesign.Base.Application.ScriptableObject
{
    /// <summary>
    /// アセットリファレンス
    /// </summary>
    /// <typeparam name="TEnum">ラベルの型</typeparam>
    public interface IAssetSettings<in TEnum>
        where TEnum : Enum, IUILabel
    {
        AssetReference GetAssetReference(TEnum label);
    }
    
    /// <summary>
    /// アセットリファレンスの設定の基底クラス。
    /// </summary>
    /// <typeparam name="TEnum">ラベルの型</typeparam>
    public abstract class AssetSettingsBase<TEnum> : UnityEngine.ScriptableObject, IAssetSettings<TEnum>
        where TEnum : Enum, IUILabel
    {
        [SerializeField]
        private List<AssetReferenceUnit<TEnum>> _assetReferenceUnits = new();
        
        private readonly Dictionary<string , AssetReference> _assetReferenceDictionary = new();
        
        public Dictionary<string , AssetReference> AssetReferenceDictionary => _assetReferenceDictionary;
        
        private void OnEnable()
        {
            BuildDictionary();
        }
        
        /// <summary>
        /// ディクショナリの構築を行う。
        /// </summary>
        private void BuildDictionary()
        {
            _assetReferenceDictionary.Clear();
            foreach (var assetReferenceUnit in _assetReferenceUnits)
            {
                _assetReferenceDictionary.TryAdd(
                    key: assetReferenceUnit.Key,
                    value: assetReferenceUnit.AssetReference);
            }
        }
        
        /// <summary>
        /// アセットリファレンスを取得する。
        /// </summary>
        /// <param name="label">アセットラベル</param>
        /// <returns>アセットリファレンス</returns>
        public AssetReference GetAssetReference(TEnum label)
        {
            var key = label.ToKey();
            if (_assetReferenceDictionary.TryGetValue(key: key, value: out var assetReference))
            {
                return assetReference;
            }
            
            throw new KeyNotFoundException(message: $"Key: {label} is not found in the dictionary.");
        }
    }
}