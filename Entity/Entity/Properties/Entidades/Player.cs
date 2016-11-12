using System;
using Microsoft.Xna.Framework;

namespace Entity
{
	public class Player : Tanque
	{
		public Player () : base ()
		{
		}
		public override void Update(GameTime gameTime)
		{
			base.Update (gameTime);
		}
		public override void OnIntercerct(CollisionRectangle ev, GameTime gameTime)
		{
			int speed = (int) _speed * (int) gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
			if (_direction.IsTop) {
				_box.Y += speed;
			} else if (_direction.IsBottom) {
				_box.Y -= speed;
			} else if (_direction.IsRight) {
				_box.X -= speed;
			} else if (_direction.IsLeft) {
				_box.X += speed;
			}
			_direction = Direction.Zero;
		}
		public override void OnDestroy ()
		{
			//_permitidoDisparar = true;
			Datos.Vidas -= 1;
			Datos.Puntage -= 1;
			Datos.Murio = true;
			if (Datos.Vidas > 0) {
				_box = _newBox;
				_world.appendAsynchronous (5, this);
			}
		}
	}
}

