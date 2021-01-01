using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace Day9
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("START");
			var sw = Stopwatch.StartNew();
			Part1();
			Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");
		}

		private static void Part1()
		{
			var routes = ReadInput();
			var unvisited = new List<string>();
			foreach (var r in routes)
			{
				if (!unvisited.Contains(r.From))
				{
					unvisited.Add(r.From);
				}
			}

			var shortestDistance = Int32.MaxValue;
			var longestDistance = 0;
			Visit(unvisited, new List<string>(), routes, ref shortestDistance, ref longestDistance);
			System.Console.WriteLine($"Shortest distance is {shortestDistance}");
			System.Console.WriteLine($"Longest distance is {longestDistance}");
		}

		private static void Visit(List<string> unvisited, List<string> visited, List<Route> routes, ref int shortestDistance, ref int longestDistance)
		{
			foreach (var city in unvisited)
			{
				var unvisitedCopy = new List<string>(unvisited);
				unvisitedCopy.Remove(city);
				var visitedCopy = new List<string>(visited);
				visitedCopy.Add(city);
				Visit(unvisitedCopy, visitedCopy, routes, ref shortestDistance, ref longestDistance);
			}

			if (!unvisited.Any())
			{
				var totalDistance = 0;
				var last = string.Empty;
				foreach (var c in visited)
				{
					System.Console.Write($"{c} -> ");
					var route = routes.SingleOrDefault(r => r.From == last && r.To == c);
					if (route != null)
					{
						totalDistance += route.Distance;
					}
					last = c;
				}
				System.Console.WriteLine(totalDistance);
				if (totalDistance < shortestDistance)
				{
					shortestDistance = totalDistance;
				}
				if (totalDistance > longestDistance)
				{
					longestDistance = totalDistance;
				}
			}
		}

		private static List<Route> ReadInput()
		{
			var routes = new List<Route>();
			var lines = File.ReadAllLines("input.txt");
			foreach (var l in lines)
			{
				var tokens = l.Split(" ");
				routes.Add(new Route(tokens[0], tokens[2], Int32.Parse(tokens[4])));
				routes.Add(new Route(tokens[2], tokens[0], Int32.Parse(tokens[4])));
			}
			return routes;
		}
	}
}
