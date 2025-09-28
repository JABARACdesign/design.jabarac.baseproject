using JABARACdesign.Base.Application.Result.UpgradeAnonymousAccount;
using JABARACdesign.Base.Domain.Interface;

namespace JABARACdesign.Base.Infrastructure.API.UpgradeAnonymousAccount
{
    /// <summary>
    /// 匿名アカウントをアップグレードした際のレスポンスDTO。
    /// </summary>
    public class UpgradeAnonymousAccountResponseDto : IDomainDataDto<UpgradeAnonymousAccountResult>
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
        /// 結果クラスに変換する。
        /// </summary>
        /// <returns>結果クラス</returns>
        public UpgradeAnonymousAccountResult ToResult()
        {
            return new UpgradeAnonymousAccountResult(
                userId: UserId,
                email: Email,
                displayName: DisplayName);
        }
    }
}