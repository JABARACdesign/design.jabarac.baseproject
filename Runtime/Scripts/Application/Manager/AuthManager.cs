using JABARACdesign.Base.Application.Interface;
using JABARACdesign.Base.Domain.Entity.API;
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
        /// ログインしているかどうかを取得する。
        /// </summary>
        /// <returns>ログインしているかどうか</returns>
        public bool GetIsLoggedIn()
        {
            var result = _authRepository.GetIsLoggedIn();
            if (result.Status != APIStatus.Code.Success)
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