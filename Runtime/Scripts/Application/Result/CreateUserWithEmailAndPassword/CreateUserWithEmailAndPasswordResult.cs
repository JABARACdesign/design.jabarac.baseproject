namespace JABARACdesign.Base.Application.Result.CreateUserWithEmailAndPassword
{
    /// <summary>
    /// メールアドレスとパスワードでユーザーを作成した結果
    /// </summary>
    public class CreateUserWithEmailAndPasswordResult {
        
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
        /// コンストラクタ
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="email">メールアドレス</param>
        /// <param name="displayName">表示名</param>
        public CreateUserWithEmailAndPasswordResult(
            string userId,
            string email,
            string displayName)
        {
            UserId = userId;
            Email = email;
            DisplayName = displayName;
        }
    }
}