
using MonoGame.Aseprite;
using EntityZero.Ogmo;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityZero.Entities
{
	public class Entity
	{
		public string name {  get; set; }	
		public int id { get; set; }
	    public Point position {  get; set; }
		public Point origin { get; set; }	

		public Sprite sprite { get; set; }


		//I'm not sure exactly how I want to define entities yet. I think my team member should have a say in this since its super important
		public Entity() { 


		}

		public Entity(OgmoEntity ogmoEntity)
		{
			name = ogmoEntity.name;
			position = ogmoEntity.position;
			origin = ogmoEntity.origin;
			
		}

		public void Draw(SpriteBatch spriteBatch){

		
			sprite.Draw(spriteBatch, position.ToVector2());
			
		}
	
	}
}
