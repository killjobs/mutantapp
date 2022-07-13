using System;
using System.Text.RegularExpressions;

namespace Utilities
{
	public static class Helper
	{
		public static void ValidateLengthDna(int lengthDna)
		{
			if (lengthDna < Constants.MIN_LENGTH)
			{
				throw new ArgumentException($"dna length must be higher than {Constants.MIN_LENGTH - 1}");
			}
		}
		public static void ValidateStringContent(string dnaPositionValue, int dnaPositionLength)
		{
			bool exp = false;
			exp = Regex.IsMatch(dnaPositionValue, @"^.{" + dnaPositionLength + "}$");
			if (!exp)
			{
				throw new ArgumentException($"the matriz must be NxN but {dnaPositionValue} length is different than dna");
			}

			exp = Regex.IsMatch(dnaPositionValue, @"^[A,T,C,G]+$");
			if (!exp)
			{
				throw new ArgumentException($"'{dnaPositionValue}' contains letters different to A, T, C, or G");
			}
		}
	}
}
