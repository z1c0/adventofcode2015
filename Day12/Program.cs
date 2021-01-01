using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Day12
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("START");
			var sw = Stopwatch.StartNew();
			Part1();
			Part2();
			Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");
		}

		private static void Part2()
		{
			var json = ReadInput();
			var pos = 0;
			long sum = ParseArrayOrObject(json, ref pos, true);
			System.Console.WriteLine($"Sum of all non-red numbers: {sum}");
		}

		private static long ParseArrayOrObject(string json, ref int pos, bool isObject)
		{
			long sum = 0;
			var isRed = false;
			
			pos++;
			while (pos < json.Length)
			{
				var c = json[pos];
				if (c == '{')
				{
					sum += ParseArrayOrObject(json, ref pos, true);
				}
				else if (c == '[')
				{
					sum += ParseArrayOrObject(json, ref pos, false);
				}
				else if (c == '}')
				{
					if (!isObject)
					{
						throw new InvalidOperationException();
					}
					pos++;
					break;
				}
				else if (c == ']')
				{
					if (isObject)
					{
						throw new InvalidOperationException();
					}
					pos++;
					break;
				}
				else if (c == '-')
				{
					pos++;
					sum -= ParseNumber(json, ref pos);
				}
				else if (Char.IsDigit(c))
				{
					sum += ParseNumber(json, ref pos);
				}
				else
				{
					if (isObject && c == 'r' && pos < json.Length - 2)
					{
						if (json[pos + 1] == 'e' && json[pos + 2] == 'd')
						{
							isRed = true;
						}
					}
					pos++;
				}
			}	

			return isRed ? 0 : sum;
		}

		private static void Part1()
		{
			var json = ReadInput();
			long sum = 0;
			for (var i = 0; i < json.Length; i++)
			{
				var c = json[i];
				if (c == '-')
				{
					i++;
					sum -= ParseNumber(json, ref i);
				}
				else if (Char.IsDigit(c))
				{
					sum += ParseNumber(json, ref i);
				}
			}
			System.Console.WriteLine($"Sum of all numbers: {sum}");
		}

		private static int ParseNumber(string json, ref int pos)
		{
			int number = 0;
			do
			{
				number *= 10;
				number += json[pos++] - '0';
			}
			while (pos < json.Length && Char.IsDigit(json[pos]));
			
			return number;
		}

		private static string ReadInput()
		{
			return File.ReadAllText("input.txt");
		}
	}
}
