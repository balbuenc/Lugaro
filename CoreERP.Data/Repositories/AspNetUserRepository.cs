using CoreERP.Model;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class AspNetUserRepository : IAspNetUserRepository
    {

        private SqlConfiguration _connectionString;
   
        private readonly IConfiguration _configuration;

        public AspNetUserRepository(SqlConfiguration connectionStringg, IConfiguration configuration)
        {
            //_connectionString = connectionStringg;
         
            _configuration = configuration;

        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_configuration.GetConnectionString("AuthenticationDB"));
        }

        public Task<bool> DeleteAspNetUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AspNetUser>> GetAllAspNetUsers()
        {
            try
            {
                var db = dbConnection();
                var sql = @"select ""Id"" , replace (""UserName"",'@lugaro','') as UserName , ""NormalizedUserName""  from ""AspNetUsers"" anu order by ""UserName""; ";

                var result = await db.QueryAsync<AspNetUser>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<AspNetUser> GetAspNetUserDetails(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAspNetUser(AspNetUser aspNetUser)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAspNetUser(AspNetUser aspNetUser)
        {
            throw new NotImplementedException();
        }
    }
}
