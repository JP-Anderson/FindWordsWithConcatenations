

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FindWordsWithConcatenations.Utilities;
using FindWordsWithConcatenations.Utilities.Collections;

namespace FindWordsWithConcatenations.WordProcessing {
    public class ConcatenatedWordsFinder {

        // Made these internal, and added InternalsVisibleTo property in AssemblyInfo.cs
        internal readonly IList<CounterDictionary> _stringCountsBySize;
        internal readonly int _targetStringsSize;
        internal readonly IList<string> _stringsToDisplay;

        public ConcatenatedWordsFinder(WordCounter wordCounter, int targetStringsSize) {
            _targetStringsSize = targetStringsSize;
            _stringCountsBySize = new List<CounterDictionary>();
            _stringsToDisplay = new List<string>();
            PopulateDictionaries(wordCounter);
            foreach (var word in DictionaryOfWordsOfLengthN(targetStringsSize).WordCounts) {
                if (FindAnyConcatenationOfTwoStrings(word.Key)) _stringsToDisplay.Add(word.Key);
            }
        }

        public void PrintToConsole() {
            Console.WriteLine("The following strings can be assembled from concatenating two other strings in the input:");
            foreach(var word in _stringsToDisplay) Console.WriteLine(word);
        }

        private void PopulateDictionaries(WordCounter wordCounter) {
            for (var i = 0; i < _targetStringsSize; i++) _stringCountsBySize.Add(new CounterDictionary());
            foreach (var wordPair in wordCounter.Dictionary.WordCounts) {
                var length = wordPair.Key.Length;
                if (length > _targetStringsSize) continue;
                _stringCountsBySize[length-1].Add(wordPair.Key, wordPair.Value);
            }
        }
        
        private CounterDictionary DictionaryOfWordsOfLengthN(int n) { return _stringCountsBySize[n - 1]; }
        
        private bool FindAnyConcatenationOfTwoStrings(string targetString) {
            var iterations = _targetStringsSize - 2;
            var midPoint = FindMidPoint();
            for (var i = 0; i <= iterations; i++) {
                var startDictionary = _stringCountsBySize[i].WordCounts;
                if (i == midPoint) {
                    // If we are in the mid point then we are trying to concatenate equal length strings.
                    // So we will use the same word dictionary for both sides of the concatenation.
                    if (CanMakeWordFromOneDictionary(targetString, startDictionary)) return true;
                } else {
                    // The lengths of the concatenated strings are different.
                    // We will use two dictionaries with the different length strings.
                    var endDictionary = _stringCountsBySize[iterations - i].WordCounts;
                    if (CanMakeWordFromTwoDictionaries(targetString, startDictionary, endDictionary)) return true;
                }
            }
            return false;
        }

        private int FindMidPoint() {
            if (NumericMethods.IsEven(_targetStringsSize)) return _targetStringsSize / 2 - 1;
            return -1;
        }
        
        private bool CanMakeWordFromTwoDictionaries(string target, Dictionary<string, int> startWords, Dictionary<string, int> endWords) {
            if (startWords.Count == 0 || endWords.Count == 0) return false;
            foreach (var startWord in startWords.Keys) {
                if (target.Substring(0, startWord.Length) != startWord) continue;
                foreach (var endWord in endWords.Keys) {
                    if (endWord == target.Substring(startWord.Length, endWord.Length)) {
                        return true;
                    }
                }
            }
            return false;
        }
        
        private bool CanMakeWordFromOneDictionary(string target, Dictionary<string, int> words) {
            if (words.Count == 0) return false;
            var wordList =  new List<string>(words.Keys);
            for (var i = 0; i < wordList.Count; i++) {
                var startWord = wordList[i];
                if (target.Substring(0, startWord.Length) != startWord) continue;
                for (var j = 0; j < wordList.Count; j++) {
                    // When comparing equal length strings, we have a special case to ensure we are not concatenating
                    // the same word with itself, unless it actually appears multiple times!
                    if (i == j && words[startWord] <= 1) continue;
                    var endWord = wordList[j];
                    if (endWord == target.Substring(startWord.Length, endWord.Length)) {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}