using System;
using System.Diagnostics;
using System.IO;

namespace Day5
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("START");
			var sw = Stopwatch.StartNew();
			Part1();
			Part2();
			Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");
		}

		private static void Part2()
		{
			var lines = ReadInput();
			var niceStrings = 0;
			foreach (var l in lines)
			{
				var repeatedPair = false;
				var repeatWithSpace = false;
				for (var i = 0; i < l.Length; i++)
				{
					var c = l[i];
					if (!repeatedPair && i < l.Length - 1)
					{
						var cc = l[i + 1];
						for (var j = i + 2; j < l.Length - 1; j++)
						{
							if (c == l[j] && cc == l[j + 1])
							{
								repeatedPair = true;
								break;
							}
						}
					}

					if (!repeatWithSpace && i < l.Length - 2)
					{
						if (c == l[i + 2])
						{
							repeatWithSpace = true;
						}
					}
				}
				if (repeatedPair && repeatWithSpace)
				{
					niceStrings++;
				}
			}
			Console.WriteLine($"Found {niceStrings} nice strings.");
		}

		private static void Part1()
		{
			var lines = ReadInput();
			var niceStrings = 0;
			foreach (var l in lines)
			{
				var doubleChar = false;
				var vowelCount = 0;
				var badCombo = false;
				for (var i = 0; i < l.Length; i++)
				{
					var c = l[i];
					if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u')
					{
						vowelCount++;
					}
					if (i < l.Length - 1)
					{
						var cc = l[i + 1];
						if (c == cc)
						{
							doubleChar = true;
						}
						if (c == 'a' && cc == 'b' || c == 'c' && cc == 'd' || c == 'p' && cc == 'q' || c == 'x' && cc == 'y')
						{
							badCombo = true;
						}
					}
				}
				if (doubleChar && vowelCount >= 3 && !badCombo)
				{
					niceStrings++;
				}
			}
			Console.WriteLine($"Found {niceStrings} nice strings.");
		}

		private static string[] ReadInput()
		{
			return File.ReadAllLines("input.txt");
		}
	}
}
