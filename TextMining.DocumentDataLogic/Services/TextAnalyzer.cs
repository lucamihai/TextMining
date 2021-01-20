using System;
using System.Collections.Generic;
using System.Linq;
using TextMining.DocumentDataLogic.Interfaces.Services;
using TextMining.Entities;
using TextMining.Helpers;
using TextMining.Helpers.Extensions;

namespace TextMining.DocumentDataLogic.Services
{
    public class TextAnalyzer : ITextAnalyzer
    {
        private List<string> stopWords;
        private readonly IStemmingService stemmingService;

        public TextAnalyzer(IStemmingService stemmingService)
        {
            this.stemmingService = stemmingService;
        }

        public TextData GetTextDataFromText(string text, List<string> stopWords)
        {
            ArgumentValidator.ValidateString(text);
            ArgumentValidator.ValidateObject(stopWords);

            this.stopWords = stopWords;

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
                if (character.IsLetter() || character.IsConnectingCharacter())
                {
                    currentWord += character;
                }
                else
                {
                    OnWordEnded(ref currentWord, textData);
                }
            }

            textData.WordDictionary = textData
                .WordDictionary
                .OrderBy(x => x.Key)
                .ToDictionary(keyItem => keyItem.Key, valueItem => valueItem.Value);

            return textData;
        }

        private void OnWordEnded(ref string word, TextData textData)
        {
            word = word.ToLower();

            if (!stopWords.Contains(word))
            {
                if (word.StringHasAtLeastOneConnectingCharacter())
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
            }

            word = string.Empty;
        }

        private void AddWordIfValid(string word, TextData textData)
        {
            if (word.Contains("the"))
            {

            }

            if (!IsValidWord(word))
            {
                return;
            }

            if (stopWords.Contains(word))
            {
                return;
            }

            if (IsAcronym(word))
            {
                textData.AcronymDictionary.AddWordToDictionary(word);
            }
            else
            {
                textData.WordDictionary.AddWordToDictionary(stemmingService.GetStemmedWord(word));
            }
        }

        private bool IsValidWord(string word)
        {
            return word.Length > 2
                   && word.StringHasAtLeastOneLetter()
                   && !stopWords.Contains(word);
        }

        private static bool IsAcronym(string word)
        {
            return word.Length > 1 && word.All(x => x >= 'A' && x <= 'Z');
        }

        private static string[] SplitStringByConnectingCharacters(string value)
        {
            return value.Split(new char[]{'-', '\''}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
