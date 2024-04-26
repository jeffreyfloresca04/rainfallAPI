namespace Rainfall.API.Application.Common
{
    /// <summary>
    /// Custom Exception
    /// </summary>
    public class CustomException : Exception
    {
        public int ErrorCode { get; private set; }
        public string Message { get; private set; }
        public CustomException(int errorCode, string message) : base()
        {
            this.ErrorCode = errorCode;
            this.Message = message;
        }
    }
}
