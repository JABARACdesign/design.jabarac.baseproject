using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;

namespace JABARACdesign.Base.Infrastructure.Network.Client
{
    public interface IMstDataApiClient
    {
        /// <summary>
        /// 指定したクラスに対応するマスターデータをリストですべて取得する。
        /// </summary>
        /// <typeparam name="TItemDto">マスターデータのアイテムの型</typeparam>
        /// <returns>マスターデータのリスト</returns>
        /// <exception cref="Exception">データ取得に失敗した際のエラー</exception>
        public UniTask<IAPIResponse<List<TItemDto>>> GetMstDataAsync<TItemDto>();
        
        /// <summary>
        /// 指定したクラスの特定のIDのデータを取得する (文字列IDの場合)
        /// </summary>
        /// <typeparam name="TItemDto">指定クラス</typeparam>
        /// <param name="id">指定ID</param>
        /// <returns>指定IDのデータ</returns>
        /// <exception cref="Exception">データ取得に失敗した際のエラー</exception>
        public UniTask<IAPIResponse<TItemDto>> GetMstDataByIdAsync<TItemDto>(string id);
        
        /// <summary>
        /// 指定したクラスの特定のIDのデータを取得する (整数IDの場合)
        /// </summary>
        /// <typeparam name="TItemDto">指定クラス</typeparam>
        /// <param name="id">指定ID</param>
        /// <returns>指定IDのデータ</returns>
        /// <exception cref="Exception">データ取得に失敗した際のエラー</exception>
        public UniTask<IAPIResponse<TItemDto>> GetMstDataByIdAsync<TItemDto>(int id);
    }
}