﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

Console.WriteLine("Day 3");
var sw = Stopwatch.StartNew();
Part1();
Part2();
Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");

static void Part1()
{
	var pos = new ValueTuple<int, int>(0, 0);
	var visits = new Dictionary<ValueTuple<int, int>, int>();
	Visit(visits, pos);
	foreach (var c in ReadInput())
	{
		Move(c, ref pos);
		Visit(visits, pos);
	}
	var count = visits.Values.Where(v => v >= 1).Count();
	Console.WriteLine($"{count} houses receive at least one present.");
}

static void Part2()
{
	var pos1 = new ValueTuple<int, int>(0, 0);
	var pos2 = new ValueTuple<int, int>(0, 0);
	var visits = new Dictionary<ValueTuple<int, int>, int>();
	Visit(visits, pos1);
	Visit(visits, pos2);
	bool realSanta = true;
	foreach (var c in ReadInput())
	{
		if (realSanta)
		{
			Move(c, ref pos1);
			Visit(visits, pos1);
		}
		else
		{
			Move(c, ref pos2);
			Visit(visits, pos2);
		}
		realSanta = !realSanta;
	}
	var count = visits.Values.Where(v => v >= 1).Count();
	Console.WriteLine($"{count} houses receive at least one present.");
}


static void Move(char c, ref ValueTuple<int, int> pos)
{
	if (c == 'v')
	{
		pos.Item2++;
	}
	else if (c == '^')
	{
		pos.Item2--;
	}
	else if (c == '<')
	{
		pos.Item1--;
	}
	else if (c == '>')
	{
		pos.Item1++;
	}
}

static void Visit(Dictionary<ValueTuple<int, int>, int> visits, ValueTuple<int, int> pos)
{
	if (visits.ContainsKey(pos))
	{
		visits[pos]++;
	}
	else
	{
		visits.Add(pos, 1);
	}
}

static string ReadInput()
{
	return File.ReadAllText("input.txt");
}
