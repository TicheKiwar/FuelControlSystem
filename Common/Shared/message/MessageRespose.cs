namespace Common.Shared.message
{
    public class MessageResponse<T> where T : class
    {
        public T? Data { get; set; } 
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        
        public MessageResponse(T data, string message = "Operación exitosa")
        {
            Data = data;
            Success = true;
            Message = message;
        }

        public MessageResponse(string errorMessage)
        {
            Success = false;
            Message = errorMessage;
        }

        public MessageResponse() { }
    }
}
