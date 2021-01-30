using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day24
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("START");
			var sw = Stopwatch.StartNew();
			Part1(3);
			Part1(4);
			Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");
		}

		private static void Part1(int parts)
		{
			var packages = ReadInput().Reverse().ToList();
			var sum = packages.Sum();
			var targetWeight = sum / parts;
			System.Console.WriteLine($"Target weight per group: {targetWeight}");

			var maxUsed = Int32.MaxValue;
			var QE = long.MaxValue;
			Try(packages, new List<int>(), targetWeight, ref maxUsed, ref QE);
			System.Console.WriteLine($"QE: {QE}");
		}

		private static void Try(List<int> packages, List<int> packagesUsed, int targetWeight, ref int maxUsed, ref long QE)
		{
			foreach (var p in packages)
			{
				var packagesCopy = new List<int>(packages);
				packagesCopy.Remove(p);
				var packagesUsedCopy = new List<int>(packagesUsed);
				packagesUsedCopy.Add(p);
				
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

		private static IEnumerable<int> ReadInput()
		{
			foreach (var l in File.ReadAllLines("input.txt"))
			{
				yield return Int32.Parse(l);
			}
		}
	}
}


