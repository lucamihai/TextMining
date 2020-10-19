using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TextMining.Entities
{
    [ExcludeFromCodeCoverage]
    public class DocumentData
    {
        public string Title { get; set; }
        public List<string> Topics { get; set; }
        public TextData TextData { get; set; }
    }
}