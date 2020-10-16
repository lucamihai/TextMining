using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TextMining.Services.Interfaces;

namespace TextMining.Services
{
    public class XmlService : IXmlService
    {
        public XDocument GetXDocumentFromText(string text)
        {
            ValidateString(text);

            return XDocument.Parse(text);
        }

        public XDocument GetXDocumentFromFile(string filepath)
        {
            ValidateString(filepath);

            return XDocument.Load(filepath);
        }

        public string GetTextFromAllElements(XDocument xDocument, string elementName, string separator = null)
        {
            ValidateXDocument(xDocument);
            ValidateString(elementName);

            separator ??= Environment.NewLine;

            return string.Join(separator, xDocument.Descendants(elementName).Select(n => n.Value));
        }

        public List<string> GetCodesFromXDocument(XDocument xDocument)
        {
            ValidateXDocument(xDocument);

            var codes = new List<string>();

            foreach (var code in xDocument.Descendants("code").Select(x => x.Attribute("code")))
            {
                if (code == null || string.IsNullOrWhiteSpace(code.Value))
                {
                    continue;
                }

                codes.Add(code.Value);
            }

            return codes;
        }

        private void ValidateString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
            }
        }

        private void ValidateXDocument(XDocument xDocument)
        {
            if (xDocument == null)
            {
                throw new ArgumentNullException(nameof(xDocument));
            }
        }
    }
}