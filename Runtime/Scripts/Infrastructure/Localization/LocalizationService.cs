using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Localization;
using JABARACdesign.Base.Domain.Entity.Helper;
using JABARACdesign.Base.Domain.Localization;
using VContainer;

namespace JABARACdesign.Base.Infrastructure.Localization
{
    /// <summary>
    /// ローカライズサービスの実装クラス。
    /// ILocalizationTableLoaderを通じてテーブルを読み込み、キーからテキストを解決する。
    /// </summary>
    public class LocalizationService : ILocalizationService
    {
        private readonly ILocalizationTableLoader _tableLoader;

        private Dictionary<string, string> _currentTable = new();

        private LanguageCode _currentLanguage;

        /// <summary>
        /// 現在の言語コード。
        /// </summary>
        public LanguageCode CurrentLanguage => _currentLanguage;

        /// <summary>
        /// 言語が変更された際に発火するイベント。
        /// </summary>
        public event Action<LanguageCode> OnLanguageChanged;

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="tableLoader">テーブルローダー</param>
        [Inject]
        public LocalizationService(ILocalizationTableLoader tableLoader)
        {
            _tableLoader = tableLoader;
        }

        /// <summary>
        /// 指定した言語でサービスを初期化する。
        /// </summary>
        /// <param name="languageCode">初期言語コード</param>
        public async UniTask InitializeAsync(LanguageCode languageCode)
        {
            _currentLanguage = languageCode;
            _currentTable = await _tableLoader.LoadTableAsync(languageCode: languageCode);

            LogHelper.Log(message: $"ローカライズサービスを初期化しました。言語: {languageCode}");
        }

        /// <summary>
        /// キーに対応するローカライズ済みテキストを取得する。
        /// </summary>
        /// <param name="key">ローカライズキー</param>
        /// <returns>ローカライズ済みテキスト。キーが見つからない場合はキー自体を返す</returns>
        public string GetText(string key)
        {
            if (_currentTable.TryGetValue(key: key, value: out var text))
            {
                return text;
            }

            LogHelper.Warning(message: $"ローカライズキーが見つかりません: {key}");
            return key;
        }

        /// <summary>
        /// キーに対応するローカライズ済みテキストを、名前付き引数を埋め込んで取得する。
        /// テンプレート内の #{paramName} を対応する値で置換する。
        /// </summary>
        /// <param name="key">ローカライズキー</param>
        /// <param name="args">パラメータ名と値のDictionary</param>
        /// <returns>ローカライズ済みテキスト</returns>
        public string GetText(string key, Dictionary<string, object> args)
        {
            var template = GetText(key: key);

            if (args == null || args.Count == 0)
            {
                return template;
            }

            var result = template;
            foreach (var kvp in args)
            {
                result = result.Replace(
                    oldValue: $"#{{{kvp.Key}}}",
                    newValue: kvp.Value?.ToString() ?? string.Empty);
            }

            return result;
        }

        /// <summary>
        /// 言語を変更する。
        /// </summary>
        /// <param name="languageCode">変更先の言語コード</param>
        public async void SetLanguage(LanguageCode languageCode)
        {
            if (_currentLanguage == languageCode) return;

            _currentLanguage = languageCode;
            _currentTable = await _tableLoader.LoadTableAsync(languageCode: languageCode);

            LogHelper.Log(message: $"言語を変更しました: {languageCode}");
            OnLanguageChanged?.Invoke(obj: languageCode);
        }
    }
}
