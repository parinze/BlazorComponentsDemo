using Azure;
using BlazorComponentsDemo.DataModels.Models;
using BlazorComponentsDemo.Services.Contracts;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorComponentsDemo.Services.Implementations
{
	public class DataAccessService : IDataAccessService
	{
		private readonly HttpClient _httpClient;
		private readonly string baseUrl = "https://localhost:7245";


		public DataAccessService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

		public async Task<List<PeopleTestData>> GetPeopleTestData()
		{
			string requestUrl = $"{baseUrl}/api/peopletestdata/GetTestData";
			Uri requestUri = new Uri(requestUrl);

			var result = await _httpClient.GetAsync(requestUri);

			var resultJSON = await result.Content.ReadAsStringAsync();

			var testData = JsonSerializer.Deserialize<List<PeopleTestData>>(resultJSON);

			if (testData == null)
			{
				return new List<PeopleTestData>();
			}

			return testData;
		}
	}
}
