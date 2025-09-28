namespace JABARACdesign.Base.Application.Result.EmailExists
{
    /// <summary>
    /// Emailの存在確認の結果
    /// </summary>
    public class EmailExistsResult
    {
        public bool IsExists { get; private set; }
        
        public EmailExistsResult(bool isExists)
        {
            IsExists = isExists;
        }
    }
}