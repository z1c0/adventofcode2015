using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Day16
{
	class Program
	{
		static Dictionary<string, int> _traits = new Dictionary<string, int>()
		{
			{ "children", 3 },
			{ "cats", 7 },
			{ "samoyeds", 2 },
			{ "pomeranians", 3 },
			{ "akitas", 0 },
			{ "vizslas", 0 },
			{ "goldfish", 5 },
			{ "trees", 3 },
			{ "cars", 2 },
			{ "perfumes", 1 },
		};
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
			var sues = ReadInput();
			foreach (var sue in sues)
			{
				if (sue.IsMatchEx(_traits))
				{
					System.Console.WriteLine($"No, {sue} got me the gift!");
				}
			}
		}

		private static void Part1()
		{
			var sues = ReadInput();
			foreach (var sue in sues)
			{
				if (sue.IsMatch(_traits))
				{
					System.Console.WriteLine($"I think {sue} got me the gift ...");
				}
			}
		}

		private static List<Sue> ReadInput()
		{
			var sues = new List<Sue>();
			var lines = File.ReadAllLines("input.txt");
			foreach (var l in lines)
			{
				var i = l.IndexOf(":");
				var name = l.Substring(0, i);
				var traits = l.Substring(i + 2);
				var sue = new Sue(name);
				var tokens = traits.Split(", ");
				foreach (var t in tokens)
				{
					var tokens2 = t.Split(": ");
					sue.Traits.Add(tokens2[0], Int32.Parse(tokens2[1]));
				}
				sues.Add(sue);
			}
			return sues;
		}
	}
}
