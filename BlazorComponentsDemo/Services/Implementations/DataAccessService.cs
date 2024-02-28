using Azure;
using BlazorComponentsDemo.DataModels.Models;
using BlazorComponentsDemo.Services.Contracts;
using System;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json.Serialization;

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

			var testDataResult = await _httpClient.GetAsync(requestUri);

			var testDataResultJSON = await testDataResult.Content.ReadAsStringAsync();

			var testData = JsonSerializer.Deserialize<List<PeopleTestData>>(testDataResultJSON);

			if (testData == null)
			{
				return new List<PeopleTestData>();
			}

			return testData;
		}

		public async Task<bool> UpdatePeopleTestDataStatus(int id, PeopleTestData testDataRecord)
		{
			string requestUrl = $"{baseUrl}/api/peopletestdata/UpdateTestDataStatus?id={id}";
			Uri requestUri = new Uri(requestUrl);

			var testDataResultJSON = JsonSerializer.Serialize(testDataRecord);

			var content = new StringContent(testDataResultJSON, Encoding.UTF8, "application/json");

			var response = await _httpClient.PutAsync(requestUri, content);

			return response.IsSuccessStatusCode ? true : false;
		}
	}
}
