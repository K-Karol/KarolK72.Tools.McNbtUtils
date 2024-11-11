namespace KarolK72.Tools.McNbtUtils
{
    public static class Helpers
    {
        public static string FormatName(this string name)
        {
            return !name.Contains(':') ? name : $"'{name}'";
        }
    }
}