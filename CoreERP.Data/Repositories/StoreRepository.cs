using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class StoreRepository : IStoreRepository
    {

        private SqlConfiguration _connectionString;

        public StoreRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteStore(int id)
        {
            var db = dbConnection();

            var sql = @"DELETE from depositos
                        WHERE id_deposito = @Id ";

            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        public async Task<IEnumerable<Store>> GetAllStores()
        {
            var db = dbConnection();
            var sql = "select * from depositos order by deposito asc";

            return await db.QueryAsync<Store>(sql, new { });
        }

        public async Task<Store> GetStoreDetails(int id)
        {
            var db = dbConnection();
            var sql = "select * from depositos  where id_deposito = @Id";


            return await db.QueryFirstOrDefaultAsync<Store>(sql, new { Id = id });
        }

        public async Task<Store> GetStoreDetailsByName(string name)
        {
            var db = dbConnection();
            var sql = "select * from depositos  where deposito = @deposito";


            return await db.QueryFirstOrDefaultAsync<Store>(sql, new { deposito = name });
        }

        public async Task<bool> InsertStore(Store store)
        {
            var db = dbConnection();

            var sql = @"INSERT INTO public.depositos (deposito,descripcion) VALUES(@deposito,@deposito);";

            var result = await db.ExecuteAsync(sql, new
            {
                store.deposito,
                store.descripcion
            }
            );

            return result > 0;
        }

        public async Task<bool> UpdateStore(Store store)
        {
            var db = dbConnection();

            var sql = @"UPDATE public.depositos
                        set deposito =@deposito,
                            descripcion=@descripcion
                        where id_deposito=@id_deposito;";

            var result = await db.ExecuteAsync(sql, new
            {
                store.id_deposito,
                store.deposito,
                store.descripcion
            }
            );

            return result > 0;
        }
    }
}
