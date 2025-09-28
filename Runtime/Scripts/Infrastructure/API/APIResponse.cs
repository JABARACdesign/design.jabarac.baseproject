using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Domain.Definition;

namespace JABARACdesign.Base.Infrastructure.API
{
    /// <summary>
    /// APIのレスポンス(返却データあり)
    /// </summary>
    /// <typeparam name="TData">データの型</typeparam>
    public class APIResponse<TData> : IAPIResponse<TData>
    {
        public APIDefinition.Code Status { get; }
        public TData Data { get; }
        public string ErrorMessage { get; }
        
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="status">ステータス</param>
        /// <param name="data">データ</param>
        /// <param name="errorMessage">エラーメッセージ</param>
        public APIResponse(
            APIDefinition.Code status,
            TData data,
            string errorMessage = "")
        {
            Status = status;
            Data = data;
            ErrorMessage = errorMessage;
        }
    }
    
    /// <summary>
    /// APIのレスポンス(返却データなし)
    /// </summary>
    public class APIResponse : IAPIResponse
    {
        public APIDefinition.Code Status { get; }
        public string ErrorMessage { get; }
        
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="status">ステータス</param>
        /// <param name="errorMessage">エラーメッセージ</param>
        public APIResponse(
            APIDefinition.Code status,
            string errorMessage = "")
        {
            Status = status;
            ErrorMessage = errorMessage;
        }
    }
}