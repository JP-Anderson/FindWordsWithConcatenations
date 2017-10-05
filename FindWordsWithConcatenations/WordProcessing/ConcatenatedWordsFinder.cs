

using System.Collections.Generic;
using System.ComponentModel;
using FindWordsWithConcatenations.Utilities.Collections;

namespace FindWordsWithConcatenations.WordProcessing {
    public class ConcatenatedWordsFinder {

        // Made these internal, and added InternalsVisibleTo property in AssemblyInfo.cs
        internal IList<CounterDictionary> _stringCountsBySize;
        internal readonly int _stringSize;

        public ConcatenatedWordsFinder(WordCounter wordCounter, int stringSize) {
            _stringSize = stringSize;
            _stringCountsBySize = new List<CounterDictionary>();
            PopulateDictionaries(wordCounter);
        }

        private void PopulateDictionaries(WordCounter wordCounter) {
            for (var i = 0; i < _stringSize; i++) _stringCountsBySize.Add(new CounterDictionary());
            foreach (var wordPair in wordCounter.Dictionary.Words) {
                var length = wordPair.Key.Length;
                if (length > _stringSize) continue;
                _stringCountsBySize[length-1].Add(wordPair.Key, wordPair.Value);
            }
        }
        
    }
}