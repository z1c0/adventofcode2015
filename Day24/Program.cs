using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using aoc;

Console.WriteLine("Day 24");
var sw = Stopwatch.StartNew();
Part1(3);
Part1(4);
Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");


static void Part1(int parts)
{
	var packages = Input.ReadIntList().Reverse().ToList();
	var sum = packages.Sum();
	var targetWeight = sum / parts;
	Console.WriteLine($"Target weight per group: {targetWeight}");

	var maxUsed = int.MaxValue;
	var QE = long.MaxValue;
	Try(packages, new List<int>(), targetWeight, ref maxUsed, ref QE);
	Console.WriteLine($"QE: {QE}");
}

static void Try(List<int> packages, List<int> packagesUsed, int targetWeight, ref int maxUsed, ref long QE)
{
	foreach (var p in packages)
	{
		var packagesCopy = new List<int>(packages);
		packagesCopy.Remove(p);
		var packagesUsedCopy = new List<int>(packagesUsed) { p };

		long qe = 1;
		foreach (var pp in packagesUsedCopy)
		{
			qe *= pp;
		}

		if (packagesUsedCopy.Sum() < targetWeight && packagesUsedCopy.Count < maxUsed)
		{
			Try(packagesCopy, packagesUsedCopy, targetWeight, ref maxUsed, ref QE);
		}
		else if (packagesUsedCopy.Sum() == targetWeight)
		{
			if (packagesUsedCopy.Count < maxUsed)
			{
				maxUsed = packagesUsedCopy.Count;
				QE = qe;
			}
			else if (packagesUsedCopy.Count == maxUsed)
			{
				QE = Math.Min(QE, qe);
			}
		}
	}
}


