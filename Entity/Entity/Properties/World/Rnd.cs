using System;
using System.Security.Cryptography;

namespace Entity
{
	public class Rnd
	{
		protected static RandomNumberGenerator _rand;
		static Rnd()
		{
			_rand = RandomNumberGenerator.Create ();
		}
		public static int Random(int min, int max)
		{
			if (min > max)
				throw new ArgumentOutOfRangeException("min");
			byte[] bytes = new byte[4]; 

			_rand.GetBytes(bytes);

			uint next = BitConverter.ToUInt32(bytes, 0);

			int range = max - min;

			return (int)((next % range) + min);
		}
	}
}

