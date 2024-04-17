using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;
using System.Security.Claims;
using MonoGame.Aseprite;
using AsepriteDotNet.Aseprite;
using EntityZero.Ogmo;
using EntityZero.Tiles;
using EntityZero.Entities;
using System.Collections.Generic;

namespace EntityZero
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		private RenderTarget2D _renderTarget;
		private Rectangle _renderDestination;

		private AnimatedSprite animatedSprite;

	

		private int _nativeWidth = 320;
		private int _nativeHeight = 180;

		AsepriteFile playerFile;
		AsepriteFile greybox;
		AsepriteFile background;
		AsepriteFile quoteFile;

		Sprite playerSprite;
		Sprite quoteSprite;
		MonoGame.Aseprite.Tilemap backgroundSprite;


		Tiles.Tilemap tilemap;
		Tiles.Tilemap collidables;


		Entity player;
		Entity quote;

		public List<Entity> entities;

		public Globals Globals;

		private OgmoFile StageZero;

	public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";



			_graphics.PreferredBackBufferWidth = 960;
			_graphics.PreferredBackBufferHeight =  540;
			_graphics.ApplyChanges();

			//Window.AllowUserResizing = true;

			Globals = new Globals();	

			IsMouseVisible = true;

		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			
			base.Initialize();
		   CalculateRenderDestination();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			Texture2D texture = Content.Load<Texture2D>("1st row 2");
			animatedSprite = new AnimatedSprite(texture, 1,5 );
			Debug.Print(animatedSprite.Texture.Width.ToString());

			
			playerFile = Content.Load<AsepriteFile>("Atlas_Entity_0");
			greybox = Content.Load<AsepriteFile>("greybox");
			background = Content.Load<AsepriteFile>("background");
			quoteFile = Content.Load<AsepriteFile>("Atlas_Quote");
			
		    //Debug.Prints
			Debug.Print(playerFile.Frames.ToString());
			Debug.Print(background.Frames.ToString());
		

			//Creating all the sprites
			playerSprite = playerFile.CreateSprite(GraphicsDevice, 0);
			backgroundSprite = background.CreateTilemap(GraphicsDevice, 0);
			quoteSprite = quoteFile.CreateSprite(GraphicsDevice, 0);

			// Creating the TilemapObjects
			Globals.Graphics = _graphics.GraphicsDevice;
			tilemap = new Tiles.Tilemap(greybox, new Point(16, 16));
			collidables = new Tiles.Tilemap(greybox, new Point(16, 16));
			
			tilemap.createTileset();

			//Load the level file as an OgmoFile Object
			StageZero = new OgmoFile("Stage 0.json");



			//Creating the Collidables Tilemap
			collidables.CreateTilesGrid(StageZero.gridLayers[0].grid2d);

			//tilemap.CreateSpriteGrid(StageZero.tileLayers[0].data2d);


			//Creating the Entities
			player = new Entity(StageZero.entityLayers[0].entities[0]);
			quote = new Entity(StageZero.entityLayers[0].entities[1]);
			
			//Giving Sprites to the entities 
			player.sprite = playerSprite;
			quote.sprite = quoteSprite;


			_renderTarget = new RenderTarget2D(_graphics.GraphicsDevice,_nativeWidth, _nativeHeight);

			// TODO: use this.Content to load your game content here
		}

		private void CalculateRenderDestination()
		{

			Point size = GraphicsDevice.Viewport.Bounds.Size;

			float scaleX = (float)size.X / _renderTarget.Width;
			float scaleY = (float)size.Y / _renderTarget.Height;

			float scale = Math.Min(scaleX, scaleY);

			_renderDestination.Width = (int)(_renderTarget.Width * scale);
			_renderDestination.Height = (int)(_renderTarget.Height * scale);


			_renderDestination.X = (size.X - _renderDestination.Width) / 2;
			_renderDestination.Y =(size.Y - _renderDestination.Height) / 2;
		}


		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here
			
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);


			GraphicsDevice.SetRenderTarget(_renderTarget);
			// TODO: Add your drawing code here
			_spriteBatch.Begin(samplerState: SamplerState.PointClamp); // this is what prevents blurry pixels

			
			//collidables.Draw(_spriteBatch, GraphicsDevice);
			_spriteBatch.Draw(backgroundSprite, Vector2.Zero, Color.White);
			player.Draw(_spriteBatch);
			quote.Draw(_spriteBatch);

			
		    tilemap.DrawSprites(StageZero.tileLayers[0].data2d, _spriteBatch);
		    
		
		
			_spriteBatch.End();

			GraphicsDevice.SetRenderTarget(null);

			_spriteBatch.Begin(samplerState:SamplerState.PointClamp);

			_spriteBatch.Draw(_renderTarget,_renderDestination, Color.White);
			_spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
