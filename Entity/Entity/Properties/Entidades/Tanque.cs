using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Entity
{
	public class Tanque : Entity
	{
		protected bool _permitidoDisparar = true;
		protected SoundEffect _explosion;
		protected SoundEffect _disparo;

		public Tanque () : base ()
		{

		}
		public override void Initialize()
		{
			_animations.Add (_grid.sequence (16, 7, 6, 5, 4, 3, 2, 1));
			_animations.Add (_grid.sequence (40, 63, 62, 61, 60, 59, 58, 57));
			_animations.Add (_grid.sequence (67, 83, 99, 115, 131, 147, 163, 178));
			_animations.Add (_grid.sequence (192, 208, 224, 240, 256, 272, 288, 305));
		}
		public override void Update (GameTime gameTime)
		{
			if (_direction.IsTop && _animation != 0)
				_animation = 0;
			else if (_direction.IsBottom && _animation != 1)
				_animation = 1;
			else if (_direction.IsRight && _animation != 2)
				_animation = 2;
			else if (_direction.IsLeft && _animation != 3)
				_animation = 3;
			if (!_direction.IsZero) {
				_animation_time += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
			}
			base.Update (gameTime);
		}
		public void Disparar()
		{
			if (!_permitidoDisparar)
				return;
			_disparo.Play ();
			_world.Append (new Bala () {
				Grid = _grid,
				Dir = _animation,
				Disparador = this,
				Speed = 4f,
				Box = _box,
				Explosion = _explosion,
			});
			_permitidoDisparar = false;
		}
		public bool PermitidoDisparar {
			get {
				return _permitidoDisparar;
			}
			set {
				_permitidoDisparar = value;
			}
		}
		public SoundEffect Explosion {
			set {
				_explosion = value;
			}
		}
		public SoundEffect Disparo {
			set {
				_disparo = value;
			}
		}
	}
}

