using CoreERP.Model;
using CoreERP.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreERP.UI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteEmployee(int id)
        {
            await _httpClient.DeleteAsync($"api/employee/{id}");
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>(
               await _httpClient.GetStreamAsync($"api/employee"),
               new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
               );
        }

        public async Task<Employee> GetEmployeeDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<Employee>(
              await _httpClient.GetStreamAsync($"api/employee/{id}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
        }


        public async Task<Employee> GetEmployeeDetailsByUsername(string usernane)
        {
            try
            { 
            return await JsonSerializer.DeserializeAsync<Employee>(
              await _httpClient.GetStreamAsync($"api/employee/GetUserId/{usernane}"),
              new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
              );
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task SaveEmployee(Employee employee)
        {
            var employeeJson = new StringContent(JsonSerializer.Serialize(employee),
               Encoding.UTF8, "application/json");

            if (employee.id_funcionario == 0)
                await _httpClient.PostAsync("api/employee", employeeJson);
            else
                await _httpClient.PutAsync("api/employee", employeeJson);
        }
    }
}
