namespace LazyBookworm.Common.Models.Common
{
    public class ActionResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public dynamic Result { get; set; }
        public bool IsSystemError { get; set; }
        public string SystemErrorMessage { get; set; }

        public static ActionResult Success(string msg = null, dynamic result = null) => new() { IsSuccess = true, Message = msg, Result = result, IsSystemError = false, SystemErrorMessage = null };
        public static ActionResult Error(string msg = null, dynamic result = null) => new() { IsSuccess = false, Message = msg, Result = result, IsSystemError = false, SystemErrorMessage = null };
        public static ActionResult SystemError(string msg = null, string systemErrorMessage = null) => new() { IsSuccess = false, IsSystemError = true, Message = msg, SystemErrorMessage = systemErrorMessage, Result = null };
    }
}