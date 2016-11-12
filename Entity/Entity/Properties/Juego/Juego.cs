using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Entity
{
	public class Juego : IScene
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
		public Juego (ContentManager content)
		{
			_spriteSeheet = content.Load<Texture2D>("Imagenes/Tiles.png");
			_explocion = content.Load<SoundEffect> ("Sonido/rumble.wav");
			_disparo = content.Load<SoundEffect> ("Sonido/gunshot.wav");
			_world = new World ();
			CreateWorld ();
		}
		protected void CreateWorld()
		{
			var mapa = new int[,]{
				{8,   9,  8,  0,  8,  9,  8,  9,  8,  9,  8,  9,  0,  9,  8,   9},
				{53,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 307},
				{51,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  50, 0,  0, 291},
				{53,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 307},
				{51,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  50, 0,  0, 291},
				{53,  0,  0,  0,  0,  0,  0,  50, 0,  0,  0,  0,  0,  0,  0, 307},
				{0,   0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,   0},
				{53,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 307},
				{51,  0,  0,  0,  0,  50, 0,  0,  0,  0,  0,  0,  0,  0,  0, 291},
				{53,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 307},
				{51,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 291},
				{53,  0,  0,  50, 0,  0,  0,  0,  0,  0, 50, 50, 50,  0,  0, 307},
				{51,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 291},
				{53, 48, 49, 48, 49, 48, 49,  0, 49, 48, 49, 48, 49, 48, 49, 307},
			};
			var grid = new Grid (_spriteSeheet, 16, 20, new Rectangle(0,0,84,84));

			_world = new World ();
			_world.Map = new Map (mapa, new Rectangle (0, 0, 56, 56), grid);
			var box = new Rectangle (392, 728, 50, 50);
			_player = _world.Append (new Player () { 
				Grid = grid,
				Box =  box,
				NewBox = box,
				Direction = Direction.Zero,
				Speed = 2,
				Explosion = _explocion,
				Disparo = _disparo,
			});
			box = new Rectangle (168,  1, 50, 50);
			_world.Append (new Enemigo () { 
				Grid = grid,
				Box =  box,
				NewBox = box,
				Direction = Direction.Bottom,
				Speed = Datos.EnemySpeed,
				Obserbado = _player,
				Explosion = _explocion,
				Disparo = _disparo,
			});
			box = new Rectangle (672,  1, 50, 50);
			_world.Append (new Enemigo () { 
				Grid = grid,
				Box =  box,
				NewBox = box,
				Direction = Direction.Bottom,
				Speed = Datos.EnemySpeed,
				Obserbado = _player,
				Explosion = _explocion,
				Disparo = _disparo,
			});
			box = new Rectangle (840, 336, 50, 50);
			_world.Append (new Enemigo () { 
				Grid = grid,
				Box =  box,
				NewBox = box,
				Direction = Direction.Bottom,
				Speed = Datos.EnemySpeed,
				Obserbado = _player,
				Explosion = _explocion,
				Disparo = _disparo,
			});
			box = new Rectangle (0, 336, 50, 50);
			_world.Append (new Enemigo () { 
				Grid = grid,
				Box =  box,
				NewBox = box,
				Direction = Direction.Bottom,
				Speed = Datos.EnemySpeed,
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

			if (Datos.Perdio && Keyboard.GetState ().IsKeyDown (Keys.Enter)) {
				Datos.Murio = false;
				Datos.Vidas = 3;
				Datos.Perdio = false;
				Datos.EnemySpeed += 1;
				_player.Box = _player.NewBox;
				_world.Append (_player);
			}

			if (Keyboard.GetState ().IsKeyDown (Keys.A)) {
				Datos.Ayuda = true;
			} else if (Keyboard.GetState ().IsKeyUp (Keys.A)) {
				Datos.Ayuda = false;
			}

			if (Keyboard.GetState ().IsKeyDown (Keys.Space) && Datos.Murio == false) {
				var disparador = (Tanque)_player;
				disparador.Disparar ();
			}
		}
		public void Update(GameTime gameTime)
		{
			float t = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
			_world.Update (gameTime);
			if (Datos.Murio && Datos.Vidas == 0) {
				Datos.Murio = false;
				Datos.Perdio = true;
				Mando ();
				return;
			}
			_segundos += t;
			//si ya paso un segundo
			if (_segundos > 1) {
				Datos.Tiempo += 1;//incrementar tiempo
				_segundos = 0;//setear segundos a cero
				//si pasaron 10 segundos
				if (Datos.Tiempo % 10 == 0 && Datos.Murio == false) {
					Datos.Puntage += 1;//incrementar puntaje
					//si tiene as de 10 puntos
					/*if (Datos.Puntage % 10 == 0) {
						Datos.Vidas += 1;//inrementar vidas
					}*/
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
	}
}

