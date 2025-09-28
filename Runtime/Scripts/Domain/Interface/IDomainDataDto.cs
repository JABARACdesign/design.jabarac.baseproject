namespace JABARACdesign.Base.Domain.Interface
{
    public interface IDomainDataDto<out TEntity>
    {
        /// <summary>
        /// 結果クラスに変換する。
        /// </summary>
        /// <returns>結果クラス</returns>
        TEntity ToResult();
    }
}