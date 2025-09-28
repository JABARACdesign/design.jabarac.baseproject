namespace JABARACdesign.Base.Application.Result.GetIsLoggedIn
{
    /// <summary>
    /// ログインしているかどうかのレスポンス
    /// </summary>
    public class GetIsLoggedInResult
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
        
        public GetIsLoggedInResult(
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