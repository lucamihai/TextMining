﻿using System;
using System.Collections.Generic;
using TextMining.Services.Interfaces;

namespace TextMining.Services
{
    public class TextAnalyzer : ITextAnalyzer
    {
        public Dictionary<string, int> GetWordFrequenciesFromText(string text)
        {
            ValidateString(text);

            var wordFrequencies = new Dictionary<string, int>();

            // TODO: Consider using StringBuilder
            var currentWord = string.Empty;
            foreach (var character in text)
            {
                if (CharacterIsALetter(character) || CharacterIsAConnectingCharacter(character))
                {
                    currentWord += character;
                }
                else if (currentWord.Length != 0)
                {
                    OnWordEnded(ref currentWord, wordFrequencies);
                }
            }

            return wordFrequencies;
        }

        private static bool CharacterIsAConnectingCharacter(char character)
        {
            switch (character)
            {
                case '-':
                {
                    return true;
                }
                default:
                {
                    return false;
                }
            }
        }

        private static bool CharacterIsALetter(char character)
        {
            return (character >= 'a' && character <= 'z')
                   || (character >= 'A' && character <= 'Z');
        }

        private static void OnWordEnded(ref string word, Dictionary<string, int> wordFrequencies)
        {
            if (wordFrequencies.ContainsKey(word))
            {
                wordFrequencies[word]++;
            }
            else
            {
                wordFrequencies.Add(word, 1);
            }

            word = string.Empty;
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