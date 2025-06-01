using System;
using UnityEngine;

namespace JABARACdesign.Base.Domain.Definition
{
    /// <summary>
    /// ストレージの定義クラス。
    /// </summary>
    public static class StorageDefinition
    {
        public enum FileType
        {
            CourseImage,
        }
        
        public enum ExtensionType
        {
            Png,
            WAV,
            MP3,
            Json
        }
        
        /// <summary>
        /// 拡張子を取得する。
        /// </summary>
        /// <param name="extensionType">拡張子タイプ</param>
        /// <returns>拡張子</returns>
        /// <exception cref="NotImplementedException">未実装の際のエラー</exception>
        public static string GetExtension(ExtensionType extensionType)
        {
            return extensionType switch
            {
                ExtensionType.Png => ".png",
                ExtensionType.WAV => ".wav",
                ExtensionType.MP3 => ".mp3",
                ExtensionType.Json => ".json",
                _ => throw new NotImplementedException()
            };
        }
        
        /// <summary>
        /// 拡張子タイプをもとに、AudioTypeを取得する。
        /// </summary>
        /// <param name="extensionType">拡張子タイプ</param>
        /// <returns>AudioType</returns>
        public static AudioType GetAudioType(ExtensionType extensionType)
        {
            switch (extensionType)
            {
                case ExtensionType.WAV:
                    return AudioType.WAV;
                case ExtensionType.MP3:
                    return AudioType.MPEG;
                default:
                    return AudioType.UNKNOWN;
            }
        }
    }
}