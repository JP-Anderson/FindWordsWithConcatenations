using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using FindWordsWithConcatenations.WordProcessing;

namespace FindWordsWithConcatenations {
	internal class Program {
		public static void Main(string[] args) {
			var path = args[0];
			
			Console.WriteLine("--------- TASK 1 ---------");
			
			WordCounter counter = null;
			try {
				counter = WordCounter.CountWordsFromFile(path);
			} catch (FileNotFoundException) {
				Console.WriteLine("Could not find file: {0}", path);
				System.Environment.Exit(1);
			}
			
			counter.PrintToConsole();
			Console.WriteLine();
			
			Console.WriteLine("--------- TASK 2 ---------");

			var targetStringSize = 6;
			if (args.Length >= 2) {
				var parseOK = int.TryParse(args[1], out targetStringSize);
				if (parseOK) Console.WriteLine("Using supplied target string size of {0}.", targetStringSize);
				else Console.WriteLine("Couldn't parse second argument, using default string size of 6");
			}
			
			var concatenationFinder = new ConcatenatedWordsFinder(counter, targetStringSize);
			concatenationFinder.PrintToConsole();
		}
	}
}