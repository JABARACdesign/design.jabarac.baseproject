using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Domain.Localization;

namespace JABARACdesign.Base.Application.Localization
{
    /// <summary>
    /// ローカライズテーブルのローダーインターフェース。
    /// JSONファイルやAddressablesなど、読み込み元を差し替え可能にする。
    /// </summary>
    public interface ILocalizationTableLoader
    {
        /// <summary>
        /// 指定した言語のローカライズテーブルを読み込む。
        /// </summary>
        /// <param name="languageCode">言語コード</param>
        /// <returns>キーと翻訳テキストのDictionary</returns>
        UniTask<Dictionary<string, string>> LoadTableAsync(LanguageCode languageCode);
    }
}
