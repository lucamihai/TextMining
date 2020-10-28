using System.Collections.Generic;

namespace TextMining.Services.Interfaces
{
    public interface IFileService
    {
        string GetAllTextFromFile(string filepath);
        List<string> GetAllLinesFromFile(string filepath);
    }
}