using System.Collections;
using System.Collections.Generic;
using FindWordsWithConcatenations.WordProcessing;
using NUnit.Framework;

namespace TestFindWordsWithConcatenations.WordProcessing {
    [TestFixture]
    public class ConcatenatedWordsFinderTests {

        [Test]
        public void CorrectWordsAreReturnedForProvidedTestCase() {
            var wordsFinder = new ConcatenatedWordsFinder(GetProvidedTestData2(), targetStringsSize: 6);
            var returnedWords = wordsFinder._stringsToDisplay;
            var expectedWords = new List<string> 
                { "albums", "barely", "befoul", "convex", "hereby", "jigsaw", "tailor", "weaver"};
            foreach (var expectedWord in expectedWords) Assert.Contains(expectedWord, (ICollection) returnedWords);
        }
        
        [Test]
        public void OneDictionaryIsCreatedForEachWordSize() {
            var wordsFinder = new ConcatenatedWordsFinder(GetProvidedTestData1(), targetStringsSize: 6);
            Assert.AreEqual(wordsFinder._stringCountsBySize.Count, 6);
        }
        
        /// <summary>
        /// Test over 32 copies of Dracula (26MB of text), takes 4.2 secs on my machine.
        /// Left as explicit to keep the tests nice and quick.
        /// </summary>
        [Explicit, Test]
        public void WordCounterCountsWordsForVeryLargeInput() {
            var wordCounter = WordCounter.CountWordsFromTestFile("stoker-dracula-32-times.txt");
            var wordsFinder = new ConcatenatedWordsFinder(wordCounter, targetStringsSize: 6);
        }
        
        private static WordCounter GetProvidedTestData1() {
            return WordCounter.CountWordsFromTestFile("ProvidedTestData.txt");
        }
        
        
        private static WordCounter GetProvidedTestData2() {
            return WordCounter.CountWordsFromTestFile("ProvidedTestData2.txt");
        }
        
    }
}