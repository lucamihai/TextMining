using System.Xml.Linq;

namespace TextMining.Services.Interfaces
{
    public interface IXmlService
    {
        XDocument GetXDocumentFromText(string text);
        string GetTextFromAllElements(XDocument xmlDocument, string elementName, string separator = null);
    }
}