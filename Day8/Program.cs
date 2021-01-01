using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace Day8
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
			var lines = ReadInput();
			long numberOfChars = 0;
			long codeMem = 0;
			long codeMemNew = 0;

			foreach (var l in lines)
			{
				numberOfChars += l.Length;

				var mem = 0;
				for (var i = 1; i < l.Length - 1; i++)
				{
					if (l[i] == '\\')
					{
						if (l[i + 1] == 'x')
						{
							i += 2;
						}
						i++;
					}
					mem++;
				}
				codeMem += mem;

				// new encoding
				mem = 2; // ""
				for (var i = 0; i < l.Length - 0; i++)
				{
					mem++;
					var ch = l[i];
					if (ch == '"' || ch == '\\')
					{
						mem++;
					}
				}
				codeMemNew += mem;
			}
			System.Console.WriteLine($"{numberOfChars} - {codeMem} = {numberOfChars - codeMem}");
			System.Console.WriteLine($"{codeMemNew} - {numberOfChars} = {codeMemNew - numberOfChars}");
		}

		private static string[] ReadInput()
		{
			return File.ReadAllLines("input.txt");
		}
	}
}
