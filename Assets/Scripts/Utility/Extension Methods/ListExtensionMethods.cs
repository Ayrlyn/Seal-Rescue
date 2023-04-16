using System.Collections.Generic;

public static class ListExtensionMethods
{
    public static bool IsEmpty<T>(this List<T> list)
    {
        return list.Count == 0;
    }
}
