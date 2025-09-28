using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Infrastructure.API.DocumentExists;

namespace JABARACdesign.Base.Infrastructure.Client
{
    /// <summary>
    /// ユーザーデータの操作クライアントインターフェース
    /// </summary>
    public interface IUserDataAPIClient
    {
        public UniTask<IAPIResponse<TData>> GetAsync<TData>(string identifier = default);
        
        public UniTask<IAPIResponse> CreateAsync<TData>(TData data, bool isSpecificId);
        
        public UniTask<IAPIResponse> UpdateAsync<TData>(TData data);
        
        public UniTask<IAPIResponse> DeleteAsync<TData>(string identifier);
        
        public UniTask<IAPIResponse<DocumentExistsDTO>> ExistsAsync<TData>(string identifier = default);
    }
}