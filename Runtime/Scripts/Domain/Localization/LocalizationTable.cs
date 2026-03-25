using System.Collections.Generic;
using Newtonsoft.Json;

namespace JABARACdesign.Base.Domain.Localization
{
    /// <summary>
    /// ローカライズテーブルのJSONモデル。
    /// </summary>
    public class LocalizationTable
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("defaultLocale")]
        public string DefaultLocale { get; set; }

        [JsonProperty("supportedLocales")]
        public List<string> SupportedLocales { get; set; }

        [JsonProperty("entries")]
        public Dictionary<string, Dictionary<string, string>> Entries { get; set; }

        [JsonProperty("entryParams")]
        public Dictionary<string, List<EntryParam>> EntryParams { get; set; }
    }

    /// <summary>
    /// エントリパラメータの定義。
    /// </summary>
    public class EntryParam
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
