using System;

internal struct Container : IComparable
{
	internal int Capacity;
	internal int Index;

	public Container(int index, int capacity)
	{
		Index = index;
		Capacity = capacity;
	}

	public int CompareTo(object obj)
	{
		return Index.CompareTo(((Container)obj).Index);
	}

	public override string ToString()
	{
		return $"{Capacity} (@{Index})";
	}
}
