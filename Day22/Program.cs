using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Day22
{
	internal enum SpellType
	{
		MagicMissile,
		Drain,
		Shield,
		Poison,
		Recharge
	}
	internal class Wizard
	{
		private int _rechargeActive;
		private int _poisonActive;
		private int _shieldActive;

		public bool IsAlive { get => Mana > 0 && HitPoints > 0; }
		public int HitPoints { get; set; }
		public int Mana { get; private set; }
		public int ManaSpent { get; private set; }
		public int Armor { get; private set; }

		public Wizard(int hitPoints, int mana)
		{
			HitPoints = hitPoints;
			Mana = mana;
		}

		public override string ToString()
		{
			return $"Player has {HitPoints} hit points, {Armor} armor, {Mana} mana";
		}

		internal Wizard Clone()
		{
			var clone = new Wizard(HitPoints, Mana);
			clone.Armor = Armor;
			clone.ManaSpent = ManaSpent;
			clone._rechargeActive = _rechargeActive;
			clone._poisonActive = _poisonActive;
			clone._shieldActive = _shieldActive;
			return clone;
		}

		internal bool CanUse(SpellType spellType)
		{
			switch (spellType)
			{
				case SpellType.Shield:
					return _shieldActive <= 1;
				case SpellType.Poison:
					return _poisonActive <= 1;
				case SpellType.Recharge:
					return _rechargeActive <= 1;
			}
			return true;
		}

		internal void UseSpell(SpellType spellType, Boss boss)
		{
			Program.Output($"Player casts {spellType}.");

			var cost = 0;
			switch (spellType)
			{
				case SpellType.MagicMissile:
					cost = 53;
					break;
				case SpellType.Drain:
					cost = 73;
					break;
				case SpellType.Shield:
					cost = 113;
					break;
				case SpellType.Poison:
					cost = 173;
					break;
				case SpellType.Recharge:
					cost = 229;
					break;
			}
			Mana -= cost;
			ManaSpent += cost;

			if (IsAlive)
			{
				switch (spellType)
				{
					case SpellType.MagicMissile:
						boss.HitPoints -= 4;
						break;
					case SpellType.Drain:
						boss.HitPoints -= 2;
						HitPoints += 2;
						break;
					case SpellType.Shield:
						Armor = 7;
						_shieldActive = 6;
						break;
					case SpellType.Poison:
						_poisonActive = 6;
						break;
					case SpellType.Recharge:
						_rechargeActive = 5;
						break;
				}
			}
		}

		internal void ApplyEffects(Boss boss)
		{
			if (_shieldActive > 0)
			{
				_shieldActive--;
				if (_shieldActive == 0)
				{
					Armor = 0;
				}
			}
			if (_poisonActive > 0)
			{
				_poisonActive--;
				boss.HitPoints -= 3;
				Program.Output($"Poison deals 3 damage; its timer is now {_poisonActive}");
			}
			if (_rechargeActive > 0)
			{
				_rechargeActive--;
				Mana += 101;
			}
		}
	}

	internal class Boss
	{
		public Boss(int hitPoints, int damage)
		{
			HitPoints = hitPoints;
			Damage = damage;
		}

		public override string ToString()
		{
			return $"Boss has {HitPoints} hit points";
		}

		public int HitPoints { get; set; }
		public int Damage { get; set; }
		public bool IsAlive { get => HitPoints > 0; }

		internal Boss Clone()
		{
			return new Boss(HitPoints, Damage);
		}

		internal void Attack(Wizard wizard)
		{
			var damage = Math.Max(1, Damage - wizard.Armor);
			Program.Output($"Boss attacks for {damage} damage.");
			wizard.HitPoints -= damage;
		}
	}

	class Program
	{
		private static bool _outputOn = false;

		static void Main(string[] args)
		{
			Console.WriteLine("START");
			var sw = Stopwatch.StartNew();
			Part1(false);
			Part1(true);
			Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");
		}

		private static void Part1(bool hardMode)
		{
			var wizard = new Wizard(50, 500);
			var boss = ReadInput();

			var minManaCost = Int32.MaxValue;
			Round(wizard, boss, hardMode, ref minManaCost);
			System.Console.WriteLine($"Minimum mana cost: {minManaCost}");
		}

		private static void Round(Wizard wizard, Boss boss, bool hardMode, ref int minManaCost)
		{
			var spellTypes = new List<SpellType>  {
				SpellType.MagicMissile,
				SpellType.Drain,
				SpellType.Shield,
				SpellType.Poison,
				SpellType.Recharge,
			};

			foreach (var spellType in spellTypes)
			{
				if (wizard.CanUse(spellType))
				{
					var copyWizard = wizard.Clone();
					var copyBoss = boss.Clone();

					Fight(copyWizard, spellType, copyBoss, hardMode, ref minManaCost);

					if (copyWizard.IsAlive && copyBoss.IsAlive)
					{
						Round(copyWizard, copyBoss, hardMode, ref minManaCost);
					}
				}
			}
		}
		private static void Fight(Wizard wizard, SpellType spellType, Boss boss, bool hardMode, ref int minManaCost)
		{
			// Wizard Turn
			Output("-- Wizard turn --");
			Output(wizard.ToString());
			Output(boss.ToString());
			if (hardMode)
			{
				wizard.HitPoints--;
				if (!wizard.IsAlive)
				{
					Output("Wizard is dead.");
					return;
				}
			}
			wizard.ApplyEffects(boss);
			if (boss.IsAlive)
			{
				wizard.UseSpell(spellType, boss);
				if (!wizard.IsAlive)
				{
					Output("Wizard is dead.");
					return;
				}
				Output();

				// Boss Turn
				Output("-- Boss turn --");
				Output(wizard.ToString());
				Output(boss.ToString());
				wizard.ApplyEffects(boss);
				if (boss.IsAlive)
				{
					boss.Attack(wizard);
				}
			}

			if (!boss.IsAlive)
			{
				Output($"Wizard is dead; mana spent: {wizard.ManaSpent}");
				minManaCost = Math.Min(minManaCost, wizard.ManaSpent);
				return;
			}
			Output();
		}

		internal static void Output(string s = null)
		{
			if (_outputOn)
			{
				System.Console.WriteLine(s);
			}
		}

		private static Boss ReadInput()
		{
			var lines = File.ReadAllLines("input.txt");
			return new Boss(
				Int32.Parse(lines[0].Split(": ")[1]),
				Int32.Parse(lines[1].Split(": ")[1])
			);
		}
	}
}


