namespace JABARACdesign.Base.Application.Manager
{
    /// <summary>
    /// 認証マネージャインターフェース。
    /// </summary>
    public interface IAuthManager
    {
        string UserId { get; }
        
        string UserName { get; }
        
        bool GetIsLoggedIn();
    }
}