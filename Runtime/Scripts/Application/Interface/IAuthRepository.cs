using System.Threading;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Domain.Entity.API;

namespace JABARACdesign.Base.Application.Interface
{
    /// <summary>
    /// 認証リポジトリインターフェース。
    /// </summary>
    public interface IAuthRepository
    {
        /// <summary>
        /// メールアドレスが存在するかどうかを取得する。
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>レスポンス</returns>
        UniTask<IAPIResponse<EmailExistsResponse>> GetIsEmailExistsAsync(
            string email,
            CancellationToken cancellationToken);

        /// <summary>
        /// 匿名でユーザー登録を行う。
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>レスポンス</returns>
        UniTask<IAPIResponse<CreateAnonymousUserResponse>> CreateAnonymousUserAsync(
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// ユーザー登録を行う。
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <param name="password">パスワード</param>
        /// <param name="displayName">表示名</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>レスポンス</returns>
        UniTask<IAPIResponse<CreateUserWithEmailAndPasswordResponse>> CreateUserWithEmailAndPasswordAsync(
            string email,
            string password,
            string displayName,
            CancellationToken cancellationToken);
        
        /// <summary>
        /// ログイン状態かどうかを判定する。
        /// </summary>
        /// <returns>レスポンス</returns>
        IAPIResponse<GetIsLoggedIn> GetIsLoggedIn();
        
        /// <summary>
        /// メールアドレスとパスワードでログインする
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <param name="password">パスワード</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>レスポンス</returns>
        UniTask<IAPIResponse<LogInWithEmailAndPasswordResponse>> LogInWithEmailAndPasswordAsync(
            string email,
            string password,
            CancellationToken cancellationToken);
    }
}