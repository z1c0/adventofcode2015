using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Day19
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("START");
			var sw = Stopwatch.StartNew();
			Part1();
			Part2(); // does not terminate
			Part2_FromInternet();
			Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");
		}

		private static void Part2_FromInternet()
		{
			var input = ReadInput();
			var text = input.Item2;
			
			Func<string, int> countStr = x =>
			{
				var count = 0;
				for (var index = text.IndexOf(x); index >= 0; index = text.IndexOf(x, index + 1), ++count) { }
				return count;
			};

			var num = text.Count(char.IsUpper) - countStr("Rn") - countStr("Ar") - 2 * countStr("Y") - 1;
			System.Console.WriteLine($"Fully replaced after step {num}");
		}

		private static void Part2()
		{
			var input = ReadInput();
			var list = input.Item1.OrderByDescending(t => t.Item2.Length).ToList();
			var text = input.Item2;
			var cache = new Dictionary<string, bool>();
			
			Replace(text, list, 0, cache);
		}

		private static bool Replace(string text, List<Tuple<string, string>> list, int step, Dictionary<string, bool> cache)
		{
			if (cache.ContainsKey(text))
			{
				return cache[text];
			}

			if (text == "e")
			{
				System.Console.WriteLine($"Fully replaced after step {step}");
				return true;
			}			
			var match = false;
			foreach (var t in list)
			{
				var i = text.IndexOf(t.Item2);
				while (i >= 0)
				{
					var sb = new StringBuilder(text);
					sb.Remove(i, t.Item2.Length);
					sb.Insert(i, t.Item1);
					var n = sb.ToString();
					match = Replace(n, list, step + 1, cache);
					cache[n] = match;
					i = text.IndexOf(t.Item2, i + 1);
				}
			}
			return match;
		}

		private static void Part1()
		{
			var input = ReadInput();
			var list = input.Item1;
			var text = input.Item2;
			var history = new SortedSet<string>();
			var sb = new StringBuilder();
			foreach (var t in list)
			{
				var i = text.IndexOf(t.Item1);
				while (i >= 0)
				{
					sb.Length = 0;
					sb.Append(text);
					sb.Remove(i, t.Item1.Length);
					sb.Insert(i, t.Item2);
					var tmp = sb.ToString();
					if (!history.Contains(tmp))
					{
						history.Add(tmp);
					}
					i = text.IndexOf(t.Item1, i + 1);
				}
			}

			System.Console.WriteLine($"{history.Count} distinct molecules can be created.");
		}
		private static Tuple<List<Tuple<string, string>>, string> ReadInput()
		{
			var lines = File.ReadAllLines("input.txt");
			var list = new List<Tuple<string, string>>();
			for (var i = 0; i < lines.Length - 2; i++)
			{
				var l = lines[i];
				var tokens = l.Split(" => ");
				list.Add(new Tuple<string, string>(tokens[0], tokens[1]));
			}
			return new Tuple<List<Tuple<string, string>>, string>(list, lines.Last());
		}
	}
}
