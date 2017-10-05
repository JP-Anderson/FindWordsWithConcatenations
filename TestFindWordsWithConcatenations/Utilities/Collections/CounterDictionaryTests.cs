using System;
using FindWordsWithConcatenations.Utilities.Collections;
using NUnit.Framework;

namespace TestFindWordsWithConcatenations.Utilities.Collections {
    [TestFixture]
    public class CounterDictionaryTests {
        
        [Test]
        public void AddingNewWordToDictionaryGivesCountOfOne() {
            var dictionary = new CounterDictionary();
            dictionary.Add("cheese");
            Assert.AreEqual(1, dictionary.GetWordCount("cheese"));
        }

        [Test]
        public void AddingMultipleWordsIncrementsCount() {
            var dictionary = new CounterDictionary();
            dictionary.Add("cheese");
            dictionary.Add("cheese");
            Assert.AreEqual(2, dictionary.GetWordCount("cheese"));
            dictionary.Add("cheese");
            Assert.AreEqual(3, dictionary.GetWordCount("cheese"));
        }
		
        [Test]
        public void AddingWordsIsCaseInsensitive() {
            var dictionary = new CounterDictionary();
            dictionary.Add("cheese");
            dictionary.Add("CHEESE");
            dictionary.Add("CheEse");
            Assert.AreEqual(3, dictionary.GetWordCount("cheese"));
        }

        [Test]
        public void GetWordCountForUnencounteredWordReturnsMinusOne() {
            var dictionary = new CounterDictionary();
            Assert.AreEqual(-1, dictionary.GetWordCount("cheese"));
        }

        [Test]
        public void CanAddMultipleWords() {
            var dictionary = new CounterDictionary();
            dictionary.Add("cheese");
            dictionary.Add("eggs");
            dictionary.Add("eggs");
            Assert.AreEqual(1, dictionary.GetWordCount("cheese"));
            Assert.AreEqual(2, dictionary.GetWordCount("eggs"));
        }

        [Test]
        public void CanGetListOfKeys() {
            var dictionary = new CounterDictionary();
            dictionary.Add("cheese");
            dictionary.Add("eggs");
            dictionary.Add("eggs");
            Assert.Contains("eggs", dictionary.Keys());
            Assert.Contains("cheese", dictionary.Keys());
        }

        [Test]
        public void AddMultipleAddsCorrectNumberOnFirstAdd() {
            var dictionary = new CounterDictionary();
            dictionary.Add("cheese", 5);
            Assert.AreEqual(5, dictionary.GetWordCount("cheese"));
        }

        [Test]
        public void AddMultipleAddsCorrectNumberOnSecondAdd() {
            var dictionary = new CounterDictionary();
            dictionary.Add("cheese");
            dictionary.Add("cheese", 5);
            Assert.AreEqual(6, dictionary.GetWordCount("cheese"));
        }
    }
}