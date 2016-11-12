using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Entity
{
	public class PruebaTexto : IScene
	{
		bool _isActive = true;
		SpriteFont _sptiteTexto;

		string _textoPuntaje;
		string _textoVidas;
		string _textoTiempo;
		string _textoPerdio = "Perdio.\nPresione Enter para seguir jugando o Escape para salir";
		string _textoAyuda = "Presione Flecha izquierda para ir a la izquierda \n" +
							 "Presione Flecha derecha para ir a la derecha \n" +
							 "Presione Flecha arriba para ir para adelante \n" +
							 "Presione flecha abajo para ir para atras \n" +
							 "Presione espace para disparar \n" +
							 "presione escape para salir";


		public PruebaTexto (ContentManager content)
		{
			_sptiteTexto = content.Load<SpriteFont> ("Fuentes/fuente1.xnb");
		}
		public void Update(GameTime gameTime)
		{
			_textoVidas = string.Format ("Vidas {0}", Datos.Vidas);
			_textoPuntaje = string.Format ("Puntaje {0}", Datos.Puntage);
			_textoTiempo = string.Format ("Tiempo {0}", Datos.Tiempo);
		}
		public void Draw(SpriteBatch painter, GameTime gameTime)
		{
			painter.Begin ();
			painter.DrawString (_sptiteTexto, _textoPuntaje, new Vector2 (100, 100), Color.Blue);
			painter.DrawString (_sptiteTexto, _textoVidas, new Vector2 (300, 100), Color.Red);
			painter.DrawString (_sptiteTexto, _textoTiempo, new Vector2 (500, 100), Color.Green);
			painter.DrawString (_sptiteTexto, "Ayuda: presione 'A'", new Vector2 (100, 200), Color.Black);
			if (Datos.Perdio) {
				painter.DrawString (_sptiteTexto, _textoPerdio, new Vector2 (200, 300), Color.Red);
			}
			if (Datos.Ayuda) {
				painter.DrawString (_sptiteTexto, _textoAyuda, new Vector2 (200, 300), Color.Yellow);
			}
			painter.End ();
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

