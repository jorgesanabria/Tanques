using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Entity
{
	public interface IScene
	{
		void Update(GameTime gameTime);
		void Draw(SpriteBatch painter, GameTime gameTime);
		bool IsActive { get; set; }
	}
}

