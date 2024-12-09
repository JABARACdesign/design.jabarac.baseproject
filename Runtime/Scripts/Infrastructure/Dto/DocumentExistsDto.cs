using JABARACdesign.Base.Domain.Entity.API;
using JABARACdesign.Base.Domain.Interface;

namespace JABARACdesign.Base.Infrastructure.Dto
{
    /// <summary>
    /// ドキュメントが存在するかどうかを確認するためのDTO。
    /// </summary>
    public class DocumentExistsDto : IDomainDataDto<DocumentExists>
    {
        public bool IsExists { get; set; }
        
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="isExists">存在するかどうか</param>
        public DocumentExistsDto(bool isExists)
        {
            IsExists = isExists;
        }
        
        /// <summary>
        /// エンティティに変換する
        /// </summary>
        /// <returns>エンティティ</returns>
        public DocumentExists ToEntity()
        {
            return new DocumentExists(
                isExists: IsExists);
        }
    }
}