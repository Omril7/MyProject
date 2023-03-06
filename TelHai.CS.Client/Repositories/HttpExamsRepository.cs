using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;
using TelHai.CS.Client.Models;

namespace TelHai.CS.Client.Repositories
{
    public class HttpExamsRepository
    {
        private List<Exam> _exams;
        HttpClient clientApi;

        static private HttpExamsRepository _instance = null;

        private HttpExamsRepository()
        {
            clientApi = new HttpClient();
            clientApi.BaseAddress = new Uri("https://localhost:7003");
            _exams = new List<Exam>();
        }

        public static HttpExamsRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HttpExamsRepository();
                }
                return _instance;
            }
        }

        public async Task<List<Exam>> GetAllExamsAsync()
        {
            var response = await clientApi.GetAsync("API/Exam");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.Content.ReadFromJsonAsync<List<Exam>>();
        }

        public async Task<Exam> GetExamAsync(int id)
        {
            var response = await clientApi.GetAsync($"API/Exam/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<Exam>();
        }

        public async Task<Exam> CreateExamAsync(Exam exam)
        {
            var response = await clientApi.PostAsJsonAsync("API/Exam", exam);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<Exam>();
        }

        public async Task<bool> UpdateExamAsync(int id, Exam exam)
        {
            var response = await clientApi.PutAsJsonAsync($"API/Exam/{id}", exam);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteExamAsync(int id)
        {
            var response = await clientApi.DeleteAsync($"API/Exam/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
