using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;

namespace FindWordsWithConcatenations.Utilities.Collections {
	/// <summary>
	/// CounterDictionary contains a wordCounts mapping strings to integer counts of the string
	/// </summary>
	public class CounterDictionary {
		
		private readonly Dictionary<string, int> wordCounts;
		private static readonly Regex invalidCharacterRegex = new Regex("[^a-zA-Z0-9 -]");

		public CounterDictionary() {
			wordCounts = new Dictionary<string, int>();
		}
		
		public Dictionary<string, int> Words => wordCounts;

		public void Add(string word) {
			word = SanitiseInput(word);
			if (word == "") return;
			if (Contains(word)) wordCounts[word]++;
			else wordCounts.Add(word, 1);
		}

		public void Add(string word, int number) {
			word = SanitiseInput(word);
			if (word == "") return;
			if (Contains(word)) wordCounts[word] = wordCounts[word] + number;
			else wordCounts.Add(word, number);
		}

		private string SanitiseInput(string input) {
			return invalidCharacterRegex.Replace(input.ToLower().Trim(), "");
		}

		public bool Contains(string word) { return wordCounts.ContainsKey(word); }

		public int GetWordCount(string word) {
			try {
				return wordCounts[word];
			} catch (KeyNotFoundException) {
				return -1;
			}
		}

		public Dictionary<string, int>.KeyCollection Keys() {
			return wordCounts.Keys;
		}
		
	}
}