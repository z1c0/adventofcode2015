using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace Day6
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
			var grid = new int[1000, 1000];
			var instructions = ReadInput();
			foreach (var i in instructions)
			{
				i.ApplyTo(grid);
			}

			long totalBrightness = 0;
			for (var y = 0; y < grid.GetLength(0); y++)
			{
				for (var x = 0; x < grid.GetLength(1); x++)
				{
					totalBrightness += grid[x, y];
				}
			}
			Console.WriteLine($"The total brightness of all lights is {totalBrightness}");
		}

		private static void Part1()
		{
			var grid = new bool[1000, 1000];
			var instructions = ReadInput();
			foreach (var i in instructions)
			{
				i.ApplyTo(grid);
			}

			var count = 0;
			for (var y = 0; y < grid.GetLength(0); y++)
			{
				for (var x = 0; x < grid.GetLength(1); x++)
				{
					if (grid[x, y])
					{
						count++;
					}
				}
			}
			Console.WriteLine($"{count} lights are lit");
		}

		private static List<Instruction> ReadInput()
		{
			var instructions = new List<Instruction>();
			var lines = File.ReadAllLines("input.txt");
			foreach (var l in lines)
			{
				var tokens = l.Split(" ");

				var op = Operation.Toggle;
				var pos = 1;
				if (tokens[0] == "turn")
				{
					pos++;
					op = tokens[1] == "on" ? Operation.On : Operation.Off;
				}

				var tokens2 = tokens[pos].Split(",");
				var x1 = Int32.Parse(tokens2[0]);
				var y1 = Int32.Parse(tokens2[1]);
				tokens2 = tokens[pos + 2].Split(",");
				var x2 = Int32.Parse(tokens2[0]);
				var y2 = Int32.Parse(tokens2[1]);

				instructions.Add(new Instruction(op, x1, y1, x2, y2));
			}
			return instructions;
		}
	}
}
