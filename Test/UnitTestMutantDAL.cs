using DAL;
using Utilities;
using Utilities.Models;
using Xunit;

namespace Test
{
	public class UnitTestMutantDAL
	{
		private MutantDAL mutantDAL;

		private const string connectionString = "Data Source=sqlserver.caddkcwf74q7.us-east-1.rds.amazonaws.com;Initial Catalog=mutantapp;Persist Security Info=False;User Id=adminTest;Password=Test1234";
		public UnitTestMutantDAL() => mutantDAL = new MutantDAL(connectionString);


		[Fact]
		public void GetMutantStats_Success_Test()
		{
			MutantStatsDto result = mutantDAL.GetMutantStats();
			Assert.NotNull(result);
		}

		[Fact]
		public void AddMutantStats_Success_Test()
		{
			MutantModel dnaTest = new MutantModel()
			{
				Dna = new string[] { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" }
			};

			int result = mutantDAL.AddMutantStats(dnaTest);
			Assert.Equal(Constants.STATUS_SUCCESS_MUTANT, result);
		}

		[Fact]
		public void ExistsDna_Success_Test()
		{
			MutantDto dnaTest = new MutantDto()
			{
				Dna = new string[] { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" }
			};

			bool result = mutantDAL.ExistsDna(string.Join(",", dnaTest));
			Assert.True(result);
		}
	}
}
