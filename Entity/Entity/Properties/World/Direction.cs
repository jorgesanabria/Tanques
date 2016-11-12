using System;

namespace Entity
{
	public struct Direction
	{
		private int _x;
		private int _y;
		public Direction(int x, int y)
		{
			_x = (x == 0)? 0 : (x > 0)? 1 : -1;
			_y = (y == 0)? 0 : (y > 0)? 1 : -1;
		}
		public int X {
			get {
				return _x;
			}
		}
		public int Y {
			get {
				return _y;
			}
		}
		public bool IsTop {
			get {
				return _y == -1 && _x == 0;
			}
		}
		public bool IsBottom {
			get {
				return _y == 1 && _x == 0;
			}
		}
		public bool IsRight {
			get {
				return _x == 1 && _y == 0;
			}
		}
		public bool IsLeft {
			get {
				return _x == -1 && _y == 0;
			}
		}
		public bool IsZero {
			get {
				return _x == 0 && _y == 0;
			}
		}
		public static Direction Top {
			get {
				return new Direction (0, -1);
			}
		}
		public static Direction Bottom {
			get {
				return new Direction (0, 1);
			}
		}
		public static Direction Right {
			get {
				return new Direction (1, 0);
			}
		}
		public static Direction Left {
			get {
				return new Direction (-1, 0);
			}
		}
		public static Direction Zero {
			get {
				return new Direction (0, 0);
			}
		}
	}
}

