using BLL;
using Utilities;
using Utilities.Models;
using Xunit;

namespace Test
{
	public class UnitTestMutantBLL
	{
		private MutantBLL mutantBLL;
												 
		private const string connectionString = "Data Source=sqlserver.caddkcwf74q7.us-east-1.rds.amazonaws.com;Initial Catalog=mutantapp;Persist Security Info=False;User Id=adminTest;Password=Test1234";
		public UnitTestMutantBLL() => mutantBLL = new MutantBLL(connectionString);


		[Fact]
		public void GetMutantStats_Success_Test()
		{
			MutantStatsDto result = mutantBLL.GetMutantStats();
			Assert.NotNull(result);
		}

		[Fact]
		public void GetMutantStats_Success_Test_1()
		{
			MutantStatsDto result = mutantBLL.GetMutantStats();
			Assert.True(result.conut_mutant_dna>=0);
			Assert.True(result.count_human_dna >= 0);
			Assert.True(result.ratio >= 0);
		}


		[Fact]
		public void ValidateMutant_Success_Test()
		{
			string[] dnaTest = { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };

			bool result = mutantBLL.ValidateMutant(dnaTest);
			Assert.True(result);

		}

		[Fact]
		public void AddMutantStats_Success_Test()
		{
			MutantDto dnaTest = new MutantDto()
			{
				Dna = new string[] { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" }
			};

			int result = mutantBLL.AddMutantStats(dnaTest);
			Assert.Equal(Constants.STATUS_SUCCESS_MUTANT, result);
		}
		[Fact]
		public void AddMutantStats_Success_Test_Fail_Test_1()
		{
			MutantDto dnaTest = new MutantDto()
			{
				Dna = new string[] { "CAA", "CAA", "TGG" }
			};

			int result = mutantBLL.AddMutantStats(dnaTest);
			Assert.Equal(Constants.STATUS_SUCCESS_MUTANT, result);
		}
		[Fact]
		public void AddMutantStats_Success_Test_Fail_Test_2()
		{
			MutantDto dnaTest = new MutantDto()
			{
				Dna = new string[] { "CAAA", "CAAG", "TGGA", "TAA" }
			};

			int result = mutantBLL.AddMutantStats(dnaTest);
			Assert.Equal(Constants.STATUS_SUCCESS_MUTANT, result);
		}
		[Fact]
		public void AddMutantStats_Success_Test_Fail_Test_3()
		{
			MutantDto dnaTest = new MutantDto()
			{
				Dna = new string[] { "CADA", "cAAG", "TGGA", "TAAC" }
			};

			int result = mutantBLL.AddMutantStats(dnaTest);
			Assert.Equal(Constants.STATUS_SUCCESS_MUTANT, result);
		}

		[Theory]
		[InlineData('A','A',3,0)]
		public void ValidateNitrogenousBaseDna_Success_Fail_Teory(char letter, char letterPosition, int quantity, int quantityBase)
		{
			NitrogenousBaseDnaDto result = mutantBLL.ValidateNitrogenousBaseDna(letter, letterPosition, quantity, quantityBase);

			Assert.Equal(1 , result.QuantityBase);

		}
	}
}
