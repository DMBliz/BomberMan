using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BomberMan
{
	class Bonus : GameObject
	{
		public Bonus(Point pos) : base()
		{
			image = Game1.bonus;
			position = pos;
		}
	}
}
