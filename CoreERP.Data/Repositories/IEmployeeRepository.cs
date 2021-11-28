using CoreERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployees();

        Task<Employee> GetEmployeeDetails(int id);

        Task<Employee> GetEmployeeDetailsByUsername(string username);

        Task<bool> InsertEmployee(Employee employee);

        Task<bool> UpdateEmployee(Employee employee);

        Task<bool> DeleteEmployee(int id);
    }
}
