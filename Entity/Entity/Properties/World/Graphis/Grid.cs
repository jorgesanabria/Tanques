using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Entity
{
	public class Grid
	{
		protected Texture2D _resource;
		protected int _columns;
		protected int _rows;
		protected Rectangle _box;
		public Grid (Texture2D resource, int columns, int rows, Rectangle box)
		{
			_resource = resource;
			_columns = columns;
			_rows = rows;
			_box = box;
		}
		public Texture2D Resource {
			get {
				return _resource;
			}
		}
		public Rectangle this [int index] {
			get {
				if (index < (_rows * _columns) && index >= 0) {
					int x = index % _columns, y = index / _columns;
					return new Rectangle (_box.X + x * _box.Width, _box.Y + y * _box.Height, _box.Width, _box.Height);
				} else {
					return new Rectangle (_box.X, _box.Y, _box.Width, _box.Height);
				}
			}
		}
		public Rectangle[] sequence(params int[] list)
		{
			Rectangle[] sequence = new Rectangle[list.Length];
			for (int i = 0; i < list.Length; i++)
				sequence [i] = this [list[i]];
			return sequence;
		}
	}
}

