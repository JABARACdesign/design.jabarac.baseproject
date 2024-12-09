using System.Threading;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Domain.Entity.API;
using JABARACdesign.Base.Infrastructure.Dto.API;

namespace JABARACdesign.Base.Infrastructure.Network.Client
{
    /// <summary>
    /// 認証クライアントのインターフェース
    /// </summary>
    public interface IAuthenticationClient
    {
        /// <summary>
        /// メールアドレスが存在するかどうかを取得する
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>レスポンス</returns>
        public UniTask<IAPIResponse<EmailExistsResponse>> GetIsEmailExistsAsync(
            string email,
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// ユーザー登録を行う
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <param name="password">パスワード</param>
        /// <param name="displayName">表示名</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>レスポンス</returns>
        public UniTask<IAPIResponse<CreateUserWithEmailAndPasswordResponseDto>> CreateUserWithEmailAndPasswordAsync(
            string email,
            string password,
            string displayName,
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// ログイン状態かどうかを判定する
        /// </summary>
        /// <returns>ログイン状態の場合はtrue、それ以外の場合はfalse</returns>
        public IAPIResponse<GetIsLoggedInDto> GetIsLoggedIn();
        
        /// <summary>
        /// メールアドレスとパスワードでログインする
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <param name="password">パスワード</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>レスポンス</returns>
        public UniTask<IAPIResponse<LogInWithEmailAndPasswordResponseDto>> SignInWithEmailAndPasswordAsync(
            string email,
            string password,
            CancellationToken cancellationToken = default);
    }
}