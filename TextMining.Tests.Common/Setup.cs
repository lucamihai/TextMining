using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace TextMining.Tests.Common
{
    [ExcludeFromCodeCoverage]
    public static class Setup
    {
        public static void CreateFileWithText(string filepath, string text)
        {
            File.WriteAllText(filepath, text);
        }
    }
}