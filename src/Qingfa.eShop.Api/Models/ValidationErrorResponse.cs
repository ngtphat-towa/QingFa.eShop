namespace QingFa.EShop.Api.Models
{
    /// <summary>
    /// Represents the response structure for validation errors.
    /// </summary>
    public class ValidationErrorResponse
    {
        /// <summary>
        /// Gets or sets the status code of the response.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the message indicating the nature of the validation errors.
        /// </summary>
        public string Message { get; set; } = "One or more validation errors occurred.";

        /// <summary>
        /// Gets or sets a dictionary of validation errors, where keys are the names of the fields and values are lists of error messages.
        /// </summary>
        public Dictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>>();
    }
}
