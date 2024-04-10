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

        public List<TreatmentCardGrid> TreatmentCardGridEntries { get; set; } = new List<TreatmentCardGrid>
        {
            new TreatmentCardGrid { Id = 1, Date = DateTime.Parse("2017-10-26T15:17:29"), Type = "C", Asst = "Prisca Sainthill", Proc = "Consult", DR = "Claire Sainthill", TxNotes = "Decentralized", AWU = "CID", BB = "Aquamarine", AWL = "Silverado 3500", OH = "US-IA", EL = "Cedar Rapids", NVT = "US", WKS = 1, Chart = "csainthill0@ask.com", Test = "Sainthill" },
            new TreatmentCardGrid { Id = 2, Date = DateTime.Parse("2023-07-31T21:18:29"), Type = "C", Asst = "Dirk Dyke", Proc = "AWNC", DR = "Nicky Dyke", TxNotes = "software", AWU = "YHM", BB = "Purple", AWL = "Montero Sport", OH = "CA-ON", EL = "Hamilton", NVT = "CA", WKS = 2, Chart = "ndyke1@ocn.ne.jp", Test = "Dyke" },
            new TreatmentCardGrid { Id = 3, Date = DateTime.Parse("2015-04-15T09:22:21"), Type = "V", Asst = "Ninnette Nenci", Proc = "CA", DR = "Stoddard Nenci", TxNotes = "composite", AWU = "KHO", BB = "Khaki", AWL = "Diamante", OH = "ZA-NP", EL = "Khoka Moya", NVT = "ZA", WKS = 3, Chart = "snenci2@is.gd", Test = "Nenci" },
            new TreatmentCardGrid { Id = 4, Date = DateTime.Parse("2015-09-23T09:42:25"), Type = "V", Asst = "Bessy Sponder", Proc = "Adjust15", DR = "Obediah Sponder", TxNotes = "clear-thinking", AWU = "UIQ", BB = "Goldenrod", AWL = "Econoline E150", OH = "VU-SEE", EL = "Quion Hill", NVT = "VU", WKS = 4, Chart = "osponder3@netvibes.com", Test = "Sponder" },
            new TreatmentCardGrid { Id = 5, Date = DateTime.Parse("2018-12-25T19:34:03"), Type = "V", Asst = "Federico McRuvie", Proc = "DB8670", DR = "Christabel McRuvie", TxNotes = "protocol", AWU = "KHE", BB = "Crimson", AWL = "Corvette", OH = "UA-65", EL = "Kherson", NVT = "UA", WKS = 5, Chart = "cmcruvie4@google.ca", Test = "McRuvie" },
            new TreatmentCardGrid { Id = 6, Date = DateTime.Parse("2019-06-12T12:44:40"), Type = "C", Asst = "Alfie Hearl", Proc = "NPC", DR = "Earl Hearl", TxNotes = "clear-thinking", AWU = "YMS", BB = "Blue", AWL = "GX", OH = "PE-LOR", EL = "Yurimaguas", NVT = "PE", WKS = 6, Chart = "ehearl5@howstuffworks.com", Test = "Hearl" },
            new TreatmentCardGrid { Id = 7, Date = DateTime.Parse("2019-09-06T06:42:57"), Type = "V", Asst = "Aldin Meineck", Proc = "CA", DR = "Lyndel Meineck", TxNotes = "capability", AWU = "TTW", BB = "Indigo", AWL = "B-Series Plus", OH = "LK-3", EL = "Tissamaharama", NVT = "LK", WKS = 7, Chart = "lmeineck6@state.gov", Test = "Meineck" },
            new TreatmentCardGrid { Id = 8, Date = DateTime.Parse("2016-10-11T09:32:42"), Type = "V", Asst = "Selestina Melloi", Proc = "BB", DR = "Theresa Melloi", TxNotes = "Public-key", AWU = "KEM", BB = "Indigo", AWL = "Insight", OH = "FI-LL", EL = "Kemi / Tornio", NVT = "FI", WKS = 8, Chart = "tmelloi7@discovery.com", Test = "Melloi" },
            new TreatmentCardGrid { Id = 9, Date = DateTime.Parse("2016-02-02T02:12:22"), Type = "C", Asst = "Charlean Hercules", Proc = "Appointment", DR = "Henrietta Hercules", TxNotes = "knowledge base", AWU = "TNB", BB = "Blue", AWL = "RX", OH = "ID-KI", EL = "Tanah Grogot-Borneo Island", NVT = "ID", WKS = 9, Chart = "hhercules8@macromedia.com", Test = "Hercules" },
            new TreatmentCardGrid { Id = 10, Date = DateTime.Parse("2021-10-07T19:18:37"), Type = "C", Asst = "Marjory Grimwad", Proc = "BB", DR = "Genovera Grimwad", TxNotes = "4th generation", AWU = "ULP", BB = "Indigo", AWL = "Accord", OH = "AU-QLD", EL = null, NVT = "AU", WKS = 10, Chart = "ggrimwad9@wunderground.com", Test = "Grimwad" },
            new TreatmentCardGrid { Id = 11, Date = DateTime.Parse("2019-04-15T18:34:10"), Type = "V", Asst = "Jourdain Wederell", Proc = "Adjust45", DR = "Waverley Wederell", TxNotes = "content-based", AWU = "BQS", BB = "Red", AWL = "Colorado", OH = "RU-AMU", EL = "Blagoveschensk", NVT = "RU", WKS = 11, Chart = "wwederella@cdbaby.com", Test = "Wederell" },
            new TreatmentCardGrid { Id = 12, Date = DateTime.Parse("2023-04-05T16:40:35"), Type = "C", Asst = "Emera Duberry", Proc = "DB8670", DR = "Avrit Duberry", TxNotes = "Object-based", AWU = "BJT", BB = "Khaki", AWL = "VUE", OH = "LK-1", EL = "Bentota", NVT = "LK", WKS = 12, Chart = "aduberryb@globo.com", Test = "Duberry" },
            new TreatmentCardGrid { Id = 13, Date = DateTime.Parse("2022-06-21T23:45:36"), Type = "C", Asst = "Winston Fydo", Proc = "Emergency", DR = "Eba Fydo", TxNotes = "Streamlined", AWU = "CTI", BB = "Orange", AWL = "Expedition", OH = "AO-CCU", EL = "Cuito Cuanavale", NVT = "AO", WKS = 13, Chart = "efydoc@unicef.org", Test = "Fydo" },
            new TreatmentCardGrid { Id = 14, Date = DateTime.Parse("2016-09-08T09:24:53"), Type = "C", Asst = "Merilyn Bourgour", Proc = "AMW", DR = "Lothaire Bourgour", TxNotes = "Enhanced", AWU = "KJK", BB = "Pink", AWL = "Suburban 2500", OH = "BE-VWV", EL = "Wevelgem", NVT = "BE", WKS = 14, Chart = "lbourgourd@lycos.com", Test = "Bourgour" },
            new TreatmentCardGrid { Id = 15, Date = DateTime.Parse("2020-03-23T12:20:10"), Type = "V", Asst = "Erinn Hastie", Proc = "DB8670", DR = "Adria Hastie", TxNotes = "human-resource", AWU = "VAP", BB = "Goldenrod", AWL = "XL7", OH = "CL-VS", EL = "Viña Del Mar", NVT = "CL", WKS = 15, Chart = "ahastiee@forbes.com", Test = "Hastie" },
            new TreatmentCardGrid { Id = 16, Date = DateTime.Parse("2018-04-29T20:44:43"), Type = "V", Asst = "Maddie Betke", Proc = "CK-RET", DR = "Locke Betke", TxNotes = "Self-enabling", AWU = "FRN", BB = "Yellow", AWL = "M", OH = "US-AK", EL = "Fort Richardson(Anchorage)", NVT = "US", WKS = 16, Chart = "lbetkef@techcrunch.com", Test = "Betke" },
            new TreatmentCardGrid { Id = 17, Date = DateTime.Parse("2018-12-16T10:53:07"), Type = "C", Asst = "Ethelin Rawson", Proc = "Adjust15", DR = "Mozelle Rawson", TxNotes = "actuating", AWU = "TIY", BB = "Fuscia", AWL = "TSX", OH = "MR-09", EL = "Tidjikja", NVT = "MR", WKS = 17, Chart = "mrawsong@skype.com", Test = "Rawson" },
            new TreatmentCardGrid { Id = 18, Date = DateTime.Parse("2020-04-23T04:01:27"), Type = "C", Asst = "Taber Anespie", Proc = "Adjust45", DR = "Jazmin Anespie", TxNotes = "service-desk", AWU = "CNH", BB = "Red", AWL = "Silverado 2500", OH = "US-NH", EL = "Claremont", NVT = "US", WKS = 18, Chart = "janespieh@cocolog-nifty.com", Test = "Anespie" },
            new TreatmentCardGrid { Id = 19, Date = DateTime.Parse("2015-11-12T13:32:11"), Type = "V", Asst = "Averil Pithie", Proc = "Appointment", DR = "Godfry Pithie", TxNotes = "web-enabled", AWU = "LKL", BB = "Green", AWL = "i-Series", OH = "NO-20", EL = "Lakselv", NVT = "NO", WKS = 19, Chart = "gpithiei@delicious.com", Test = "Pithie" },
            new TreatmentCardGrid { Id = 20, Date = DateTime.Parse("2017-08-08T07:05:01"), Type = "C", Asst = "Lenard Spire", Proc = "AWNDET", DR = "Marlie Spire", TxNotes = "implementation", AWU = "YYI", BB = "Mauv", AWL = "Mariner", OH = "CA-MB", EL = null, NVT = "CA", WKS = 20, Chart = "mspirej@newyorker.com", Test = "Spire" }
        };


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

        public List<TreatmentCardGrid> GetTreatmentCardEntries()
        {
            //var TreatmentCardGridEntries = await _httpClient.GetFromJsonAsync<List<TreatmentCardGrid>>("sample-data/treatmentgrid2.json");
            
            //if (TreatmentCardGridEntries == null)
            //{
            //    return new List<TreatmentCardGrid>();
            //}

            return TreatmentCardGridEntries;
        }

        public void AddTreatmentCardEntry(TreatmentCardGrid treatmentEntry)
        {
            TreatmentCardGridEntries.Add(treatmentEntry);
        }

        public void UpdateTreatmentCardEntry(TreatmentCardGrid treatmentEntry)
        {
            var existingEntry = TreatmentCardGridEntries.FirstOrDefault(t => t.Id == treatmentEntry.Id);

            if (existingEntry != null)
            {
                //TODO
            }
        }

        public void DeleteTreatmentCardEntry(TreatmentCardGrid treatmentEntry)
        {
            var existingEntry = TreatmentCardGridEntries.FirstOrDefault(t => t.Id == treatmentEntry.Id);

            if (existingEntry != null)
            {
                TreatmentCardGridEntries.Remove(existingEntry);
            }
        }
    }
}
