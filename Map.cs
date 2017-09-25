using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace BomberMan
{
	class Map
	{
		public Tile[,] tileMap;
		public List<GameObject> players = new List<GameObject>();
		public List<GameObject> enemys = new List<GameObject>();
		public List<Bonus> bonuses = new List<Bonus>();
		public List<GameObject> bombs = new List<GameObject>();
		public static Map instance;
		public bool bIsEnd = false;
		public bool bIsLoose = false;
		public int numb = 0;

		public static Map getInstance()
		{
			return instance;
		}

		public Map()
		{
			tileMap = new Tile[0, 0];
			instance = this;
		}

		public Map(int x,int y)
		{
			tileMap = new Tile[x, y];
			
			instance = this;
		}

		public void generate()
		{
			Random rand = new Random();

			for (int i = 0; i < tileMap.GetLength(0); i++)
			{
				for (int j = 0; j < tileMap.GetLength(1); j++)
				{
					tileMap[i, j] = new Tile(new Point(i,j),(rand.Next(0, 100) > 60) ? false : true);
					if (rand.Next(0, 100) > 96)
					{
						bonuses.Add(new Bonus(new Point(i, j)));
					}

					if (rand.Next(0, 1000) > 993 && tileMap[i,j].WalkAble)
					{
						enemys.Add(new Enemy(new Point(i, j)));
					}
				}
			}
		}

		public Vector2 getWorldPos(Point pos)
		{
			return new Vector2(pos.x * 15, pos.y * 15);
		}

		public void Update(GameTime time)
		{
			if (!bIsEnd && !bIsLoose)
			{
				for (int i = 0; i < tileMap.GetLength(0); i++)
				{
					for (int j = 0; j < tileMap.GetLength(1); j++)
					{
						tileMap[i, j].Update(time);

					}
				}

				for (int i = 0; i < enemys.Count; i++)
				{
					enemys[i].Update(time);
				}

				for (int i = 0; i < players.Count; i++)
				{
					players[i].Update(time);
				}

				for (int i = 0; i < bombs.Count; i++)
				{
					bombs[i].Update(time);
				}
			}

			if (bonuses.Count <= 0)
			{
				bIsEnd = true;
				if (players.Count > 1)
				{
					int first = (players[0] as Player).score;
					int second = (players[1] as Player).score;
					Win((first > second) ? players[1] as Player : players[0] as Player);
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			
			for (int i = 0; i < tileMap.GetLength(0); i++)
			{
				for (int j = 0; j < tileMap.GetLength(1); j++)
				{
					tileMap[i, j].Draw(spriteBatch);
				}
			}

			for (int i = 0; i < bonuses.Count; i++)
			{
				if (tileMap[bonuses[i].position.x, bonuses[i].position.y].WalkAble)
				{
					bonuses[i].Draw(spriteBatch);
				}
			}

			for (int i = 0; i < enemys.Count; i++)
			{
				enemys[i].Draw(spriteBatch);
			}

			for (int i = 0; i < players.Count; i++)
			{
				players[i].Draw(spriteBatch);
			}

			for (int i = 0; i < bombs.Count; i++)
			{
				bombs[i].Draw(spriteBatch);
			}

			if (Game1.playersNumb == 1)
			{
				spriteBatch.DrawString(Game1.mainFont, "Lives:" + (players[0] as Player).lives, new Vector2(0, 0), Color.White);
				spriteBatch.DrawString(Game1.mainFont, "Score:" + (players[0] as Player).score, new Vector2(0, 14), Color.White);
			}
			else if (Game1.playersNumb == 2)
			{
				spriteBatch.DrawString(Game1.mainFont, "Lives:" + (players[0] as Player).lives, new Vector2(0, 0), Color.White);
				spriteBatch.DrawString(Game1.mainFont, "Score:" + (players[0] as Player).score, new Vector2(0, 14), Color.White);

				spriteBatch.DrawString(Game1.mainFont, "Lives:" + (players[1] as Player).lives, new Vector2(1140, 0), Color.White);
				spriteBatch.DrawString(Game1.mainFont, "Score:" + (players[1] as Player).score, new Vector2(1140, 14), Color.White);
			}

			if (bIsEnd)
			{
				spriteBatch.DrawString(Game1.mainFont, "Player number " + numb + " win", new Vector2(570, 340), Color.Brown);
			}

			if (bIsLoose)
			{
				spriteBatch.DrawString(Game1.mainFont, "You loose", new Vector2(570, 340), Color.Brown);
			}
		}

		public Bonus checkBonuses(Point pos)
		{
			foreach (var bonus in bonuses)
			{
				if (bonus.position.x == pos.x && bonus.position.y == pos.y)
					return bonus;
			}
			return null;
		}

		public List<Enemy> CheckEnemy(Point pos)
		{
			List<Enemy> findedEnemys = new List<Enemy>();
			foreach (var enemy in enemys)
			{
				if (enemy.position.x + 1 == pos.x && enemy.position.y == pos.y || enemy.position.x - 1 == pos.x && enemy.position.y == pos.y || enemy.position.x == pos.x && enemy.position.y == pos.y + 1 || enemy.position.x == pos.x && enemy.position.y == pos.y - 1 || enemy.position.x == pos.x && enemy.position.y == pos.y) 
					findedEnemys.Add(enemy as Enemy);
			}
			return findedEnemys;
		}

		public List<Player> CheckPlayer(Point pos)
		{
			List<Player> findedPlayers = new List<Player>();
			foreach (var player in players)
			{
				if (player.position.x + 1 == pos.x && player.position.y == pos.y || player.position.x - 1 == pos.x && player.position.y == pos.y || player.position.x == pos.x && player.position.y == pos.y + 1 || player.position.x == pos.x && player.position.y == pos.y - 1 || player.position.x == pos.x && player.position.y == pos.y)
					findedPlayers.Add(player as Player);
			}
			return findedPlayers;
		}

		public void Win(Player pl)
		{
			if (players.Count == 1)
			{
				bIsLoose = true;
			}

			for (int i = 0; i < players.Count; i++)
			{
				if (players[i] != pl)
				{
					bIsEnd = true;
					numb = i + 1;
					return;
				}
			}
		}
	}
}
