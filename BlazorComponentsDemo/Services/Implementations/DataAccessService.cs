using BlazorComponentsDemo.DataModels.Models;
using BlazorComponentsDemo.Services.Contracts;
using System.Text;
using BlazorComponentsDemo.DataModels.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

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

		public async Task<StatementQueryVm?> GetStatements()
		{
            try
            {
                // Specify the URL or file path of the JSON file
                string url = "https://gist.githubusercontent.com/parinze/339f04a2ac6697d18be89bfd6537ba8d/raw/1fe36286e94ad75a3ce1d3b5005593305fe9bb09/statements.json?_sm_au_=iVV8WkJvNNRH65Drs6RsjKtRK4VLt"; // or local file path

                // Send GET request and get the content as a stream
                using (HttpResponseMessage response = await _httpClient.GetAsync(url))
                {
                    // Check if the request was successful
                    response.EnsureSuccessStatusCode();

                    // Read the content as a string
                    string jsonContent = await response.Content.ReadAsStringAsync();

					JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings ??= new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore,

                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new CamelCaseNamingStrategy
                            {
                                ProcessDictionaryKeys = true   //Case-insensitive deserialization
                            }
                        }
                    };

                    var data = JsonConvert.DeserializeObject<StatementQueryVm>(jsonContent, settings);
                    return data;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return new StatementQueryVm();
            }
        }
	}
}
