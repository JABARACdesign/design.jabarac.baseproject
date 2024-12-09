using System.Text;

namespace JABARACdesign.Base.Domain.Entity.Helper
{
    /// <summary>
    /// ログ出力を行うためのヘルパークラス。
    /// </summary>
    public static class LogHelper
    {
        public enum LogLevel
        {
            Debug = 0,
            Info = 1,
            Warning = 2,
            Error = 3,
            None = 999 // ログ出力を無効化するためのレベル
        }
        
        // 現在のログレベルを設定する。
        // TODO : Configから設定できるようにする
        public static LogLevel CurrentLogLevel = LogLevel.Debug;
        
        /// <summary>
        /// ログレベルに応じたログを出力する。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="level">ログレベル。</param>
        public static void Log(string message, LogLevel level = LogLevel.Debug)
        {
            if (level < CurrentLogLevel) return;
            
            switch (level)
            {
                case LogLevel.Debug:
                    UnityEngine.Debug.Log(message: message);
                    break;
                
                case LogLevel.Info:
                    UnityEngine.Debug.Log(message: $"INFO: {message}");
                    break;
                
                case LogLevel.Warning:
                    UnityEngine.Debug.LogWarning(message: message);
                    break;
                
                case LogLevel.Error:
                    UnityEngine.Debug.LogError(message: message);
                    break;
            }
        }
        
        /// <summary>
        /// デバッグ用のログを出力する。
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public static void Debug(string message)
        {
            Log(message: message, level: LogLevel.Debug);
        }
        
        /// <summary>
        /// 情報用のログを出力する。
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public static void Info(string message)
        {
            Log(message: message, level: LogLevel.Info);
        }
        
        /// <summary>
        /// 警告用のログを出力する。
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public static void Warning(string message)
        {
            Log(message: message, level: LogLevel.Warning);
        }
        
        /// <summary>
        /// エラー用のログを出力する。
        /// </summary>
        /// <param name="message">ログメッセージ</param>
        public static void Error(string message)
        {
            Log(message: message, level: LogLevel.Error);
        }
        
        /// <summary>
        /// 変数の内容をログ出力する。
        /// </summary>
        /// <param name="obj">出力したい変数</param>
        public static void LogVariables(object obj)
        {
            var sb = new StringBuilder();
            foreach (var prop in obj.GetType().GetProperties())
            {
                sb.Append(value: $"{prop.Name} : {prop.GetValue(obj: obj, index: null)}, ");
            }
            
            // 末尾のコンマとスペースを削除
            if (sb.Length > 0)
            {
                sb.Length -= 2;
            }
            
            Log(message: sb.ToString(), level: LogLevel.Debug);
        }
    }
}