using AsepriteDotNet.Aseprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace EntityZero.Tiles
{


	// the tilemap code should assign sprites to each tile based on the values in a 2d array.


	public class Tilemap
	{
		public MonoGame.Aseprite.TextureAtlas tilesetFile { get; set; }
		public AsepriteFile asepriteFile { get; set; }


		public MonoGame.Aseprite.Sprite[] tileset;

		public Point tileSize { get; set; }
	

		/* This is a grid of rectangles which don't necessarily have any sprites attached to them.
		 * They could just be a grid of colliders for example. */
		public Rectangle[][] tilesGrid { get; set; } 
		/*This is an array of positions for the tiles which actually have sprites to be drawn to.
		 */
		public MonoGame.Aseprite.Sprite[][] spritesGrid { get; set; }

		public Tilemap(AsepriteFile aseprite, Point tileSize) 
		{
			asepriteFile = aseprite;
		
            this.tileSize = tileSize;
		
		     tilesetFile =  aseprite.CreateTextureAtlas(Globals.Graphics);
			
		}	

		public void createTileset(){
			tileset = new MonoGame.Aseprite.Sprite[asepriteFile.FrameCount];
			for (int i = 0; i < asepriteFile.FrameCount; i++) {
				MonoGame.Aseprite.Sprite sprite = tilesetFile.CreateSprite(string.Format("{0} {1}", asepriteFile.Name, i));
				tileset[i] = sprite;
				Debug.Print(tileset[i].Name);
			}
		} 
		public void CreateTilesGrid(string[][] grid2D)
		{
			
			tilesGrid = new Rectangle[grid2D.Length][];
			for(int j = 0; j < grid2D.Length; j++)
			{
				Rectangle[] tilesRow = new Rectangle[grid2D[j].Length];
				for (int i = 0; i <= grid2D[j].Length; i++)
				{
					if (i == grid2D[j].Length)
					{
						tilesGrid[j] = tilesRow;
					}
					else
					{
						if (grid2D[j][i]=="1")
						{
							tilesRow[i] = new Rectangle(i * tileSize.X, j * tileSize.Y, tileSize.X, tileSize.Y);
						}
					}
				}
			}

			
			
		}

		public void CreateSpriteGrid(int[][] data)
		{
			spritesGrid = new MonoGame.Aseprite.Sprite[data.Length][];
			Debug.Print("Creating a sprites grid....");
			//it should take the parsed data2d array and create an array of sprites if it matches an index in the 
			for (int j = 0; j < data.Length; j++)
			{
				MonoGame.Aseprite.Sprite[] spritesRow = new MonoGame.Aseprite.Sprite[data[j].Length];
				for(int i =0; i <= data[j].Length; i++)
				{

					if ( i < data[j].Length)
					{
						if(data[j][i] > -1)
						{
							spritesRow[i] = tileset[data[j][i]];
							Debug.Print("the index isn't -1");
						}
						
					}
				   
					if(i == data[j].Length)
					{
						spritesGrid[j] = spritesRow;
					}
				}



			}
		}

		public void DrawSprites(int[][] data, SpriteBatch spriteBatch)
		{
			for(int j =  0; j < data.Length;j++)
			{

				for (int i = 0; i < data[j].Length;i++)
				{
					if (data[j][i] > -1)
					{
						
						tileset[data[j][i]].Draw(spriteBatch, new Vector2((i * tileSize.X), (j * tileSize.Y)));
					}
					
					
				}
			}
		
		}

		public void Draw(SpriteBatch spriteBatch,GraphicsDevice device) // It's location is gonna be (0,0)
		{
			
			Texture2D blankTexture = new Texture2D(device, 1, 1);
			blankTexture.SetData<Color>(new Color[] { Color.White });
			foreach (var tileArray in tilesGrid)
			{
				foreach(var tile in tileArray )
				{
					spriteBatch.Draw(blankTexture,destinationRectangle:tile,Color.White);

				}
			
				
			}
		}
	}

	public class Tile // this class is pointless since Rectangles already have a position.
	{
	



		public Rectangle bounds;

		public int? sprite;


		public Tile( Rectangle bounds)
		{
			this.bounds = bounds;
		}
	}
	



}
