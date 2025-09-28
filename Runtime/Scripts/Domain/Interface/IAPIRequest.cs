using JABARACdesign.Base.Domain.Definition;

namespace JABARACdesign.Base.Domain.Interface
{
    /// <summary>
    /// APIリクエストインターフェース。
    /// </summary>
    public interface IAPIRequest
    {
        string Uri { get; }

        APIDefinition.HttpMethodType MethodType { get; }
    }
    
    /// <summary>
    /// APIリクエストインターフェース(リクエストDTO付き)。
    /// </summary>
    /// <typeparam name="T">リクエストDTO</typeparam>
    public interface IAPIRequest<T> : IAPIRequest
    {
        T Dto { get; }
    }
}