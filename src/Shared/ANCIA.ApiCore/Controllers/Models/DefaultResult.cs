namespace ANCIA.ApiCore.Controllers.Models
{
    public class DefaultResult
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public DefaultResult(bool success)
        {
            Success = success;
        }

        public DefaultResult(object data)
        {
            Success = true;
            Data = data;
        }

        public DefaultResult(IEnumerable<string> errors)
        {
            Success = false;
            Errors = errors;
        }
    }
}
