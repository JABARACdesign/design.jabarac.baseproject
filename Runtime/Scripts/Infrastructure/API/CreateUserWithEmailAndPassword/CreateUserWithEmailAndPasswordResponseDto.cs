using JABARACdesign.Base.Application.Result.CreateUserWithEmailAndPassword;
using JABARACdesign.Base.Domain.Interface;

namespace JABARACdesign.Base.Infrastructure.API.CreateUserWithEmailAndPassword
{
    /// <summary>
    /// メールアドレスとパスワードでユーザーを作成するAPIのレスポンスDTO
    /// </summary>
    public class CreateUserWithEmailAndPasswordResponseDto : IDomainDataDto<CreateUserWithEmailAndPasswordResult>
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
        /// コンストラクタ
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="email">メールアドレス</param>
        /// <param name="displayName">表示名</param>
        public CreateUserWithEmailAndPasswordResponseDto(
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
        public CreateUserWithEmailAndPasswordResult ToResult()
        {
            return new CreateUserWithEmailAndPasswordResult(
                userId: UserId,
                email: Email,
                displayName: DisplayName);
        }
    }
}