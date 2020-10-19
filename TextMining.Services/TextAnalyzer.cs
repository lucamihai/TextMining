using System;
using System.Collections.Generic;
using System.Linq;
using TextMining.Entities;
using TextMining.Services.Interfaces;

namespace TextMining.Services
{
    public class TextAnalyzer : ITextAnalyzer
    {
        public TextData GetTextDataFromText(string text)
        {
            ValidateString(text);

            var textData = new TextData
            {
                WordDictionary = new Dictionary<string, int>(),
                AcronymDictionary = new Dictionary<string, int>()
            };
            
            // TODO: Consider using StringBuilder
            // TODO: Investigate how words like "don't, won't" will be treated (probably the word will be the string without "n't" at the end)
            var currentWord = string.Empty;
            foreach (var character in text)
            {
                if (CharacterIsALetter(character) || CharacterIsAConnectingCharacter(character))
                {
                    currentWord += character;
                }
                else
                {
                    OnWordEnded(ref currentWord, textData);
                }
            }

            return textData;
        }

        private static bool CharacterIsALetter(char character)
        {
            return (character >= 'a' && character <= 'z')
                   || (character >= 'A' && character <= 'Z');
        }

        private static bool CharacterIsAConnectingCharacter(char character)
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

        private static bool StringHasAtLeastOneLetter(string value)
        {
            return value.Any(CharacterIsALetter);
        }

        private static bool StringHasAtLeastOneConnectingCharacter(string value)
        {
            return value.Any(CharacterIsAConnectingCharacter);
        }

        private static void OnWordEnded(ref string word, TextData textData)
        {
            if (StringHasAtLeastOneConnectingCharacter(word))
            {
                var words = SplitStringByConnectingCharacters(word);
                foreach (var wordFromSplit in words)
                {
                    AddWordIfValid(wordFromSplit, textData);
                }
            }
            else
            {
                AddWordIfValid(word, textData);
            }

            word = string.Empty;
        }

        private static void AddWordIfValid(string word, TextData textData)
        {
            if (!IsValidWord(word))
            {
                return;
            }

            if (IsAcronym(word))
            {
                AddWordToDictionary(word, textData.AcronymDictionary);
            }
            else
            {
                AddWordToDictionary(word, textData.WordDictionary);
            }
        }

        private static bool IsValidWord(string word)
        {
            return word.Length != 0
                   && StringHasAtLeastOneLetter(word);
        }

        private static bool IsAcronym(string word)
        {
            return word.All(x => x >= 'A' && x <= 'Z');
        }

        private static void AddWordToDictionary(string word, Dictionary<string, int> dictionary)
        {
            if (dictionary.ContainsKey(word))
            {
                dictionary[word]++;
            }
            else
            {
                dictionary.Add(word, 1);
            }
        }

        private static string[] SplitStringByConnectingCharacters(string value)
        {
            return value.Split(new char[]{'-', '\''}, StringSplitOptions.RemoveEmptyEntries);
        }


        private void ValidateString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
            }
        }
    }
}
