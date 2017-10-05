using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using FindWordsWithConcatenations.WordProcessing;

namespace FindWordsWithConcatenations {
	internal class Program {
		public static void Main(string[] args) {
			var path = args[0];
			WordCounter counter = null;
			try {
				counter = WordCounter.CountWordsFromFile(path);
			} catch (FileNotFoundException) {
				Console.WriteLine("Could not find file: {0}", path);
				System.Environment.Exit(1);
			}
			counter.PrintToConsole();
		}
	}
}