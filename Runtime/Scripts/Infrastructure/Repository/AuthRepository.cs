using System.Threading;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Domain.Entity.API;
using JABARACdesign.Base.Infrastructure.Dto.API;
using JABARACdesign.Base.Infrastructure.Extension;
using JABARACdesign.Base.Infrastructure.Network.Client;
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
        public async UniTask<IAPIResponse<EmailExistsResponse>> GetIsEmailExistsAsync(
            string email,
            CancellationToken cancellationToken = default)
        {
            var response = await _authenticationClient.GetIsEmailExistsAsync(
                email: email,
                cancellationToken: cancellationToken);
            
            return response;
        }
        
        /// <summary>
        /// ユーザー登録を行う
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <param name="password">パスワード</param>
        /// <param name="displayName">表示名</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>レスポンス</returns>
        public async UniTask<IAPIResponse<CreateUserWithEmailAndPasswordResponse>>
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
                .ToEntityResponse<CreateUserWithEmailAndPasswordResponseDto, CreateUserWithEmailAndPasswordResponse>();
        }
        
        /// <summary>
        /// ログイン状態かどうかを判定する。
        /// </summary>
        public IAPIResponse<GetIsLoggedIn> GetIsLoggedIn()
        {
            var response = _authenticationClient.GetIsLoggedIn();
            
            return response.ToEntityResponse<GetIsLoggedInDto, GetIsLoggedIn>();
        }
        
        /// <summary>
        /// メールアドレスとパスワードでログインする
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <param name="password">パスワード</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>レスポンス</returns>
        public async UniTask<IAPIResponse<LogInWithEmailAndPasswordResponse>> LogInWithEmailAndPasswordAsync(
            string email,
            string password,
            CancellationToken cancellationToken = default)
        {
            var response = await _authenticationClient.SignInWithEmailAndPasswordAsync(
                email: email,
                password: password,
                cancellationToken: cancellationToken);
            
            return response.ToEntityResponse<LogInWithEmailAndPasswordResponseDto, LogInWithEmailAndPasswordResponse>();
        }
    }
}