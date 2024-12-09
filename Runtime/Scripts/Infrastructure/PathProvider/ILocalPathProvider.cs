using System;
using JABARACdesign.Base.Domain.Definition;

namespace JABARACdesign.Base.Infrastructure.PathProvider
{
    public interface ILocalPathProvider
    {
        /// <summary>
        /// 指定した型に対応するパスを取得する。
        /// </summary>
        /// <typeparam name="TDto">対象の型</typeparam>
        /// <param name="userId">ユーザーID</param>
        /// <returns>パス</returns>
        /// <exception cref="ArgumentException">型が未サポートのエラー</exception>
        public string GetPath<TDto>(string userId);
        
        /// <summary>
        /// 指定した型に対応するキーを取得する。
        /// </summary>
        /// <typeparam name="TDto">対象の型</typeparam>
        /// <returns>指定した型に対応するキー</returns>
        /// <exception cref="ArgumentException">型が未サポートのエラー</exception>
        public string GetKey<TDto>();
        
        /// <summary>
        /// 指定したファイル情報に対応するファイルのフルパスを取得する。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="identifier">識別子</param>
        /// <param name="extensionType">拡張子</param>
        /// <param name="fileType">取得するファイルのタイプ</param>
        /// <returns>ローカル上のファイルのパス</returns>
        public string GetFilePath(
            string userId,
            string identifier,
            StorageDefinition.ExtensionType extensionType,
            StorageDefinition.FileType fileType);
    }
}