namespace JABARACdesign.Base.Domain.Entity.API
{
    /// <summary>
    /// APIのステータス
    /// </summary>
    public static class APIStatus
    {
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