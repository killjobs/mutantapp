using System.Collections.Generic;
using Utilities;
using Utilities.Models;

namespace BLL
{
	public interface IMutantBLL
	{
		MutantStatsDto GetMutantStats();
		bool ValidateMutant(string[] dna);
		int AddMutantStats(MutantDto dna);
		NitrogenousBaseDnaDto ValidateNitrogenousBaseDna(char letter, char letterPosition, int quantity, int quantityBase);
	}
}
