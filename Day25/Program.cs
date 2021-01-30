using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day25
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
			var (rows, cols) = ReadInput();
			System.Console.WriteLine($"row: {rows}, col: {cols}");

			long maxIndex = 0;
			var lookupTable = new int[rows, cols];
			var startY = 1;
			for (var y = 0; y < rows; y++)
			{
				startY += y;
				var n = startY;
				for (var x = 0; x < cols; x++)
				{
					maxIndex = Math.Max(maxIndex, n);
					lookupTable[y, x] = n;
					n += (x + y + 2);
				}
			}
			
			var codeList = new long[maxIndex];
			codeList[0] = 20151125;
			for (var i = 1; i < codeList.Length; i++)
			{
				codeList[i] = codeList[i - 1] * 252533;
				codeList[i] %= 33554393;
			}

			var r = rows;
			var c = cols;
			System.Console.WriteLine($"Code at {r}/{c} = {codeList[lookupTable[r - 1, c - 1] - 1]}");

			//Print(lookupTable, codeList);
		}

		private static void Print(int[,] lookupTable, long[] codeList)
		{
			var rows = lookupTable.GetLength(0);
			var cols = lookupTable.GetLength(1);
			for (var y = 0; y < rows; y++)
			{
				for (var x = 0; x < cols; x++)
				{
					var i = lookupTable[y, x];
					//System.Console.Write($" {i} ");
					System.Console.Write($" {codeList[i - 1]} ");
				}
				System.Console.WriteLine();
			}
		}

		private static (int rows, int cols) ReadInput()
		{
			var tokens = File.ReadAllText("input.txt").Split();
			{
				return (
					Int32.Parse(tokens[16][..^1]),
					Int32.Parse(tokens[18][..^1])
				);
			}
		}
	}
}


