using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Day11
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
			var password = ReadInput().ToArray();
			do
			{
				password = IncrementPassword(password, password.Length - 1);
			}
			while (!ValidatePassword(password));

			System.Console.WriteLine($"New Password: {new string(password)}");
		}

		private static char[] IncrementPassword(char[] password, int pos)
		{
			var ch = password[pos];
			if (ch == 'z')
			{
				password[pos] = 'a';
				if (pos > 0)
				{
					IncrementPassword(password, pos - 1);
				}
			}
			else
			{
				password[pos] = ++ch;
			}
			return password;
		}

		private static bool ValidatePassword(char[] password)
		{
			var illegalChar = false;
			var straight3Letters = false;
			var pairs = 0;
			var lastPairChar = 0;
			for (var i = 0; i < password.Length; i++)
			{	
				var c = password[i];

				if (i < password.Length - 2 && password[i + 1] == c + 1 && password[i + 2] == c + 2)
				{
					straight3Letters = true;
				}

				if (c == 'i' || c == 'o' || c == 'l')
				{
					illegalChar = true;
				}

				if (i < password.Length - 1 && password[i + 1] == c && c != lastPairChar)
				{
					lastPairChar = c;
					pairs++;
				}
			}
			return straight3Letters && !illegalChar && pairs >= 2;
		}

		private static string ReadInput()
		{
			return File.ReadAllText("input.txt");
		}
	}
}
