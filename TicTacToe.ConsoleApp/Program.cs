using System;
using TicTacToe.ClassLibrary;

namespace TicTacToe.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				StartGame();
			}
		}

		public static void StartGame()
		{
			Console.Clear();
			Console.WriteLine("Tic-Tac-Toe");
			Console.Write("Anzahl der Spieler?: ");
			var playerCount = Convert.ToInt32(Console.ReadLine());
			var board = new Board(playerCount);

			board.Draw();

			for (int i = 1; i <= board.Fields.Count; i++)
			{
				Console.WriteLine();

				if (!board.GameIsOver)
				{
					var position = 0;

					if (i % 2 != 0)
					{
						do
						{
							Console.Write("Spieler 1 (X): ");
							position = Convert.ToInt32(Console.ReadLine());
						} while (!board.Move(board.PlayerOne, position));
					}
					else
					{
						if (board.PlayerTwo == Player.Bot)
						{
							board.Move(board.PlayerTwo);
						}
						else
						{
							do
							{
								Console.Write("Spieler 2 (X): ");
								position = Convert.ToInt32(Console.ReadLine());
							} while (!board.Move(board.PlayerTwo, position));
						}
					}
				}
				else
				{
					Console.WriteLine();
					Console.WriteLine("Zum wiederholen Enter drücken");
					Console.ReadLine();
				}

				board.Draw();
			}
		}
	}
}
