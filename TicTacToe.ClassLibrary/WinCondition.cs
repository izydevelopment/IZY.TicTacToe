using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.ClassLibrary
{
	public class WinCondition
	{
		public int FirstValue { get; set; }
		public int SecondValue { get; set; }
		public int ThirdValue { get; set; }

		public WinCondition(int firstValue, int secondValue, int thirdValue)
		{
			this.FirstValue = firstValue;
			this.SecondValue = secondValue;
			this.ThirdValue = thirdValue;
		}
	}
}
