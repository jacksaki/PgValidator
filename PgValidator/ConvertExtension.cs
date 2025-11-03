using System.Linq;

namespace PgValidator;

internal static class ConvertExtension
{
    private static List<string> YesValues = new List<string> { "Y", "Yes", "True", "T" };

    public static bool? ToBoolN(this object? value)
    {
        if(value==null || value == DBNull.Value)
        {
            return null;
        }

        return YesValues.Where(x => x.Equals(value.ToString(), StringComparison.OrdinalIgnoreCase)).Any();
    }

    public static bool? ToBoolN(this object? value, params string[] yesValues)
    {
        if (value == null || value == DBNull.Value)
        {
            return null;
        }

        return yesValues.Where(x => x.Equals(value.ToString(), StringComparison.OrdinalIgnoreCase)).Any();
    }

    public static int? ToIntN(this object? value)
    {
        if (value == null || value == DBNull.Value)
        {
            return null;
        }

        return int.TryParse(value.ToString(), out var ret) ? ret : (int?)null;
    }

    internal static int ToInt32(this object? value, int defaultValue)
    {
        return value.ToIntN() ?? defaultValue;
    }

    internal static DateTime? ToDateTime(this object? value, string? dateFormat)
    {
        if (value == null || value == DBNull.Value)
        {
            return null;
        }

        if (value is DateTime d)
        {
            return d;
        }
        else if (value is DateOnly o)
        {
            return o.ToDateTime(TimeOnly.MinValue); ;
        }
        if (dateFormat == null)
        {
            return DateTime.TryParse(value.ToString(), out var ret) ? ret : null;
        }
        else
        {
            return DateTime.TryParseExact(value.ToString(), dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var ret) ? ret : null;
        }
    }

    internal static decimal? ToDecimalN(this object? value)
    {
        if (value == null || value == DBNull.Value)
        {
            return null;
        }
        return decimal.TryParse(value.ToString(), out var ret) ? ret : null;
    }

    internal static decimal ToDecimal(this object? value, decimal defaultValue)
    {
        return value.ToDecimalN() ?? defaultValue;
    }
    internal static float? ToFloatN(this object? value)
    {
        if (value == null || value == DBNull.Value)
        {
            return null;
        }
        return float.TryParse(value.ToString(), out var ret) ? ret : null;
    }

    internal static float ToFloat(this object? value, float defaultValue)
    {
        return value.ToFloatN() ?? defaultValue;
    }

    internal static double? ToDoubleN(this object? value)
    {
        if (value == null || value == DBNull.Value)
        {
            return null;
        }
        return double.TryParse(value.ToString(), out var ret) ? ret : null;
    }

    internal static double ToDouble(this object? value, double defaultValue)
    {
        return value.ToDoubleN() ?? defaultValue;
    }
    internal static long? ToLongN(this object? value)
    {
        if (value == null || value == DBNull.Value)
        {
            return null;
        }

        return long.TryParse(value.ToString(), out var ret) ? ret : null;
    }

    internal static long ToLong(this object? value, long defaultValue)
    {
        return value.ToLongN() ?? defaultValue;
    }
}
