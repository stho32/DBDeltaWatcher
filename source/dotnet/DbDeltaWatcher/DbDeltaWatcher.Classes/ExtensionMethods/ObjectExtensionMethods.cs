using System;
using System.Globalization;

namespace DbDeltaWatcher.Classes.ExtensionMethods
{
    /**
     * A bunch of converters from object to a data type
     */
    public static class ObjectExtensionMethods
    {
        public static long ToLong(this object o, long defaultValue = 0)
        {
            try
            {
                return (long) o;
            }
            catch
            {
                return defaultValue;
            }
        }
        
        public static int ToInt(this object o, int defaultValue = 0)
        {
            try
            {
                if (o == null)
                    return defaultValue;

                if (int.TryParse(o.ToString(), out var result))
                {
                    return result;
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static bool ToBool(this object o, bool defaultValue = false)
        {
            if (o == null || o == DBNull.Value)
                return defaultValue;
            var valueAsString = o.ToString()?.ToLower().Trim();
            return (valueAsString is "1" or "true");
        }

        public static DateTime? ToNullableDateTime(this object o, DateTime? defaultValue = null)
        {
            if (o == null || o == DBNull.Value)
                return defaultValue;

            var valueAsString = o.ToString();
            if (DateTime.TryParse(valueAsString, out DateTime asDateTime))
            {
                return asDateTime;
            }
            return defaultValue;
        }
    }
}