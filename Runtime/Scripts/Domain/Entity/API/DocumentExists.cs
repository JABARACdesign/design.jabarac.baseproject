namespace JABARACdesign.Base.Domain.Entity.API
{
    /// <summary>
    /// ドキュメントが存在するかどうかのエンティティ
    /// </summary>
    public class DocumentExists : IDomainData
    {
        public bool IsExists { get; }
        
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="isExists">存在するかどうか</param>
        public DocumentExists(bool isExists)
        {
            IsExists = isExists;
        }
    }
}