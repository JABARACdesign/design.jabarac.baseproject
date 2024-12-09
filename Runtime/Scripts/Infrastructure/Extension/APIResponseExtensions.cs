using System;
using System.Collections.Generic;
using System.Linq;
using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Domain.Entity;
using JABARACdesign.Base.Domain.Entity.API;
using JABARACdesign.Base.Domain.Interface;
using JABARACdesign.Base.Infrastructure.Network.API;

namespace JABARACdesign.Base.Infrastructure.Extension
{
    /// <summary>
    /// APIレスポンス拡張クラス。
    /// </summary>
    public static class APIResponseExtensions
    {
        /// <summary>
        /// DTOからEntityへの変換を行う
        /// </summary>
        public static IAPIResponse<TEntity> ToEntityResponse<TDto, TEntity>(
            this IAPIResponse<TDto> response)
            where TDto : IDomainDataDto<TEntity>
            where TEntity : IDomainData
        {
            if (response.Status != APIStatus.Code.Success)
            {
                return new APIResponse<TEntity>(
                    status: response.Status,
                    data: default,
                    errorMessage: response.ErrorMessage
                );
            }
            
            try
            {
                return new APIResponse<TEntity>(
                    status: APIStatus.Code.Success,
                    data: response.Data.ToEntity(),
                    errorMessage: null
                );
            }
            catch (Exception ex)
            {
                return new APIResponse<TEntity>(
                    status: APIStatus.Code.Error,
                    data: default,
                    errorMessage: $"エンティティへの変換に失敗しました: {ex.Message}"
                );
            }
        }
        
        /// <summary>
        /// DTOリストからEntityリストへの変換を行う
        /// </summary>
        public static IAPIResponse<List<TEntity>> ToEntityListResponse<TDto, TEntity>(
            this IAPIResponse<List<TDto>> response)
            where TDto : IDomainDataDto<TEntity>
            where TEntity : IDomainData
        {
            if (response.Status != APIStatus.Code.Success)
            {
                return new APIResponse<List<TEntity>>(
                    status: response.Status,
                    data: default,
                    errorMessage: response.ErrorMessage
                );
            }
            
            try
            {
                var entities = response.Data.Select(dto => dto.ToEntity()).ToList();
                return new APIResponse<List<TEntity>>(
                    status: APIStatus.Code.Success,
                    data: entities,
                    errorMessage: null
                );
            }
            catch (Exception ex)
            {
                return new APIResponse<List<TEntity>>(
                    status: APIStatus.Code.Error,
                    data: default,
                    errorMessage: $"エンティティリストへの変換に失敗しました: {ex.Message}"
                );
            }
        }
        
        /// <summary>
        /// レスポンスを新しい型にマップする
        /// </summary>
        public static IAPIResponse<TResult> Map<TSource, TResult>(
            this IAPIResponse<TSource> response,
            Func<TSource, TResult> mapper)
        {
            if (response.Status != APIStatus.Code.Success)
            {
                return new APIResponse<TResult>(
                    status: response.Status,
                    data: default,
                    errorMessage: response.ErrorMessage
                );
            }
            
            try
            {
                return new APIResponse<TResult>(
                    status: APIStatus.Code.Success,
                    data: mapper(arg: response.Data),
                    errorMessage: null
                );
            }
            catch (Exception ex)
            {
                return new APIResponse<TResult>(
                    status: APIStatus.Code.Error,
                    data: default,
                    errorMessage: $"データの変換に失敗しました: {ex.Message}"
                );
            }
        }
    }
}