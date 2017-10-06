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
        public void StartWordOfConcatenationAtEndOfInput() {
            var input = "donkey key jaguar caiman monkey mon";
            var wordsFinder = new ConcatenatedWordsFinder(WordCounter.CountWordsFromString(input), targetStringsSize: 6);
            Assert.Contains("monkey", (ICollection) wordsFinder._stringsToDisplay);
        }
        
        [Test]
        public void StartWordOfConcatenationAtStartOfInput() {
            var input = "mon donkey key jaguar caiman monkey";
            var wordsFinder = new ConcatenatedWordsFinder(WordCounter.CountWordsFromString(input), targetStringsSize: 6);
            Assert.Contains("monkey", (ICollection) wordsFinder._stringsToDisplay);
        }
        
        [Test]
        public void EndWordOfConcatenationAtEndOfInput() {
            var input = "donkey mon jaguar caiman monkey key";
            var wordsFinder = new ConcatenatedWordsFinder(WordCounter.CountWordsFromString(input), targetStringsSize: 6);
            Assert.Contains("monkey", (ICollection) wordsFinder._stringsToDisplay);
        }
        
        [Test]
        public void EndWordOfConcatenationAtStartOfInput() {
            var input = "key donkey jaguar caiman mon monkey";
            var wordsFinder = new ConcatenatedWordsFinder(WordCounter.CountWordsFromString(input), targetStringsSize: 6);
            Assert.Contains("monkey", (ICollection) wordsFinder._stringsToDisplay);
        }

        [Test]
        public void OneWordCanBeConcatenatedForMultipleWords() {
            var input = "key donkey don jaguar mon monkey";
            var wordsFinder = new ConcatenatedWordsFinder(WordCounter.CountWordsFromString(input), targetStringsSize: 6);
            // both use key
            Assert.Contains("monkey", (ICollection) wordsFinder._stringsToDisplay);
            Assert.Contains("donkey", (ICollection) wordsFinder._stringsToDisplay);
        }

        [Test]
        public void OneWordCannotBeConcatenatedWithItself() {
            var input = "dodo do";
            var wordsFinder = new ConcatenatedWordsFinder(WordCounter.CountWordsFromString(input), targetStringsSize: 4);
            Assert.IsEmpty((ICollection) wordsFinder._stringsToDisplay);
        }

        [Test]
        public void IdenticalWordsCanBeConcatenated() {
            var input = "dodo do do";
            var wordsFinder = new ConcatenatedWordsFinder(WordCounter.CountWordsFromString(input), targetStringsSize: 4);
            Assert.Contains("dodo", (ICollection) wordsFinder._stringsToDisplay);
        }

        [Test]
        public void OneLetterWordAndFiveLetterWordConcatenationDetected() {
            var input = "jaguar caiman w alrus walrus monkey";
            var wordsFinder = new ConcatenatedWordsFinder(WordCounter.CountWordsFromString(input), targetStringsSize: 6);
            Assert.Contains("walrus", (ICollection) wordsFinder._stringsToDisplay);
        }
        
        [Test]
        public void TwoLetterWordAndFourLetterWordConcatenationDetected() {
            var input = "donkey  wa lrus walrus monkey";
            var wordsFinder = new ConcatenatedWordsFinder(WordCounter.CountWordsFromString(input), targetStringsSize: 6);
            Assert.Contains("walrus", (ICollection) wordsFinder._stringsToDisplay);
        }
        
        [Test]
        public void ThreeWordsConcatenationDetected() {
            var input = "donkey rabbit rab bit";
            var wordsFinder = new ConcatenatedWordsFinder(WordCounter.CountWordsFromString(input), targetStringsSize: 6);
            Assert.Contains("rabbit", (ICollection) wordsFinder._stringsToDisplay);
        }
               
        [Test]
        public void FourLetterWordAndTwoLetterWordConcatenationDetected() {
            var input = "donk ey donkey bobcat";
            var wordsFinder = new ConcatenatedWordsFinder(WordCounter.CountWordsFromString(input), targetStringsSize: 6);
            Assert.Contains("donkey", (ICollection) wordsFinder._stringsToDisplay);
        }
        
        [Test]
        public void FiveLetterWordAndOneLetterWordConcatenationDetected() {
            var input = "gerbil b obcat jaguar caiman bobcat";
            var wordsFinder = new ConcatenatedWordsFinder(WordCounter.CountWordsFromString(input), targetStringsSize: 6);
            Assert.Contains("bobcat", (ICollection) wordsFinder._stringsToDisplay);
        }

        [Test]
        public void ConcatenationsNotCountedIfWordIsBelowTargetStringSize() {
            var input = "deer d eer ape ap e";
            var wordsFinder = new ConcatenatedWordsFinder(WordCounter.CountWordsFromString(input), targetStringsSize: 6);
            Assert.IsEmpty((ICollection) wordsFinder._stringsToDisplay);
        }
        
        [Test]
        public void ConcatenationsNotCountedIfWordIsAboveTargetStringSize() {
            var input = "gorilla gorill a aardvark ardvark";
            var wordsFinder = new ConcatenatedWordsFinder(WordCounter.CountWordsFromString(input), targetStringsSize: 6);
            Assert.IsEmpty((ICollection) wordsFinder._stringsToDisplay);
        }

        [Test]
        public void ConcatenationsDetectedWithShorterTargetString() {
            var input = "deer d eer ape ap e bobcat bob cat";
            var wordsFinder = new ConcatenatedWordsFinder(WordCounter.CountWordsFromString(input), targetStringsSize: 4);
            Assert.Contains("deer", (ICollection) wordsFinder._stringsToDisplay);
            Assert.AreEqual(1, wordsFinder._stringsToDisplay.Count);
        }
        
        [Test]
        public void ConcatenationsDetectedWithLargerTargetString() {
            var input = "deer wob wob begong wobbegong";
            var wordsFinder = new ConcatenatedWordsFinder(WordCounter.CountWordsFromString(input), targetStringsSize: 9);
            Assert.Contains("wobbegong", (ICollection) wordsFinder._stringsToDisplay);
            Assert.AreEqual(1, wordsFinder._stringsToDisplay.Count);
        }
        
        [Test]
        public void ConcatenationsDetectedWithTargetStringOfOne() {
            var input = "a aa a";
            var wordsFinder = new ConcatenatedWordsFinder(WordCounter.CountWordsFromString(input), targetStringsSize: 2);
            Assert.Contains("aa", (ICollection) wordsFinder._stringsToDisplay);
            Assert.AreEqual(1, wordsFinder._stringsToDisplay.Count);
        }

        [Test]
        public void ConcatenationsWithEmptyStringDoesntError() {
            var input = "";
            var wordsFinder = new ConcatenatedWordsFinder(WordCounter.CountWordsFromString(input), targetStringsSize: 2);
            Assert.IsEmpty((ICollection) wordsFinder._stringsToDisplay);
        }
        
        [Test]
        public void OneDictionaryIsCreatedForEachWordSize() {
            var wordsFinder = new ConcatenatedWordsFinder(GetProvidedTestData1(), targetStringsSize: 6);
            Assert.AreEqual(wordsFinder._stringCountsBySize.Count, 6);
        }
        
        /// <summary>
        /// Test over 32 copies of Dracula (26MB of text), takes 3-4 secs on my machine.
        /// Left as explicit to keep the tests nice and quick.
        /// </summary>
        [Explicit, Test]
        public void WordsFinderCanProcessVeryLargeInput() {
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