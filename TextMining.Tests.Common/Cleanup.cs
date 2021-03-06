﻿using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace TextMining.Tests.Common
{
    [ExcludeFromCodeCoverage]
    public static class Cleanup
    {
        public static void DeleteFileIfExists(string filepath)
        {
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }
    }
}
