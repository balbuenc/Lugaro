using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private SqlConfiguration _connectionString;

        public EmployeeRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }
        public async Task<bool> DeleteEmployee(int id)
        {
            var db = dbConnection();

            var sql = @"DELETE from funcionarios
                        WHERE id_funcionario = @Id ";

            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var db = dbConnection();
            var sql = @"select f.*, c.cargo, r2.rol , r2.can_approve, r2.can_insert, r2.can_update, r2.can_delete  
                        from funcionarios f
                        left outer join cargos c on f.id_cargo = c.id_cargo
                        left outer join roles r2 on r2.id_rol = f.id_rol
                        order by f.apellidos, f.nombres asc";

            return await db.QueryAsync<Employee>(sql, new { });
        }

        public async Task<Employee> GetEmployeeDetails(int id)
        {
            var db = dbConnection();
            var sql = "select * from funcionarios  where id_funcionario = @Id";


            return await db.QueryFirstOrDefaultAsync<Employee>(sql, new { Id = id });
        }

        public async Task<Employee> GetEmployeeDetailsByUsername(string username)
        {
            var db = dbConnection();
            var sql = @"select f.*, c.cargo , r2.*
                        from funcionarios f
                        left outer join cargos c on f.id_cargo = c.id_cargo
                        left outer join roles r2 on r2.id_rol = f.id_rol
                        where usuario = @User";


            return await db.QueryFirstOrDefaultAsync<Employee>(sql, new { User = username });
        }

        public async Task<bool> InsertEmployee(Employee employee)
        {
            var db = dbConnection();

            var sql = @"INSERT INTO public.funcionarios
                        (usuario, nombres, apellidos, id_cargo,correo)
                        VALUES(@usuario, @nombres, @apellidos, @id_cargo, @correo);";

            var result = await db.ExecuteAsync(sql, new
            {
                employee.usuario,
                employee.nombres,
                employee.apellidos,
                employee.id_cargo,
                employee.correo
            }
            );

            return result > 0;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            var db = dbConnection();

            var sql = @"UPDATE public.funcionarios
                        SET usuario=@usuario, nombres=@nombres, apellidos=@apellidos, id_cargo=@id_cargo, correo=@correo
                        WHERE id_funcionario=@id_funcionario;";

            var result = await db.ExecuteAsync(sql, new
            {
                employee.id_funcionario,
                employee.usuario,
                employee.nombres,
                employee.apellidos,
                employee.id_cargo,
                employee.correo
            }
            );

            return result > 0;
        }
    }
}
