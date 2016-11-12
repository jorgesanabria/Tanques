using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Entity
{
	public class Entity
	{
		protected World _world;

		protected Color _color = Color.White;
		protected Grid _grid;
		protected List<Rectangle[]> _animations;
		protected int _animation = 0;
		protected int _frame = 0;
		protected float _animation_time = 0;
		protected float _animation_time_limit = 0.06f;

		protected Rectangle _box;
		protected Rectangle _newBox;
		protected Direction _direction;
		protected float _speed = 0;
		public Entity ()
		{
			_direction = Direction.Zero;
			_animations = new List<Rectangle[]> ();
		}
		public Grid Grid {
			get {
				return _grid;
			}
			set {
				_grid = value;
			}
		}
		public Rectangle Box {
			get {
				return _box;
			}
			set {
				_box = value;
			}
		}
		public Rectangle NewBox {
			get {
				return _newBox;
			}
			set {
				_newBox = value;
			}
		}
		public Direction Direction {
			get {
				return _direction;
			}
			set {
				_direction = value;
			}
		}
		public float Speed {
			get {
				return _speed;
			}
			set {
				_speed = value;
			}
		}
		public int X {
			get {
				return _box.X;
			}
			set {
				_box.X = value;
			}
		}
		public int Y {
			get {
				return _box.Y;
			}
			set {
				_box.Y = value;
			}
		}
		/// <summary>
		/// Initialize this instance.
		/// </summary>
		public virtual void Initialize ()
		{
		}
		public virtual void Update (GameTime gameTime)
		{
			int x = _box.X + (int)_speed * _direction.X, y = _box.Y + (int)_speed * _direction.Y;
			if (validPosition (x, _box.Y) == false)
				x = _box.X;
			if (validPosition (_box.X, y) == false)
				y = _box.Y;
			if (x < 0 || y < 0)
				return;
			_box.X = x;
			_box.Y = y;
		}
		public virtual void Draw(SpriteBatch painter, GameTime gameTime)
		{
			if (_animation_time >= _animation_time_limit) {
				_animation_time = 0;
				_frame = (_frame < _animations[_animation].Length - 1) ? _frame + 1 : 0;
			}
			painter.Begin ();
			painter.Draw (_grid.Resource, _box, _animations[_animation][_frame], _color);
			painter.End ();
		}

		/// <summary>
		/// Ons the intercerct.
		/// </summary>
		/// <param name="ev">Ev.</param>
		/// <param name="gameTime">Game time.</param>
		public virtual void OnIntercerct (CollisionRectangle ev, GameTime gameTime)
		{
		}
		public virtual void OnDestroy ()
		{
		}

		/// <summary>
		/// Valids the position.
		/// </summary>
		/// <returns><c>true</c>, if position was valided, <c>false</c> otherwise.</returns>
		/// <param name="map">Map.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		protected bool validPosition(int x, int y)
		{
			//comprobar coliciones con el mapa
			if (_world.Map.Check (new Vector2(x, y)) != 0)
				return false;
			else if (_world.Map.Check (new Vector2(x + _box.Width, y)) != 0)
				return false;
			else if (_world.Map.Check (new Vector2(x, y + _box.Height)) != 0)
				return false;
			else if (_world.Map.Check (new Vector2(x + _box.Width, y + _box.Height)) != 0)
				return false;
			return true;
		}
		public void AppendTo(World world)
		{
			_world = world;
		}
		public double GetDistance(Entity entity)
		{
			var dx = Box.X - entity.Box.X;
			var dy = Box.Y - entity.Box.Y;
			return Math.Sqrt (dx*dx+dy*dy);
		}
		public double GetDistanceX(Entity entity)
		{
			return (double) Math.Abs(Box.X - entity.Box.X);
		}
		public double GetDistanceY(Entity entity)
		{
			return (double) Math.Abs(Box.Y - entity.Box.Y);
		}
	}
}