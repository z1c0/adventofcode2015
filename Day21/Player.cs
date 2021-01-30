using System;
using System.Linq;
using System.Collections.Generic;

namespace Day21
{
	internal class Player
	{

		public Player(int damage, int armor, int hitPoints)
		{
			Damage = damage;
			Armor = armor;
			HitPoints = hitPoints;
		}

		public override string ToString()
		{
			return $"Damage: {Damage}, Armor: {Armor}, Hit Points: {HitPoints}";
		}

		public int EquipmentCost { get; set;}
		public int Damage { get; private set; }
		public int Armor { get; private set; }
		public int HitPoints { get; private set; }
		public bool IsAlive { get => HitPoints > 0; }
		public string Equipment { get; private set; }

		internal void Attack(Player defender)
		{
			var score = Math.Max(1, Damage - defender.Armor);
			defender.HitPoints -= score;
		}

		internal void Equip(List<Item> equipment)
		{
			foreach (var item in equipment)
			{
				Armor += item.Armor;
				Damage += item.Damage;
				EquipmentCost += item.Cost;
			}
			Equipment = string.Join(", ", equipment.Select(e => e.Name));
		}
	}
}