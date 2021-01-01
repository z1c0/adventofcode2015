using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Day13
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("START");
			var sw = Stopwatch.StartNew();
			Part1(null);
			Part1("Wolfgang");
			Console.WriteLine($"END (after {sw.Elapsed.TotalSeconds} seconds)");
		}

		private static void Part1(string addedPerson)
		{
			var rules = ReadInput();
			
			var unseated = new List<string>();
			foreach (var r in rules)
			{
				if (!unseated.Contains(r.Person1))
				{
					unseated.Add(r.Person1);
				}
			}

			if (!string.IsNullOrEmpty(addedPerson))
			{
				foreach (var person in unseated)
				{
					rules.Add(new Rule(addedPerson, person, 0));
					rules.Add(new Rule(person, addedPerson, 0));
				}
				unseated.Add(addedPerson);
			}

			var maxHappiness = 0;
			Seat(unseated, new List<string>(), rules, ref maxHappiness);
			System.Console.WriteLine($"The maximum happyness is {maxHappiness}");
		}

		private static void Seat(List<string> unseated, List<string> seated, List<Rule> rules, ref int maxHappiness)
		{
			foreach (var person in unseated)
			{
				var unseatedCopy = new List<string>(unseated);
				unseatedCopy.Remove(person);
				var seatedCopy = new List<string>(seated);
				seatedCopy.Add(person);
				Seat(unseatedCopy, seatedCopy, rules, ref maxHappiness);
			}

			if (!unseated.Any())
			{
				var totalHappyness = 0;
				var last = seated.Last();
				foreach (var person in seated)
				{
					var rule1 = rules.Single(r => r.Person1 == person && r.Person2 == last);
					var rule2 = rules.Single(r => r.Person1 == last && r.Person2 == person);
					totalHappyness += rule1.Happyness;
					totalHappyness += rule2.Happyness;
					last = person;
				}

				maxHappiness = Math.Max(maxHappiness, totalHappyness);
			}
		}

		private static List<Rule> ReadInput()
		{
			var rules = new List<Rule>();
			var lines = File.ReadAllLines("input.txt");
			foreach (var l in lines)
			{
				var tokens = l.Split(" ");
				var person1 = tokens[0];
				var person2 = tokens.Last().Remove(tokens.Last().Length - 1);
				var happyness = Int32.Parse(tokens[3]);
				if (tokens[2] == "lose")
				{
					happyness *= -1;
				}
				rules.Add(new Rule(person1, person2, happyness));
			}
			return rules;
		}
	}
}
