using System;
using System.Diagnostics;
using System.IO;

Console.WriteLine("Day 1");
var sw = Stopwatch.StartNew();
Part1();
Part2();
Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");

static void Part1()
{
	var floor = 0;
	foreach (var c in ReadInput())
	{
		floor += (c == '(' ? 1 : -1);
	}
	Console.WriteLine($"floor: {floor}");
}

static void Part2()
{
	var floor = 0;
	var pos = 0;
	foreach (var c in ReadInput())
	{
		pos++;
		floor += (c == '(' ? 1 : -1);
		if (floor == -1)
		{
			break;
		}
	}
	Console.WriteLine($"pos: {pos}");
}

static string ReadInput()
{
	return File.ReadAllText("input.txt");
}
