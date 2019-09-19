using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace RateMyP.Db
    {
    public static class SQLExtensions
        {
        public static string SafeGetString(this IDataRecord reader, string propertyName, string defaultValue = null)
            {
            var colIndex = reader.GetOrdinal(propertyName);
            return !reader.IsDBNull(colIndex) ? reader.GetString(colIndex) : defaultValue;
            }

        public static int SafeGetInt(this IDataRecord reader, string propertyName, int defaultValue = 0)
            {
            var colIndex = reader.GetOrdinal(propertyName);
            return !reader.IsDBNull(colIndex) ? reader.GetInt32(colIndex) : defaultValue;
            }

        public static long SafeGetLong(this IDataRecord reader, string propertyName, long defaultValue = 0)
            {
            var colIndex = reader.GetOrdinal(propertyName);
            return !reader.IsDBNull(colIndex) ? reader.GetInt64(colIndex) : defaultValue;
            }

        public static bool SafeGetBool(this IDataRecord reader, string propertyName, bool defaultValue = false)
            {
            var colIndex = reader.GetOrdinal(propertyName);
            return !reader.IsDBNull(colIndex) ? reader.GetBoolean(colIndex) : defaultValue;
            }

        public static Guid SafeGetGuid(this IDataRecord reader, string propertyName, Guid defaultValue)
            {
            var colIndex = reader.GetOrdinal(propertyName);
            return !reader.IsDBNull(colIndex) ? reader.GetGuid(colIndex) : defaultValue;
            }

        public static T SafeGetEnum<T>(this IDataRecord reader, string propertyName, T defaultValue) where T : struct, IConvertible
            {
            if (!typeof(T).IsEnum)
                return defaultValue;
            var stringValue = reader.SafeGetString(propertyName, defaultValue.ToString(CultureInfo.InvariantCulture));

            if (!Enum.IsDefined(typeof(T), stringValue))
                return defaultValue;
            return (T)Enum.Parse(typeof(T), stringValue, true);
            }
        }
    }
