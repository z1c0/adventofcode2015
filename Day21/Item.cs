namespace Day21
{
	internal class Item
	{
		public Item(string name, int cost, int damage, int armor)
		{
			Name = name;
			Cost = cost;
			Damage = damage;
			Armor = armor;
		}

		public override string ToString()
		{
			return $"{Name} - Damage: {Damage}, Armor: {Armor}";
		}

		public string Name { get; }
		public int Cost { get; }
		public int Damage { get; }
		public int Armor { get; }
	}
}