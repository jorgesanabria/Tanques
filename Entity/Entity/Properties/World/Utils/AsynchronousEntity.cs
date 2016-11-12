using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq.Expressions;

namespace Entity
{
	public class AsynchronousEntity : Entity
	{
		protected float _delay;
		protected Entity _entity;
		protected float _segundos = 0;
		public AsynchronousEntity (float delay, Entity entity)
		{
			_delay = delay;
			_entity = entity;
			_box.X = -100;
			_box.Y = -100;
		}
		public override void Update(GameTime gameTime)
		{
			_segundos += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
			if (_segundos > _delay) {
				_segundos = 0;
				_world.Append (_entity);
				_world.Remove (this);
				if (_entity is Player) {
					Datos.Murio = false;
				}
			}
			return;
		}
		public override void Draw(SpriteBatch painter, GameTime gameTime)
		{
			return;
		}
	}
}

