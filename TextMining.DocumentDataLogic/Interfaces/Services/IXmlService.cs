using System.Collections.Generic;
using System.Xml.Linq;

namespace TextMining.DocumentDataLogic.Interfaces.Services
{
    public interface IXmlService
    {
        XDocument GetXDocumentFromText(string text);
        XDocument GetXDocumentFromFile(string filepath);
        string GetTextFromAllElements(XDocument xDocument, string elementName, string separator = null);
        List<string> GetTopicsFromXDocument(XDocument xDocument);
    }
}