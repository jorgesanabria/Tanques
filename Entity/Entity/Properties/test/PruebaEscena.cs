using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Entity
{
	public class PruebaEscena : IScene
	{
		protected bool _isActive = true;
		protected Grid _grilla;
		protected World _world;
		protected Entity _player;
		protected Texture2D _spriteSeheet;
		protected SoundEffect _explocion;
		protected SoundEffect _disparo;
		protected float _tiempoMuerto = 0;
		protected float _segundos = 0;
		public PruebaEscena (ContentManager content)
		{

			_spriteSeheet = content.Load<Texture2D>("Imagenes/Tiles.png");
			_explocion = content.Load<SoundEffect> ("Sonido/rumble.wav");
			_disparo = content.Load<SoundEffect> ("Sonido/gunshot.wav");
			_world = new World ();
			CreateWorld ();
		}
		public void Update(GameTime gameTime)
		{
			float t = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
			_world.Update (gameTime);
			/*if (Datos.Murio && Datos.Vidas > 0) {
				_tiempoMuerto += t;
				if (_tiempoMuerto > 2 && Datos.Murio) {
					_player.X = 200;
					_world.Append (_player);
					Datos.Murio = false;
				}
			}*/
			_segundos += t;
			//si ya paso un segundo
			if (_segundos > 1) {
				Datos.Tiempo += 1;//incrementar tiempo
				_segundos = 0;//setear segundos a cero
				//si pasaron 10 segundos
				if (Datos.Tiempo % 10 == 0) {
					Datos.Puntage += 1;//incrementar puntaje
					//si tiene as de 10 puntos
					if (Datos.Puntage % 10 == 0) {
						Datos.Vidas += 1;//inrementar vidas
					}
				}
			}
			Mando ();
		}
		public void Draw(SpriteBatch painter, GameTime gameTime)
		{
			_world.Draw (painter, gameTime);
		}
		public bool IsActive {
			get {
				return _isActive;
			}
			set {
				_isActive = value;
			}
		}

		protected void CreateWorld()
		{
			var mapa = new int[,]{
				{8,   9,  8,  9,  8,  9,  8,  9,  8,   9},
				{51,  0,  0,  0,  0,  0,  0,  0,  0, 291},
				{53,  0,  0,  0,  0,  0,  0,  0,  0, 307},
				{51,  0,  0,  0,  0,  0, 50,  0,  0, 291},
				{53,  0,  0,  0,  0,  0,  0,  0,  0, 307},
				{51,  0,  0, 50,  0,  0,  0,  0,  0, 291},
				{53,  0,  0,  0,  0,  0,  0,  0,  0, 307},
				{51,  0,  0,  0,  0,  0,  0,  0,  0, 291},
				{53,  0,  0,  0, 50, 50, 50,  0,  0, 307},
				{51,  0,  0,  0,  0,  0,  0,  0,  0, 291},
				{53, 48, 49, 48, 49, 48, 49, 48, 49, 307},
			};
			var grid = new Grid (_spriteSeheet, 16, 20, new Rectangle(0,0,84,84));

			_world = new World ();
			_world.Map = new Map (mapa, new Rectangle (0, 0, 56, 56), grid);
			_player = _world.Append (new Player () { 
				Grid = grid,
				Box =  new Rectangle (200, 150, 50, 50),
				Direction = Direction.Zero,
				Speed = 2,
				Explosion = _explocion,
				Disparo = _disparo,
			});
			_world.Append (new Enemigo () { 
				Grid = grid,
				Box =  new Rectangle (400, 390, 50, 50),
				Direction = Direction.Bottom,
				Speed = 1,
				Obserbado = _player,
				Explosion = _explocion,
				Disparo = _disparo,
			});
			_world.Append (new Enemigo () { 
				Grid = grid,
				Box =  new Rectangle (200, 100, 50, 50),
				Direction = Direction.Bottom,
				Speed = 1,
				Obserbado = _player,
				Explosion = _explocion,
				Disparo = _disparo,
			});
			_world.Append (new Enemigo () { 
				Grid = grid,
				Box =  new Rectangle (150, 400, 50, 50),
				Direction = Direction.Bottom,
				Speed = 1,
				Obserbado = _player,
				Explosion = _explocion,
				Disparo = _disparo,
			});_world.Append (new Enemigo () { 
				Grid = grid,
				Box =  new Rectangle (100, 390, 50, 50),
				Direction = Direction.Bottom,
				Speed = 1,
				Obserbado = _player,
				Explosion = _explocion,
				Disparo = _disparo,
			});

		}
		public void Mando()
		{
			if (Keyboard.GetState ().IsKeyDown (Keys.Up))
				_player.Direction = Direction.Top;
			else if (Keyboard.GetState ().IsKeyDown (Keys.Down))
				_player.Direction = Direction.Bottom;
			else if (Keyboard.GetState ().IsKeyDown (Keys.Left))
				_player.Direction = Direction.Left;
			else if (Keyboard.GetState ().IsKeyDown (Keys.Right))
				_player.Direction = Direction.Right;

			else if (Keyboard.GetState ().IsKeyUp (Keys.Up))
				_player.Direction = Direction.Zero;
			else if (Keyboard.GetState ().IsKeyUp (Keys.Down))
				_player.Direction = Direction.Zero;
			else if (Keyboard.GetState ().IsKeyUp (Keys.Left))
				_player.Direction = Direction.Zero;
			else if (Keyboard.GetState ().IsKeyUp (Keys.Right))
				_player.Direction = Direction.Zero;

			if (Keyboard.GetState ().IsKeyDown (Keys.Space) && !Datos.Murio) {
				var disparador = (Tanque)_player;
				disparador.Disparar ();
			}
		}
	}
}

