using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Day17
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("START");
			var sw = Stopwatch.StartNew();
			var combinations = Part1();
			Part2(combinations);
			Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");
		}

		private static void Part2(List<List<Container>> combinations)
		{
			var minimumCount = combinations.Min(c => c.Count);
			var count = combinations.Count(c => c.Count == minimumCount);
			System.Console.WriteLine($"{count} combinations have a minimum container usage.");
		}

		private static List<List<Container>> Part1()
		{
			var containers = ReadInput();
			var combinations = new List<List<Container>>();
			Try(containers, new List<Container>(), 0, combinations);
			System.Console.WriteLine($"Found {combinations.Count} combinations.");
			return combinations;
		}

		private static void Try(List<Container> freeContainers, List<Container> usedContainers, int capacity, List<List<Container>> combinations)
		{
			var target = 150;
			foreach (var container in freeContainers)
			{
				var newCapacity = container.Capacity + capacity;
				if (newCapacity <= target)
				{
					var copyFreeContainers = new List<Container>(freeContainers);
					copyFreeContainers.Remove(container);
					var copyUsedContainers = new List<Container>(usedContainers);
					copyUsedContainers.Add(container);

					if (newCapacity == target)
					{
						copyUsedContainers.Sort();
						var found = false;
						foreach (var c in combinations)
						{
							if (c.SequenceEqual(copyUsedContainers))
							{
								found = true;
								break;
							}
						}
						if (!found)
						{
							combinations.Add(copyUsedContainers);
						}
					}

					Try(copyFreeContainers, copyUsedContainers, newCapacity, combinations);
				}
			}
		}

		private static List<Container> ReadInput()
		{
			var containers = new List<Container>();
			var lines = File.ReadAllLines("input.txt");
			var pos = 0;
			foreach (var l in lines)
			{
				containers.Add(new Container(pos++, Int32.Parse(l)));
			}
			return containers;
		}
	}
}
