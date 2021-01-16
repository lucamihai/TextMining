using System.Collections.Generic;

namespace TextMining.Helpers.Extensions
{
    public static class CollectionOperations
    {
        public static void AddWordToDictionary(this Dictionary<string, int> dictionary, string word)
        {
            if (dictionary.ContainsKey(word))
            {
                dictionary[word]++;
            }
            else
            {
                dictionary.Add(word, 1);
            }
        }
    }
}