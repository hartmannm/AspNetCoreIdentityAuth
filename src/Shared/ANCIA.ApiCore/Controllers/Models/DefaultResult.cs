namespace ANCIA.ApiCore.Controllers.Models
{
    public class DefaultResult
    {
        public bool Success { get; set; }
        public object? Data { get; set; }
        public IEnumerable<string>? Errors { get; set; }

        public DefaultResult(bool success)
        {
            Success = success;
        }

        public static DefaultResult Ok(object data)
        {
            return new DefaultResult(true)
            {
                Data = data
            };
        }

        public static DefaultResult Fail(IEnumerable<string> errors)
        {
            return new DefaultResult(false)
            {
                Errors = errors
            };
        }
    }
}
