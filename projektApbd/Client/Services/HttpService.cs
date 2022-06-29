using projektApbd.Shared.Models.DTOs;

using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace projektApbd.Client.Services
{
    public interface IHttpService
    {
        Task<T> Get<T>(string uri);
        Task Delete(string uri);
        Task<T> Login<T, T2>(T2 requestBody);
        Task<T> Post<T>(string uri, Object requestBody);
        Task Post(string ui, Object requestBody);
    }
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationMenager;
        private readonly ILocalStorageService _localStorageService;
        private readonly IConfiguration _configuration;

        public HttpService(
            HttpClient httpClient,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService,
            IConfiguration configuration
            )
        {
            _httpClient = httpClient;
            _navigationMenager = navigationManager;
            _localStorageService = localStorageService;
            _configuration = configuration;
        }

        public async Task<T> Login<T, T2>(T2 requestBody)
        {
            T resultDate;
            StringContent data = null;

            try
            {
                if (requestBody != null)
                {
                    string json = JsonConvert.SerializeObject(requestBody);
                    data = new StringContent(json, Encoding.UTF8, "application/json");
                }

                HttpResponseMessage response = await _httpClient.PostAsync("https://localhost:7040/api/User/login", data);

                if (!response.IsSuccessStatusCode) throw new Exception($"PostRequest: Response returned {response.StatusCode}");

                string result = await response.Content.ReadAsStringAsync();
                resultDate = JsonConvert.DeserializeObject<T>(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return default;
            }
            return resultDate;
        }

        public async Task<T> Get<T>(string uri)
        {
            T resultDate;

            try
            {
                var user = await _localStorageService.GetItem<UserLoginResponse>("user");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.JwtToken);
                HttpResponseMessage response = await _httpClient.GetAsync(uri);

                if (!response.IsSuccessStatusCode) throw new Exception($"GetRequest: Response returned {response.StatusCode}");

                string result = await response.Content.ReadAsStringAsync();
                resultDate = JsonConvert.DeserializeObject<T>(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return default;
            }
            return resultDate;
        }

        public async Task<T> Post<T>(string uri, object requestBody)
        {
            T resultDate;
            StringContent data = null;

            try
            {
                if (requestBody != null)
                {
                    string json = JsonConvert.SerializeObject(requestBody);
                    data = new StringContent(json, Encoding.UTF8, "application/json");
                }

                var user = await _localStorageService.GetItem<UserLoginResponse>("user");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.JwtToken);
                HttpResponseMessage response = await _httpClient.PostAsync(uri, data);

                if (!response.IsSuccessStatusCode) throw new Exception($"PostRequest: Response returned {response.StatusCode}");

                string result = await response.Content.ReadAsStringAsync();
                resultDate = JsonConvert.DeserializeObject<T>(result);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return default;
            }

            return resultDate;
        }

        public async Task Post(string uri, object requestBody)
        {
            StringContent data = null;

            try
            {
                if (requestBody != null)
                {
                    string json = JsonConvert.SerializeObject(requestBody);
                    data = new StringContent(json, Encoding.UTF8, "application/json");
                }

                var user = await _localStorageService.GetItem<UserLoginResponse>("user");
                if (user != null)
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.JwtToken);

                HttpResponseMessage response = await _httpClient.PostAsync(uri, data);

                if (!response.IsSuccessStatusCode) throw new Exception($"PostRequest: Response returned {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
        }

        public async Task Delete(string uri)
        {
            try
            {
                var user = await _localStorageService.GetItem<UserLoginResponse>("user");
                if (user != null)
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.JwtToken);

                HttpResponseMessage response = await _httpClient.DeleteAsync(uri);

                if (!response.IsSuccessStatusCode) throw new Exception($"DeleteRequest: Response returned {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
        }
    }
}
