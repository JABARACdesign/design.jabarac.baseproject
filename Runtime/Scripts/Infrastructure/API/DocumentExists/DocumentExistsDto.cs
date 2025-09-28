using JABARACdesign.Base.Application.Result.DocumentExists;
using JABARACdesign.Base.Domain.Interface;

namespace JABARACdesign.Base.Infrastructure.API.DocumentExists
{
    /// <summary>
    /// ドキュメントが存在するかどうかを確認するためのDTO。
    /// </summary>
    public class DocumentExistsDto : IDomainDataDto<DocumentExistsResult>
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
        /// 結果クラスに変換する
        /// </summary>
        /// <returns>結果クラス</returns>
        public DocumentExistsResult ToResult()
        {
            return new DocumentExistsResult(
                isExists: IsExists);
        }
    }
}