using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BookStoreApp.Models;

namespace BookStoreApp.Services
{
    public interface IProjectService
    {
        bool AddProject(Project project);
        List<Project> GetAllProjects();
        bool DeleteProject(int id);
    }
    public class ProjectService : IProjectService
    {
        private readonly HttpClient _httpClient;
            public ProjectService(HttpClient httpClient,IConfiguration configuration)
        {
HttpClientHandler clientHandler = new HttpClientHandler();
clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
_httpClient=new HttpClient(clientHandler);
         var apiSettings = configuration.GetSection("ApiSettings").Get<ApiSettings>();
        _httpClient.BaseAddress =new Uri(apiSettings.BaseUrl) ;
        }

        public bool AddProject(Project project)
        {
            try
            {
                var json = JsonConvert.SerializeObject(project);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress+$"/Project", content).Result;

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        public List<Project> GetAllProjects()
        {
            try
            {
                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress+"/Project").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<Project>>(data);
                }

                return new List<Project>();
            }
            catch (HttpRequestException)
            {
                return new List<Project>();
            }
        }

        public bool DeleteProject(int id)
        {
            try
            {
                HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress+$"/Project/{id}").Result;

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }
    }
}
