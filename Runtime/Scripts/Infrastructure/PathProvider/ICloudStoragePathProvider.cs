using JABARACdesign.Base.Domain.Definition;

namespace JABARACdesign.Base.Infrastructure.PathProvider
{
    /// <summary>
    /// ファイルストレージのパスを提供するインターフェース。
    /// </summary>
    public interface ICloudStoragePathProvider
    {
        /// <summary>
        /// ストレージ上のファイルのパスを取得する。
        /// </summary>
        /// <param name="identifier">識別子</param>
        /// <param name="extensionType">拡張子</param>
        /// <param name="fileType">取得するファイルのタイプ</param>
        /// <returns>ストレージ上のファイルのパス</returns>
        public string GetFilePath(
            string identifier,
            StorageDefinition.ExtensionType extensionType,
            StorageDefinition.FileType fileType);
    }
}