using System;
using System.Diagnostics;
using aoc;

Console.WriteLine("Day 1");
var sw = Stopwatch.StartNew();
Part1(false);
Part1(true);
Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");

static void Part1(bool cornersAlwaysOn)
{
	var grid = Input.ReadBoolGrid();
	var iterations = 100;
	var h = grid.Height;
	var w = grid.Width;

	if (cornersAlwaysOn)
	{
		CornersOn(grid);
	}

	var newGrid = grid.Clone();
	for (var i = 0; i < iterations; i++)
	{
		grid.ForEach(c =>
		{
			newGrid[c] = grid[c];
			grid.CountAdjacent8Distinct(c.X, c.Y).TryGetValue(true, out var neighbors);
			if (grid[c])
			{
				if (neighbors != 2 && neighbors != 3)
				{
					newGrid[c] = false;
				}
			}
			else
			{
				if (neighbors == 3)
				{
					newGrid[c] = true;
				}
			}
		});
		(grid, newGrid) = (newGrid, grid);

		if (cornersAlwaysOn)
		{
			CornersOn(grid);
		}
	}

	var count = grid.Count(true);
	Console.WriteLine($"After {iterations} iterations, {count} lights are on.");
}

static void CornersOn(Grid<bool> grid)
{
	var h = grid.Height;
	var w = grid.Width;
	grid[0, 0] = true;
	grid[0, h - 1] = true;
	grid[w - 1, 0] = true;
	grid[w - 1, h - 1] = true;
}
