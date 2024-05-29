using Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Client.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private string _authToken;

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44353/api/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void SetAuthToken(string token)
        {
            _authToken = token;
        }

        private void AddAuthorizationHeader()
        {
            if (!string.IsNullOrEmpty(_authToken))
            {
                if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
                {
                    _httpClient.DefaultRequestHeaders.Remove("Authorization");
                }
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authToken}");
            }
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            AddAuthorizationHeader();
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }
            else
            {
                throw new Exception($"API request failed with status code {response.StatusCode}");
            }
        }

        public async Task<bool> PostAsync<TData>(string endpoint, TData data)
        {
            AddAuthorizationHeader();
            var jsonContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(endpoint, jsonContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string endpoint)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.DeleteAsync(endpoint);
            return response.IsSuccessStatusCode;
        }

        public async Task<TResponse> PostAsync<TResponse, TData>(string endpoint, TData data)
        {
            AddAuthorizationHeader();
            var jsonContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(endpoint, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<TResponse>();
            }
            else
            {
                throw new Exception($"API request failed with status code {response.StatusCode}");
            }
        }

        public async Task<TResponse> PostAsync<TResponse>(string endpoint, string token)
        {
            var url = $"{endpoint}/{token}";
            var response = await _httpClient.PostAsync(url, null);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<TResponse>();
            }
            else
            {
                throw new Exception($"API request failed with status code {response.StatusCode}");
            }
        }
    }
}
