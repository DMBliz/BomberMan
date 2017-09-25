using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace BomberMan
{

	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		
		Map map;

		public static Texture2D walkAble;
		public static Texture2D unWalkAble;
		public static Texture2D bomb1;
		public static Texture2D bomb2;
		public static Texture2D Explode;
		public static Texture2D bonus;
		public static Texture2D enemy;
		public static Texture2D button;
		public static SpriteFont mainFont;
		public static Random rand;
		public static int playersNumb = 0;
		public bool isChose;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			graphics.PreferredBackBufferHeight = 690;
			graphics.PreferredBackBufferWidth = 1200;
			map = new Map(80, 44);
			rand = new Random();
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
			IsMouseVisible = true;
		}

		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			walkAble = Content.Load<Texture2D>("walkable");
			unWalkAble = Content.Load<Texture2D>("UNwalkable");
			bomb1 = Content.Load<Texture2D>("Bomb1");
			bomb2 = Content.Load<Texture2D>("Bomb2");
			Explode = Content.Load<Texture2D>("Explosion");
			bonus = Content.Load<Texture2D>("coin");
			enemy = Content.Load<Texture2D>("Enemy");
			mainFont = Content.Load<SpriteFont>("File");
			button = Content.Load<Texture2D>("Button");

			map.generate();
			
			// TODO: use this.Content to load your game content here
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here

			if (!isChose)
			{
				ChosePlayers();
			}else
			{
				map.Update(gameTime);
			}



			base.Update(gameTime);
		}

		public void ChosePlayers()
		{
			KeyboardState keyboardState = Keyboard.GetState();
			MouseState mouseState = Mouse.GetState();
			if (mouseState.LeftButton == ButtonState.Pressed)
			{
				if (mouseState.Position.X > 300 && mouseState.Position.X < 480 && mouseState.Position.Y > 350 && mouseState.Position.Y < 380)
				{
					isChose = true;
					playersNumb = 1;
					IsMouseVisible = false;
				}
				else if(mouseState.Position.X > 600 && mouseState.Position.X < 780 && mouseState.Position.Y > 350 && mouseState.Position.Y < 380)
				{
					isChose = true;
					playersNumb = 2;
					IsMouseVisible = false;
				}
			}
			if (isChose && playersNumb == 2)
			{
				map.players.Add(new Player(Content.Load<Texture2D>("Man"), new Point(3, 3)));
				map.players.Add(new Player(Content.Load<Texture2D>("Second"), new Point(5, 4)));
				(map.players[1] as Player).controller = new Controller(Keys.Left, Keys.Right, Keys.Up, Keys.Down, Keys.Enter);
			}
			else if (isChose && playersNumb == 1)
			{
				map.players.Add(new Player(Content.Load<Texture2D>("Man"), new Point(3, 3)));
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);
			spriteBatch.Begin();

			if (!isChose)
			{
				spriteBatch.Draw(button, new Vector2(300, 350));
				spriteBatch.Draw(button, new Vector2(600, 350));
				spriteBatch.DrawString(mainFont, "1 player", new Vector2(360, 355), Color.Black);
				spriteBatch.DrawString(mainFont, "2 players", new Vector2(660, 355), Color.Black);
			}
			else
			{
				map.Draw(spriteBatch);
			}
				

			// TODO: Add your drawing code here
			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
