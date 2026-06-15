namespace TicTacToeBackend.DTOs
{

    public class Response<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public static Response<T> Ok(T data, string message = "Success")
            => new() { Success = true, Data = data, Message = message };

        public static Response<T> Error(string message)
            => new() { Success = false, Message = message };
    }
}
