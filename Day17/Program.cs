using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using aoc;

Console.WriteLine("Day 17");
var sw = Stopwatch.StartNew();
var combinations = Part1();
Part2(combinations);
Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");

static List<List<Container>> Part1()
{
	var containers = ReadInput();
	var combinations = new List<List<Container>>();
	Try(containers, new List<Container>(), 0, combinations);
	Console.WriteLine($"Found {combinations.Count} combinations.");
	return combinations;
}

static void Part2(List<List<Container>> combinations)
{
	var minimumCount = combinations.Min(c => c.Count);
	var count = combinations.Count(c => c.Count == minimumCount);
	Console.WriteLine($"{count} combinations have a minimum container usage.");
}


static void Try(List<Container> freeContainers, List<Container> usedContainers, int capacity, List<List<Container>> combinations)
{
	var target = 150;
	foreach (var container in freeContainers)
	{
		var newCapacity = container.Capacity + capacity;
		if (newCapacity <= target)
		{
			var copyFreeContainers = new List<Container>(freeContainers);
			copyFreeContainers.Remove(container);
			var copyUsedContainers = new List<Container>(usedContainers)
			{
				container
			};

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

static List<Container> ReadInput()
{
	var containers = new List<Container>();
	var lines = File.ReadAllLines("input.txt");
	var pos = 0;
	foreach (var i in Input.ReadIntList())
	{
		containers.Add(new Container(pos++, i));
	}
	return containers;
}