namespace CancunHotel.Shared.Common
{
    public class Response<T>
    {
        public string Message { get; set; }
        public int ResponseCode { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }

        public static TResponse CreateSuccessfulResponse<TResponse>(T data) where TResponse: Response<T>, new()
        {
            var p = new TResponse()
            {
                Data = data,
                Message = "Successfully executed.",
                ResponseCode = 200,
                Success = true
            };
            return p;
        }

        public static TResponse CreateFailedResponse<TResponse>() where TResponse : Response<T>, new()
        {
            var p = new TResponse()
            {
                Message = "Execution failed",
                ResponseCode = 500,
                Success = false
            };
            return p;
        }

        public static TResponse CreateFailedResponse<TResponse>(string message) where TResponse : Response<T>, new()
        {
            var p = new TResponse()
            {
                Message = message,
                ResponseCode = 500,
                Success = false
            };
            return p;
        }
    }
}
