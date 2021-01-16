using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace TextMining.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class ArgumentValidator
    {
        public static void ValidateObject(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
        }

        public static void ValidateString(string filepath)
        {
            if (string.IsNullOrWhiteSpace(filepath))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(filepath));
            }
        }

        public static void ValidateExistingFilepath(string filepath)
        {
            ValidateString(filepath);

            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException(filepath);
            }
        }

        public static void ValidateNotEmptyList<T>(List<T> list)
        {
            ValidateObject(list);

            if (list.Count == 0)
            {
                throw new ArgumentException(nameof(list));
            }
        }
    }
}