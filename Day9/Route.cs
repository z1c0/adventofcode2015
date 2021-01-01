namespace Day9
{
	internal class Route
	{
		public Route(string from, string to, int distance)
		{
			From = from;
			To = to;
			Distance = distance;
		}

		public string From { get; }
		public string To { get; }
		public int Distance { get; }

		public override string ToString()
		{
			return $"{From} -> {To}: {Distance}";
		}
	}
}