
namespace JABARACdesign.Base.Application.Result.CreateAnonymousUser
{
    /// <summary>
    /// 匿名認証でユーザーを作成した結果。
    /// </summary>
    public class CreateAnonymousUserResult
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
        public CreateAnonymousUserResult(
            string userId,
            string displayName)
        {
            UserId = userId;
            DisplayName = displayName;
        }
    }
}