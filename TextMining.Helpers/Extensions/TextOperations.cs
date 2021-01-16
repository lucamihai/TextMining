using System.Linq;

namespace TextMining.Helpers.Extensions
{
    public static class TextOperations
    {
        public static bool IsLetter(this char character)
        {
            return (character >= 'a' && character <= 'z')
                   || (character >= 'A' && character <= 'Z');
        }

        public static bool IsConnectingCharacter(this char character)
        {
            switch (character)
            {
                case '-':
                {
                    return true;
                }
                case '\'':
                {
                    return true;
                }
                default:
                {
                    return false;
                }
            }
        }

        public static bool StringHasAtLeastOneLetter(this string value)
        {
            return value.Any(x => x.IsLetter());
        }

        public static bool StringHasAtLeastOneConnectingCharacter(this string value)
        {
            return value.Any(x => x.IsConnectingCharacter());
        }
    }
}