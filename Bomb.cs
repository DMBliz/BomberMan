using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace BomberMan
{
	class Bomb:GameObject
	{
		Player owner;
		Texture2D image1;
		Texture2D image2;
		Texture2D explodeTexture;
		float timeToExplode=2;
		float explodeTime=0;
		float timeBtweenChange = 0.2f;
		float changeTime=0;
		float timeToDisapiare=0.2f;
		float disapiareTime;

		public Bomb(Point pos, Player owner,GameTime time) : base()
		{
			this.owner = owner;
			position = pos;
			image1 = Game1.bomb1;
			image2 = Game1.bomb2;
			explodeTexture = Game1.Explode;
			image = image1;

			explodeTime = (float)time.TotalGameTime.TotalSeconds + timeToExplode;
			changeTime = (float)time.TotalGameTime.TotalSeconds + timeBtweenChange;
		}
		


		public override void Update(GameTime time)
		{
			base.Update(time);
			if (image != explodeTexture && (float)time.TotalGameTime.TotalSeconds >= changeTime)
			{
				changeTime = (float)time.TotalGameTime.TotalSeconds + timeBtweenChange;
				image = (image == image1) ? image2 : image1;
			}

			if(image != explodeTexture && (float)time.TotalGameTime.TotalSeconds>= explodeTime)
			{
				Explode((float)time.TotalGameTime.TotalSeconds);
			}

			if (image == explodeTexture && (float)time.TotalGameTime.TotalSeconds >= disapiareTime) 
			{
				Map.instance.bombs.Remove(this);
				if (Map.instance.tileMap.GetLength(0) > position.x + 1 && position.x + 1 >= 0 && Map.instance.tileMap.GetLength(1) > position.y && position.y >= 0)
				{
					Map.instance.tileMap[position.x + 1, position.y].WalkAble = true;
				}
				if (Map.instance.tileMap.GetLength(0) > position.x && position.x >= 0 && Map.instance.tileMap.GetLength(1) > position.y + 1 && position.y + 1 >= 0)
				{
					Map.instance.tileMap[position.x, position.y + 1].WalkAble = true;
				}
				if (Map.instance.tileMap.GetLength(0) > position.x + 1 && position.x + 1 >= 0 && Map.instance.tileMap.GetLength(1) > position.y + 1 && position.y + 1 >= 0)
				{
					Map.instance.tileMap[position.x + 1, position.y + 1].WalkAble = true;
				}
				if (Map.instance.tileMap.GetLength(0) > position.x + 2 && position.x + 2 >= 0 && Map.instance.tileMap.GetLength(1) > position.y + 1 && position.y + 1 >= 0)
				{
					Map.instance.tileMap[position.x + 2, position.y + 1].WalkAble = true;
				}
				if (Map.instance.tileMap.GetLength(0) > position.x + 1 && position.x + 1 >= 0 && Map.instance.tileMap.GetLength(1) > position.y + 2 && position.y + 2 >= 0)
				{
					Map.instance.tileMap[position.x + 1, position.y + 2].WalkAble = true;
				}


			}
		}		

		private void Explode(float explodeTime)
		{
			image = explodeTexture;
			disapiareTime = timeToDisapiare + explodeTime;

			List<Player> findedPlasyers = Map.instance.CheckPlayer(new Point(position.x, position.y));
			foreach (var item in findedPlasyers)
			{
				item.TakeDamage();
			}

			List<Enemy> findedEnemys = Map.instance.CheckEnemy(position);
			foreach (var item in findedEnemys)
			{
				item.Lives -= 1;
				if (item.Lives <= 0)
				{
					owner.score += 2;
				}
			}

			position.x -= 1;
			position.y -= 1;
		}
	}
}
