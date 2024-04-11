using BlazorComponentsDemo.DataModels.Models;
using BlazorComponentsDemo.Services.Contracts;
using System.Text;
using BlazorComponentsDemo.DataModels.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Net.Http.Json;

namespace BlazorComponentsDemo.Services.Implementations
{
	public class DataAccessService : IDataAccessService
	{
        private readonly HttpClient _httpClient;
		private readonly string baseUrl = "https://localhost:7245";

        public List<TreatmentCardGrid> TreatmentCardGridEntries { get; set; } = new List<TreatmentCardGrid>();


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

        public async Task<List<TreatmentCardGrid>> GetTreatmentCardEntries()
        {
            if (TreatmentCardGridEntries.Count == 0)
            {
                //var data =  await _httpClient.GetFromJsonAsync<List<TreatmentCardGrid>>("sample-data/treatmentgrid2.json");
                var data =  await _httpClient.GetFromJsonAsync<List<TreatmentCardGrid>>("sample-data/treatmentgrid3_comment.json");

                if (data == null)
                {
                    return new List<TreatmentCardGrid>();
                }

                TreatmentCardGridEntries = data;
                return TreatmentCardGridEntries;
            }

            return TreatmentCardGridEntries;
        }

        public void HandleTreatmentCardEntry(TreatmentCardGrid treatmentEntry, bool delete)
        {
            var existingEntry = TreatmentCardGridEntries.FirstOrDefault(t => t.Id == treatmentEntry.Id);

            if (existingEntry != null && delete == false)
            {
                UpdateTreatmentCardEntry(treatmentEntry, existingEntry);
            }
            else if (existingEntry != null && delete == true)
            {
                DeleteTreatmentCardEntry(treatmentEntry);
            }
            else
            {
                AddTreatmentCardEntry(treatmentEntry);
            }
        }

        public void AddTreatmentCardEntry(TreatmentCardGrid treatmentEntry)
        {
            if (treatmentEntry.Id == 0)
            {
                var newId = TreatmentCardGridEntries.Max(t => t.Id) + 1;
                treatmentEntry.Id = newId;
            }

            TreatmentCardGridEntries.Insert(0, treatmentEntry); // insert at beginning of list for demo purposes
            //TreatmentCardGridEntries.Add(treatmentEntry);
        }

        public void UpdateTreatmentCardEntry(TreatmentCardGrid treatmentEntry, TreatmentCardGrid existingEntry)
        {
            existingEntry.Asst = treatmentEntry.Asst;
            existingEntry.Proc = treatmentEntry.Proc;
            existingEntry.Comment = treatmentEntry.Comment;
            existingEntry.DR = treatmentEntry.DR;
            existingEntry.TxNotes = treatmentEntry.TxNotes;
            existingEntry.AWU = treatmentEntry.AWU;
            existingEntry.BB = treatmentEntry.BB;
            existingEntry.AWL = treatmentEntry.AWL;
            existingEntry.OH = treatmentEntry.OH;
            existingEntry.EL = treatmentEntry.EL;
            existingEntry.NVT = treatmentEntry.NVT;
            existingEntry.WKS = treatmentEntry.WKS;
            existingEntry.Chart = treatmentEntry.Chart;
            existingEntry.Test = treatmentEntry.Test;
        }

        public void DeleteTreatmentCardEntry(TreatmentCardGrid treatmentEntry)
        {
                TreatmentCardGridEntries.Remove(treatmentEntry);
        }
    }
}
