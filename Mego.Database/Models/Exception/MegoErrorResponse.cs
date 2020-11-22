namespace Mego.Database.Models.Exception
{
    /// <summary>
    /// Model which represent error from custom error handler 
    /// </summary>
    public class MegoErrorResponse
    {
        /// <summary>
        /// Type or custom error which occurred
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Our custom error message for response
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Status code of particular error
        /// </summary>
        public int StatusCode { get; }

        public MegoErrorResponse(System.Exception ex, int code)
        {
            Type = ex?.GetType().Name;
            Message = ex?.Message;
            StatusCode = code;
        }
    }
}