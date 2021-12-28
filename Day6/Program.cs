using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using aoc;

Console.WriteLine("Day 6");
var sw = Stopwatch.StartNew();
Part1();
Part2();
Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");

static void Part1()
{
	var grid = new Grid<bool>(1000, 1000);
	foreach (var i in ReadInput())
	{
		i.ApplyTo(grid);
	}
	Console.WriteLine($"{grid.Count(true)} lights are lit");
}

static void Part2()
{
	var grid = new Grid<int>(1000, 1000);
	var instructions = ReadInput();
	foreach (var i in instructions)
	{
		i.ApplyTo(grid);
	}

	long totalBrightness = 0;
	grid.ForEach(c => totalBrightness += grid[c]);
	Console.WriteLine($"The total brightness of all lights is {totalBrightness}");
}


static List<Instruction> ReadInput()
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
		var x1 = int.Parse(tokens2[0]);
		var y1 = int.Parse(tokens2[1]);
		tokens2 = tokens[pos + 2].Split(",");
		var x2 = int.Parse(tokens2[0]);
		var y2 = int.Parse(tokens2[1]);

		instructions.Add(new Instruction(op, x1, y1, x2, y2));
	}
	return instructions;
}
