using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace BomberMan
{
	class Enemy : GameObject
	{
		float waitBetweenWalks = 5;
		float waitBetweenMoves = 0.5f;
		float waitBetweenAttack = 2f;
		float nextAttack = 0;
		float nextMove = 0;
		float nextWalk = 0;
		int movesCount = 5;
		private int lives=1;
		public int Lives { get { return lives; } set { lives = value; CheckDead(); } }
		Player target;
		EEnemyState state;

		public Enemy(Point pos)
		{
			image = Game1.enemy;
			
			position = pos;
			state = (Game1.rand.Next(1, 2) / 2 == 1) ? EEnemyState.wait : EEnemyState.walk;
			if (state == EEnemyState.wait)
			{
				nextMove = waitBetweenMoves + 2;
			}
		}

		public override void Update(GameTime time)
		{
			List<Player> players;
			switch (state)
			{
				case EEnemyState.walk:
					if ((float)time.TotalGameTime.TotalSeconds >= nextMove)
					{
						players = Map.instance.CheckPlayer(position);
						if (players.Count > 0)
							target = Map.instance.CheckPlayer(position)[0];
						else
							target = null;
						if (target != null)
						{
							state = EEnemyState.attack;
							return;
						}
						Move();
						movesCount--;
						if (movesCount <= 0)
						{
							state = EEnemyState.wait;
							nextWalk = (float)time.TotalGameTime.TotalSeconds + waitBetweenWalks;
							movesCount = Game1.rand.Next(4, 9);
						}
						nextMove = (float)time.TotalGameTime.TotalSeconds + waitBetweenMoves;

						players = Map.instance.CheckPlayer(position);
						if (players.Count > 0)
							target = Map.instance.CheckPlayer(position)[0];
						else
							target = null;
						if (target!=null)
						{
							state = EEnemyState.attack;
						}
					}
					break;
				case EEnemyState.wait:
					if ((float)time.TotalGameTime.TotalSeconds >= nextWalk)
					{
						state = EEnemyState.walk;
					}
					break;
				case EEnemyState.attack:
					players = Map.instance.CheckPlayer(position);
					if (players.Count > 0)
						target = Map.instance.CheckPlayer(position)[0];
					else
						target = null;
					if (target == null)
					{
						state = EEnemyState.walk;
					}
					if ((float)time.TotalGameTime.TotalSeconds >= nextAttack)
					{
						if (target != null)
						{
							target.TakeDamage();
							nextAttack = (float)time.TotalGameTime.TotalSeconds + waitBetweenAttack;
						}
					}
					break;	
				default:
					break;
			}
		}

		public void Move()
		{
			
			for (int i = 0; i < 10; i++)
			{
				int dir = Game1.rand.Next(0, 4);
				switch (dir)
				{
					case 0:
						if (position.x+1 >= 0 && position.x+1 < Map.instance.tileMap.GetLength(0) && position.y >= 0 && position.y < Map.instance.tileMap.GetLength(1))
						{
							if (Map.instance.tileMap[position.x + 1, position.y].WalkAble)
							{
								position.x += 1;
								return;
							}
						}
						break;
					case 1:
						if (position.x - 1 >= 0 && position.x - 1 < Map.instance.tileMap.GetLength(0) && position.y >= 0 && position.y < Map.instance.tileMap.GetLength(1))
						{
							if (Map.instance.tileMap[position.x - 1, position.y].WalkAble)
							{
								position.x -= 1;
								return;
							}
						}
						break;
					case 2:
						if (position.x >= 0 && position.x  < Map.instance.tileMap.GetLength(0) && position.y + 1 >= 0 && position.y + 1 < Map.instance.tileMap.GetLength(1))
						{
							if (Map.instance.tileMap[position.x, position.y + 1].WalkAble)
							{
								position.y += 1;
								return;
							}
						}
						break;
					case 3:
						if (position.x >= 0 && position.x < Map.instance.tileMap.GetLength(0) && position.y - 1 >= 0 && position.y - 1 < Map.instance.tileMap.GetLength(1))
						{
							if (Map.instance.tileMap[position.x, position.y - 1].WalkAble)
							{
								position.y -= 1;
								return;
							}
						}
						break;
					default:
						break;
				}
			}
		}

		public void CheckDead()
		{
			if (lives <= 0)
			{
				Map.instance.enemys.Remove(this);
			}
		}
	}

	public enum EEnemyState
	{
		walk,
		wait,
		attack
	}
}
