using System;
using aoc;

internal enum Operation
{
	On,
	Off,
	Toggle
}
internal class Instruction
{
	private readonly Operation _op;
	private readonly int _x1;
	private readonly int _y1;
	private readonly int _x2;
	private readonly int _y2;

	public Instruction(Operation op, int x1, int y1, int x2, int y2)
	{
		_op = op;
		_x1 = x1;
		_y1 = y1;
		_x2 = x2;
		_y2 = y2;
	}
	internal void ApplyTo(Grid<int> grid)
	{
		for (var y = _y1; y <= _y2; y++)
		{
			for (var x = _x1; x <= _x2; x++)
			{
				switch (_op)
				{
					case Operation.On:
						grid[x, y]++;
						break;

					case Operation.Off:
						grid[x, y] = Math.Max(0, grid[x, y] - 1);
						break;

					case Operation.Toggle:
						grid[x, y] += 2;
						break;
				}
			}
		}
	}

	internal void ApplyTo(Grid<bool> grid)
	{
		for (var y = _y1; y <= _y2; y++)
		{
			for (var x = _x1; x <= _x2; x++)
			{
				switch (_op)
				{
					case Operation.On:
						grid[x, y] = true;
						break;

					case Operation.Off:
						grid[x, y] = false;
						break;

					case Operation.Toggle:
						grid[x, y] = !grid[x, y];
						break;
				}
			}
		}
	}
}