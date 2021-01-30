using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Day21
{
	class Program
	{
		private static List<Item> _weapons = new List<Item>
		{
			new Item("Dagger",      8, 4, 0),
			new Item("Shortsword", 10, 5, 0),
			new Item("Warhammer",  25, 6, 0),
			new Item("Longsword",  40, 7, 0),
			new Item("Greataxe",   74, 8, 0),
		};
		private static List<Item> _armor = new List<Item>
		{
			new Item("None",        0, 0, 0),
			new Item("Leather",    13, 0, 1),
			new Item("Chainmail",  31, 0, 2),
			new Item("Splintmail", 53, 0, 3),
			new Item("Bandedmail", 75, 0, 4),
			new Item("Platemail", 102, 0, 5),
		};
		private static List<Item> _rings = new List<Item>
		{
			new Item("None",        0, 0, 0),
			new Item("Damage +1",  25, 1, 0),
			new Item("Damage +2",  50, 2, 0),
			new Item("Damage +3", 100, 3, 0),
			new Item("Defense +1", 20, 0, 1),
			new Item("Defense +2", 40, 0, 2),
			new Item("Defense +3", 80, 0, 3),
		};

		static void Main(string[] args)
		{
			Console.WriteLine("START");
			var sw = Stopwatch.StartNew();
			Part1();
			Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");
		}

		private static void Part1()
		{
			var combinations = new List<List<Item>>();
			foreach (var w in _weapons)
			{
				var equipment = new List<Item>();
				equipment.Add(w);

				foreach (var a in _armor)
				{
					var equipment2 = new List<Item>(equipment);
					equipment2.Add(a);

					foreach (var r1 in _rings)
					{
						var equipment3 = new List<Item>(equipment2);
						equipment3.Add(r1);

						foreach (var r2 in _rings)
						{
							var equipment4 = new List<Item>(equipment3);
							if (r2 != r1)
							{
								equipment4.Add(r2);
								combinations.Add(equipment4);
							}
						}

						combinations.Add(equipment3);
					}

					combinations.Add(equipment2);
				}

				combinations.Add(equipment);
			}
			System.Console.WriteLine($"Equipment combinations: {combinations.Count}");

			var minWinCost = Int32.MaxValue;
			var maxLoseCost = 0;
			foreach (var e in combinations)
			{
				var player = new Player(0, 0, 100);
				player.Equip(e);
				var boss = ReadInput();

				while (true)
				{
					player.Attack(boss);
					if (!boss.IsAlive)
					{
						break;
					}
					boss.Attack(player);
					if (!player.IsAlive)
					{
						break;
					}
				}

				if (boss.IsAlive)
				{
					//System.Console.WriteLine($"The boss wins (player equipment cost: {player.EquipmentCost}).");
					maxLoseCost = Math.Max(maxLoseCost, player.EquipmentCost);
				}
				else
				{
					//System.Console.WriteLine($"You win with an equipment worth of {player.EquipmentCost}");
					minWinCost = Math.Min(minWinCost, player.EquipmentCost);
				}
			}

			System.Console.WriteLine($"Minimum equipment cost for a win {minWinCost}");
			System.Console.WriteLine($"Maximum equipment cost for a loss {maxLoseCost}");
		}

		private static Player ReadInput()
		{
			var lines = File.ReadAllLines("input.txt");
			return new Player(
				Int32.Parse(lines[1].Split(": ")[1]),
				Int32.Parse(lines[2].Split(": ")[1]),
				Int32.Parse(lines[0].Split(": ")[1])
			);
		}
	}
}


