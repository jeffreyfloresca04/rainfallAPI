namespace Rainfall.API.Application.Common
{
    public class CustomException : Exception
    {
        public int code { get; set; }
        public CustomException(int code) : base()
        {
            this.code = code;
        }
    }
}
