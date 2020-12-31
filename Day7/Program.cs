using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace Day7
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("START");
			var sw = Stopwatch.StartNew();
			var signal = Part1("a");
			Part2(signal);
			Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");
		}

		private static void Part2(ushort signal)
		{
			var instructions = ReadInput();
			var a = instructions.SingleOrDefault(i => i.OutWire == "a");
			var b = instructions.SingleOrDefault(i => i.OutWire == "b");
			b.InWire1 = signal.ToString();
			signal = a.Resolve(instructions);
			System.Console.WriteLine($"Wire '{a}' produces signal {signal}");
		}

		private static ushort Part1(string wire)
		{
			var instructions = ReadInput();
			var a = instructions.SingleOrDefault(i => i.OutWire == wire);
			var signal = a.Resolve(instructions);
			System.Console.WriteLine($"Wire '{a}' produces signal {signal}");
			return signal;
		}

		private static List<Instruction> ReadInput()
		{
			var instructions = new List<Instruction>();
			var lines = File.ReadAllLines("input.txt");
			foreach (var l in lines)
			{
				var tokens = l.Split("->");
				var outWire = tokens[1].Trim();

				var op = Operation.None;
				string inWire1 = null;
				string inWire2 = null;
				tokens = tokens[0].Trim().Split(" ");
				if (tokens.Length == 1)
				{
					inWire1 = tokens[0];
				}
				else if (tokens.Length == 2)
				{
					if (tokens[0] != "NOT")
					{
						throw new InvalidProgramException();
					}
					op = Operation.NOT;
					inWire1 = tokens[1];
				}
				else if (tokens.Length == 3)
				{
					if (tokens[1] == "AND")
					{
						op = Operation.AND;
					}
					else if (tokens[1] == "OR")
					{
						op = Operation.OR;
					}
					else if (tokens[1] == "LSHIFT")
					{
						op = Operation.LSHIFT;
					}
					else if (tokens[1] == "RSHIFT")
					{
						op = Operation.RSHIFT;
					}
					else
					{
						throw new InvalidProgramException();
					}
					inWire1 = tokens[0];
					inWire2 = tokens[2];
				}

				instructions.Add(new Instruction(op, inWire1, inWire2, outWire));
			}
			return instructions;
		}
	}
}
