using System.Globalization;


namespace Accelerator.Backend.Utils.Extensions
{
    /// <summary>
    /// String extensions
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Checks the URL valid.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static bool CheckUrlValid(this string source) => Uri.TryCreate(source, UriKind.Absolute, out Uri uriResult) &&
                                                                   (uriResult.Scheme == Uri.UriSchemeHttp ||
                                                                    uriResult.Scheme == Uri.UriSchemeHttps);

        /// <summary>
        /// Concats the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static string ConcatDictionary(this IDictionary<string, string> parameters)
        {
            var concaParameters = string.Empty;
            parameters.ToList().ForEach(parameter => concaParameters += $"{parameter.Key}={parameter.Value}&");
            concaParameters = concaParameters.Substring(0, concaParameters.Length - 1);
            return concaParameters;
        }

        /// <summary>
        /// Covert string text to hexa representation
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns>hexadecimal representation</returns>
        public static string ToHex(this string plainText)
        {
            byte[] byteArray = null;
            System.Text.StringBuilder hexNumbers = new System.Text.StringBuilder();
            byteArray = System.Text.Encoding.ASCII.GetBytes(plainText);
            for (int i = 0; i <= byteArray.Length - 1; i++)
            {
                hexNumbers.Append(byteArray[i].ToString("x"));
            }

            return hexNumbers.ToString();
        }

        /// <summary>
        /// Get string from hexa representation
        /// </summary>
        /// <param name="hexText"></param>
        /// <returns>string</returns>
        public static string FromHex(this string hexText)
        {
            string st = hexText.ToString();
            string plainText = "";
            for (int x = 0; x <= st.Length - 1; x += 2)
            {
                plainText += Convert.ToChar(int.Parse(st.Substring(x, 2), NumberStyles.HexNumber));
            }

            return plainText;
        }

        /// <summary>
        /// Base64 Encoded text
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns>encoded text</returns>
        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Base64 Decode Text
        /// </summary>
        /// <param name="base64EncodedData"></param>
        /// <returns>decoded string</returns>
        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        /// <summary>
        /// Remove character ocurrences from string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        public static string RemoveCharacter(this string source, string characters) => source.Replace(characters, string.Empty);
    }
}
