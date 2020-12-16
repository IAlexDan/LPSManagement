using LPSManagement.Client.Features;
using LPSManagement.Shared.Models;
using LPSManagement.Shared.RequestFeatures;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace LPSManagement.Client.Pages.EmployeComponent
{
    public class EmployeClient
    {
        private  HttpClient _httpClient { get; set; }

        public EmployeClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateEmployeAsync(Employe employe) =>
            await _httpClient.PostAsJsonAsync("api/employes", employe);

        //public async Task<List<Employe>> GetEmployesAsync() =>
        //    await _httpClient.GetFromJsonAsync<List<Employe>>("api/employe");

        public async Task<PagingResponse<Employe>> GetEmployesAsync(EmployeParameters employeParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = employeParameters.PageNumber.ToString(),
                ["searchTerm"] = employeParameters.SearchTerm == null ? "" : employeParameters.SearchTerm,
                ["expDateSearch"] = employeParameters.ExpDateSearch == null || employeParameters.ExpDateSearch != "expdate" ? "" : employeParameters.ExpDateSearch = "expdate",
                ["orderBy"] = employeParameters.OrderBy
            };

            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("api/employes", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<Employe>
            {
                Items = JsonSerializer.Deserialize<List<Employe>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;
        }       

        //public async Task<List<Employe>> GetExpEmployesAsync()
        //{
        //    var all = await GetEmployesAsync();
        //    var exp = all.Where(d =>
        //    d.StartDate.Date < DateTime.Now.Date.AddDays(-336)
        //    && d.StartDate.Date > DateTime.Now.Date.AddDays(-396))
        //    ;
        //    return exp.ToList();
        //}

        public async Task<Employe> GetEmployeByIdAsync(int employeId) =>
            await _httpClient.GetFromJsonAsync<Employe>($"api/employes/{employeId}");

        public async Task DeleteEmployeAsync(int employeId) =>
            await _httpClient.DeleteAsync($"api/employes/{employeId}");

        //public async Task DeleteEmployeAsync(int employeId)
        //{
        //    var url = Path.Combine("api/employes", employeId.ToString());
        //}


        public async Task UpdateEmployeAsync(Employe employe)
        {
            await _httpClient.PutAsJsonAsync("api/employes", employe);
        }


    }
}
