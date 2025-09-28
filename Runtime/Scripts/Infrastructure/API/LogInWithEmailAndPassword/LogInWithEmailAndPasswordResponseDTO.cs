using JABARACdesign.Base.Application.Result.LogInWithEmailAndPassword;
using JABARACdesign.Base.Domain.Interface;

namespace JABARACdesign.Base.Infrastructure.API.LogInWithEmailAndPassword
{
    /// <summary>
    /// メールアドレスとパスワードでログインのレスポンスDTO。
    /// </summary>
    public class LogInWithEmailAndPasswordResponseDTO : IAPIResponseDTO<LogInWithEmailAndPasswordResult>
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
        public LogInWithEmailAndPasswordResponseDTO(
            string userId,
            string email,
            string displayName)
        {
            UserId = userId;
            Email = email;
            DisplayName = displayName;
        }
        
        /// <summary>
        /// 結果クラスに変換する。
        /// </summary>
        /// <returns>結果クラス</returns>
        public LogInWithEmailAndPasswordResult ToResult()
        {
            return new LogInWithEmailAndPasswordResult(
                userId: UserId,
                email: Email,
                displayName: DisplayName);
        }
    }
}