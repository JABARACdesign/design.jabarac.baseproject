using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;

namespace JABARACdesign.Base.Infrastructure.Client
{
    public interface IMstDataApiClient
    {
        /// <summary>
        /// 指定したクラスに対応するマスターデータをリストですべて取得する。
        /// </summary>
        /// <typeparam name="TDto">マスターデータのアイテムの型</typeparam>
        /// <returns>マスターデータのリスト</returns>
        /// <exception cref="Exception">データ取得に失敗した際のエラー</exception>
        UniTask<IAPIResponse<List<TDto>>> GetMstDataAsync<TDto, TEnum>(TEnum identifier)
            where TEnum : struct, Enum;

        /// <summary>
        /// 指定したクラスの特定のIDのデータを取得する (文字列IDの場合)。
        /// </summary>
        /// <typeparam name="TDto">指定クラス</typeparam>
        /// <typeparam name="TEnum">データの型</typeparam>
        ///　/// <param name="identifier">識別子</param>
        /// <param name="id">指定ID</param>
        /// <returns>指定IDのデータ</returns>
        /// <exception cref="Exception">データ取得に失敗した際のエラー</exception>
        public UniTask<IAPIResponse<TDto>> GetMstDataByIdAsync<TDto, TEnum>(TEnum identifier, string id)
            where TEnum : struct, Enum;

        /// <summary>
        /// 指定したクラスの特定のIDのデータを取得する (整数IDの場合)。
        /// </summary>
        /// <typeparam name="TDto">指定クラス</typeparam>
        /// <typeparam name="TEnum">データの型</typeparam>
        /// <param name="identifier">識別子</param>
        /// <param name="id">指定ID</param>
        /// <returns>指定IDのデータ</returns>
        /// <exception cref="Exception">データ取得に失敗した際のエラー</exception>
        UniTask<IAPIResponse<TDto>> GetMstDataByIdAsync<TDto, TEnum>(
            TEnum identifier,
            int id)
            where TEnum : struct, Enum;
    }
}