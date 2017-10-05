using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;

namespace FindWordsWithConcatenations.Utilities.Collections {
	/// <summary>
	/// CounterDictionary contains a wordCounts mapping strings to integer counts of the string
	/// </summary>
	public class CounterDictionary {

		private static readonly Regex InvalidCharacterRegex = new Regex("[^a-zA-Z0-9 -]");
		private readonly Dictionary<string, int> _wordCounts;
		private int _totalCount;
		
		public CounterDictionary() {
			_wordCounts = new Dictionary<string, int>();
		}
		
		public Dictionary<string, int> WordCounts => _wordCounts;
		public int TotalCount => _totalCount;

		public void Add(string word) {
			word = SanitiseInput(word);
			if (word == "") return;
			if (Contains(word)) _wordCounts[word]++;
			else _wordCounts.Add(word, 1);
			_totalCount++;
		}

		public void Add(string word, int number) {
			word = SanitiseInput(word);
			if (word == "") return;
			if (Contains(word)) _wordCounts[word] = _wordCounts[word] + number;
			else _wordCounts.Add(word, number);
			_totalCount += number;
		}

		private string SanitiseInput(string input) {
			return InvalidCharacterRegex.Replace(input.ToLower().Trim(), "");
		}

		public bool Contains(string word) { return _wordCounts.ContainsKey(word); }

		public int GetWordCount(string word) {
			try {
				return _wordCounts[word];
			} catch (KeyNotFoundException) {
				return -1;
			}
		}
		
		public Dictionary<string, int>.KeyCollection Keys() {
			return _wordCounts.Keys;
		}
		
	}
}