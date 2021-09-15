using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEDC.Homework.Class02.ClientApp.DataModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.Homework.Class02.ClientApp.Services
{
    public class ApiService : IApiService
    {
        private readonly string _baseUrl = $"http://localhost:21094";
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<User>> GetAll()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_baseUrl}/api/users");

            return await response.Content.ReadAsAsync<List<User>>();

        
        }
        public ActionResult<bool> CreateUser(User user)
        {
            var userJson = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8,
                "application/json");

            var response = _httpClient.PostAsync($"{_baseUrl}/api/users/create-user", userJson).Result;

            return response.IsSuccessStatusCode;
        }
    }
}
