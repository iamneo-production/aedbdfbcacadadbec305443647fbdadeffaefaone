using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BookStoreApp.Models;

namespace BookStoreApp.Services
{
    public interface IFreelancerService
    {
        bool AddFreelancer(Freelancer freelancer);
        List<Freelancer> GetAllFreelancers();
        bool DeleteFreelancer(int id);
        Task<IEnumerable<string>> GetFreelancerNames();
    }
    public class FreelancerService : IFreelancerService
    {
        private readonly HttpClient _httpClient;
        public FreelancerService(HttpClient httpClient, IConfiguration configuration)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            _httpClient = new HttpClient(clientHandler);
            var apiSettings = configuration.GetSection("ApiSettings").Get<ApiSettings>();
            _httpClient.BaseAddress = new Uri(apiSettings.BaseUrl);
        }

        public bool AddFreelancer(Freelancer freelancer)
        {
            try
            {
                var json = JsonConvert.SerializeObject(freelancer);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + $"/Freelancer", content).Result;

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }
       public async Task<IEnumerable<string>> GetFreelancerNames()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/Freelancer/FreelancerTitle");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<string>>(data);
            }

            return new List<string>();
        }
        catch (HttpRequestException)
        {
            return new List<string>();
        }
    }
        public List<Freelancer> GetAllFreelancers()
        {
            try
            {
                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Freelancer").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<Freelancer>>(data);
                }

                return new List<Freelancer>();
            }
            catch (HttpRequestException)
            {
                return new List<Freelancer>();
            }
        }


        public bool DeleteFreelancer(int id)
        {
            try
            {
                HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + $"/Freelancer/{id}").Result;

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }
    }
}
