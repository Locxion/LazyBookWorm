namespace LazyBookworm.Common.Models.Common
{
    public class ActionResult<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T? Result { get; set; }
        public bool IsSystemError { get; set; }
        public string SystemErrorMessage { get; set; }

        public static ActionResult<T> Success(T? result, string msg = null) => new() { IsSuccess = true, Message = msg, Result = result, IsSystemError = false, SystemErrorMessage = null };

        public static ActionResult<T> Error(string msg = null) => new() { IsSuccess = false, Message = msg, IsSystemError = false, SystemErrorMessage = null };

        public static ActionResult<T> SystemError(string msg = null, string systemErrorMessage = null) => new() { IsSuccess = false, IsSystemError = true, Message = msg, SystemErrorMessage = systemErrorMessage };
    }
}