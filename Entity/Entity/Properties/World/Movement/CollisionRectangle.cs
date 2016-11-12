using System;
using Microsoft.Xna.Framework;

namespace Entity
{
	public struct CollisionRectangle
	{
		private Rectangle _box;
		private Entity _self;
		private Entity _entity;
		public CollisionRectangle(Rectangle box, Entity self, Entity entity)
		{
			_box = box;
			_self = self;
			_entity = entity;
		}
		public Rectangle Box {
			get {
				return _box;
			}
		}
		public Entity Entity {
			get {
				return _entity;
			}
		}
		public bool IsTop {
			get {
				return _box.Width > _box.Height && _box.Y < _self.Box.Y;
			}
		}
		public bool IsBottom {
			get {
				return _box.Width > _box.Height && _box.Y > _self.Box.Y;
			}
		}
		public bool IsRight {
			get {
				return _box.Width < _box.Height && _box.X > _self.Box.X;
			}
		}
		public bool IsLeft {
			get {
				return _box.Width < _box.Height && _box.X < _self.Box.Y;
			}
		}
	}
}

