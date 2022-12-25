using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class ClientInvoiceRepository : IClientInvoiceRepository
    {
        private SqlConfiguration _connectionString;

        public ClientInvoiceRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

      

        public async Task<IEnumerable<ClientInvoice>> GetAllClientInvoices()
        {
            try
            {
                var db = dbConnection();
                var sql = "select * from public.v_clientes_facturas order by fecha desc";

                var result = await db.QueryAsync<ClientInvoice>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ClientInvoice>> GetClientInvoiceDetails(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = "select * from public.v_clientes_facturas  where id_cliente = @Id order by fecha desc";


                return await db.QueryAsync<ClientInvoice>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
    }
}
