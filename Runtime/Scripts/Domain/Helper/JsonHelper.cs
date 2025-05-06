using System;
using System.Collections.Generic;
using UnityEngine;

namespace JABARACdesign.Base.Domain.Helper
{
    /// <summary>
    /// JSONのヘルパークラス
    /// </summary>
    public static class JsonHelper
    {
        public const string LOCAL_DATA_INTERMEDIARY_KEY = "value";
        
        /// <summary>
        /// JSONデシリアライズ用のラッパークラス
        /// </summary>
        /// <typeparam name="T">対象のクラス</typeparam>
        [Serializable]
        private class WrapperArray<T>
        {
            public T[] value;
        }
        
        /// <summary>
        /// JSONデシリアライズ用のラッパークラス
        /// </summary>
        /// <typeparam name="T">対象のクラス</typeparam>
        [Serializable]
        private class WrapperList<T>
        {
            public T[] value;
        }
        
        /// <summary>
        /// JSONをデシリアライズし、配列にする
        /// </summary>
        /// <typeparam name="T">対象のクラス</typeparam>
        /// <param name="json">JSON形式の文字列</param>
        /// <returns>対象クラスの配列</returns>
        public static T[] FromJsonArray<T>(string json)
        {
            var wrappedJson = GetWrappedJson(json: json);
            var wrapper = JsonUtility.FromJson<WrapperArray<T>>(json: wrappedJson);
            
            return wrapper.value;
        }
        
        /// <summary>
        /// ラップされたJSONを取得する
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private static string GetWrappedJson(string json)
        {
            return "{ \"" + LOCAL_DATA_INTERMEDIARY_KEY + "\": " + json + " }";
        }
        
        /// <summary>
        /// 配列をシリアライズし、JSONにする
        /// </summary>
        /// <typeparam name="T">対象のクラス</typeparam>
        /// <param name="array">対象の配列</param>
        /// <returns>JSON文字列</returns>
        public static string ToJsonArray<T>(T[] array)
        {
            var wrapper = new WrapperArray<T>
            {
                value = array
            };
            
            return JsonUtility.ToJson(obj: wrapper);
        }
        
        /// <summary>
        /// JSONをデシリアライズし、リストにする
        /// </summary>
        /// <typeparam name="T">対象のクラス</typeparam>
        /// <param name="json">JSON形式の文字列</param>
        /// <returns>デシリアライズされたリスト</returns>
        public static List<T> FromJsonList<T>(string json)
        {
            var wrappedJson = GetWrappedJson(json: json);
            var wrapper = JsonUtility.FromJson<WrapperList<T>>(json: wrappedJson);
            
            return new List<T>(collection: wrapper.value);
        }
        
        /// <summary>
        /// リストをシリアライズし、JSONにする
        /// </summary>
        /// <typeparam name="T">対象のクラス</typeparam>
        /// <param name="list">対象のリスト</param>
        /// <returns>JSON文字列</returns>
        public static string ToJsonList<T>(List<T> list)
        {
            var wrapper = new WrapperList<T>
            {
                value = list.ToArray()
            };
            
            return JsonUtility.ToJson(obj: wrapper);
        }
    }
}