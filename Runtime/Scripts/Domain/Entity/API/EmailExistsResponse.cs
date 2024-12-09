namespace JABARACdesign.Base.Domain.Entity.API
{
    /// <summary>
    /// Emailの存在確認のレスポンス
    /// </summary>
    public class EmailExistsResponse
    {
        public bool IsExists { get; private set; }
        
        public EmailExistsResponse(bool isExists)
        {
            IsExists = isExists;
        }
    }
}