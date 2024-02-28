using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorComponentsDemo.DataModels.Models;
using BlazorComponentsDemo.Server.Services.Contracts;

namespace BlazorComponentsDemo.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PeopleTestDataController : ControllerBase
	{
		private readonly IPeopleTestDataService _peopleTestDataService;
		public PeopleTestDataController(IPeopleTestDataService peopleTestDataService)
		{
			_peopleTestDataService = peopleTestDataService;
		}

		[HttpGet("GetTestData")]
		public async Task<ActionResult<List<PeopleTestData>>> GetTestData()
		{
			return await _peopleTestDataService.GetTestData();
		}

		[HttpPut("UpdateTestDataStatus")]
		public async Task<ActionResult<bool>> UpdateTestDataStatus(int id, PeopleTestData request)
		{
			var result = await _peopleTestDataService.UpdateTestDataStatus(id, request);

			if (result == false) 
			{ 
				return NotFound($"No matching records");
			}

			return Ok(result);
		}
	}
}
