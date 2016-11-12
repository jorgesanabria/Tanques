using System;
using Microsoft.Xna.Framework;

namespace Entity
{
	public class Enemigo : Tanque
	{
		float _ia_time = 0;
		float _ia_time_limit = 0;
		float _ia_fire_time = 0;
		float _ia_fire_time_limit = 0;

		protected Entity _obserbado;
		protected bool _siguiendo_obserbado = false;

		public Enemigo () : base ()
		{
			_direction = Direction.Bottom;
			_ia_time_limit = Rnd.Random (1, 6);
			_ia_fire_time_limit = Rnd.Random (2, 4);
		}
		public override void Update(GameTime gameTime)
		{
			base.Update (gameTime);
			if (_ia_time >= _ia_time_limit) {
				_ia_time = 0;
				_ia_time_limit = Rnd.Random (1, 6);
				switch (Rnd.Random (1, 5)) {
					case 1:
					_direction = Direction.Top;
					break;
					case 2:
					_direction = Direction.Right;
					break;
					case 3:
					_direction = Direction.Bottom;
					break;
					case 4:
					_direction = Direction.Left;
					break;
				}
			}
			float t = (float) gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
			_ia_time += t;
			int x = _box.X + (int) _speed * _direction.X, y = _box.Y + (int) _speed * _direction.Y;
			if (!validPosition (x, _box.Y)) {
				x = _box.X;
				_direction = (Rnd.Random (0, 9) >= 5) ? Direction.Top : Direction.Bottom;
			}
			if (!validPosition (_box.X, y)) {
				y = _box.Y;
				_direction = (Rnd.Random (0, 9) >= 5) ? Direction.Left : Direction.Right;
			}
			_box.X = x;
			_box.Y = y;
			//SeguirPlayer ();
			_ia_fire_time += t;
			if (_ia_fire_time >= _ia_fire_time_limit) {
				_ia_fire_time = 0;
				_ia_fire_time_limit = Rnd.Random (2, 4);
				Disparar ();
			}
		}
		public override void OnIntercerct(CollisionRectangle ev, GameTime gameTime)
		{
			if (ev.Entity is Bala) {
				var bala = (Bala)ev.Entity;
				if (bala.Disparador is Enemigo)
					return;
			}
			int speed = (int) _speed * (int) gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
			if (_direction.IsTop) {
				_direction = Direction.Bottom;
				_box.Y += speed;
			} else if (_direction.IsBottom) {
				_direction = Direction.Top;
				_box.Y -= speed;
			} else if (_direction.IsRight) {
				_direction = Direction.Left;
				_box.X -= speed;
			} else if (_direction.IsLeft) {
				_direction = Direction.Right;
				_box.X += speed;
			}
		}
		public Entity Obserbado {
			set {
				_obserbado = value;
			}
		}
		/*
		protected void SeguirPlayer()
		{
			if (_siguiendo_obserbado) {
				_siguiendo_obserbado = false;
				return;
			}
			if (GetDistance (_obserbado) < 200) {
				if (GetDistanceX (_obserbado) > GetDistanceY (_obserbado)) {
					if (Box.X < _obserbado.Box.X) {
						Direction = Direction.Right;
					} else {
						Direction = Direction.Left;
					}
				} else {
					if (Box.Y < _obserbado.Box.Y) {
						Direction = Direction.Bottom;
					} else {
						Direction = Direction.Top;
					}
				}
				_siguiendo_obserbado = true;
			}
			_siguiendo_obserbado = false;
		}
		*/
		public override void OnDestroy ()
		{
			//insertar nueva entidad
			_box = _newBox;
			_world.appendAsynchronous (5, this);
		}
	}
}

