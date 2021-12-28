using System.Collections.Generic;
using System.IO;

namespace aoc
{
	public static partial class Input
	{
		private const string DEFAULT_INPUT_FILENAME = "input.txt";
		
		public static IEnumerable<int> ReadIntList(string fileName = DEFAULT_INPUT_FILENAME)
		{
			foreach (var line in File.ReadAllLines(fileName))
			{
				yield return int.Parse(line);
			}
		}

		internal static IEnumerable<(int Int1, int Int2, int Int3)> ReadIntIntIntList(string separator = null, string fileName = DEFAULT_INPUT_FILENAME)
		{
			foreach (var tokens in ReadTokenized(separator, fileName))
			{
				yield return (int.Parse(tokens[0]), int.Parse(tokens[1]), int.Parse(tokens[2]));
			}
		}

		public static IEnumerable<(string String, int Int)> ReadStringIntList(string separator = null, string fileName = DEFAULT_INPUT_FILENAME)
		{
			foreach (var tokens in ReadTokenized(separator, fileName))
			{
				yield return (tokens[0], int.Parse(tokens[1]));
			}
		}

		private static IEnumerable<string[]> ReadTokenized(string separator, string fileName)
		{
			foreach (var line in File.ReadAllLines(fileName))
			{
				var tokens = line.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);
				yield return tokens;
			}
		}
	}
}
