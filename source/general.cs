namespace SW220
{
    public static class General
    {
        public static string Repeat(string text, int amount)
        {
            string output = string.Concat(Enumerable.Repeat(text, amount));
            return output;
        }
    }
}