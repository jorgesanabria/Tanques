using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Entity
{
	public class World : IScene
	{
		protected bool _isActive;
		protected Map _map;
		protected List<Entity> _news;
		protected List<Entity> _entities;
		protected List<Entity> _trash;
		public World ()
		{
			_news = new List<Entity> ();
			_entities = new List<Entity> ();
			_trash = new List<Entity> ();
		}
		public void Update(GameTime gameTime)
		{
			CleanTrash ();
			AppendNews ();
			UpdateEntities (gameTime);
			CollideEntities (gameTime);
		}
		public void Draw(SpriteBatch painter, GameTime gameTime)
		{
			if (_map != null)
				_map.Draw (painter, gameTime);
			foreach (var entity in _entities)
				entity.Draw (painter, gameTime);

		}
		public bool IsActive {
			get {
				return _isActive;
			}
			set {
				_isActive = value;
			}
		}
		public Map Map {
			get {
				return _map;
			}
			set {
				_map = value;
			}
		}
		public Entity appendAsynchronous(float delay, Entity entity)
		{
			Append(new AsynchronousEntity(delay, entity));
			return entity;
		}
		public Entity Append(Entity entity)
		{
			_news.Add (entity);
			entity.Initialize ();
			entity.AppendTo (this);
			return entity;
		}
		public Entity Remove(Entity entity)
		{
			_trash.Add (entity);
			entity.OnDestroy ();
			return entity;
		}
		protected void AppendNews()
		{
			foreach (var toInsert in _news) {
				_entities.Add (toInsert);
			}
			_news.Clear ();
		}
		protected void CleanTrash()
		{
			foreach (var trash in _trash) {
				foreach (var entity in _entities) {
					if (trash.Equals (entity)) {
						_entities.Remove (entity);
						break;
					}
				}
			}
			_trash.Clear ();
		}
		protected void UpdateEntities(GameTime gameTime)
		{
			foreach (var entity in _entities)
				entity.Update (gameTime);
		}
		protected void CollideEntities(GameTime gameTime)
		{
			for (int i = 0; i < _entities.Count - 1; i++) {
				for (int j = i + 1; j < _entities.Count; j++) {
					Rectangle rect = Rectangle.Intersect (_entities [i].Box, _entities [j].Box);
					if (!rect.IsEmpty) {
						Entity e1 = _entities [j];
						Entity e2 = _entities [i];
						e1.OnIntercerct (new CollisionRectangle(rect, e1, e2), gameTime);
						e2.OnIntercerct (new CollisionRectangle(rect, e2, e1), gameTime);
					}
				}
			}
		}
	}
}

