using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TextMining.Tests.Common
{
    [ExcludeFromCodeCoverage]
    public static class Constants
    {
        public const string NullString = null;
        public const string EmptyString = "";
        public const string OnlyWhitespacesString = " \t  ";

        public static readonly string TestFileName = "TestFile.xml";

        public static readonly string XmlFileText =
@"<?xml version=""1.0"" encoding=""iso-8859-1"" ?>
<newsitem itemid=""2538"" id=""root"" date=""1996-08-20"" xml:lang=""en"">
<title>Title from XML</title>
<text>
text stuff.
<p>text from paragraph.</p>
more text stuff.
some other text stuff.
composite-word.
something's.
ACRONYM.
stop words stop-words.
</text>
<copyright>(c) Reuters Limited 1996</copyright>
<metadata>
<codes class=""bip:countries:1.0"">
  <code code=""Code1"">
    <editdetail attribution=""Reuters BIP Coding Group"" action=""confirmed"" date=""1996-08-20""/>
  </code>
</codes>
<codes class=""bip:topics:1.0"">
  <code code=""Code2"">
    <editdetail attribution=""Reuters BIP Coding Group"" action=""confirmed"" date=""1996-08-20""/>
  </code>
  <code code=""Code3"">
    <editdetail attribution=""Reuters BIP Coding Group"" action=""confirmed"" date=""1996-08-20""/>
  </code>
  <code code=""Code4"">
    <editdetail attribution=""Reuters BIP Coding Group"" action=""confirmed"" date=""1996-08-20""/>
  </code>
  <code>
    just a code without the attribute
  </code>
</codes>
</metadata>
</newsitem>";
        public const string DocumentTitle = "Title from XML";

        public static readonly List<string> StopWords = new List<string>
        {
            "stop", "words"
        };
        public static readonly string TextFromXmlFileFromTextElements = 
@"
text stuff.
text from paragraph.
more text stuff.
some other text stuff.
composite-word.
something's.
ACRONYM.
stop words stop-words.
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
            {"composite", 1},
            {"word", 1},
            {"something", 1},
            {"s", 1}
        };

        public static readonly Dictionary<string, int> AcronymFrequenciesFromText = new Dictionary<string, int>
        {
            {"ACRONYM", 1}
        };

        public static readonly List<string> CodesFromXml = new List<string>
        {
            "Code2", "Code3", "Code4",
        };
    }
}