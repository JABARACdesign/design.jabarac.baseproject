using System.Threading;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Result.CreateAnonymousUser;
using JABARACdesign.Base.Application.Result.CreateUserWithEmailAndPassword;
using JABARACdesign.Base.Application.Result.EmailExists;
using JABARACdesign.Base.Application.Result.GetIsLoggedIn;
using JABARACdesign.Base.Application.Result.LogInWithEmailAndPassword;

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
        UniTask<IAPIResponse<EmailExistsResult>> GetIsEmailExistsAsync(
            string email,
            CancellationToken cancellationToken);

        /// <summary>
        /// 匿名でユーザー登録を行う。
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>レスポンス</returns>
        UniTask<IAPIResponse<CreateAnonymousUserResult>> CreateAnonymousUserAsync(
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// ユーザー登録を行う。
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <param name="password">パスワード</param>
        /// <param name="displayName">表示名</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>レスポンス</returns>
        UniTask<IAPIResponse<CreateUserWithEmailAndPasswordResult>> CreateUserWithEmailAndPasswordAsync(
            string email,
            string password,
            string displayName,
            CancellationToken cancellationToken);
        
        /// <summary>
        /// ログイン状態かどうかを判定する。
        /// </summary>
        /// <returns>レスポンス</returns>
        IAPIResponse<GetIsLoggedInResult> GetIsLoggedIn();
        
        /// <summary>
        /// メールアドレスとパスワードでログインする
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <param name="password">パスワード</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>レスポンス</returns>
        UniTask<IAPIResponse<LogInWithEmailAndPasswordResult>> LogInWithEmailAndPasswordAsync(
            string email,
            string password,
            CancellationToken cancellationToken);

        /// <summary>
        /// 匿名アカウントをアップグレードする。
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <param name="password">パスワード</param>
        /// <param name="displayName">表示名</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>APIレスポンス</returns>
        UniTask<IAPIResponse> UpgradeAnonymousAccountAsync(
            string email,
            string password,
            string displayName,
            CancellationToken cancellationToken = default);
    }
}