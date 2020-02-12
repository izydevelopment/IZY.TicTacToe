using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.ClassLibrary
{
	public class Logic
	{
		private protected Dictionary<int, Player> TakenFields { get; set; } = new Dictionary<int, Player>();
		private protected Dictionary<int, Player> FreeFields { get; set; } = new Dictionary<int, Player>();
		private protected Dictionary<Player, Dictionary<WinCondition, int>> PriorityLists { get; set; } = new Dictionary<Player, Dictionary<WinCondition, int>>();

		public int GetSuggestedMove(Board board)
		{
			AnalyseBoard(board);
			var suggestion = ProgressSuggestion();

			return suggestion;
		}

		private void AnalyseBoard(Board board)
		{
			foreach (var field in board.Fields)
			{
				if (field.Value == Player.None)
				{
					this.FreeFields.Add(field.Key, field.Value);
				}
				else
				{
					this.TakenFields.Add(field.Key, field.Value);
				}
			}

			foreach (var player in board.Players)
			{
				this.PriorityLists.Add(player, GetPriorityList(player, board.WinConditions));
			}
		}

		private Dictionary<WinCondition, int> GetPriorityList(Player player, List<WinCondition> winConditions)
		{
			var priorityList = new Dictionary<WinCondition, int>();

			foreach (var winCondition in winConditions)
			{
				var score = 0;

				foreach (var field in this.TakenFields)
				{
					if (winCondition.FirstValue == field.Key || winCondition.SecondValue == field.Key || winCondition.ThirdValue == field.Key)
					{
						if (field.Value == player)
						{
							if (winCondition.FirstValue == field.Key && winCondition.SecondValue == field.Key && this.FreeFields.ContainsKey(field.Key))
							{
								score++;
							}
							if (winCondition.FirstValue == field.Key && this.FreeFields.ContainsKey(field.Key) && winCondition.ThirdValue == field.Key)
							{
								score++;
							}
							if (this.FreeFields.ContainsKey(field.Key) && winCondition.SecondValue == field.Key && winCondition.ThirdValue == field.Key)
							{
								score++;
							}

							score++;
						}
					}
				}

				priorityList.Add(winCondition, score);
			}

			priorityList = priorityList.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
			return priorityList;
		}

		private int ProgressSuggestion()
		{
			var bestChoices = new Dictionary<Player, Dictionary<WinCondition, int>>();

			bestChoices.Add(Player.One, this.PriorityLists[Player.One].Take(1).ToDictionary(x => x.Key, x => x.Value));
			bestChoices.Add(Player.Bot, this.PriorityLists[Player.Bot].Take(1).ToDictionary(x => x.Key, x => x.Value));

			var bestPosition = GetBestPosition(bestChoices);

			return bestPosition;
		}

		private int GetBestPosition(Dictionary<Player, Dictionary<WinCondition, int>> bestChoices)
		{
			if (this.TakenFields.Count == 1)
			{

			}

			if (bestChoices[Player.Bot].FirstOrDefault().Value == 2)
			{
				foreach (var freeField in this.FreeFields)
				{
					if (freeField.Key == bestChoices[Player.One].FirstOrDefault().Key.FirstValue)
					{
						return freeField.Key;
					}
					if (freeField.Key == bestChoices[Player.One].FirstOrDefault().Key.SecondValue)
					{
						return freeField.Key;
					}
					if (freeField.Key == bestChoices[Player.One].FirstOrDefault().Key.ThirdValue)
					{
						return freeField.Key;
					}
				}
			}

			if (bestChoices[Player.One].FirstOrDefault().Value == 2)
			{
				foreach (var freeField in this.FreeFields)
				{
					if (freeField.Key == bestChoices[Player.One].FirstOrDefault().Key.FirstValue)
					{
						return freeField.Key;
					}
					if (freeField.Key == bestChoices[Player.One].FirstOrDefault().Key.SecondValue)
					{
						return freeField.Key;
					}
					if (freeField.Key == bestChoices[Player.One].FirstOrDefault().Key.ThirdValue)
					{
						return freeField.Key;
					}
				}
			}

			if (this.TakenFields.Count % 2 != 0)
			{
				if (bestChoices[Player.Bot].FirstOrDefault().Value == 0)
				{
					foreach (var freeField in this.FreeFields)
					{
						if (freeField.Key == bestChoices[Player.One].FirstOrDefault().Key.FirstValue)
						{
							return freeField.Key;
						}
						if (freeField.Key == bestChoices[Player.One].FirstOrDefault().Key.SecondValue)
						{
							return freeField.Key;
						}
						if (freeField.Key == bestChoices[Player.One].FirstOrDefault().Key.ThirdValue)
						{
							return freeField.Key;
						}
					}
				}

				foreach (var freeField in this.FreeFields)
				{
					if (freeField.Key == bestChoices[Player.Bot].FirstOrDefault().Key.FirstValue)
					{
						return freeField.Key;
					}
					if (freeField.Key == bestChoices[Player.Bot].FirstOrDefault().Key.SecondValue)
					{
						return freeField.Key;
					}
					if (freeField.Key == bestChoices[Player.Bot].FirstOrDefault().Key.ThirdValue)
					{
						return freeField.Key;
					}
				}
			}

			return 0;
		}
	}
}
