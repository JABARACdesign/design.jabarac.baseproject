using System;

namespace JABARACdesign.Base.Application.Interface
{
    public interface IPathProvider
    {
        /// <summary>
        /// 指定した識別子に対応するファイルのフルパスを取得する。
        /// </summary>
        /// <param name="identifier">識別子(プロジェクトで定義したEnum)</param>
        /// <typeparam name="TEnum">Enum</typeparam>
        /// <returns>指定した識別子に対応するファイルのフルパス</returns>
        public string GetFilePath<TEnum>(TEnum identifier)
            where TEnum : struct, Enum;
    }
}