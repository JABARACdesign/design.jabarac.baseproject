namespace JABARACdesign.Base.Infrastructure.Dto.API
{
    /// <summary>
    /// クエリパラメータ変換可能インターフェース。
    /// </summary>
    public interface IQueryParamConvertible
    {
        string ToQueryParams();
    }

}