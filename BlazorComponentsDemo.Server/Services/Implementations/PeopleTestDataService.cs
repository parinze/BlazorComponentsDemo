using BlazorComponentsDemo.DataModels.Data;
using BlazorComponentsDemo.DataModels.Models;
using BlazorComponentsDemo.Server.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorComponentsDemo.Server.Services.Implementations
{
	public class PeopleTestDataService : IPeopleTestDataService
	{
		private readonly DataModels.Data.EFTestDataDBContext _dbContext;

        public PeopleTestDataService(DataModels.Data.EFTestDataDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PeopleTestData>> GetTestData()
		{
			var testData = await _dbContext.PeopleTestData.ToListAsync();
			return testData;
		}

		public async Task<bool> UpdateTestDataStatus(int id, PeopleTestData request)
		{
			var testDataRecord = await _dbContext.PeopleTestData.FindAsync(id);

			if (testDataRecord == null)
			{
				return false;
			}

			testDataRecord.Status = request.Status;

			await _dbContext.SaveChangesAsync();

			return true;
		}
	}
}
