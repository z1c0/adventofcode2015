using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Day4
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("START");
			var sw = Stopwatch.StartNew();
			Part1("00000");
			Part1("000000");
			Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");
		}

		private static void Part1(string pattern)
		{
			var key = ReadInput();
			for (var i = 0; i < Int32.MaxValue; i++)
			{
				var input = key + i;
				var bytes = MD5.HashData(Encoding.ASCII.GetBytes(input));
				var text = BitConverter.ToString(bytes).Replace("-", string.Empty);
				if (text.StartsWith(pattern))
				{
					Console.WriteLine($"The answer is: {i}");
					break;
				}
			}
		}

		private static string ReadInput()
		{
			return File.ReadAllText("input.txt");
		}
	}
}
