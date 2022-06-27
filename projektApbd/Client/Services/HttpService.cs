using projektApbd.Shared.Models.DTOs;

using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text;
using System.Diagnostics;

namespace projektApbd.Client.Services
{
    public interface IHttpService
    {
        Task<T> Get<T>(string uri);
        Task Delete<T>(string uri, object value);
        Task<T> Login<T, T2>(T2 requestBody);
        Task<T> Post<T>(string uri, T requestBody);
        Task<T> Post<T>(string uri, Object requestBody);
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

        public async Task Delete<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            await send<T>(request);
        }

        //public async Task<T> Get<T>(string uri)
        //{
         //   var request = new HttpRequestMessage(HttpMethod.Get, uri);
        //    return await send<T>(request);
        //}

        private async Task<T> send<T>(HttpRequestMessage request)
        {
            var user = await _localStorageService.GetItem<UserLoginResponse>("user");
            var isApiUrl = !request.RequestUri.IsAbsoluteUri;
            if (user != null && isApiUrl)
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", user.JwtToken);

            using var response = await _httpClient.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _navigationMenager.NavigateTo("login");
                return default;
            }

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                throw new Exception(error["message"]);
            }

            return await response.Content.ReadFromJsonAsync<T>();
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

        public async Task<T> Post<T>(string uri, T requestBody) 
        {
            T resultDate;
            StringContent data = null;

            try
            {
                if(requestBody != null)
                {
                    string json = JsonConvert.SerializeObject(requestBody);
                    data = new StringContent(json, Encoding.UTF8, "application/json");
                }

                HttpResponseMessage response = await _httpClient.PostAsync(uri, data);

                if(!response.IsSuccessStatusCode) throw new Exception($"PosrRequest: Response returned {response.StatusCode}");

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
            StringContent data = null;

            try
            {
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
    }
}
