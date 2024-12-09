namespace JABARACdesign.Base.Domain.Entity.Extension
{
    /// <summary>
    /// string型の拡張メソッドを管理するクラス。
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 文字列をローカライズされた文字列に変換します。
        /// </summary>
        /// <param name="key">この文字列のローカライズキー。</param>
        /// <returns>ローカライズされた文字列(UniTask)。</returns>
        public static string Localize(this string key)
        {
            return key;
            
            // TODO: ローカライゼーションファイルを作成する
            // return LocalizationUtil.GetLocalizedString(key: key);
        }
    }
}