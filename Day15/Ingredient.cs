using System;

namespace Day15
{
	internal class Ingredient
	{
		public Ingredient(string name, int capacity, int durability, int flavor, int texture, int calories)
		{
			Name = name;
			Capacity = capacity;
			Durability = durability;
			Flavor = flavor;
			Texture = texture;
			Calories = calories;
		}

		public string Name { get; }
		public int Capacity { get; }
		public int Durability { get; }
		public int Flavor { get; }
		public int Texture { get; }
		public int Calories { get; }
	}
}