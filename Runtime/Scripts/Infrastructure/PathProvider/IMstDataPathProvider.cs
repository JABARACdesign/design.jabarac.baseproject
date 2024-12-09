using Firebase.Database;

namespace JABARACdesign.Base.Infrastructure.PathProvider
{
    /// <summary>
    /// マスターデータのパスを提供するインターフェース
    /// </summary>
    public interface IMstDataPathProvider
    {
        public string GetPath<TDomain>();
        
        public DatabaseReference GetPath<TDomain>(DatabaseReference rootReference);
    }
}