using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Domain.Entity.API;
using JABARACdesign.Base.Domain.Entity.Helper;

namespace JABARACdesign.Base.Application.Manager
{
    public class ManagerBase
    {
        /// <summary>
        /// APIのレスポンスを処理する。
        /// </summary>
        /// <param name="response">APIレスポンス</param>
        /// <typeparam name="TData">レスポンスデータの型</typeparam>
        /// <returns>レスポンス</returns>
        protected async UniTask<TData> HandleAPIResponseAsync<TData>(IAPIResponse<TData> response)
        {
            switch (response.Status)
            {
                case APIStatus.Code.Success:
                    return response.Data;
                
                case APIStatus.Code.Error:
                    var message = response.ErrorMessage;
                    LogHelper.Warning(message: $"APIの実行においてエラーが発生しました。{message}");
                    await UniTask.CompletedTask;
                    return default;
                
                default:
                    return default;
            }
        }
        
        /// <summary>
        /// APIのレスポンスを処理する。
        /// </summary>
        /// <param name="response">APIレスポンス</param>
        protected async UniTask HandleAPIResponseAsync(IAPIResponse response)
        {
            switch (response.Status)
            {
                case APIStatus.Code.Success:
                    return;
                
                case APIStatus.Code.Error:
                    var message = response.ErrorMessage;
                    LogHelper.Warning(message: $"APIの実行においてエラーが発生しました。{message}");
                    await UniTask.CompletedTask;
                    return;
                
                default:
                    return;
            }
        }
    }
}