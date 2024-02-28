using BlazorComponentsDemo.DataModels.Models;
using System.Collections;

namespace BlazorComponentsDemo.Services.Contracts
{
    public interface IDataAccessService
    {
        Task<List<PeopleTestData>> GetPeopleTestData();
    }
}
