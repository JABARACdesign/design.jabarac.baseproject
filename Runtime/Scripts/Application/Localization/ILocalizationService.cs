using System;
using System.Collections.Generic;
using JABARACdesign.Base.Domain.Localization;

namespace JABARACdesign.Base.Application.Localization
{
    /// <summary>
    /// ローカライズサービスのインターフェース。
    /// UI文字列のローカライズを担う。
    /// </summary>
    public interface ILocalizationService
    {
        /// <summary>
        /// 現在の言語コード。
        /// </summary>
        LanguageCode CurrentLanguage { get; }

        /// <summary>
        /// キーに対応するローカライズ済みテキストを取得する。
        /// </summary>
        /// <param name="key">ローカライズキー</param>
        /// <returns>ローカライズ済みテキスト。キーが見つからない場合はキー自体を返す</returns>
        string GetText(string key);

        /// <summary>
        /// キーに対応するローカライズ済みテキストを、名前付き引数を埋め込んで取得する。
        /// テンプレート内の #{paramName} を対応する値で置換する。
        /// </summary>
        /// <param name="key">ローカライズキー</param>
        /// <param name="args">パラメータ名と値のDictionary</param>
        /// <returns>ローカライズ済みテキスト</returns>
        string GetText(string key, Dictionary<string, object> args);

        /// <summary>
        /// 言語を変更する。
        /// </summary>
        /// <param name="languageCode">変更先の言語コード</param>
        void SetLanguage(LanguageCode languageCode);

        /// <summary>
        /// 言語が変更された際に発火するイベント。
        /// </summary>
        event Action<LanguageCode> OnLanguageChanged;
    }
}
