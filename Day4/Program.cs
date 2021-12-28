using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

Console.WriteLine("Day 4");
var sw = Stopwatch.StartNew();
Part1("00000");
Part1("000000");
Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");

static void Part1(string pattern)
{
	var key = ReadInput();
	for (var i = 0; i < int.MaxValue; i++)
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

static string ReadInput()
{
	return File.ReadAllText("input.txt");
}
