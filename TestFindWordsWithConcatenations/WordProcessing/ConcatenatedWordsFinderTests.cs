using FindWordsWithConcatenations.WordProcessing;
using NUnit.Framework;

namespace TestFindWordsWithConcatenations.WordProcessing {
    [TestFixture]
    public class ConcatenatedWordsFinderTests {

        private ConcatenatedWordsFinder getSimpleConcatenatedWordsFinder(int targetWordSize) {
            return new ConcatenatedWordsFinder(GetSimpleWordCounter(), targetWordSize);
        }
        
        private WordCounter GetSimpleWordCounter() {
            return WordCounter.CountWordsFromTestFile("ProvidedTestData.txt");
        }

        [Test]
        public void OneDictionaryIsCreatedForEachWordSize() {
            var wordsFinder = getSimpleConcatenatedWordsFinder(targetWordSize: 6);
            Assert.AreEqual(wordsFinder._stringCountsBySize.Count, 6);
        }
        
        
        
    }
}