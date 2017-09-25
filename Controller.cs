using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BomberMan
{
	class Controller
	{
		Keys Left = Keys.A;
		Keys Right = Keys.D;
		Keys Down = Keys.S;
		Keys Up = Keys.W;
		Keys bomb = Keys.Space;


		public Controller(Keys left, Keys right,Keys up,Keys down,Keys explode)
		{
			this.Left = left;
			this.Right=right;
			this.Down = down;
			this.Up = up;
			this.bomb = explode;
		}

		public Controller()
		{

		}

		public EJob Update(KeyboardState current,KeyboardState previous,Point currentPos)
		{
			if (current.IsKeyDown(Left) && !previous.IsKeyDown(Left))
			{
				return EJob.left;
			}
			else if (current.IsKeyDown(Right) && !previous.IsKeyDown(Right))
			{
				return EJob.right;
			}
			else if (current.IsKeyDown(Down) && !previous.IsKeyDown(Down))
			{
				return EJob.down;
			}
			else if (current.IsKeyDown(Up) && !previous.IsKeyDown(Up))
			{
				return EJob.up;
			}
			else if(current.IsKeyDown(bomb) && !previous.IsKeyDown(bomb))
			{
				return EJob.explode;
			}
			else
			{
				return EJob.none;
			}
		}
	}

	public enum EJob
	{
		left,
		right,
		up,
		down,
		explode,
		none
	}
}


