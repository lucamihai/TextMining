using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TextMining.Entities
{
    [ExcludeFromCodeCoverage]
    public class DocumentData
    {
        public string Title { get; set; }
        public List<string> Topics { get; set; }
        public Dictionary<string, int> WordDictionary { get; set; }
        public Dictionary<string, int> AcronymDictionary { get; set; }

        public int GetTotalWordCount()
        {
            return WordDictionary?.Values.Sum() ?? 0;
        }
    }
}