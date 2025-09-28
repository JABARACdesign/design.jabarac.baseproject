using JABARACdesign.Base.Application.Result.EmailExists;
using JABARACdesign.Base.Domain.Interface;

namespace JABARACdesign.Base.Infrastructure.API.EmailExists
{
    /// <summary>
    /// Emailの存在確認のレスポンスDTO
    /// </summary>
    public class EmailExistsResponseDto : IDomainDataDto<EmailExistsResult>
    {
        public bool IsExists { get; private set; }
        
        public EmailExistsResponseDto(bool isExists)
        {
            IsExists = isExists;
        }
        
        /// <summary>
        /// 結果クラスに変換する。
        /// </summary>
        /// <returns>結果クラス</returns>
        public EmailExistsResult ToResult()
        {
            return new EmailExistsResult(isExists: IsExists);
        }
    }
}