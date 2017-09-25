using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BomberMan
{
	class Tile:GameObject
	{
		private bool isWalkAble = true;

		public bool WalkAble { get { return isWalkAble; } set { isWalkAble = value; ChangeState(); }  }

		public Tile(Point pos,bool walkable) : base()
		{
			isWalkAble = walkable;
			position = pos;

			ChangeState();
		}

		private void ChangeState()
		{
			image = (isWalkAble) ? Game1.walkAble : Game1.unWalkAble;
		}
	}
}
