using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TextMining.Entities
{
    [ExcludeFromCodeCoverage]
    public class TextData
    {
        public Dictionary<string, int> WordDictionary { get; set; }
        public Dictionary<string, int> AcronymDictionary { get; set; }

        public int GetTotalWordCount()
        {
            return WordDictionary?.Values.Sum() ?? 0;
        }

        public int GetUniqueWordCount()
        {
            return WordDictionary?.Select(x => x.Key).Distinct().Count() ?? 0;
        }

        public int GetTotalAcronymCount()
        {
            return AcronymDictionary?.Values.Sum() ?? 0;
        }

        public int GetUniqueAcronymCount()
        {
            return AcronymDictionary?.Select(x => x.Key).Distinct().Count() ?? 0;
        }
    }
}