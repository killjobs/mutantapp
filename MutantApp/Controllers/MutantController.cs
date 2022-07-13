using BLL;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Utilities;
using Utilities.Models;

namespace MutantApp.Controllers
{
	[RoutePrefix("api/mutantapp")]
	public class MutantController : ApiController
	{
		private readonly IMutantBLL _mutantBLL;

		public MutantController()
		{
			_mutantBLL = new MutantBLL(ConfigurationManager.AppSettings["connection"].ToString());
		}

		/// <summary>
		/// This method get summary and calculate ratio according to number of mutant and normal people
		/// </summary>
		/// <remarks>
		/// Example Results:
		/// {
		///		"conut_mutant_dna": 2,
		///		"count_human_dna": 3,
		///		"ratio": 0.7
		///	}
		/// </remarks>
		/// <param></param>
		/// <response code="200">Ok</response>
		/// <response code="500">Server error</response>
		/// <returns>HttpResponseMessage</returns>
		[HttpGet]
		[Route("stats")]
		public HttpResponseMessage GetMutantStats()
		{
			try
			{
				return Request.CreateResponse(HttpStatusCode.OK, _mutantBLL.GetMutantStats());	
			}
			catch (Exception ex)
			{
				return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
			}
			
		}

		/// <summary>
		/// This method add possible mutant on database according to algorithm defined into class _mutantBLL
		/// </summary>
		/// <remarks>
		/// This method only return Status Http according to internal logic, on <response> you can see the different possibilities
		/// </remarks>
		/// <param name="dna">Object Mutant Dto that containt string[]</param>
		/// <response code="200">Ok</response>
		/// <response code="403">Forbidden, but the value was insert on database</response>
		/// <response code="500">Internal Server Error, when dna is smaller than min length necessary on matriz to continue </response>
		/// <response code="500">Internal Server Error, when the matriz is not be NxN length</response>
		/// <response code="500">Internal Server Error, when any position on dna contain letters different to A,T,C, or G</response>
		/// <response code="500">Internal Server Error, when dna exists in database</response>
		/// <returns>HttpResponseMessage</returns>
		[HttpPost]
		[Route("mutant")]
		public HttpResponseMessage AddMutantStats([FromBody] MutantDto dna)
		{
			try
			{
				switch (_mutantBLL.AddMutantStats(dna))
				{
					case Constants.STATUS_SUCCESS_MUTANT:
						return Request.CreateResponse(HttpStatusCode.OK);
					case Constants.STATUS_FAIL_MUTANT:
						return Request.CreateResponse(HttpStatusCode.Forbidden);
					default:
						return Request.CreateResponse(HttpStatusCode.InternalServerError);
				}
			}
			catch(Exception ex)
			{
				return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
			}
		}
	}
}
