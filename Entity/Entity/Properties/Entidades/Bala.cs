using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Entity
{
	public class Bala : Entity
	{
		SoundEffect _explosion;
		int _dir = 0;
		Tanque _disparador;
		public Bala ()
		{
		}
		public Tanque Disparador {
			get {
				return _disparador;
			}
			set {
				_disparador = value;
			}
		}
		public int Dir {
			set {
				_dir = value;
			} 
		}
		public override void Initialize()
		{
			//calcular direccion
			if (_dir == 0)
				Direction = Direction.Top;
			else if (_dir == 1)
				Direction = Direction.Bottom;
			else if (_dir == 2)
				Direction = Direction.Right;
			else if (_dir == 3)
				Direction = Direction.Left;

			_animations.Add(Grid.sequence(29));
		}
		public override void Update(GameTime gameTime)
		{
			int x = _box.X + (int)_speed * _direction.X, y = _box.Y + (int)_speed * _direction.Y;
			if (validPosition (x, y) == false) {
				explotar ();
			}
			_box.X = x;
			_box.Y = y;
			//base.Update (gameTime);
		}
		public override void OnIntercerct (CollisionRectangle ev, GameTime gameTime)
		{
			/*if (ev.Entity is Enemigo && _disparador is Enemigo) {
				return;
			}*/
			if (ev.Entity != _disparador) {
				_world.Remove (ev.Entity);
				explotar ();
			}
		}
		protected void explotar()
		{
			_disparador.PermitidoDisparar = true;
			_world.Append (new Explosion () {
				Grid = _grid,
				Box = _box,
				ExplosionSonido = _explosion,
			});
			_world.Remove (this);
			_disparador.PermitidoDisparar = true;
		}
		public SoundEffect Explosion {
			set {
				_explosion = value;
			}
		}
	}
}

