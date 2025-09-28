using JABARACdesign.Base.Domain.Entity;

namespace JABARACdesign.Base.Application.Result.DocumentExists
{
    /// <summary>
    /// ドキュメントが存在するかどうかの結果。
    /// </summary>
    public class DocumentExistsResult : IDomainData
    {
        public bool IsExists { get; }
        
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="isExists">存在するかどうか</param>
        public DocumentExistsResult(bool isExists)
        {
            IsExists = isExists;
        }
    }
}