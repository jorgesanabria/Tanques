using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Entity
{
	public class Explosion : Entity
	{
		float _tiempo_de_vida = 0.3f;
		float _tiempo_viva = 0f;
		bool explotando = false;

		SoundEffect _explosion;

		public Explosion ()
		{
			_animation_time_limit = 0.09f;
		}
		public override void Initialize ()
		{
			_animations.Add(Grid.sequence(25, 26, 27));
		}
		public override void Update (GameTime gameTime)
		{
			//base.Update (gameTime);
			if (explotando == false) {
				explotando = true;
				_explosion.Play ();
			}
			if (_tiempo_viva > _tiempo_de_vida) {
				_world.Remove (this);
			}
			_tiempo_viva += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
			_animation_time += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
		}
		public SoundEffect ExplosionSonido {
			set {
				_explosion = value;
			}
		}
	}
}

