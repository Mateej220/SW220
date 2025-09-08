using System;

namespace SW220
{
    public static class General
    {
        public static string Repeat(string text, int amount)
        {
            string output = String.Concat(Enumerable.Repeat(text, amount));
            return output;
        }
    }
}