using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class EnterpriseRepository : IEnterpriseRepository
    {
        private SqlConfiguration _connectionString;

        public EnterpriseRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteEnterprise(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"DELETE from empresas
                        WHERE cod_empresa = @Id ";

                var result = await db.ExecuteAsync(sql, new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Enterprise>> GetAllEnterprises()
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT cod_empresa, empresa, ruc, direccion, telefono, email
                            FROM public.empresas;";

                var result = await db.QueryAsync<Enterprise>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Enterprise> GetEnterpriseDetails(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = "select * from empresas  where cod_empresa = @Id";


                return await db.QueryFirstOrDefaultAsync<Enterprise>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertEnterprise(Enterprise enterprise)
        {
            try
            {
                var db = dbConnection();

                var sql = @"INSERT INTO public.empresas
                                        (empresa, ruc, direccion, telefono, email)
                                        VALUES(@empresa, @ruc, @direccion, @telefono, @email);
                                        ";

                var result = await db.ExecuteAsync(sql, new
                {
                    enterprise.empresa,
                    enterprise.ruc,
                    enterprise.direccion,
                    enterprise.telefono,
                    enterprise.email
                }
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateEnterprise(Enterprise enterprise)
        {
            try
            {
                var db = dbConnection();

                var sql = @"UPDATE public.empresas
                                    SET empresa=@empresa, ruc=@ruc, direccion=@direccion, telefono=@telefono, email=@email
                                    WHERE cod_empresa=@cod_empresa;
                                    ";

                var result = await db.ExecuteAsync(sql, new
                {
                    enterprise.empresa,
                    enterprise.ruc,
                    enterprise.direccion,
                    enterprise.telefono,
                    enterprise.email,
                    enterprise.cod_empresa
                }
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
