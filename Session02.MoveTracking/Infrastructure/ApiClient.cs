using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Session02.MoveTracking.Infrastructure
{
    public class ApiClient
    {
        private HttpClient _httpClient;
        public ApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://127.0.0.1:4914/");
        }
        public async Task<ResponseInfo> GetPersons()
        {
            var str = await _httpClient.GetStringAsync("PersonLocations");
            return await _httpClient.GetFromJsonAsync<ResponseInfo>("PersonLocations");
        }
    }
    public class ResponseInfo
    {
        public List<Person> Response { get; set; }
    }
}
