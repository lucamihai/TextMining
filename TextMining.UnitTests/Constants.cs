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
</text>";

        public static readonly string ExpectedTextFromXmlFileFromTextElements = 
@"
text stuff.
text from paragraph.
more text stuff.
some other text stuff.
";
        public static readonly string ExpectedTextFromXmlFileFromParagraphElements = "text from paragraph.";

        public static readonly string TextTagName = "text";
        public static readonly string ParagraphTagName = "p";
    }
}