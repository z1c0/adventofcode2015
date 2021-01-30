using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Day20
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
			var input = ReadInput();

			var points = new long[input];
			for (long elf = 1; elf < points.Length; elf++)
			{
				var visitCount = 0;
				for (long house = elf; house < points.Length; house += elf)
				{
					points[house] += elf * 11;
					if (visitCount++ == 50)
					{
						break;
					}
				}
			}
			for (var h = 0; h < points.Length; h++)
			{
				if (points[h] >= input)
				{
					System.Console.WriteLine($"House {h} gets {points[h]} presents.");
					break;
				}
			}
		}

		private static void Part1()
		{
			var input = ReadInput();

			var points = new long[input];
			for (long elf = 1; elf < points.Length; elf++)
			{
				for (long house = elf; house < points.Length; house += elf)
				{
					points[house] += elf * 10;
				}
			}
			for (var h = 0; h < points.Length; h++)
			{
				if (points[h] >= input)
				{
					System.Console.WriteLine($"House {h} gets {points[h]} presents.");
					break;
				}
			}
		}

		private static int ReadInput()
		{
			return Int32.Parse(File.ReadAllText("input.txt"));
		}
	}
}
