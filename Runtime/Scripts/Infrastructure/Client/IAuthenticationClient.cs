using System.Threading;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Application.Result.EmailExists;
using JABARACdesign.Base.Infrastructure.API.CreateAnonymousUser;
using JABARACdesign.Base.Infrastructure.API.CreateUserWithEmailAndPassword;
using JABARACdesign.Base.Infrastructure.API.GetIsLoggedIn;
using JABARACdesign.Base.Infrastructure.API.LogInWithEmailAndPassword;
using JABARACdesign.Base.Infrastructure.API.UpgradeAnonymousAccount;

namespace JABARACdesign.Base.Infrastructure.Client
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
        UniTask<IAPIResponse<EmailExistsResult>> GetIsEmailExistsAsync(
            string email,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 匿名でユーザー登録を行う。
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>レスポンス</returns>
        UniTask<IAPIResponse<CreateAnonymousUserResponseDTO>>
            CreateAnonymousUserAsync(CancellationToken cancellationToken = default);
        
        /// <summary>
        /// ユーザー登録を行う。
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <param name="password">パスワード</param>
        /// <param name="displayName">表示名</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>レスポンス</returns>
        UniTask<IAPIResponse<CreateUserWithEmailAndPasswordResponseDTO>> CreateUserWithEmailAndPasswordAsync(
            string email,
            string password,
            string displayName,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 匿名アカウントをメールアドレス認証にアップグレードする。
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <param name="password">パスワード</param>
        /// <param name="displayName">表示名</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>APIレスポンス</returns>
        UniTask<IAPIResponse<UpgradeAnonymousAccountResponseDTO>> UpgradeAnonymousAccountAsync(
            string email,
            string password,
            string displayName,
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// ログイン状態かどうかを判定する。
        /// </summary>
        /// <returns>ログイン状態の場合はtrue、それ以外の場合はfalse</returns>
        IAPIResponse<GetIsLoggedInDTO> GetIsLoggedIn();
        
        /// <summary>
        /// メールアドレスとパスワードでログインする。
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <param name="password">パスワード</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>レスポンス</returns>
        UniTask<IAPIResponse<LogInWithEmailAndPasswordResponseDTO>> SignInWithEmailAndPasswordAsync(
            string email,
            string password,
            CancellationToken cancellationToken = default);
    }
}