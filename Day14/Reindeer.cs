using System;

namespace Day14
{
	internal class Reindeer
	{
		private bool _isFlying;
		private string _name;
		private int _speed;
		private int _stamina;
		private int _flyTime;
		private int _rest;
		private int _restTime;

		public long Distance { get; set; }
		public int Score { get; internal set; }

		public Reindeer(string name, int speed, int stamina, int rest)
		{
			_isFlying = true;
			_name = name;
			_speed = speed;
			_stamina = stamina;
			_flyTime = stamina;
			_rest = rest;
			_restTime = rest;
			Distance = 0;
		}

		public override string ToString()
		{
			return _name;
		}

		internal void Simulate()
		{
			if (_isFlying)
			{
				Distance += _speed;
				_flyTime--;
				if (_flyTime == 0)
				{
					_flyTime = _stamina;
					_isFlying = false;
				}
			}
			else
			{
				_restTime--;
				if (_restTime == 0)
				{
					_restTime = _rest;
					_isFlying = true;
				}
			}
		}
	}
}