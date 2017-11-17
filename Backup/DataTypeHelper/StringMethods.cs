using System;

namespace StringHelpers
{
    public static class StringMethods
    {

        public static string CorrectDBNull(this string value, object oValue)
        {
            if (Convert.IsDBNull(oValue))
            {
                return "";
            }
            else
            {
                return oValue.ToString();

            }
        }

        public static int ConvertToInt(this string value)
        {
            if (Convert.IsDBNull(value) || value == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(value.ToString());

            }
        }

        public static DateTime ConvertToDate(this string value)
        {
            if (Convert.IsDBNull(value) || value == "")
            {
                return Convert.ToDateTime("1-jan-1900");
            }
            else
            {
                return Convert.ToDateTime(value.ToString());
            }
        }

        public static decimal ConvertToDecimal(this string value)
        {
            if (Convert.IsDBNull(value) || value == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(value.ToString());
            }
        }
    }







}