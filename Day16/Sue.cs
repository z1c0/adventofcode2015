using System;
using System.Collections.Generic;

namespace Day16
{
	internal class Sue
	{
		private string _name;

		public Sue(string name)
		{
			_name = name;
		}

		public Dictionary<string, int> Traits { get; } = new Dictionary<string, int>();

		public override string ToString()
		{
			return _name;	
		}

		internal bool IsMatch(Dictionary<string, int> traits)
		{
			foreach (var t in traits)
			{
				if (Traits.ContainsKey(t.Key))
				{
					if (Traits[t.Key] != t.Value)
					{
						return false;
					}
				}
			}
			return true;
		}

		internal bool IsMatchEx(Dictionary<string, int> traits)
		{
			foreach (var t in traits)
			{
				if (Traits.ContainsKey(t.Key))
				{
					var v = Traits[t.Key];
					if (t.Key == "cats" || t.Key == "trees")
					{
						if (v <= t.Value)
						{
							return false;
						}
					}
					else if (t.Key == "pomeranians" || t.Key == "goldfish")
					{
						if (v >= t.Value)
						{
							return false;
						}
					}
					else
					{
						if (v != t.Value)
						{
							return false;
						}
					}
				}
			}
			return true;
		}
	}
}