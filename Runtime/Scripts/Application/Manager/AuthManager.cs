using System.Threading;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Domain.Definition;
using JABARACdesign.Base.Domain.Entity.Helper;
using VContainer;

namespace JABARACdesign.Base.Application.Manager
{
    /// <summary>
    /// 認証マネージャクラス。
    /// </summary>
    public class AuthManager : IAuthManager
    {
        private IAuthRepository _authRepository;
        
        public string UserId { get; private set; }
        
        public string UserName { get; private set; }
        
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="authRepository">認証リポジトリ</param>
        [Inject]
        public AuthManager(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        
        /// <summary>
        /// 匿名アカウントを作成する。
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>成功したかどうか</returns>
        public async UniTask<bool> CreateAnonymousAccountAsync(CancellationToken cancellationToken = default)
        {
            // 既にログインしているかどうかを確認
            if (GetIsLoggedIn())
            {
                LogHelper.Debug(message: $"既にログインしています。ユーザーID: {UserId}");
                return true;
            }

            // 匿名アカウントを作成
            var response = await _authRepository.CreateAnonymousUserAsync(cancellationToken: cancellationToken);
            
            if (response.Status == APIDefinition.Code.Success)
            {
                UserId = response.Data.UserId;
                UserName = response.Data.DisplayName;
                LogHelper.Debug(message: $"匿名アカウントを作成しました。ユーザーID: {UserId}");
                return true;
            }
            else
            {
                LogHelper.Error(message: $"匿名アカウントの作成に失敗しました: {response.ErrorMessage}");
                return false;
            }
        }
        
        /// <summary>
        /// 匿名アカウントをメールアドレスとパスワードを持つアカウントにアップグレードする。
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <param name="password">パスワード</param>
        /// <param name="displayName">表示名</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>成功したかどうか</returns>
        public async UniTask<bool> UpgradeAnonymousAccountAsync(
            string email, 
            string password, 
            string displayName, 
            CancellationToken cancellationToken = default)
        {
            // 入力値の検証
            if (string.IsNullOrEmpty(value: email) || string.IsNullOrEmpty(value: password))
            {
                LogHelper.Error(message: "メールアドレスとパスワードは必須です");
                return false;
            }

            // メールアドレスがすでに存在するか確認
            var emailExistsResponse = await _authRepository.GetIsEmailExistsAsync(email: email, cancellationToken: cancellationToken);
            if (emailExistsResponse.Status == APIDefinition.Code.Success && emailExistsResponse.Data.IsExists)
            {
                LogHelper.Error(message: "このメールアドレスは既に使用されています");
                return false;
            }

            // 匿名アカウントをアップグレード
            var response = await _authRepository.UpgradeAnonymousAccountAsync(
                email: email, password: password, displayName: displayName, cancellationToken: cancellationToken);
            
            if (response.Status == APIDefinition.Code.Success)
            {
                LogHelper.Info(message: $"アカウントを正常にアップグレードしました: {email}");
                return true;
            }
            else
            {
                LogHelper.Error(message: $"アカウントのアップグレードに失敗しました: {response.ErrorMessage}");
                return false;
            }
        }
        
        /// <summary>
        /// ログインしているかどうかを取得する。
        /// </summary>
        /// <returns>ログインしているかどうか</returns>
        public bool GetIsLoggedIn()
        {
            var result = _authRepository.GetIsLoggedIn();
            if (result.Status != APIDefinition.Code.Success)
            {
                LogHelper.Warning(message: $"ログイン状態を取得できませんでした : {result.Status}");
                return false;
            }
            
            if (result.Data.IsLoggedIn)
            {
                UserId = result.Data.UserId;
                UserName = result.Data.DisplayName;
                
                LogHelper.Info(message: $"ログインしています : {UserId}");
            }
            
            return result.Data.IsLoggedIn;
        }
    }
}