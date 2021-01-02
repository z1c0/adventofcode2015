using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Day18
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("START");
			var sw = Stopwatch.StartNew();
			Part1(false);
			Part1(true);
			Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");
		}

		private static void Part1(bool cornersAlwaysOn)
		{
			var grid = ReadInput();
			var iterations = 100;
			var h = grid.GetLength(0);
			var w = grid.GetLength(1);

			if (cornersAlwaysOn)
			{
				CornersOn(grid);
			}

			for (var i = 0; i < iterations; i++)
			{
				for (var y = 0; y < h; y++)
				{
					for (var x = 0; x < w; x++)
					{
						grid[x, y].NewState = grid[x, y].State;
						var neighbors = CountNeighbors(x, y, grid);
						if (grid[x, y].State)
						{
							if (neighbors != 2 && neighbors != 3)
							{
								grid[x, y].NewState = false;
							}
						}
						else
						{
							if (neighbors == 3)
							{
								grid[x, y].NewState = true;
							}
						}
					}
				}

				for (var y = 0; y < h; y++)
				{
					for (var x = 0; x < w; x++)
					{
						grid[y, x].State = grid[y, x].NewState;
					}
				}
			  if (cornersAlwaysOn)
				{
					CornersOn(grid);
				}
			}

			var count = 0;
			for (var y = 0; y < h; y++)
			{
				for (var x = 0; x < w; x++)
				{
					if (grid[x, y].State)
					{
						count++;
					}
				}
			}

			System.Console.WriteLine($"After {iterations} iterations, {count} lights are on.");
		}

		private static void CornersOn(Light[,] grid)
		{
			var h = grid.GetLength(0);
			var w = grid.GetLength(1);
			grid[0, 0].State = true;
			grid[0, h - 1].State = true;
			grid[w - 1, 0].State = true;
			grid[w - 1, h -1].State = true;
		}

		private static int CountNeighbors(int x, int y, Light[,] grid)
		{
			var count = 0;
			var h = grid.GetLength(0);
			var w = grid.GetLength(1);
			for (var yy = y - 1; yy <= y + 1; yy++)
			{
				for (var xx = x - 1; xx <= x + 1; xx++)
				{
					if (xx >= 0 && yy >= 0 && xx < w && yy < h)
					{
						if (x != xx || y != yy)
						{
							if (grid[xx, yy].State)
							{
								count++;
							}
						}
					}
				}
			}
			return count;
		}

		private static Light[,] ReadInput()
		{
			var lines = File.ReadAllLines("input.txt");
			var grid = new Light[lines.Length, lines.First().Length];
			for (var y = 0; y < lines.Length; y++)
			{
				var l = lines[y];
				for (var x = 0; x < l.Length; x++)
				{
					grid[x, y].State = l[x] == '#';
				}
			}
			return grid;
		}
	}
}
