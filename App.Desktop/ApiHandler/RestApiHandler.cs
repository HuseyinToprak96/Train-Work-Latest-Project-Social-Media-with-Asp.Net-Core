using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;

namespace App.Desktop.ApiHandler
{
    public class RestApiHandler
    {
        private readonly string _baseUrl;

        public RestApiHandler(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<string> PostAsync(string url, object data, string jwtToken)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                var requestContent = new StringContent(
                    content: Newtonsoft.Json.JsonConvert.SerializeObject(data),
                    encoding: Encoding.UTF8,
                    mediaType: "application/json"
                );

                using (var response = await httpClient.PostAsync(url, requestContent).ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return responseContent;
                }
            }
        }

        public async Task<string> GetAsync(string url, string jwtToken)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                using (var response = await httpClient.GetAsync(url).ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return responseContent;
                }
            }
        }


        public async Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest data, string jwtToken)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseUrl);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                var jsonData = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync(url, content);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<TResponse>(responseContent);

                return responseObject;
            }
        }

        public async Task<bool> DeleteAsync(string url, string jwtToken)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseUrl);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                var response = await httpClient.DeleteAsync(url);
                return response.IsSuccessStatusCode;
            }
        }
    }
}
