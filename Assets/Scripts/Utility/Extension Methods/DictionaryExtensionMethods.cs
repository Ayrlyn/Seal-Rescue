using System.Collections.Generic;

public static class DictionaryExtensionMethods
{
    public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
    {
        TValue value;
        dict.TryGetValue(key, out value);
        return value;
    }
}