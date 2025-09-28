using JABARACdesign.Base.Application.Result.CreateAnonymousUser;
using JABARACdesign.Base.Domain.Interface;

namespace JABARACdesign.Base.Infrastructure.API.CreateAnonymousUser
{
    /// <summary>
    /// 匿名認証でユーザーを作成するAPIのレスポンスDTO
    /// </summary>
    public class CreateAnonymousUserResponseDto : IDomainDataDto<CreateAnonymousUserResult>
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
        /// 結果クラスに変換する。
        /// </summary>
        /// <returns>結果クラス</returns>
        public CreateAnonymousUserResult ToResult()
        {
            return new CreateAnonymousUserResult(
                userId: UserId,
                displayName: DisplayName);
        }
    }
}