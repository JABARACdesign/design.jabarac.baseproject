using JABARACdesign.Base.Domain.Entity.API;

namespace JABARACdesign.Base.Application.Interface
{
    /// <summary>
    /// APIのレスポンスのインターフェース(返却データあり)
    /// </summary>
    /// <typeparam name="TData">データの型</typeparam>
    public interface IAPIResponse<out TData>
    {
        APIStatus.Code Status { get; }
        
        TData Data { get; }
        
        string ErrorMessage { get; }
    }
    
    /// <summary>
    /// APIのレスポンスのインターフェース(返却データなし)
    /// </summary>
    public interface IAPIResponse
    {
        APIStatus.Code Status { get; }
        
        string ErrorMessage { get; }
    }
}