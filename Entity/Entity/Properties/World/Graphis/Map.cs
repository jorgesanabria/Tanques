using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Entity
{
	public class Map
	{
		protected int[,] _map;
		protected Rectangle _tile;
		protected Grid _grid;
		public Map (int[,] map, Rectangle tile, Grid grid)
		{
			_map = map;
			_tile = tile;
			_grid = grid;
		}
		public void Draw(SpriteBatch painter, GameTime gameTime)
		{
			painter.Begin ();
			for (int x = 0; x < _map.GetLength(1); x++) {
				for (int y = 0; y < _map.GetLength(0); y++) {
					painter.Draw(
						_grid.Resource,
						new Rectangle(x * _tile.Width, y * _tile.Height, _tile.Width,  _tile.Height),
						_grid [_map [y, x]],
						Color.White
						);
				}
			}
			painter.End ();
		}
		public int Check(Vector2 point)
		{
			int x = (int) point.X / _tile.Width, y = (int) point.Y / _tile.Height;
			return (x >= 0 &&
			        y >= 0 &&
			        y <= _map.GetLength(0) -1 &&
			        x <= _map.GetLength(1) -1
			        )?
				_map[y, x] : -1;
		}
	}
}

