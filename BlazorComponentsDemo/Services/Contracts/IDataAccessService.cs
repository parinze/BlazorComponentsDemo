using BlazorComponentsDemo.DataModels.Models;
using System.Collections;

namespace BlazorComponentsDemo.Services.Contracts
{
    public interface IDataAccessService
    {
        Task<List<PeopleTestData>> GetPeopleTestData();
        Task<bool> UpdatePeopleTestDataStatus(int id, PeopleTestData testDataRecord);

	}
}
