using JABARACdesign.Base.Domain.Entity.API;
using JABARACdesign.Base.Domain.Interface;

namespace JABARACdesign.Base.Infrastructure.Dto.API
{
    /// <summary>
    /// メールアドレスとパスワードでユーザーを作成するAPIのレスポンスDTO
    /// </summary>
    public class CreateUserWithEmailAndPasswordResponseDto : IDomainDataDto<CreateUserWithEmailAndPasswordResponse>
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
        /// エンティティに変換する。
        /// </summary>
        /// <returns>エンティティ</returns>
        public CreateUserWithEmailAndPasswordResponse ToEntity()
        {
            return new CreateUserWithEmailAndPasswordResponse(
                userId: UserId,
                email: Email,
                displayName: DisplayName);
        }
    }
}