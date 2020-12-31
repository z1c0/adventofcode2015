using System;
using System.Collections.Generic;
using System.IO;

namespace Day2
{
	class Program
	{
		static void Main(string[] args)
		{
			Part1();
			Part2();
		}

		private static void Part2()
		{
			var list = ReadInput();
			long total = 0;
			foreach (var e in list)
			{
				var sides = new List<int> { e.Item1, e.Item2, e.Item3 };
				sides.Sort();
				var volume = e.Item1 * e.Item2 * e.Item3;
				var a = sides[0];
				var b = sides[1];
				total += (2 * a + 2 * b + volume);
			}
			System.Console.WriteLine($"total ribbon: {total}");
		}

		private static void Part1()
		{
			var list = ReadInput();
			long total = 0;
			foreach (var e in list)
			{
				var a = e.Item1 * e.Item2;
				var b = e.Item2 * e.Item3;
				var c = e.Item1 * e.Item3;
				var d = Math.Min(a, Math.Min(b, c));
				total += (2 * a + 2 * b + 2 * c + d);
			}
			System.Console.WriteLine($"total paper: {total}");
		}

		private static List<Tuple<int, int, int>> ReadInput()
		{
			var list = new List<Tuple<int, int, int>>();
			var lines = File.ReadAllLines("input.txt");
			foreach (var l in lines)
			{
				var tokens = l.Split("x");
				list.Add(new Tuple<int, int, int>(
					Int32.Parse(tokens[0]),
					Int32.Parse(tokens[1]),
					Int32.Parse(tokens[2])
				));	
			}
			return list;
		}
	}
}
