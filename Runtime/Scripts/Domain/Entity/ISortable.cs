namespace JABARACdesign.Base.Domain.Entity
{
    /// <summary>
    /// ソート可能なインターフェース。
    /// </summary>
    public interface ISortable
    {
        int SortOrder { get; }
    }
}