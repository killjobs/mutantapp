using System;
using System.Data;
using System.Data.SqlClient;
using Utilities;
using Utilities.Models;

namespace DAL
{
	public class MutantDAL : IMutantDAL
	{
		private string _connectionString;
		private Connection con;
		public MutantDAL(string connectionString)
		{
			_connectionString = connectionString;
			con = new Connection(_connectionString);
		}

		/// <summary>
		/// This method add the MutantModel on database but first validate if dna exists on database and evaluate the response according a if this is mutant (1) or not (0)
		/// </summary>
		/// <remarks>
		/// Example Results:
		/// 0
		/// </remarks>
		/// <param name="MutantModel">Object MutantModel with complete information to insert on database</param>
		/// <returns>int</returns>
		public int AddMutantStats(MutantModel mutant)
		{
			if (ExistsDna(string.Join(",", mutant.Dna)))
			{
				throw new ApplicationException("This dna already exists in database");
			}

			con = new Connection(_connectionString);
			SqlParameter[] sqlParameters = new SqlParameter[]
			{
				con.GetParameter("@dna", string.Join(",",mutant.Dna)),
				con.GetParameter("@isMutant", mutant.IsMutant)
			};

			int result = con.ExecuteNonQuery("sp_add_mutant", sqlParameters, CommandType.StoredProcedure);
			
			if(result == 1)
			{
				result = (!mutant.IsMutant) ? 0 : 1;
			}

			return result;
		}
		/// <summary>
		/// This method validate if the dna exists on database
		/// </summary>
		/// <remarks>
		/// Example Results:
		/// false
		/// </remarks>
		/// <param name="dna">string with all array defined on initial string[] </param>
		/// <returns>bool</returns>
		public bool ExistsDna(string dna)
		{
			con = new Connection(_connectionString);
			bool result = false;
			SqlParameter[] sqlParameters = new SqlParameter[]
			{
				con.GetParameter("@dna", dna),
			};

			DataTable mutantDataTable = con.ExecuteDataTableSqlDA(CommandType.StoredProcedure, "sp_validate_mutant", sqlParameters);

			if (mutantDataTable.Rows.Count > 0)
			{
				foreach (DataRow mutant in mutantDataTable.Rows)
				{
					result = (int)mutant["Result"] == 1 ? true : false;
				}
			}

			return result;
		}
		
		public MutantStatsDto GetMutantStats()
		{
			con = new Connection(_connectionString);
			MutantStatsDto mutantRatio = new MutantStatsDto();
			SqlParameter[] sqlParameters = new SqlParameter[] { };
			DataTable mutantDataTable = con.ExecuteDataTableSqlDA(CommandType.StoredProcedure, "sp_get_mutants", sqlParameters);

			if (mutantDataTable.Rows.Count > 0)
			{
				foreach(DataRow mutant in mutantDataTable.Rows)
				{
					mutantRatio.conut_mutant_dna = (int)mutant["CountMutantDna"];
					mutantRatio.count_human_dna = (int)mutant["CountHumanDna"];
					mutantRatio.ratio = (decimal)mutant["Ratio"];
				}
			}

			return mutantRatio;
		}
	}
}
