namespace Day13
{
	internal class Rule
	{
		public Rule(string person1, string person2, int happyness)
		{
			Person1 = person1;
			Person2 = person2;
			Happyness = happyness;
		}

		public override string ToString()
		{
			return $"{Person1} - {Person2}: {Happyness}";
		}

		public string Person1 { get; }
		public string Person2 { get; }
		public int Happyness { get; }
	}
}