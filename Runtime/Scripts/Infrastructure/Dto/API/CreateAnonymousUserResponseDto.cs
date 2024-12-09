using JABARACdesign.Base.Domain.Entity.API;
using JABARACdesign.Base.Domain.Interface;

namespace JABARACdesign.Base.Infrastructure.Dto.API
{
    /// <summary>
    /// 匿名認証でユーザーを作成するAPIのレスポンスDTO
    /// </summary>
    public class CreateAnonymousUserResponseDto : IDomainDataDto<CreateAnonymousUserResponse>
    {
        public string UserId { get; }
        
        public string DisplayName { get; }
        
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="displayName">表示名</param>
        public CreateAnonymousUserResponseDto(
            string userId,
            string displayName)
        {
            UserId = userId;
            DisplayName = displayName;
        }
        
        /// <summary>
        /// エンティティに変換する。
        /// </summary>
        /// <returns>エンティティ</returns>
        public CreateAnonymousUserResponse ToEntity()
        {
            return new CreateAnonymousUserResponse(
                userId: UserId,
                displayName: DisplayName);
        }
    }
}