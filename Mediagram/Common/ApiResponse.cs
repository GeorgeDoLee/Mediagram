namespace Mediagram.Common
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }
        
        public ApiResponse()
        {
            Success = true;
            Message = "Request was successful.";
            Data = null;
        }

        public ApiResponse(bool success, string message, object data = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }
        
        public ApiResponse(string message)
        {
            Success = false;
            Message = message;
            Data = null;
        }
    }
}
