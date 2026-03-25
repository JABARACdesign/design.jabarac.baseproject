using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Domain.Definition;
using JABARACdesign.Base.Infrastructure.API;

namespace JABARACdesign.Base.Tests.Utility
{
    /// <summary>
    /// テスト用のAPIレスポンスヘルパー。
    /// 成功/失敗のレスポンスを簡潔に生成する。
    /// </summary>
    public static class TestAPIResponseHelper
    {
        /// <summary>
        /// 成功レスポンスを生成する。
        /// </summary>
        /// <typeparam name="TData">データの型</typeparam>
        /// <param name="data">レスポンスデータ</param>
        /// <returns>成功APIレスポンス</returns>
        public static IAPIResponse<TData> Success<TData>(TData data)
        {
            return new APIResponse<TData>(
                status: APIDefinition.Code.Success,
                data: data);
        }
        
        /// <summary>
        /// エラーレスポンスを生成する。
        /// </summary>
        /// <typeparam name="TData">データの型</typeparam>
        /// <param name="errorMessage">エラーメッセージ</param>
        /// <returns>エラーAPIレスポンス</returns>
        public static IAPIResponse<TData> Error<TData>(string errorMessage = "テストエラー")
        {
            return new APIResponse<TData>(
                status: APIDefinition.Code.Error,
                data: default,
                errorMessage: errorMessage);
        }
        
        /// <summary>
        /// 成功レスポンス（データなし）を生成する。
        /// </summary>
        /// <returns>成功APIレスポンス</returns>
        public static IAPIResponse Success()
        {
            return new APIResponse(status: APIDefinition.Code.Success);
        }
        
        /// <summary>
        /// エラーレスポンス（データなし）を生成する。
        /// </summary>
        /// <param name="errorMessage">エラーメッセージ</param>
        /// <returns>エラーAPIレスポンス</returns>
        public static IAPIResponse Error(string errorMessage = "テストエラー")
        {
            return new APIResponse(
                status: APIDefinition.Code.Error,
                errorMessage: errorMessage);
        }
    }
}
