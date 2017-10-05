using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FindWordsWithConcatenations.Utilities.Collections;

namespace FindWordsWithConcatenations.WordProcessing {
	public class WordCounter {
		
		private readonly CounterDictionary _wordCounts;
		private List<string> _sortedWords;
		
		public static WordCounter CountWordsFromString(string text) { return new WordCounter(text); }

		public static WordCounter CountWordsFromTestFile(string filename) {
			var path = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
			return new WordCounter(File.ReadAllText(path + @"\..\TestData\" + filename));
		}

		public static WordCounter CountWordsFromFile(string filepath) {
			var path = Path.Combine(filepath);
			return new WordCounter(File.ReadAllText(path));
		}
		
		private WordCounter(string text) {
			_wordCounts = new CounterDictionary();
			CountWords(text);
			SortAlphabetically();
		}

		public CounterDictionary Dictionary => _wordCounts;

		public int Count() {
			return _wordCounts.TotalCount;
		}

		public void PrintToConsole() {
			Console.WriteLine("Here are the words and their counts in alphabetical order:");
			foreach (var word in _sortedWords) {
				Console.WriteLine("{0} {1}", word, _wordCounts.WordCounts[word]);
			}
		}

		private void CountWords(string text) {
			var words = text.Split(new char[] {' ', ',', '.', '\n', '\t', '-'});
			foreach (var word in words) {
				_wordCounts.Add(word);
			}
		}

		private void SortAlphabetically() {
			_sortedWords = _wordCounts.WordCounts.Keys.ToList();
			_sortedWords.Sort();
		}
	}
}