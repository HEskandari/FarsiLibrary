namespace FarsiLibrary.Utils
{
	/// <summary>
	/// Helper class to convert numbers to it's farsi equivalent. Use this class' methods to overcome a problem in displaying farsi numeric values.
	/// </summary>
    public sealed class toEnglish
    {
        /// <summary>
        /// Converts a Farsi number to it's English numeric values.
        /// </summary>
        /// <remarks>This method only converts the numbers in a string, and does not convert any non-numeric characters.</remarks>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string Convert(string num)
        {
            if (string.IsNullOrEmpty(num))
                return num;

            var result = string.Empty;

            for (var i = 0; i < num.Length; i++)
            {
                var numTemp = num.Substring(i, 1);
                switch (numTemp)
                {
                    case "۰":
                        result = result + "0";
                        break;
                    case "۱":
                        result = result + "1";
                        break;
                    case "۲":
                        result = result + "2";
                        break;
                    case "۳":
                        result = result + "3";
                        break;
                    case "۴":
                        result = result + "4";
                        break;
                    case "۵":
                        result = result + "5";
                        break;
                    case "۶":
                        result = result + "6";
                        break;
                    case "۷":
                        result = result + "7";
                        break;
                    case "۸":
                        result = result + "8";
                        break;
                    case "۹":
                        result = result + "9";
                        break;
                    default:
                        result = result + numTemp;
                        break;
                }
            }
            return (result);
        }
    }
}
