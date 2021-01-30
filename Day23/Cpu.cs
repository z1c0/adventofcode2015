using System;
using System.Collections.Generic;
using System.Linq;

namespace Day23
{
	internal class Cpu
	{
		private uint[] _registers = new uint[2];

		public Cpu(uint a, uint b)
		{
			_registers[0] = a;
			_registers[1] = b;
		}
		
		internal void Run(List<Instruction> instructions)
		{
			var pc =  0;
			while (true)
			{
				var i = instructions.ElementAtOrDefault(pc);
				if (i == null)
				{
					System.Console.WriteLine($"Register A: {_registers[0]}");
					System.Console.WriteLine($"Register B: {_registers[1]}");
					break;
				}
				switch (i.OpCode)
				{
					case OpCode.HLF:
						_registers[i.Register] /= 2;
						pc++;
						break;
					case OpCode.TPL:
						_registers[i.Register] *= 3;
						pc++;
						break;
					case OpCode.INC:
						_registers[i.Register]++;
						pc++;
						break;
					case OpCode.JIE:
						if (_registers[i.Register] % 2 == 0)
						{
							pc += i.Offset;
						}
						else
						{
							pc++;
						}
						break;
					case OpCode.JIO:
						if (_registers[i.Register] == 1)
						{
							pc += i.Offset;
						}
						else
						{
							pc++;
						}
						break;
					case OpCode.JMP:
						pc += i.Offset;
						break;
					default:
						throw new InvalidOperationException();
				}
			}
		}
	}
}