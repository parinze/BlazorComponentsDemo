using BlazorComponentsDemo.DataModels.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorComponentsDemo.Server.Services.Contracts
{
	public interface IPeopleTestDataService
	{
		Task<List<PeopleTestData>> GetTestData();
		Task<bool> UpdateTestDataStatus(int id, PeopleTestData request);
	}
}
