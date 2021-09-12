namespace LangBuilder.Source.Extensions
{
    public static class StringExtensions
    {
        public static string ToCapitalizedFirstLetter(this string s)
        {
            char[] chars = s.ToCharArray();
            chars[0] = chars[0].ToUppercase();
            return new string(chars);
        }

        public static char ToUppercase(this char c)
        {
            return char.ToUpper(c);
        }
    }
}
