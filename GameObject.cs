using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BomberMan
{
	class GameObject
	{
		public Point position;
		public Texture2D image;

		public virtual Vector2 worldPosition { get { return new Vector2(position.x * 15, position.y * 15); } }

		public GameObject()
		{
			position = new Point();
			image = null;
		}

		public GameObject(Texture2D image)
		{
			position = new Point();
			this.image = image;
		}

		public GameObject(Texture2D image, Point position)
		{
			this.image = image;
			this.position = position;
		}

		public virtual void Update(GameTime time)
		{
			
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			if (image != null)
			{
				spriteBatch.Draw(image, new Vector2(worldPosition.X, worldPosition.Y + 30));
			}
		}
	}
}
