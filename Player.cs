using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace BomberMan
{
	class Player:GameObject
	{
		public int lives = 2;
		public int score = 0;

		public bool wasPressed = false;
		private float timeBtweenBombs = 2;
		private float nextBomb = 0;

		public Controller controller;

		KeyboardState previousState;

		public Player() : base()
		{
			previousState = Keyboard.GetState();
			controller = new Controller();
		}

		public Player(Texture2D image) : base(image)
		{
			previousState = Keyboard.GetState();
			controller = new Controller();
		}

		public Player(Texture2D image, Point pos) : base(image,pos)
		{
			previousState = Keyboard.GetState();
			controller = new Controller();
		}

		public override void Update(GameTime time)
		{
			KeyboardState keyboardState = Keyboard.GetState();

			switch (controller.Update(keyboardState,previousState,position))
			{
				case EJob.left:
					Move(new Point(position.x - 1, position.y));
					break;
				case EJob.right:
					Move(new Point(position.x + 1, position.y));
					break;
				case EJob.up:
					Move(new Point(position.x, position.y - 1));
					break;
				case EJob.down:
					Move(new Point(position.x, position.y + 1));
					break;
				case EJob.explode:
					if ((float)time.TotalGameTime.TotalSeconds >= nextBomb)
					{
						Map.instance.bombs.Add(new Bomb(position,this, time));
						nextBomb = (float)time.TotalGameTime.TotalSeconds + timeBtweenBombs;
					}
					break;
				case EJob.none:
					break;
				default:
					break;
			}
		
			previousState = keyboardState;

			base.Update(time);
		}
		

		public void Move(Point dest)
		{
		
			if(dest.x < Map.instance.tileMap.GetLength(0) && dest.y < Map.instance.tileMap.GetLength(1) && dest.x >= 0 && dest.y >= 0)
			{
				if((Map.getInstance().tileMap[dest.x,dest.y] as Tile).WalkAble)
				{
					position = dest;
					Bonus bon = Map.instance.checkBonuses(dest);
					if (bon != null)
					{
						score++;
						Map.instance.bonuses.Remove(bon);
					}
				}
			}
		}

		public void TakeDamage()
		{
			lives--;
			if (lives <= 0)
			{
				Map.instance.Win(this);
			}
		}
	}
}
