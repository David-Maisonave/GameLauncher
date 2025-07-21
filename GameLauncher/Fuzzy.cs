using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GameLauncher
{
    public static class Fuzzy
    {
        #region Similarity functions
        /// <summary>
        ///     Calculate the difference between 2 strings using the LevenshteinDistance distance algorithm.
        ///     Calculate how str#1 can be equal to str#2 by doing insertions, deletions, or substitutions
        /// </summary>
        /// <param name="source1">First string</param>
        /// <param name="source2">Second string</param>
        /// <returns>
        ///     Returns the number of characters difference.
        ///     If all characters are different, it returns the length of the longest string between source1 and source2 {Math.Max(source1.Length, source2.Length)}
        ///     It returns a zero if strings are exactly the same.
        /// </returns>
        public static int LevenshteinDistance(string source1, string source2, bool isCaseSensitive = true)
        {
            // If any entry empty return full length of other
            if (source1.Length == 0 || source2.Length == 0)
                return Math.Max(source1.Length, source2.Length);
            if (!isCaseSensitive)
            {
                source1 = source1.ToLower();
                source2 = source2.ToLower();
            }
            int source1Length = source1.Length;
            int source2Length = source2.Length;
            // Initialization of matrix with row size source1Length and columns size source2Length
            int[,] matrix = new int[source1Length + 1, source2Length + 1];
            for (int i = 0; i <= source1Length; i++) 
                matrix[i, 0] = i;
            for (int j = 0; j <= source2Length; j++) 
                matrix[0, j] = j;
            // Calculate rows and columns distances
            for (int i = 1; i <= source1Length; i++)
            {
                for (int j = 1; j <= source2Length; j++)
                {
                    int cost = (source2[j - 1] == source1[i - 1]) ? 0 : 1;
                    matrix[i, j] = Math.Min(
                        Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                        matrix[i - 1, j - 1] + cost);
                }
            }
            return matrix[source1Length, source2Length];
        }
        /// <summary>
        ///     Calculate the difference between 2 strings using the Damerau-LevenshteinDistance distance algorithm
        ///     Calculate how str#1 can be equal to str#2 by doing insertions, deletions, substitutions, or adjacent character swap
        /// </summary>
        /// <param name="source1">First string</param>
        /// <param name="source2">Second string</param>
        /// <returns>
        ///     Returns the number of characters difference.
        ///     If all characters are different, it returns the length of the longest string between source1 and source2 {Math.Max(source1.Length, source2.Length)}
        ///     It returns a zero if strings are exactly the same.
        /// </returns>
        public static int DamerauLevenshteinDistance(string source1, string source2, bool isCaseSensitive = true)
        {
            if (source1 == source2)
                return 0;
            // If any entry empty return full length of other
            if (source1.Length == 0 || source2.Length == 0)
                return Math.Max(source1.Length, source2.Length);
            if (!isCaseSensitive)
            {
                source1 = source1.ToLower();
                source2 = source2.ToLower();
            }
            int source1Length = source1.Length;
            int source2Length = source2.Length;
            int[,] matrix = new int[source1Length + 1, source2Length + 1];
            for (int i = 1; i <= source1Length; i++)
            {
                matrix[i, 0] = i;
                for (int j = 1; j <= source2Length; j++)
                {
                    int cost = source2[j - 1] == source1[i - 1] ? 0 : 1;
                    if (i == 1)
                        matrix[0, j] = j;
                    int[] vals = new int[] {
                            matrix[i - 1, j] + 1,
                            matrix[i, j - 1] + 1,
                            matrix[i - 1, j - 1] + cost
                    };
                    matrix[i, j] = vals.Min();
                    if (i > 1 && j > 1 && source1[i - 1] == source2[j - 2] && source1[i - 2] == source2[j - 1])
                        matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + cost);
                }
            }
            return matrix[source1Length, source2Length];
        }
        /// <summary>
        ///     Jaro-Winkler distance between the specified strings.
        ///     Similar to Damerau-LevenshteinDistance, but it has a character edit-once restriction to make equal.
        /// </summary>
        /// <param name="source1">First String</param>
        /// <param name="source2">Second String</param>
        /// <returns>
        ///     Returns the Jaro-Winkler distance between the specified strings. The distance is symmetric and will fall in the range 0 (perfect match) to 1 (no match). 
        /// </returns>
        public static float JaroWinklerDistance(string source1, string source2, bool isCaseSensitive = true)
        {
            return 1.0f - JaroWinklerProximity(source1, source2, isCaseSensitive);
        }
        public enum DistanceMethod
        {
            UseDefaultDistanceMethod,
            Levenshtein,
            DamerauLevenshtein,
            JaroWinkler
        }
        private static DistanceMethod defaultDistanceMethod = DistanceMethod.DamerauLevenshtein;
        public static void SetDefaultDistanceMethod(DistanceMethod distanceMethod) => defaultDistanceMethod = distanceMethod;
        public static bool SetDefaultDistanceMethod(string distanceMethod_Name) // This function is to let SQL access to setting default method
        {
            if (distanceMethod_Name.Equals("Levenshtein", StringComparison.OrdinalIgnoreCase))
                defaultDistanceMethod = DistanceMethod.Levenshtein;
            else if (distanceMethod_Name.Equals("DamerauLevenshtein", StringComparison.OrdinalIgnoreCase))
                defaultDistanceMethod = DistanceMethod.DamerauLevenshtein;
            else if (distanceMethod_Name.Equals("JaroWinkler", StringComparison.OrdinalIgnoreCase))
                defaultDistanceMethod = DistanceMethod.JaroWinkler;
            else
                return false;
            return true;
        }
        public static bool IsSimilar(string source1, string source2, float desiredSimilarity, bool isCaseSensitive, DistanceMethod distanceMethod = DistanceMethod.UseDefaultDistanceMethod) => HowSimilar(source1, source2, isCaseSensitive, distanceMethod) >= desiredSimilarity;
        public static float HowSimilar(string source1, string source2, bool isCaseSensitive, DistanceMethod distanceMethod = DistanceMethod.UseDefaultDistanceMethod)
        {
            if (distanceMethod == DistanceMethod.UseDefaultDistanceMethod)
                distanceMethod = defaultDistanceMethod;
            float sourceLength = Math.Max(source1.Length, source2.Length);
            float diff = distanceMethod == DistanceMethod.Levenshtein ? LevenshteinDistance(source1, source2, isCaseSensitive) : (distanceMethod == DistanceMethod.JaroWinkler ? JaroWinklerDistance(source1, source2, isCaseSensitive) : DamerauLevenshteinDistance(source1, source2, isCaseSensitive));
            return diff == 0 ? 1.0f : (sourceLength - diff) / sourceLength;
        }
        public static bool IsNotSimilar(string source1, string source2, bool isCaseSensitive, DistanceMethod distanceMethod)
        {
            if (distanceMethod == DistanceMethod.UseDefaultDistanceMethod)
                distanceMethod = defaultDistanceMethod;
            switch (distanceMethod)
            {
                case DistanceMethod.Levenshtein:
                    return LevenshteinDistance(source1, source2, isCaseSensitive) == Math.Max(source1.Length, source2.Length);
                case DistanceMethod.JaroWinkler:
                    return JaroWinklerDistance(source1, source2, isCaseSensitive) == Math.Max(source1.Length, source2.Length);
                case DistanceMethod.DamerauLevenshtein:
                default:
                    return DamerauLevenshteinDistance(source1, source2, isCaseSensitive) == Math.Max(source1.Length, source2.Length);
            }
        }
        // Following functions are case sensitive
        public static bool IsVerySimilar(string source1, string source2, DistanceMethod distanceMethod = DistanceMethod.UseDefaultDistanceMethod) => IsSimilar(source1, source2, 0.9f, true, distanceMethod); // Is 90% similar
        public static bool IsSimilar(string source1, string source2, DistanceMethod distanceMethod = DistanceMethod.UseDefaultDistanceMethod) => IsSimilar(source1, source2, 0.75f, true, distanceMethod); // Is 75% similar
        public static bool IsSomeWhatSimilar(string source1, string source2, DistanceMethod distanceMethod = DistanceMethod.UseDefaultDistanceMethod) => IsSimilar(source1, source2, 0.5f, true, distanceMethod); // Is 50% similar
        public static bool IsSlightlySimilar(string source1, string source2, DistanceMethod distanceMethod = DistanceMethod.UseDefaultDistanceMethod) => IsSimilar(source1, source2, 0.3f, true, distanceMethod); // Is 30% similar
        public static bool IsHardlySimilar(string source1, string source2, DistanceMethod distanceMethod = DistanceMethod.UseDefaultDistanceMethod) => IsSimilar(source1, source2, 0.1f, true, distanceMethod); // Is 10% similar
        public static bool IsNotSimilar(string source1, string source2, DistanceMethod distanceMethod = DistanceMethod.UseDefaultDistanceMethod) => IsNotSimilar(source1, source2, true, distanceMethod);

        // The following functions are case insensitive
        public static int iLevDistance(string source1, string source2) => LevenshteinDistance(source1, source2, false);
        public static int iDamLevDistance(string source1, string source2) => DamerauLevenshteinDistance(source1, source2, false);
        public static float iJaroWinDistance(string source1, string source2) => JaroWinklerDistance(source1, source2, false);
        public static bool VerySimilar(string source1, string source2) => IsSimilar(source1, source2, 0.9f, false); // Is 90% similar
        public static bool Similar(string source1, string source2) => IsSimilar(source1, source2, 0.75f, false); // Is 75% similar
        public static bool SomeWhatSimilar(string source1, string source2) => IsSimilar(source1, source2, 0.5f, false); // Is 50% similar
        public static bool SlightlySimilar(string source1, string source2) => IsSimilar(source1, source2, 0.3f, false); // Is 30% similar
        public static bool HardlySimilar(string source1, string source2) => IsSimilar(source1, source2, 0.1f, false); // Is 10% similar
        public static bool NotSimilar(string source1, string source2, DistanceMethod distanceMethod = DistanceMethod.UseDefaultDistanceMethod) => IsNotSimilar(source1, source2, false, distanceMethod);
        //) => DamerauLevenshteinDistance(source1, source2, false) == Math.Max(source1.Length, source2.Length); // Has no character matches
        /// The Winkler modification will not be applied unless the percent match was at or above the JaroWinklerWeightThreshold percent without the modification. Winkler's paper used a default value of 0.7
        private static readonly float JaroWinklerWeightThreshold = 0.7f;
        /// Size of the prefix to be considered by the Winkler modification. Winkler's paper used a default value of 4
        private static readonly int JaroWinklerNumChars = 4;
        /// <summary>
        /// Jaro-Winkler distance between the specified strings.
        /// </summary>
        /// <param name="source1">First String</param>
        /// <param name="source2">Second String</param>
        /// <returns>
        /// Returns the Jaro-Winkler distance between the specified strings. The distance is symmetric and will fall in the range 0 (no match) to 1 (perfect match). 
        /// </returns>
        public static float JaroWinklerProximity(string source1, string source2, bool isCaseSensitive = true)
        {
            int source1Length = source1.Length;
            int source2Length = source2.Length;
            if (source1Length == 0)
                return source2Length == 0 ? 1.0f : 0.0f;
            if (!isCaseSensitive)
            {
                source1 = source1.ToLower();
                source2 = source2.ToLower();
            }
            int lSearchRange = Math.Max(0, (Math.Max(source1Length, source2Length) / 2) - 1);
            bool[] lMatched1 = new bool[source1Length];
            bool[] lMatched2 = new bool[source2Length];
            int lNumCommon = 0;
            for (int i = 0; i < source1Length; ++i)
            {
                int lStart = Math.Max(0, i - lSearchRange);
                int lEnd = Math.Min(i + lSearchRange + 1, source2Length);
                for (int j = lStart; j < lEnd; ++j)
                {
                    if (lMatched2[j]) 
                        continue;
                    if (source1[i] != source2[j])
                        continue;
                    lMatched1[i] = true;
                    lMatched2[j] = true;
                    ++lNumCommon;
                    break;
                }
            }
            if (lNumCommon == 0) 
                return 0.0f;
            int lNumHalfTransposed = 0;
            int k = 0;
            for (int i = 0; i < source1Length; ++i)
            {
                if (!lMatched1[i]) 
                    continue;
                while (!lMatched2[k]) 
                    ++k;
                if (source1[i] != source2[k])
                    ++lNumHalfTransposed;
                ++k;
            }
            int lNumTransposed = lNumHalfTransposed / 2;
            float lNumCommonD = lNumCommon;
            float lWeight = ((lNumCommonD / source1Length) + (lNumCommonD / source2Length) + ((lNumCommon - lNumTransposed) / lNumCommonD)) / 3.0f;
            if (lWeight <= JaroWinklerWeightThreshold) 
                return lWeight;
            int lMax = Math.Min(JaroWinklerNumChars, Math.Min(source1.Length, source2.Length));
            int lPos = 0;
            while (lPos < lMax && source1[lPos] == source2[lPos])
                ++lPos;
            return lPos == 0 ? lWeight : lWeight + (0.1f * lPos * (1.0f - lWeight));
        }
        public static string HasCharInSameOrder(string word, string sep = "%") // SQL function extension
        {
            string returnWord = "";
            foreach (char c in word)
                returnWord += $"{c}{sep}";
            return returnWord;
        }
        #endregion Similarity functions
        #region Case INSENSITIVE phrase comparison associated functions
        /// <summary>
        ///     Calculate the number of key words not in each others phrase (All phrase comparisons functions in this class are case INSENSITIVE)
        /// </summary>
        /// <param name="source1">First string</param>
        /// <param name="source2">Second string</param>
        /// <param name="simplify">Set to true in order to remove (A,The), numbers, abbreviated letters, non-alpha</param>
        /// <param name="insertSpacesBetweenCapitalLetters">True to insert spaces between capital letters. IE "MakeThisNormal" converts to "Make This Normal"</param>
        /// <returns>
        ///     Returns the number of key words not in each others phrase
        /// </returns>
        public static int GetPhraseDifference(string source1, string source2, bool simplify = false, bool insertSpacesBetweenCapitalLetters = true)
        {
            string[] list1 = GetKeywordList(ref source1, simplify, insertSpacesBetweenCapitalLetters);
            string[] list2 = GetKeywordList(ref source2, simplify, insertSpacesBetweenCapitalLetters);
            int misMatchCount = 0;
            foreach (string s in list1)
                if (!list2.Contains(s))
                    ++misMatchCount;
            foreach (string s in list2)
                if (!list1.Contains(s))
                    ++misMatchCount;
            return misMatchCount;
        }
        public static int PhraseSimplifiedDiff(string source1, string source2) => GetPhraseDifference(source1, source2, true);
        public static float PhraseHowSimilar(string source1, string source2)
        {
            string[] list1 = GetKeywordList(ref source1, false, true);
            string[] list2 = GetKeywordList(ref source2, false, true);
            float sourceLength = Math.Max(list1.Length, list2.Length);
            float diff = GetPhraseDifference(source1, source2,false, true);
            return diff == 0 ? 1.0f : (sourceLength - diff) / sourceLength;
        }
        public static bool IsPhraseSimilar(string source1, string source2, float desiredSimilarity) => PhraseHowSimilar(source1, source2) >= desiredSimilarity;
        public static bool PhraseVerySimilar(string source1, string source2) => IsPhraseSimilar(source1, source2, 0.9f); // Is 90% similar
        public static bool PhraseSimilar(string source1, string source2) => IsPhraseSimilar(source1, source2, 0.75f); // Is 75% similar
        public static bool PhraseSomeWhatSimilar(string source1, string source2) => IsPhraseSimilar(source1, source2, 0.5f); // Is 50% similar
        public static bool PhraseSlightlySimilar(string source1, string source2) => IsPhraseSimilar(source1, source2, 0.3f); // Is 30% similar
        public static bool PhraseHardlySimilar(string source1, string source2) => IsPhraseSimilar(source1, source2, 0.1f); // Is 10% similar
        public static string GetKeywordStr(string name, bool removeSpace = false, bool simplify = false, bool insertSpacesBetweenCapitalLetters = true)
        {
            if (insertSpacesBetweenCapitalLetters)
            {
                name = Regex.Replace(name, "([a-z])([A-Z])", "$1 $2"); // Put spaces between words like FooDigitHere
                name = Regex.Replace(name, "([A-Z][a-z]*)([A-Z][a-z]*)", "$1 $2"); // Put spaces between words like FooDigitHere
            }
            if (simplify)
            {
                name = Regex.Replace(name, @"([A-Z]\.){2,}", ""); // Remove abbreviated letters
                name = Regex.Replace(name, "[^a-zA-Z]", " "); // Remove non-alpha
                name = Regex.Replace(name, @"(?i)^A\s", " ");
                name = Regex.Replace(name, @"(?i)^The\s", " ");
                name = Regex.Replace(name, @"(?i)\sA\s", " ");
                name = Regex.Replace(name, @"(?i)\sThe\s", " ");
                // name = Regex.Replace(name, @"(?i)\sand\s", "");
            }
            name = removeSpace ? name.Replace(" ", "") : name.Replace("  ", " ");
            name = name.Trim();
            return name;
        }
        public static string[] GetKeywordList(ref string name, bool simplify = false, bool insertSpacesBetweenCapitalLetters = true)
        {
            name = GetKeywordStr(name,false, simplify, insertSpacesBetweenCapitalLetters);
            name = name.Replace(" ", ",");
            string[] keywords = name.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim().ToLower()).Distinct().ToArray();
            return keywords;
        }
        public static int GetWordCount(string words, bool simplify = false, bool insertSpacesBetweenCapitalLetters = true)
        {
            string[] list1 = GetKeywordList(ref words, simplify, insertSpacesBetweenCapitalLetters);
            return list1.Length;
        }
        public static int GetMatchCount(string phrase1, string phrase2, ref int qtyNotMatching, bool simplify = true, bool insertSpacesBetweenCapitalLetters = true, bool isCaseSensitive = false)
        {
            phrase1 = GetKeywordStr(phrase1, true, simplify, insertSpacesBetweenCapitalLetters);
            phrase2 = GetKeywordStr(phrase2, true, simplify, insertSpacesBetweenCapitalLetters);
            if (String.IsNullOrEmpty(phrase1) || String.IsNullOrEmpty(phrase2))
                return 0;
            if (!isCaseSensitive)
            {
                phrase1 = phrase1.ToLower();
                phrase2 = phrase2.ToLower();
            }
            qtyNotMatching = 0;
            int matchCount = 0;
            string matchingLetters = "";
            foreach (char c in phrase1)
            {
                if (phrase2.Contains(c))
                {
                    if (!matchingLetters.Contains(c))
                    {
                        ++matchCount;
                        matchingLetters += $"{c}";
                    }
                    else
                    {
                        matchingLetters += $"{c}";
                        int count1 = matchingLetters.Count(x => x == c);
                        int count2 = phrase2.Count(x => x == c);
                        if (count2 >= count1)
                            ++matchCount;
                        else
                            ++qtyNotMatching;
                    }
                }
                else
                    ++qtyNotMatching;
            }
            return matchCount;
        }
        #endregion Phrase comparison associated functions
    }
}
