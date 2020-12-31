using System.IO;

namespace Day1
{
	class Program
	{
		static void Main(string[] args)
		{
			Part1();
			Part2();
		}

		private static void Part2()
		{
			var floor = 0;
			var pos = 0;
			foreach (var c in ReadInput())
			{
				pos++;
				floor += (c == '(' ? 1 : -1);
				if (floor == -1)
				{
					break;
				}
			}
			System.Console.WriteLine($"pos: {pos}");
		}

		private static void Part1()
		{
			var floor = 0;
			foreach (var c in ReadInput())
			{
				floor += (c == '(' ? 1 : -1);
			}
			System.Console.WriteLine($"floor: {floor}");
		}

		private static string ReadInput()
		{
			return File.ReadAllText("input.txt");
		}
	}
}
