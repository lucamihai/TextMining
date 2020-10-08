using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TextMining.UnitTests
{
    [ExcludeFromCodeCoverage]
    public static class Constants
    {
        public static readonly string TestFileName = "TestFile.txt";

        public static readonly string XmlFileText =
@"<?xml version=""1.0"" encoding=""iso-8859-1"" ?>
<text>
text stuff.
<p>text from paragraph.</p>
more text stuff.
some other text stuff.
composite-word.
</text>";

        public static readonly string TextFromXmlFileFromTextElements = 
@"
text stuff.
text from paragraph.
more text stuff.
some other text stuff.
composite-word.
";
        public static readonly string ExpectedTextFromXmlFileFromParagraphElements = "text from paragraph.";

        public static readonly string TextTagName = "text";
        public static readonly string ParagraphTagName = "p";

        public static readonly Dictionary<string, int> WordFrequenciesFromText = new Dictionary<string, int>
        {
            {"text", 4},
            {"stuff", 3},
            {"from", 1},
            {"paragraph", 1},
            {"more", 1},
            {"some", 1},
            {"other", 1},
            {"composite-word", 1}
        };
    }
}