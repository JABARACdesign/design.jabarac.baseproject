namespace JABARACdesign.Base.Domain.Entity.API
{
    /// <summary>
    /// 匿名認証でユーザーを作成するAPIのレスポンス。
    /// </summary>
    public class CreateAnonymousUserResponse　: IDomainData
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public string UserId { get; }
        
        /// <summary>
        /// 表示名
        /// </summary>
        public string DisplayName { get; }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="displayName">表示名</param>
        public CreateAnonymousUserResponse(
            string userId,
            string displayName)
        {
            UserId = userId;
            DisplayName = displayName;
        }
    }
}