using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using aoc;

Console.WriteLine("Day 2");
var sw = Stopwatch.StartNew();
Part1();
Part2();
Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");

static void Part1()
{
	long total = 0;
	foreach (var e in Input.ReadIntIntIntList("x"))
	{
		var a = e.Item1 * e.Item2;
		var b = e.Item2 * e.Item3;
		var c = e.Item1 * e.Item3;
		var d = Math.Min(a, Math.Min(b, c));
		total += 2 * a + 2 * b + 2 * c + d;
	}
	Console.WriteLine($"total paper: {total}");
}

static void Part2()
{
	long total = 0;
	foreach (var (x, y, z) in Input.ReadIntIntIntList("x"))
	{
		var sides = new List<int> { x, y, z };
		sides.Sort();
		var volume = x * y * z;
		var a = sides[0];
		var b = sides[1];
		total += 2 * a + 2 * b + volume;
	}
	Console.WriteLine($"total ribbon: {total}");
}
