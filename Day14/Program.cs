using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Day14
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
			var reindeer = ReadInput();
			for (var i = 0; i < 2503; i++)
			{
				foreach (var r in reindeer)
				{
					r.Simulate();
				}
				var max = reindeer.Max(r => r.Distance);
				var leads = reindeer.Where(r => r.Distance == max);
				foreach (var r in leads)
				{
					r.Score++;
				}
			}
			var winner = reindeer.OrderBy(r => r.Score).Last();
			System.Console.WriteLine($"Winner is {winner} with a score of {winner.Score}");
		}

		private static void Part1()
		{
			var reindeer = ReadInput();
			for (var i = 0; i < 2503; i++)
			{
				foreach (var r in reindeer)
				{
					r.Simulate();
				}
			}
			var winner = reindeer.OrderBy(r => r.Distance).Last();
			System.Console.WriteLine($"Winner is {winner} with a distance of {winner.Distance}");
		}

		private static List<Reindeer> ReadInput()
		{
			var reindeer = new List<Reindeer>();
			var lines = File.ReadAllLines("input.txt");
			foreach (var l in lines)
			{
				var tokens = l.Split(" ");
				var name = tokens[0];
				var speed = Int32.Parse(tokens[3]);
				var stamina = Int32.Parse(tokens[6]);
				var rest = Int32.Parse(tokens[13]);
				reindeer.Add(new Reindeer(name, speed, stamina, rest));
			}
			return reindeer;
		}
	}
}
