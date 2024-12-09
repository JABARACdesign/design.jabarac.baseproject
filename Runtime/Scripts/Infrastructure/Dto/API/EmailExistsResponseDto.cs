using JABARACdesign.Base.Domain.Entity.API;
using JABARACdesign.Base.Domain.Interface;

namespace JABARACdesign.Base.Infrastructure.Dto.API
{
    /// <summary>
    /// Emailの存在確認のレスポンスDTO
    /// </summary>
    public class EmailExistsResponseDto : IDomainDataDto<EmailExistsResponse>
    {
        public bool IsExists { get; private set; }
        
        public EmailExistsResponseDto(bool isExists)
        {
            IsExists = isExists;
        }
        
        /// <summary>
        /// エンティティに変換する。
        /// </summary>
        /// <returns>エンティティ</returns>
        public EmailExistsResponse ToEntity()
        {
            return new EmailExistsResponse(isExists: IsExists);
        }
    }
}