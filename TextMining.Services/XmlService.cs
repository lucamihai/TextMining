using System;
using System.Linq;
using System.Xml.Linq;
using TextMining.Services.Interfaces;

namespace TextMining.Services
{
    public class XmlService : IXmlService
    {
        public XDocument GetXDocumentFromText(string text)
        {
            return XDocument.Parse(text);
        }

        public string GetTextFromAllElements(XDocument xmlDocument, string elementName, string separator = null)
        {
            if (xmlDocument == null)
            {
                throw new ArgumentNullException(nameof(xmlDocument));
            }
            
            if (string.IsNullOrWhiteSpace(elementName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(elementName));
            }

            separator ??= Environment.NewLine;

            return string.Join(separator, xmlDocument.Descendants(elementName).Select(n => n.Value));
        }
    }
}