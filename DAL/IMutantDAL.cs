using System.Collections.Generic;
using Utilities;
using Utilities.Models;

namespace DAL
{
	public interface IMutantDAL
	{
		MutantStatsDto GetMutantStats();
		int AddMutantStats(MutantModel mutant);
	}
}
