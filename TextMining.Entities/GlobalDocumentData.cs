using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TextMining.Entities
{
    [ExcludeFromCodeCoverage]
    public class GlobalDocumentData
    {
        public List<string> Words { get; set; }
        public int[,] Frequencies { get; set; }
    }
}