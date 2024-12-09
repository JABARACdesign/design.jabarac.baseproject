namespace JABARACdesign.Base.Domain.Entity.API
{
    /// <summary>
    /// ログインしているかどうかのレスポンス
    /// </summary>
    public class GetIsLoggedIn : IDomainData
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
        
        public GetIsLoggedIn(
            bool isLoggedIn,
            string userId,
            string displayName)
        {
            IsLoggedIn = isLoggedIn;
            UserId = userId;
            DisplayName = displayName;
        }
    }
}