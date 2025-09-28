namespace JABARACdesign.Base.Domain.Definition
{
    /// <summary>
    /// API関連の定義クラス。
    /// </summary>
    public static class APIDefinition
    {
        /// <summary>HTTPメソッド</summary>
        public enum HttpMethodType
        {
            GET,

            POST,

            PUT,

            DELETE,

            NONE,
        }
        
        /// <summary>
        /// APIのステータスコード
        /// </summary>
        public enum Code
        {
            /// <summary>成功</summary>
            Success,
            
            /// <summary>エラー</summary>
            Error,
            
            /// <summary>メンテナンス中</summary>
            Maintenance
        }
    }
}