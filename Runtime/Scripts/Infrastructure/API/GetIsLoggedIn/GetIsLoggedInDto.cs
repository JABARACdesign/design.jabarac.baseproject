using JABARACdesign.Base.Application.Result.GetIsLoggedIn;
using JABARACdesign.Base.Domain.Interface;

namespace JABARACdesign.Base.Infrastructure.API.GetIsLoggedIn
{
    /// <summary>
    /// ログインしているかどうかのレスポンスDTO
    /// </summary>
    public class GetIsLoggedInDto : IDomainDataDto<GetIsLoggedInResult>
    {
        /// <summary>
        /// ログインしているかどうか
        /// </summary>
        public bool IsLoggedIn { get; }
        
        /// <summary>
        /// ユーザーID
        /// </summary>
        public string UserId { get; }
        
        /// <summary>
        /// 表示名
        /// </summary>
        public string DisplayName { get; }
        
        public GetIsLoggedInDto(
            bool isLoggedIn,
            string userId,
            string displayName)
        {
            IsLoggedIn = isLoggedIn;
            UserId = userId;
            DisplayName = displayName;
        }
        
        /// <summary>
        /// 結果クラスに変換する。
        /// </summary>
        /// <returns>結果クラス</returns>
        public GetIsLoggedInResult ToResult()
        {
            return new GetIsLoggedInResult(
                isLoggedIn: IsLoggedIn,
                userId: UserId,
                displayName: DisplayName);
        }
    }
}