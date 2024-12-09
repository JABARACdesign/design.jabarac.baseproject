using Cysharp.Threading.Tasks;

namespace JABARACdesign.Base.Application.Manager
{
    /// <summary>
    /// マスターデータの管理インターフェース。
    /// </summary>
    public interface IMstDataManager : IDomainDataManagerBase
    {
        UniTask SetUpAsync();
    }
}