using System.Collections.Generic;
using System.Xml.Linq;

namespace TextMining.Services.Interfaces
{
    public interface IXmlService
    {
        XDocument GetXDocumentFromText(string text);
        XDocument GetXDocumentFromFile(string filepath);
        string GetTextFromAllElements(XDocument xDocument, string elementName, string separator = null);
        List<string> GetCodesFromXDocument(XDocument xDocument);
    }
}