namespace QingFa.EShop.Application.Features.Common.Extensions
{
    internal static class ValidatorExtension
    {
        /// <summary>
        /// Checks if a string is a well-formed URL.
        /// </summary>
        /// <param name="url">The URL string to validate.</param>
        /// <returns>True if the URL is well-formed; otherwise, false.</returns>
        public static bool IsValidUrl(string? url)
        {
            if (string.IsNullOrEmpty(url)) return true; // null or empty URLs are allowed

            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }

        /// <summary>
        /// Checks if a GUID is valid.
        /// </summary>
        /// <param name="guid">The GUID to validate.</param>
        /// <returns>True if the GUID is valid; otherwise, false.</returns>
        public static bool IsValidGuid(Guid? guid)
        {
            return guid.HasValue && guid.Value != Guid.Empty;
        }

        /// <summary>
        /// Checks if a string is not null or empty and has a minimum length.
        /// </summary>
        /// <param name="value">The string value to validate.</param>
        /// <param name="minLength">The minimum length of the string.</param>
        /// <returns>True if the string is valid; otherwise, false.</returns>
        public static bool IsValidString(string? value, int minLength)
        {
            return !string.IsNullOrEmpty(value) && value.Length >= minLength;
        }

        /// <summary>
        /// Checks if a string does not exceed a maximum length.
        /// </summary>
        /// <param name="value">The string value to validate.</param>
        /// <param name="maxLength">The maximum length of the string.</param>
        /// <returns>True if the string is valid; otherwise, false.</returns>
        public static bool IsStringLengthValid(string? value, int maxLength)
        {
            return value == null || value.Length <= maxLength;
        }
    }
}
