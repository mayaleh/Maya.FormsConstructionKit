namespace Maya.FormsConstructionKit.Shared.Model
{
    public class DataResult<T> : CommandResult
    {
        public T? Data { get; set; }
    }

    public static class DataResult
    {
        public static DataResult<T> Success<T>(T data)
        {
            return new DataResult<T>
            {
                IsSuccess = true,
                Data = data
            };
        }

        public static DataResult<T> Failure<T>(string message)
        {
            return new DataResult<T>
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}