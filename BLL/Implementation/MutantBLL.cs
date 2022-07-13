using DAL;
using Utilities;
using Utilities.Models;

namespace BLL
{
	public class MutantBLL : IMutantBLL
	{
		private readonly IMutantDAL _dataAccess;
		private string _connectionString;

		public MutantBLL(string connectionString)
		{
			_connectionString = connectionString;
			_dataAccess = new MutantDAL(_connectionString);
		}

		public int AddMutantStats(MutantDto dna)
		{
			MutantModel mutant = new MutantModel()
			{
				Id = 0,
				Dna = dna.Dna,
				IsMutant = ValidateMutant(dna.Dna)

			};

			return _dataAccess.AddMutantStats(mutant);
		}

		public MutantStatsDto GetMutantStats()
		{
			return _dataAccess.GetMutantStats();
		}

		/// <summary>
		/// this method repeats the same operation for each letter validation way and return NitrogenousBaseDnaDto for continue de validation on ValidateMutant method
		/// </summary>
		/// <remarks>
		/// Example Results:
		/// NitrogenousBaseDnaDto(){
		/// Letter = 'A'
		/// Quantity = 1,
		/// QuantityBase = 0
		/// }
		/// </remarks>
		/// <param name="letter">current letter to evaluate</param>
		/// <param name="letterPosition">current letter on loop position to compare with letter</param>
		/// <param name="quantity">rep count on comparing letter with letterPosition</param>
		/// <param name="quantityBase">if quantity is equal than 4 add one to this variable, if this more than 2, finish the loop and that flag the person with mutant</param>
		/// <returns>NitrogenousBaseDnaDto</returns>
		public NitrogenousBaseDnaDto ValidateNitrogenousBaseDna(char letter, char letterPosition, int quantity, int quantityBase)
		{
			char finalLetter = letter;
			int finalQuantity = quantity;
			int finalQuantityBase = quantityBase;

			if (char.IsWhiteSpace(letter))
			{
				finalLetter = letterPosition;
				finalQuantity++;
			}
			else if (letter == letterPosition)
			{
				finalQuantity++;
				if (finalQuantity == Constants.MIN_LENGTH)
				{
					finalQuantity = 0;
					finalQuantityBase++;
				}
			}
			else
			{
				finalLetter = letterPosition;
				finalQuantity = 0;
			}

			return new NitrogenousBaseDnaDto() { 
				Letter = finalLetter,
				Quantity = finalQuantity,
				QuantityBase = finalQuantityBase
			};

		}
		/// <summary>
		/// This validate if dna success according to found more than one sequence of four equals letters in these ways: horizontal, vertical or oblique
		/// If the response is true, the dna is mutant, otherwise, is normal person
		/// </summary>
		/// <remarks>
		/// Example Results:
		/// true
		/// </remarks>
		/// <param name="dna">string[] that contain base ADN to evaluate</param>
		/// <returns>bool</returns>
		public bool ValidateMutant(string[] dna)
		{
			bool isMutant = false;
			int lengthDna = dna.Length;
			char[][] nitrogenousBaseDna = new char[lengthDna][];
			NitrogenousBaseDnaDto validatorBaseDna = new NitrogenousBaseDnaDto();
			Helper.ValidateLengthDna(lengthDna);

			for (var index = 0; index < lengthDna; index++)
			{
				Helper.ValidateStringContent(dna[index], lengthDna);

				nitrogenousBaseDna[index] = new char[lengthDna];
				nitrogenousBaseDna[index] = dna[index].ToCharArray();
			}

			char letterDialog = ' ';
			char letterDialogInv = ' ';
			
			int cantbaseADN = 0;
			int cantDialog = 0;
			int cantDialogInv = 0;

			for (var i = 0; i < nitrogenousBaseDna.Length; i++)
			{
				char letterHorizontal = ' ';
				int cantHorizontal = 0;

				char letterVertical = ' ';
				int cantVertical = 0;

				for (var j = 0; j < nitrogenousBaseDna[i].Length; j++)
				{
					//diagonal inicial
					if (i == j)
					{
						validatorBaseDna = ValidateNitrogenousBaseDna(letterDialog, nitrogenousBaseDna[i][j], cantDialog, cantbaseADN);
						letterDialog = validatorBaseDna.Letter;
						cantDialog = validatorBaseDna.Quantity;
						cantbaseADN = validatorBaseDna.QuantityBase;
					}
					//diagonal inversa
					if (i == (nitrogenousBaseDna[i].Length - 1) - j)
					{
						validatorBaseDna = ValidateNitrogenousBaseDna(letterDialogInv, nitrogenousBaseDna[j][i], cantDialogInv, cantbaseADN);
						letterDialogInv = validatorBaseDna.Letter;
						cantDialogInv = validatorBaseDna.Quantity;
						cantbaseADN = validatorBaseDna.QuantityBase;
					}
					//vertical
					validatorBaseDna = ValidateNitrogenousBaseDna(letterVertical, nitrogenousBaseDna[j][i], cantVertical, cantbaseADN);
					letterVertical = validatorBaseDna.Letter;
					cantVertical = validatorBaseDna.Quantity;
					cantbaseADN = validatorBaseDna.QuantityBase;

					// horizontal
					validatorBaseDna = ValidateNitrogenousBaseDna(letterHorizontal, nitrogenousBaseDna[i][j], cantHorizontal, cantbaseADN);
					letterHorizontal = validatorBaseDna.Letter;
					cantHorizontal = validatorBaseDna.Quantity;
					cantbaseADN = validatorBaseDna.QuantityBase;

					if (cantbaseADN >= Constants.QUANTITY_NECESSARY_TO_BE_MUTANT)
					{
						isMutant = true;
						break;
					}
				}

				if (cantbaseADN >= Constants.QUANTITY_NECESSARY_TO_BE_MUTANT)
				{
					isMutant = true;
					break;
				}
			}

			return isMutant;
		}
	}
}