namespace Library.Utils
{
    public static class NullUtils
    {
        public static bool IsAnyNull (params object?[] nullables)
        {
            return nullables.Any(x => x == null);
        }
    }
}
