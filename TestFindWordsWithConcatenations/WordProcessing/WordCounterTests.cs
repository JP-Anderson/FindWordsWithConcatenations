using System;
using FindWordsWithConcatenations.WordProcessing;
using NUnit.Framework;


namespace TestFindWordsWithConcatenations.WordProcessing {
    [TestFixture]
    public class WordCounterTests {
        
        [Test]
        public void WordCounterGivesCorrectWordCountFromString() {
            var wordCounter = WordCounter.CountWordsFromString("this sentence has 7 words in it");
            Assert.AreEqual(1, wordCounter.Dictionary.GetWordCount("this"));
            Assert.AreEqual(1, wordCounter.Dictionary.GetWordCount("sentence"));
            Assert.AreEqual(1, wordCounter.Dictionary.GetWordCount("has"));
            Assert.AreEqual(1, wordCounter.Dictionary.GetWordCount("7"));
            Assert.AreEqual(1, wordCounter.Dictionary.GetWordCount("words"));
            Assert.AreEqual(1, wordCounter.Dictionary.GetWordCount("in"));
            Assert.AreEqual(1, wordCounter.Dictionary.GetWordCount("it"));
        }

        [Test]
        public void WordCounterGivesCorrectWordCountFromFile() {
            var wordCounter = WordCounter.CountWordsFromTestFile("SimpleTextFile.txt");
            Assert.AreEqual(1, wordCounter.Dictionary.GetWordCount("this"));
            Assert.AreEqual(1, wordCounter.Dictionary.GetWordCount("sentence"));
            Assert.AreEqual(1, wordCounter.Dictionary.GetWordCount("has"));
            Assert.AreEqual(1, wordCounter.Dictionary.GetWordCount("7"));
            Assert.AreEqual(1, wordCounter.Dictionary.GetWordCount("words"));
            Assert.AreEqual(1, wordCounter.Dictionary.GetWordCount("in"));
            Assert.AreEqual(1, wordCounter.Dictionary.GetWordCount("it"));
        }

        [Test]
        public void TotalWordCountIsUpdated() {
            var wordCounter = WordCounter.CountWordsFromString("this sentence has the word this in it twice");
            Assert.AreEqual(9, wordCounter.Count());
        }

        [Test]
        public void WordCounterSplitsOnPunctuation() {
            var wordCounter = WordCounter.CountWordsFromString("it}{[] works//()\\.with spaces;#:=+-,-and punctuation!");
            Assert.AreEqual(6, wordCounter.Count());
        }
        
        [Test]
        public void WordCounterSplitsOnNewLine() {
            var wordCounter = WordCounter.CountWordsFromString("it even works with " + Environment.NewLine + " new lines!");
            Assert.AreEqual(6, wordCounter.Count());
        }

        /// <summary>
        /// This test was validated by doing a word count of 'dracula' in the notepad++. It doesn't
        /// include words which might span a new line, e.g. Dra-cula. It is case insensitive.
        /// It does not count "dracula" in the word "dracula's", which Notepad++ does.
        /// </summary>
        [Test]
        public void WordCounterCountsWordsForLargeInput() {
            var wordCounter = WordCounter.CountWordsFromTestFile("stoker-dracula.txt");
            Assert.AreEqual(31, wordCounter.Dictionary.GetWordCount("dracula"));
        }
        
        /// <summary>
        /// Test over 32 copies of Dracula (26MB of text), takes 3.6 secs on my machine.
        /// Left as explicit to keep the tests nice and quick.
        /// </summary>
        [Explicit, Test]
        public void WordCounterCountsWordsForVeryLargeInput() {
            var wordCounter = WordCounter.CountWordsFromTestFile("stoker-dracula-32-times.txt");
            Assert.AreEqual(31 * 32, wordCounter.Dictionary.GetWordCount("dracula"));
        }
        
    }
}