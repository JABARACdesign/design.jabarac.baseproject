using System;
using System.Collections.Generic;
using System.Linq;
using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Domain.Definition;
using JABARACdesign.Base.Domain.Interface;
using JABARACdesign.Base.Infrastructure.API;

namespace JABARACdesign.Base.Infrastructure.Extension
{
    /// <summary>
    /// APIレスポンス拡張クラス。
    /// </summary>
    public static class APIResponseExtensions
    {
        /// <summary>
        /// DTOから結果への変換を行う
        /// </summary>
        public static IAPIResponse<TResult> ToResult<TDto, TResult>(
            this IAPIResponse<TDto> response)
            where TDto : IDomainDataDto<TResult>
        {
            if (response.Status != APIDefinition.Code.Success)
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
                    status: APIDefinition.Code.Success,
                    data: response.Data.ToResult(),
                    errorMessage: null
                );
            }
            catch (Exception ex)
            {
                return new APIResponse<TResult>(
                    status: APIDefinition.Code.Error,
                    data: default,
                    errorMessage: $"結果への変換に失敗しました: {ex.Message}"
                );
            }
        }
        
        /// <summary>
        /// DTOリストからEntityリストへの変換を行う
        /// </summary>
        public static IAPIResponse<List<TResult>> ToResultList<TDto, TResult>(
            this IAPIResponse<List<TDto>> response)
            where TDto : IDomainDataDto<TResult>
        {
            if (response.Status != APIDefinition.Code.Success)
            {
                return new APIResponse<List<TResult>>(
                    status: response.Status,
                    data: default,
                    errorMessage: response.ErrorMessage
                );
            }
            
            try
            {
                var entities = response.Data.Select(dto => dto.ToResult()).ToList();
                return new APIResponse<List<TResult>>(
                    status: APIDefinition.Code.Success,
                    data: entities,
                    errorMessage: null
                );
            }
            catch (Exception ex)
            {
                return new APIResponse<List<TResult>>(
                    status: APIDefinition.Code.Error,
                    data: default,
                    errorMessage: $"結果リストへの変換に失敗しました: {ex.Message}"
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
            if (response.Status != APIDefinition.Code.Success)
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
                    status: APIDefinition.Code.Success,
                    data: mapper(arg: response.Data),
                    errorMessage: null
                );
            }
            catch (Exception ex)
            {
                return new APIResponse<TResult>(
                    status: APIDefinition.Code.Error,
                    data: default,
                    errorMessage: $"データの変換に失敗しました: {ex.Message}"
                );
            }
        }
    }
}