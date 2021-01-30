using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day23
{
	internal enum OpCode
	{
		JIO,
		JIE,
		HLF,
		TPL,
		INC,
		JMP,
	}

	internal record Instruction(OpCode OpCode)
	{
		internal int Offset { get; init; }
		internal int Register { get; init; }
	}

	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("START");
			var sw = Stopwatch.StartNew();
			Part1(0, 0);
			Part1(1, 0);
			Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");
		}

		private static void Part1(uint a, uint b)
		{
			var cpu = new Cpu(a, b);
			cpu.Run(ReadInput().ToList());
		}

		private static IEnumerable<Instruction> ReadInput()
		{
			foreach (var l in File.ReadAllLines("input.txt"))
			{
				var tokens = l.Split(" ");
				var op = tokens[0];
				if (op == "jie")
				{
					yield return new Instruction(OpCode.JIE)
					{
						Register = tokens[1][0] - 'a',
						Offset = Int32.Parse(tokens[2])
					};
				}
				else if (op == "jio")
				{
					yield return new Instruction(OpCode.JIO)
					{
						Register = tokens[1][0] - 'a',
						Offset = Int32.Parse(tokens[2])
					};
				}
				else if (op == "hlf")
				{
					yield return new Instruction(OpCode.HLF)
					{
						Register = tokens[1][0] - 'a'
					};
				}
				else if (op == "tpl")
				{
					yield return new Instruction(OpCode.TPL)
					{
						Register = tokens[1][0] - 'a'
					};
				}
				else if (op == "inc")
				{
					yield return new Instruction(OpCode.INC)
					{
						Register = tokens[1][0] - 'a'
					};
				}
				else if (op == "jmp")
				{
					yield return new Instruction(OpCode.JMP)
					{
						Offset = Int32.Parse(tokens[1])
					};
				}
				else
				{
					throw new InvalidProgramException();
				}
			}
		}
	}
}


