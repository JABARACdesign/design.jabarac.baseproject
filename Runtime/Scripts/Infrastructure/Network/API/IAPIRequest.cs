using JABARACdesign.Base.Infrastructure.Helper;

namespace JABARACdesign.Base.Infrastructure.Network.API
{
    /// <summary>
    /// APIリクエストインターフェース。
    /// </summary>
    public interface IAPIRequest
    {
        string Uri { get; }

        APIHelper.HttpMethod Method { get; }
    }
    
    /// <summary>
    /// APIリクエストインターフェース(リクエストDTO付き)。
    /// </summary>
    /// <typeparam name="T">リクエストDTO</typeparam>
    public interface IApiRequest<T> : IAPIRequest
    {
        T Dto { get; }
    }
}