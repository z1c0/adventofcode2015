using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Day10
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("START");
			var sw = Stopwatch.StartNew();
			Part1(40);
			Part1(50);
			Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");
		}

		private static void Part1(int maxRounds)
		{
			var input = ReadInput();
			for (var round = 0; round < maxRounds; round++)
			{
				var result = new StringBuilder(10000);
				for (var i = 0; i < input.Length; i++)
				{
					var count = 1;
					var c = input[i];
					while (i < input.Length - 1 && c == input[i + 1])
					{
						count++;
						i++;
					}
					result.Append(count);
					result.Append(c);
				}
				//System.Console.WriteLine(result);
				input = result.ToString();
				result.Length = 0;
			}
			System.Console.WriteLine($"Length of last result: {input.Length}");
		}

		private static string ReadInput()
		{
			return File.ReadAllText("input.txt");
		}
	}
}
