using JABARACdesign.Base.Domain.Entity.API;
using JABARACdesign.Base.Domain.Interface;

namespace JABARACdesign.Base.Infrastructure.Dto.API
{
    /// <summary>
    /// 匿名アカウントをアップグレードした際のレスポンスDTO。
    /// </summary>
    public class UpgradeAnonymousAccountResponseDto : IDomainDataDto<UpgradeAnonymousAccountResponse>
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public string UserId { get; }
        
        /// <summary>
        /// メールアドレス
        /// </summary>
        public string Email { get; }
        
        /// <summary>
        /// 表示名
        /// </summary>
        public string DisplayName { get; }
        
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="email">メールアドレス</param>
        /// <param name="displayName">表示名</param>
        public UpgradeAnonymousAccountResponseDto(string userId, string email, string displayName)
        {
            UserId = userId;
            Email = email;
            DisplayName = displayName;
        }
        
        /// <summary>
        /// エンティティに変換する。
        /// </summary>
        /// <returns>エンティティ</returns>
        public UpgradeAnonymousAccountResponse ToEntity()
        {
            return new UpgradeAnonymousAccountResponse(
                userId: UserId,
                email: Email,
                displayName: DisplayName);
        }
    }
}