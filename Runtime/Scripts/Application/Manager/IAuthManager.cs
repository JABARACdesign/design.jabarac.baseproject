using System.Threading;
using Cysharp.Threading.Tasks;

namespace JABARACdesign.Base.Application.Manager
{
    /// <summary>
    /// 認証マネージャインターフェース。
    /// </summary>
    public interface IAuthManager
    {
        string UserId { get; }
        
        string UserName { get; }

        UniTask<bool> CreateAnonymousAccountAsync(CancellationToken cancellationToken = default);
        
        UniTask<bool> UpgradeAnonymousAccountAsync(
            string email,
            string password,
            string displayName,
            CancellationToken cancellationToken = default);
        
        bool GetIsLoggedIn();
    }
}