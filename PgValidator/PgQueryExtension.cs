using System.Collections.Concurrent;
using System.Reflection;

namespace PgValidator;

internal static class PgQueryExtension
{
    public static T Create<T>(this PgQueryResultRow row)
    {
        var obj = ConstructorCache<T>.CreateInstance();
        obj.SetValues(row);
        return obj;
    }

    public static T Create<T, T0>(this PgQueryResultRow row, T0 param0)
    {
        var obj = ConstructorCache<T, T0>.CreateInstance(param0);
        obj.SetValues(row);
        return obj;
    }
    public static T Create<T, T0, T1>(this PgQueryResultRow row, T0 param0, T1 param1)
    {
        var obj = ConstructorCache<T, T0, T1>.CreateInstance(param0, param1);
        obj.SetValues(row);
        return obj;
    }
    public static T Create<T, T0, T1, T2>(this PgQueryResultRow row, T0 param0, T1 param1, T2 param2)
    {
        var obj = ConstructorCache<T, T0, T1, T2>.CreateInstance(param0, param1, param2);
        obj.SetValues(row);
        return obj;
    }
    public static T Create<T, T0, T1, T2, T3>(this PgQueryResultRow row, T0 param0, T1 param1, T2 param2, T3 param3)
    {
        var obj = ConstructorCache<T, T0, T1, T2, T3>.CreateInstance(param0, param1, param2, param3);
        obj.SetValues(row);
        return obj;
    }
    public static T Create<T, T0, T1, T2, T3, T4>(this PgQueryResultRow row, T0 param0, T1 param1, T2 param2, T3 param3, T4 param4)
    {
        var obj = ConstructorCache<T, T0, T1, T2, T3, T4>.CreateInstance(param0, param1, param2, param3, param4);
        obj.SetValues(row);
        return obj;
    }
    public static T Create<T, T0, T1, T2, T3, T4, T5>(this PgQueryResultRow row, T0 param0, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
    {
        var obj = ConstructorCache<T, T0, T1, T2, T3, T4, T5>.CreateInstance(param0, param1, param2, param3, param4, param5);
        obj.SetValues(row);
        return obj;
    }
    public static T Create<T, T0, T1, T2, T3, T4, T5, T6>(this PgQueryResultRow row, T0 param0, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
    {
        var obj = ConstructorCache<T, T0, T1, T2, T3, T4, T5, T6>.CreateInstance(param0, param1, param2, param3, param4, param5, param6);
        obj.SetValues(row);
        return obj;
    }

    public static T Create<T, T0, T1, T2, T3, T4, T5, T6, T7>(this PgQueryResultRow row, T0 param0, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
    {
        var obj = ConstructorCache<T, T0, T1, T2, T3, T4, T5, T6, T7>.CreateInstance(param0, param1, param2, param3, param4, param5, param6, param7);
        obj.SetValues(row);
        return obj;
    }

    #region ConstructorCache
    private static class ConstructorCache<T>
    {
        static ConstructorCache()
        {
            ConstructorInfo = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, Type.DefaultBinder, Type.EmptyTypes, null)!;
        }

        private static readonly ConstructorInfo ConstructorInfo;
        public static T CreateInstance()
        {
            return (T)ConstructorInfo?.Invoke(null)!;
        }
    }
    private static class ConstructorCache<T, T0>
    {
        static ConstructorCache()
        {
            ConstructorInfo = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, Type.DefaultBinder, new Type[] { typeof(T0) }, null)!;
        }
        private static readonly ConstructorInfo ConstructorInfo;
        public static T CreateInstance(T0 param0)
        {
            return (T)ConstructorInfo.Invoke(new object?[] { param0 });
        }
    }
    private static class ConstructorCache<T, T0, T1>
    {
        static ConstructorCache()
        {
            ConstructorInfo = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, Type.DefaultBinder, new Type[] { typeof(T0), typeof(T1) }, null)!;
        }
        private static readonly ConstructorInfo ConstructorInfo;
        public static T CreateInstance(T0 param0, T1 param1)
        {
            return (T)ConstructorInfo.Invoke(new object?[] { param0, param1 });
        }
    }
    private static class ConstructorCache<T, T0, T1, T2>
    {
        static ConstructorCache()
        {
            ConstructorInfo = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, Type.DefaultBinder, new Type[] { typeof(T0), typeof(T1), typeof(T2) }, null)!;
        }
        private static readonly ConstructorInfo ConstructorInfo;
        public static T CreateInstance(T0 param0, T1 param1, T2 param2)
        {
            return (T)ConstructorInfo.Invoke(new object?[] { param0, param1, param2 });
        }
    }
    private static class ConstructorCache<T, T0, T1, T2, T3>
    {
        static ConstructorCache()
        {
            ConstructorInfo = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, Type.DefaultBinder, new Type[] { typeof(T0), typeof(T1), typeof(T2), typeof(T3) }, null)!;
        }
        private static readonly ConstructorInfo ConstructorInfo;
        public static T CreateInstance(T0 param0, T1 param1, T2 param2, T3 param3)
        {
            return (T)ConstructorInfo.Invoke(new object?[] { param0, param1, param2, param3 });
        }
    }
    private static class ConstructorCache<T, T0, T1, T2, T3, T4>
    {
        static ConstructorCache()
        {
            ConstructorInfo = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, Type.DefaultBinder, new Type[] { typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4) }, null)!;
        }
        private static readonly ConstructorInfo ConstructorInfo;
        public static T CreateInstance(T0 param0, T1 param1, T2 param2, T3 param3, T4 param4)
        {
            return (T)ConstructorInfo.Invoke(new object?[] { param0, param1, param2, param3, param4 });
        }
    }
    private static class ConstructorCache<T, T0, T1, T2, T3, T4, T5>
    {
        static ConstructorCache()
        {
            ConstructorInfo = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, Type.DefaultBinder, new Type[] { typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5) }, null)!;
        }
        private static readonly ConstructorInfo ConstructorInfo;
        public static T CreateInstance(T0 param0, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
        {
            return (T)ConstructorInfo.Invoke(new object?[] { param0, param1, param2, param3, param4, param5 });
        }
    }
    private static class ConstructorCache<T, T0, T1, T2, T3, T4, T5, T6>
    {
        static ConstructorCache()
        {
            ConstructorInfo = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, Type.DefaultBinder, new Type[] { typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6) }, null)!;
        }
        private static readonly ConstructorInfo ConstructorInfo;
        public static T CreateInstance(T0 param0, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
        {
            return (T)ConstructorInfo.Invoke(new object?[] { param0, param1, param2, param3, param4, param5, param6 });
        }
    }
    private static class ConstructorCache<T, T0, T1, T2, T3, T4, T5, T6, T7>
    {
        static ConstructorCache()
        {
            ConstructorInfo = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, Type.DefaultBinder, new Type[] { typeof(T0), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7) }, null)!;
        }
        private static readonly ConstructorInfo ConstructorInfo;
        public static T CreateInstance(T0 param0, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
        {
            return (T)ConstructorInfo.Invoke(new object?[] { param0, param1, param2, param3, param4, param5, param6, param7 });
        }
    }
    #endregion
    #region FieldInfoCache
    private static class FieldInfoCache<T>
    {
        static FieldInfoCache()
        {
            FieldInfo = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            Fields = new ConcurrentDictionary<FieldInfo, DbColumnAttribute>(
                FieldInfo.Where(x => x.GetCustomAttribute<DbColumnAttribute>() != null).
                ToDictionary(x => x, y => y.GetCustomAttribute<DbColumnAttribute>())!);
        }
        private static readonly FieldInfo[] FieldInfo;
        public static readonly ConcurrentDictionary<FieldInfo, DbColumnAttribute> Fields;
    }
    #endregion
    #region PropertyInfoCache
    private static class PropertyInfoCache<T>
    {
        static PropertyInfoCache()
        {
            PropertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            Properties = new ConcurrentDictionary<PropertyInfo, DbColumnAttribute>(
                PropertyInfo.Where(x => x.GetCustomAttribute<DbColumnAttribute>() != null).
                ToDictionary(x => x, y => y.GetCustomAttribute<DbColumnAttribute>())!);
        }
        private static readonly PropertyInfo[] PropertyInfo;
        public static readonly ConcurrentDictionary<System.Reflection.PropertyInfo, DbColumnAttribute> Properties;
    }
    #endregion

    public static void SetValues<T>(this T src, PgQueryResultRow row)
    {
        foreach (var p in PropertyInfoCache<T>.Properties)
        {
            p.Key.SetValue(src, p.GetValue(row), null);
        }
        foreach (var f in FieldInfoCache<T>.Fields)
        {
            f.Key.SetValue(src, f.GetValue(row));
        }
    }

    private static object? GetValue(this KeyValuePair<FieldInfo, DbColumnAttribute> f, PgQueryResultRow row)
    {
        return row[f.Value.ColumnName].GetValue(f.Key.FieldType, f.Value?.DateFormat);
    }

    private static object? GetValue(this KeyValuePair<PropertyInfo, DbColumnAttribute> p, PgQueryResultRow row)
    {
        return row[p.Value.ColumnName].GetValue(p.Key.PropertyType, p.Value?.DateFormat);
    }

    private static object? GetValue(this object? value, Type t, string? format)
    {
        if (value == null)
        {
            return null;
        }

        if (t == typeof(DateTime))
        {
            return value.ToDateTime(format)!.Value;
        }
        else if (t == typeof(DateTime?))
        {
            return value.ToDateTime(format);
        }
        else if (t == typeof(int))
        {
            return value.ToIntN()!.Value;
        }
        else if (t == typeof(int?))
        {
            return value.ToIntN();
        }
        else if (t == typeof(decimal))
        {
            return value.ToDecimalN()!.Value;
        }
        else if (t == typeof(decimal?))
        {
            return value.ToDecimalN();
        }
        else if (t == typeof(float))
        {
            return value.ToFloatN()!.Value;
        }
        else if (t == typeof(float?))
        {
            return value.ToFloatN();
        }
        else if (t == typeof(double))
        {
            return value.ToDoubleN()!.Value;
        }
        else if (t == typeof(double?))
        {
            return value.ToDoubleN();
        }
        else if (t == typeof(long))
        {
            return value.ToLongN()!.Value;
        }
        else if (t == typeof(long?))
        {
            return value.ToLongN();
        }
        else if (t == typeof(string))
        {
            return value.ToString();
        }
        else if (t == typeof(bool))
        {
            return value.ToBoolN()!.Value;
        }
        else if (t == typeof(bool?))
        {
            return value.ToBoolN();
        }
        else
        {
            return Convert.ChangeType(value, t);
        }
    }
}
