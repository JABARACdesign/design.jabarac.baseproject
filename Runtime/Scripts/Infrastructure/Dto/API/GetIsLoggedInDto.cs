using JABARACdesign.Base.Domain.Entity.API;
using JABARACdesign.Base.Domain.Interface;

namespace JABARACdesign.Base.Infrastructure.Dto.API
{
    /// <summary>
    /// ログインしているかどうかのレスポンスDTO
    /// </summary>
    public class GetIsLoggedInDto : IDomainDataDto<GetIsLoggedIn>
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
        /// エンティティに変換する。
        /// </summary>
        /// <returns>エンティティ</returns>
        public GetIsLoggedIn ToEntity()
        {
            return new GetIsLoggedIn(
                isLoggedIn: IsLoggedIn,
                userId: UserId,
                displayName: DisplayName);
        }
    }
}