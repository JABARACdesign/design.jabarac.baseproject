namespace JABARACdesign.Base.Domain.Interface
{
    /// <summary>
    /// クエリパラメータ変換可能インターフェース。
    /// </summary>
    public interface IQueryParamConvertible
    {
        string ToQueryParams();
    }

}