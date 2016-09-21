namespace PSN.APIResponses
{
    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }

    public class ErrorResponse
    {
        public Error Error { get; set; }
    }
}