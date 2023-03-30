public static class ArrayExtensionMethods
{

    public static bool IsEmpty<T>(this T[] array)
    {
        return array.Length == 0;
    }

}