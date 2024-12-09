namespace JABARACdesign.Base.Domain.Interface
{
    public interface IDomainDataDto<out TEntity>
    {
        /// <summary>
        /// エンティティに変換する。
        /// </summary>
        /// <returns>エンティティ</returns>
        TEntity ToEntity();
    }
}