using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TextMining.DocumentDataLogic.Interfaces.Services;
using TextMining.Helpers;

namespace TextMining.DocumentDataLogic.Services
{
    public class XmlService : IXmlService
    {
        public XDocument GetXDocumentFromText(string text)
        {
            ArgumentValidator.ValidateString(text);

            return XDocument.Parse(text);
        }

        public XDocument GetXDocumentFromFile(string filepath)
        {
            ArgumentValidator.ValidateString(filepath);

            return XDocument.Load(filepath);
        }

        public string GetTextFromAllElements(XDocument xDocument, string elementName, string separator = null)
        {
            ArgumentValidator.ValidateObject(xDocument);
            ArgumentValidator.ValidateString(elementName);

            separator ??= Environment.NewLine;

            return string.Join(separator, xDocument.Descendants(elementName).Select(n => n.Value));
        }

        public List<string> GetTopicsFromXDocument(XDocument xDocument)
        {
            ArgumentValidator.ValidateObject(xDocument);

            var topics = new List<string>();
            var xmlCodesFromTopics = xDocument
                .Descendants("codes")
                .Where(x => x.Attribute("class")?.Value == "bip:topics:1.0")
                .Descendants("code")
                .Select(x => x.Attribute("code"));

            foreach (var code in xmlCodesFromTopics)
            {
                if (code == null || string.IsNullOrWhiteSpace(code.Value))
                {
                    continue;
                }

                topics.Add(code.Value);
            }

            return topics;
        }
    }
}