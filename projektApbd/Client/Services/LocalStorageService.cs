using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace projektApbd.Client.Services
{
    public interface ILocalStorageService
    {
        public Task<T> GetItem<T>(string key);
        public Task SetItem<T>(string key, T value);
        public Task RemoveItem(string key);
    }
    public class LocalStorageService : ILocalStorageService
    {
        private readonly IJSRuntime _JSRuntime;

        public LocalStorageService(IJSRuntime iJSRuntime)
        {
            _JSRuntime = iJSRuntime;
        }

        public async Task<T> GetItem<T>(string key)
        {
            var json = await _JSRuntime.InvokeAsync<string>("localStorage.getItem", key);

            if (json == null)
                return default;

            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task RemoveItem(string key)
        {
            await _JSRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }

        public async Task SetItem<T>(string key, T value)
        {
            string valueString = JsonConvert.SerializeObject(value);
            await _JSRuntime.InvokeVoidAsync("localStorage.setItem", key, valueString);
        }
    }
}