using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.ClassLibrary
{
	public class Board
	{
		public Dictionary<int, Player> Fields { get; set; } = new Dictionary<int, Player>();
		public bool GameIsOver { get; set; } = false;
		public Player Winner { get; set; }
		public Player PlayerOne { get; set; }
		public Player PlayerTwo { get; set; }
		public List<Player> Players { get; set; } = new List<Player>();
		public WinCondition WinningMove { get; set; }
		public List<WinCondition> WinConditions { get; set; }

		public Board(int playerCount)
		{
			InitializeBoard(playerCount);
		}
		private void InitializeBoard(int playerCount)
		{
			SetFields();
			SetPlayers(playerCount);
			SetWinConditions();
		}

		private void SetFields()
		{
			for (int i = 1; i <= 9; i++)
			{
				this.Fields.Add(i, Player.None);
			}
		}
		private void SetPlayers(int playerCount)
		{
			if (playerCount == 1)
			{
				this.PlayerOne = Player.One;
				this.Players.Add(Player.One);
				this.PlayerTwo = Player.Bot;
				this.Players.Add(Player.Bot);
			}
			else if (playerCount == 2)
			{
				this.PlayerOne = Player.One;
				this.PlayerTwo = Player.Two;
			}
		}
		private void SetWinConditions()
		{
			this.WinConditions = new List<WinCondition>();

			this.WinConditions.Add(new WinCondition(1, 2, 3));
			this.WinConditions.Add(new WinCondition(4, 5, 6));
			this.WinConditions.Add(new WinCondition(7, 8, 9));
			this.WinConditions.Add(new WinCondition(1, 4, 7));
			this.WinConditions.Add(new WinCondition(2, 5, 8));
			this.WinConditions.Add(new WinCondition(3, 6, 9));
			this.WinConditions.Add(new WinCondition(7, 5, 3));
			this.WinConditions.Add(new WinCondition(1, 5, 9));
		}

		private bool FieldIsTaken(int position)
		{
			if (this.Fields[position] == Player.None)
			{
				return false;
			}

			return true;
		}
		public bool GameIsWon()
		{
			var playerOneFields = new List<int>();
			var playerTwoFields = new List<int>();

			foreach (var field in this.Fields)
			{
				if (field.Value == Player.One)
				{
					playerOneFields.Add(field.Key);
				}
				if (field.Value == Player.Two || field.Value == Player.Bot)
				{
					playerTwoFields.Add(field.Key);
				}
			}

			foreach (var winCondition in this.WinConditions)
			{
				if (playerOneFields.Contains(winCondition.FirstValue) && playerOneFields.Contains(winCondition.SecondValue) && playerOneFields.Contains(winCondition.ThirdValue))
				{
					this.Winner = Player.One;
					this.GameIsOver = true;
					this.WinningMove = new WinCondition(playerOneFields.FirstOrDefault(x => x == winCondition.FirstValue), playerOneFields.FirstOrDefault(x => x == winCondition.SecondValue), playerOneFields.FirstOrDefault(x => x == winCondition.ThirdValue));

					return true;
				}
				if (playerTwoFields.Contains(winCondition.FirstValue) && playerTwoFields.Contains(winCondition.SecondValue) && playerTwoFields.Contains(winCondition.ThirdValue))
				{
					this.Winner = Player.Two;
					this.GameIsOver = true;
					this.WinningMove = new WinCondition(playerOneFields.FirstOrDefault(x => x == winCondition.FirstValue), playerOneFields.FirstOrDefault(x => x == winCondition.SecondValue), playerOneFields.FirstOrDefault(x => x == winCondition.ThirdValue));

					return true;
				}
			}

			return false;
		}
		public bool GameIsDraw()
		{
			foreach (var field in this.Fields)
			{
				if (field.Value != Player.One && field.Value != Player.Two && field.Value != Player.Bot)
				{
					return false;
				}
			}

			this.GameIsOver = true;
			return true;
		}

		public bool Move(Player player, int position = 1)
		{
			if (player == Player.Bot)
			{
				var logic = new Logic();
				var suggestedPosition = logic.GetSuggestedMove(this);
				Fields[suggestedPosition] = player;
				return true;
			}
			if (!FieldIsTaken(position))
			{
				Fields[position] = player;
				return true;
			}
			else
			{
				return false;
			}
		}

		private string GetPlayerSymbol(Player player, int position)
		{
			if (player == Player.One)
			{
				return "X";
			}
			if (player == Player.Two || player == Player.Bot)
			{
				return "O";
			}
			else
			{
				return position.ToString();
			}
		}

		public void Draw()
		{
			Console.Clear();
			Console.WriteLine("     |     |      ");
			Console.WriteLine("  {0}  |  {1}  |  {2}", GetPlayerSymbol(this.Fields[1], 1), GetPlayerSymbol(this.Fields[2], 2), GetPlayerSymbol(this.Fields[3], 3));
			Console.WriteLine("_____|_____|_____ ");
			Console.WriteLine("     |     |      ");
			Console.WriteLine("  {0}  |  {1}  |  {2}", GetPlayerSymbol(this.Fields[4], 4), GetPlayerSymbol(this.Fields[5], 5), GetPlayerSymbol(this.Fields[6], 6));
			Console.WriteLine("_____|_____|_____ ");
			Console.WriteLine("     |     |      ");
			Console.WriteLine("  {0}  |  {1}  |  {2}", GetPlayerSymbol(this.Fields[7], 7), GetPlayerSymbol(this.Fields[8], 8), GetPlayerSymbol(this.Fields[9], 9));
			Console.WriteLine("     |     |      ");

			Console.WriteLine();

			if (this.GameIsDraw())
			{
				Console.WriteLine("Game Tied!");
			}

			if (this.GameIsWon())
			{
				Console.WriteLine("Spieler {0} hat gewonnen!", this.Winner);
			}
		}
	}
}
