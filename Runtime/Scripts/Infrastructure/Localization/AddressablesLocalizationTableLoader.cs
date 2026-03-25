using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Localization;
using JABARACdesign.Base.Domain.Entity.Helper;
using JABARACdesign.Base.Domain.Localization;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace JABARACdesign.Base.Infrastructure.Localization
{
    /// <summary>
    /// Addressablesを用いたJSONベースのローカライズテーブルローダー。
    ///
    /// JSON形式:
    /// {
    ///   "id": "common",
    ///   "entries": {
    ///     "VOICE_TRAINING": {
    ///       "ja": "ボイトレ",
    ///       "en": "Voice Training"
    ///     }
    ///   }
    /// }
    ///
    /// Addressablesのラベル規約: "localization_table" (全言語を含む単一テーブル)
    /// </summary>
    public class AddressablesLocalizationTableLoader : ILocalizationTableLoader
    {
        private const string ADDRESSABLES_LABEL = "localization_table";

        /// <summary>
        /// 指定した言語のローカライズテーブルを読み込む。
        /// JSONファイルから該当言語のエントリのみを抽出して返す。
        /// </summary>
        /// <param name="languageCode">言語コード</param>
        /// <returns>キーと翻訳テキストのDictionary</returns>
        public async UniTask<Dictionary<string, string>> LoadTableAsync(LanguageCode languageCode)
        {
            var locale = languageCode.ToString().ToLowerInvariant();

            try
            {
                var textAsset = await Addressables.LoadAssetAsync<TextAsset>(key: ADDRESSABLES_LABEL)
                    .ToUniTask();

                if (textAsset == null)
                {
                    LogHelper.Error(message: $"ローカライズテーブルが見つかりません: {ADDRESSABLES_LABEL}");
                    return new Dictionary<string, string>();
                }

                var localizationTable = JsonConvert.DeserializeObject<LocalizationTable>(
                    value: textAsset.text);

                if (localizationTable?.Entries == null)
                {
                    LogHelper.Error(message: "ローカライズテーブルのentriesが空です。");
                    return new Dictionary<string, string>();
                }

                var table = new Dictionary<string, string>();
                foreach (var entry in localizationTable.Entries)
                {
                    if (entry.Value.TryGetValue(key: locale, value: out var text))
                    {
                        table[key: entry.Key] = text;
                    }
                }

                LogHelper.Log(
                    message: $"ローカライズテーブルを読み込みました: {locale} ({table.Count} keys)");

                return table;
            }
            catch (System.Exception e)
            {
                LogHelper.Error(
                    message: $"ローカライズテーブルの読み込みに失敗しました: {locale}, {e.Message}");
                return new Dictionary<string, string>();
            }
        }
    }
}
