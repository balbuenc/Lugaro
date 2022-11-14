using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class AccountPlanRepository : IAccountPlanRepository
    {
        private SqlConfiguration _connectionString;

        public AccountPlanRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteAccountPlan(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"DELETE from plan_cuentas
                        WHERE id_plan_cuenta = @Id ";

                var result = await db.ExecuteAsync(sql, new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<AccountPlan>> GetAllAccountPlans()
        {
            try
            {
                var db = dbConnection();
                var sql = @"select  pc.id_plan_cuenta, pc.cuenta, pc.descripcion, pc.imputable , pc.cuenta  || ' (' || upper( pc.descripcion) || ')' as denominacion, pc.impositivo, pc.con_costo, pc.nivel
                            from plan_cuentas pc
                            where pc.imputable = true
                            order by pc.cuenta   ";

                var result = await db.QueryAsync<AccountPlan>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AccountPlan> GetAccountPlanDetails(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = "select * from plan_cuentas  where id_plan_cuenta = @Id";


                return await db.QueryFirstOrDefaultAsync<AccountPlan>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertAccountPlan(AccountPlan accountPlan)
        {
            try
            {
                var db = dbConnection();

                var sql = @"INSERT INTO public.plan_cuentas
                                        (cuenta, descripcion, imputable, impositivo, con_costo, nivel)
                                        VALUES(@cuenta, @descripcion, @imputable, @impositivo, @con_costo, @nivel);
                                        ";

                var result = await db.ExecuteAsync(sql, new
                {
                    accountPlan.cuenta,
                    accountPlan.descripcion,
                    accountPlan.imputable,
                    accountPlan.impositivo,
                    accountPlan.con_costo,
                    accountPlan.nivel
                }
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateAccountPlan(AccountPlan accountPlan)
        {
            try
            {
                var db = dbConnection();

                var sql = @"UPDATE public.plan_cuentas
                            SET cuenta=@cuenta, descripcion=@descripcion, imputable=@imputable, impositivo=@impositivo, con_costo=@con_costo, nivel=@nivel
                            WHERE id_plan_cuenta=@id_plan_cuenta;";

                var result = await db.ExecuteAsync(sql, new
                {
                    accountPlan.id_plan_cuenta,
                    accountPlan.cuenta,
                    accountPlan.descripcion,
                    accountPlan.imputable,
                    accountPlan.impositivo,
                    accountPlan.con_costo,
                    accountPlan.nivel
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
