using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;
using UnityEngine;

namespace JABARACdesign.Base.Infrastructure.Client
{
    /// <summary>
    /// ローカルストレージのAPIクライアントインターフェース。
    /// </summary>
    public interface ILocalStorageAPIClient
    {
        /// <summary>
        /// ローカルファイルが存在するかどうかを返す。
        /// </summary>
        /// <param name="identifier">識別子</param>
        /// <returns>ローカルファイルが存在するかどうか</returns>
        bool IsLocalFileExists<TEnum>(TEnum identifier)
            where TEnum : struct, Enum;
        
        /// <summary>
        /// ローカルからテクスチャをロードする。
        /// </summary>
        /// <param name="identifier">識別子</param>
        /// <returns>APIレスポンス(Texture2D)</returns>
        UniTask<IAPIResponse<Texture2D>> LoadTextureAsync<TEnum>(TEnum identifier)
            where TEnum : struct, Enum;
        
        /// <summary>
        /// ローカルから音声をロードする。
        /// </summary>
        /// <param name="identifier">識別子</param>
        /// <param name="audioType">音声のタイプ</param>
        /// <returns>APIレスポンス(AudioClip)</returns>
        UniTask<IAPIResponse<AudioClip>> LoadAudioAsync<TEnum>(
            TEnum identifier,
            AudioType audioType)
            where TEnum : struct, Enum;
        
        /// <summary>
        /// ローカルからJSONデータをロードする。
        /// </summary>
        /// <typeparam name="TData">JSONデータの型</typeparam>
        /// <typeparam name="TEnum">識別子の列挙型</typeparam>
        /// <param name="identifier">識別子</param>
        /// <returns>APIレスポンス(T型のデータ)</returns>
        UniTask<IAPIResponse<TData>> LoadJsonAsync<TData, TEnum>(TEnum identifier)
            where TEnum : struct, Enum;

        /// <summary>
        /// ローカルからJSON配列データをロードしてリストとして取得する。
        /// </summary>
        /// <typeparam name="TData">JSONデータの型</typeparam>
        /// <typeparam name="TEnum">識別子の列挙型</typeparam>
        /// <param name="identifier">識別子</param>
        /// <returns>APIレスポンス(T型のリスト)</returns>
        UniTask<IAPIResponse<List<TData>>> LoadJsonListAsync<TData, TEnum>(TEnum identifier)
            where TEnum : struct, Enum;

        /// <summary>
        /// JSONデータをローカルに保存する。
        /// </summary>
        /// <param name="data">データ</param>
        /// <param name="identifier">識別子</param>
        /// <typeparam name="TData">保存するデータの型</typeparam>
        /// <typeparam name="TEnum">識別子の型</typeparam>
        /// <returns>APIレスポンス</returns>
        UniTask<IAPIResponse> SaveJsonAsync<TData, TEnum>(
            TData data,
            TEnum identifier)
            where TEnum : struct, Enum;

        /// <summary>
        /// リストデータをJSON形式でローカルに保存する。
        /// </summary>
        /// <param name="dataList">データのリスト</param>
        /// <param name="identifier">識別子</param>
        /// <typeparam name="TData">保存するデータの型</typeparam>
        /// <typeparam name="TEnum">識別子の型</typeparam>
        /// <returns>APIレスポンス</returns>
        UniTask<IAPIResponse> SaveJsonListAsync<TData, TEnum>(
            List<TData> dataList,
            TEnum identifier)
            where TEnum : struct, Enum;
    }
}