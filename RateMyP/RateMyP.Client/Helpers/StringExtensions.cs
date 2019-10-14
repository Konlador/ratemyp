using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RateMyP.Helpers
    {
    public static class StringExtensions
        {
        private static Dictionary<char, string> s_regexes = new Dictionary<char, string>
            {
                { 'a', "[ą]" },
                { 'A', "[Ą]" },
                { 'c', "[č]" },
                { 'C', "[Č]" },
                { 'e', "[ęė]" },
                { 'E', "[ĘĖ]" },
                { 'i', "[į]" },
                { 'I', "[Į]" },
                { 's', "[š]" },
                { 'S', "[Š]" },
                { 'u', "[ųū]" },
                { 'U', "[ŲŪ]" },
                { 'z', "[ž]" },
                { 'Z', "[Ž]" },
            };

        public static string Denationalize(this string str)
            {
            foreach (var pair in s_regexes)
                {
                var letter = pair.Key.ToString();
                var regex = pair.Value;
                str = Regex.Replace(str, regex, letter);
                }
            return str;
            }
        }
    }
