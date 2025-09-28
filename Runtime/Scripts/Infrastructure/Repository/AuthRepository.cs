using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Application.Result.CreateAnonymousUser;
using JABARACdesign.Base.Application.Result.CreateUserWithEmailAndPassword;
using JABARACdesign.Base.Application.Result.EmailExists;
using JABARACdesign.Base.Application.Result.GetIsLoggedIn;
using JABARACdesign.Base.Application.Result.LogInWithEmailAndPassword;
using JABARACdesign.Base.Domain.Definition;
using JABARACdesign.Base.Domain.Entity.Helper;
using JABARACdesign.Base.Infrastructure.API;
using JABARACdesign.Base.Infrastructure.API.CreateAnonymousUser;
using JABARACdesign.Base.Infrastructure.API.CreateUserWithEmailAndPassword;
using JABARACdesign.Base.Infrastructure.API.GetIsLoggedIn;
using JABARACdesign.Base.Infrastructure.API.LogInWithEmailAndPassword;
using JABARACdesign.Base.Infrastructure.Client;
using JABARACdesign.Base.Infrastructure.Extension;
using VContainer;

namespace JABARACdesign.Base.Infrastructure.Repository
{
    /// <summary>
    /// 認証リポジトリクラス。
    /// </summary>
    public class AuthRepository : IAuthRepository
    {
        private readonly IAuthenticationClient _authenticationClient;
        
        [Inject]
        public AuthRepository(
            IAuthenticationClient authenticationClient
        )
        {
            _authenticationClient = authenticationClient;
        }
        
        /// <summary>
        /// メールアドレスが存在するかどうかを取得する。
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>レスポンス</returns>
        public async UniTask<IAPIResponse<EmailExistsResult>> GetIsEmailExistsAsync(
            string email,
            CancellationToken cancellationToken = default)
        {
            var response = await _authenticationClient.GetIsEmailExistsAsync(
                email: email,
                cancellationToken: cancellationToken);
            
            return response;
        }
        
        /// <summary>
        /// 匿名でユーザー登録を行う。
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>レスポンス</returns>
        public async UniTask<IAPIResponse<CreateAnonymousUserResult>> CreateAnonymousUserAsync(
            CancellationToken cancellationToken = default)
        {
            var response = await _authenticationClient.CreateAnonymousUserAsync(
                cancellationToken: cancellationToken);
            
            return response
                .ToResult<CreateAnonymousUserResponseDTO, CreateAnonymousUserResult>();
        }
        
        /// <summary>
        /// ユーザー登録を行う。
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <param name="password">パスワード</param>
        /// <param name="displayName">表示名</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>レスポンス</returns>
        public async UniTask<IAPIResponse<CreateUserWithEmailAndPasswordResult>>
            CreateUserWithEmailAndPasswordAsync(
                string email,
                string password,
                string displayName,
                CancellationToken cancellationToken = default)
        {
            var response = await _authenticationClient.CreateUserWithEmailAndPasswordAsync(
                email: email,
                password: password,
                displayName: displayName,
                cancellationToken: cancellationToken);
            
            return response
                .ToResult<CreateUserWithEmailAndPasswordResponseDTO, CreateUserWithEmailAndPasswordResult>();
        }
        
        /// <summary>
        /// ログイン状態かどうかを判定する。
        /// </summary>
        public IAPIResponse<GetIsLoggedInResult> GetIsLoggedIn()
        {
            var response = _authenticationClient.GetIsLoggedIn();
            
            return response.ToResult<GetIsLoggedInDTO, GetIsLoggedInResult>();
        }
        
        /// <summary>
        /// メールアドレスとパスワードでログインする。
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <param name="password">パスワード</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>レスポンス</returns>
        public async UniTask<IAPIResponse<LogInWithEmailAndPasswordResult>> LogInWithEmailAndPasswordAsync(
            string email,
            string password,
            CancellationToken cancellationToken = default)
        {
            var response = await _authenticationClient.SignInWithEmailAndPasswordAsync(
                email: email,
                password: password,
                cancellationToken: cancellationToken);
            
            return response.ToResult<LogInWithEmailAndPasswordResponseDTO, LogInWithEmailAndPasswordResult>();
        }
        
        /// <summary>
        /// 匿名アカウントをアップグレードする。
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <param name="password">パスワード</param>
        /// <param name="displayName">表示名</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>APIレスポンス</returns>
        public async UniTask<IAPIResponse> UpgradeAnonymousAccountAsync(
            string email,
            string password,
            string displayName,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _authenticationClient.UpgradeAnonymousAccountAsync(
                    email, password, displayName, cancellationToken);
                
                if (response.Status == APIDefinition.Code.Success)
                {
                    return new APIResponse(
                        status: APIDefinition.Code.Success);
                }
                else
                {
                    return new APIResponse(
                        status: APIDefinition.Code.Error,
                        errorMessage: response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"アカウントアップグレード中に例外が発生しました: {ex.Message}";
                LogHelper.Error(errorMessage);
                return new APIResponse(
                    status: APIDefinition.Code.Error,
                    errorMessage: errorMessage);
            }
        }
    }
}