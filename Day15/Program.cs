using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Day15
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("START");
			var sw = Stopwatch.StartNew();
			Part1(null);
			Part1(500);
			Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");
		}

		private static void Part1(Nullable<int> calorieTarget)
		{
			var ingredients = ReadInput();
			var sprinkles = ingredients.Single(i => i.Name == "Sprinkles");
			var butterscotch = ingredients.Single(i => i.Name == "Butterscotch");
			var chocolate = ingredients.Single(i => i.Name == "Chocolate");
			var candy = ingredients.Single(i => i.Name == "Candy");
			var topScore = long.MinValue;
			for (var s = 100; s >= 0; s--)
			{
				var rest1 = 100 - s;
				for (var b = rest1; b >= 0; b--)
				{
					var rest2 = 100 - s - b;
					for (var c = rest2; c >= 0; c--)
					{
						var rest3 = 100 - s - b - c;
						for (var ca = rest3; ca >= 0; ca--)
						{
							var total = s + b + c + ca;
							if (total == 100)
							{
								var include = true;
								if (calorieTarget.HasValue)
								{
									var calories = sprinkles.Calories * s + butterscotch.Calories * b + chocolate.Calories * c + candy.Calories * ca;
									include = calories == calorieTarget.Value;
								}
								if (include)
								{
									long capacity = sprinkles.Capacity * s + butterscotch.Capacity * b + chocolate.Capacity * c + candy.Capacity * ca;
									long durability = sprinkles.Durability * s + butterscotch.Durability * b + chocolate.Durability * c + candy.Durability * ca;
									long flavor = sprinkles.Flavor * s + butterscotch.Flavor * b + chocolate.Flavor * c + candy.Flavor * ca;
									long texture = sprinkles.Texture * s + butterscotch.Texture * b + chocolate.Texture * c + candy.Texture * ca;
									var score = Math.Max(0, capacity) * Math.Max(0, durability) * Math.Max(0, flavor) * Math.Max(0, texture);
									topScore = Math.Max(topScore, score);
								}
							}
						}
					}
				}
			}
			System.Console.WriteLine($"Top-scoring cookie: {topScore}");
		}

		private static List<Ingredient> ReadInput()
		{
			var ingredients = new List<Ingredient>();
			var lines = File.ReadAllLines("input.txt");
			foreach (var l in lines)
			{
				var tokens = l.Split(":");
				var name = tokens[0];
				tokens = tokens[1].Trim().Split(", ");
				var capacity = Int32.Parse(tokens[0].Split(" ")[1]);
				var durability = Int32.Parse(tokens[1].Split(" ")[1]);
				var flavor = Int32.Parse(tokens[2].Split(" ")[1]);
				var texture = Int32.Parse(tokens[3].Split(" ")[1]);
				var calories = Int32.Parse(tokens[4].Split(" ")[1]);
				ingredients.Add(new Ingredient(name, capacity, durability, flavor, texture, calories));
			}
			return ingredients;
		}
	}
}
