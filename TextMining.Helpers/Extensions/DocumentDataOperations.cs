using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TextMining.Entities;

namespace TextMining.Helpers.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class DocumentDataOperations
    {
        // TODO: Refactor
        public static DatasetRepresentation ToDatasetRepresentation(this List<DocumentData> documentDataList)
        {
            var uniqueWords = documentDataList
                .SelectMany(x => x.TextData.WordDictionary.Select(y => y.Key))
                .Distinct()
                .ToList();

            var frequencies = new int[documentDataList.Count, uniqueWords.Count];
            var topics = new List<List<string>>();
            for (var documentIndex = 0; documentIndex < documentDataList.Count; documentIndex++)
            {
                var documentData = documentDataList[documentIndex];

                for (var wordIndex = 0; wordIndex < uniqueWords.Count; wordIndex++)
                {
                    var word = uniqueWords[wordIndex];

                    if (documentData.TextData.WordDictionary.TryGetValue(word, out var frequency))
                    {
                        frequencies[documentIndex, wordIndex] = frequency;
                    }
                }

                var uniqueTopics = documentData
                    .Topics
                    .Distinct()
                    .ToList();
                topics.Add(uniqueTopics);
            }

            return new DatasetRepresentation
            {
                Words = uniqueWords,
                Frequencies = frequencies,
                Topics = topics
            };
        }
    }
}