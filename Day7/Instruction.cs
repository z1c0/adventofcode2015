using System;
using System.Linq;
using System.Collections.Generic;

namespace Day7
{
	internal enum Operation
	{
		None,
		NOT,
		AND,
		OR,
		LSHIFT,
		RSHIFT,
	}

	internal class Instruction
	{
		private Nullable<ushort> _signal;
		private Operation _op;

		public string InWire1 { get; set; }

		private string _inWire2;

		public string OutWire { get; }

		public Instruction(Operation op, string inWire1, string inWire2, string outWire)
		{
			_op = op;
			InWire1 = inWire1;
			_inWire2 = inWire2;
			OutWire = outWire;
		}

		public override string ToString()
		{
			return OutWire;
		}

		internal ushort Resolve(List<Instruction> instructions)
		{
			if (!_signal.HasValue)
			{
				switch (_op)
				{
					case Operation.None:
						_signal = ResolveWire(InWire1, instructions);
						break;

					case Operation.NOT:
						_signal = (ushort)(~ResolveWire(InWire1, instructions));
						break;
						
					case Operation.AND:
						_signal = (ushort)(ResolveWire(InWire1, instructions) & ResolveWire(_inWire2, instructions));
						break;

					case Operation.OR:
						_signal = (ushort)(ResolveWire(InWire1, instructions) | ResolveWire(_inWire2, instructions));
						break;

					case Operation.LSHIFT:
						_signal = (ushort)(ResolveWire(InWire1, instructions) << Int32.Parse(_inWire2));
						break;

					case Operation.RSHIFT:
						_signal = (ushort)(ResolveWire(InWire1, instructions) >> Int32.Parse(_inWire2));
						break;
				}
			}
			return _signal.Value;
		}

		private static ushort ResolveWire(string wire, List<Instruction> instructions)
		{
			ushort signal = 0;		
			if (!ushort.TryParse(wire, out signal))
			{
				signal = instructions.SingleOrDefault(i => i.OutWire == wire).Resolve(instructions);
			}
			return signal;
		}
	}
}