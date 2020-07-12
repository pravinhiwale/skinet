namespace API.Errors
{
    public class ApiException : ApiResponse
    {
        //Created this new class  to extend our API response  to accommodate the extra field that we want to send down with a response.

        //ecause we don't have a parameter less constructor we need to create one
        public ApiException(int statusCode, string message = null,string details =null) : base(statusCode, message)
        {
            Details = details;
        }
        public string Details { get; set; }

    }
}