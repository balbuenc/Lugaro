using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class DiscountLimitRepository : IDiscountLimitRepository
    {
        private SqlConfiguration _connectionString;

        public DiscountLimitRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteDiscountLimit(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"DELETE from limite_descuentos
                        WHERE id_limite_descuento = @Id ";

                var result = await db.ExecuteAsync(sql, new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<DiscountLimit>> GetAllDiscountLimits()
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT ld.id_limite_descuento, ld.id_marca, ld.limite, m.marca 
                            FROM public.limite_descuentos ld
                            left outer join marcas m on m.id_marca = ld.id_marca ;";

                var result = await db.QueryAsync<DiscountLimit>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DiscountLimit>> GetDiscountLimitsByBudgetID(int budgetId)
        {
            try
            {
                var db = dbConnection();
                var sql = @"select distinct ld.id_limite_descuento, ld.id_marca, ld.limite, m2.marca 
                            from presupuesto_detalles pd 
                            right join productos p2 on p2.id_producto  = pd.id_producto 
                            right  join marcas m2 on m2.id_marca = p2.id_marca 
                            right join limite_descuentos ld on ld.id_marca = m2.id_marca 
                            where pd.id_presupuesto = @Id;";

                var result = await db.QueryAsync<DiscountLimit>(sql, new { Id = budgetId});

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DiscountLimit> GetDiscountLimitDetails(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT ld.id_limite_descuento, ld.id_marca, ld.limite, m.marca 
                            FROM public.limite_descuentos ld
                            left outer join marcas m on m.id_marca = ld.id_marca
                            where id_limite_descuento = @Id";


                return await db.QueryFirstOrDefaultAsync<DiscountLimit>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertDiscountLimit(DiscountLimit discountLimit)
        {
            try
            {
                var db = dbConnection();

                var sql = @"INSERT INTO public.limite_descuentos
                            (id_marca, limite)
                            VALUES(@id_marca, @limite);";

                var result = await db.ExecuteAsync(sql, new
                {
                    discountLimit.id_marca,
                    discountLimit.limite
                }
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateDiscountLimit(DiscountLimit discountLimit)
        {
            try
            {
                var db = dbConnection();

                var sql = @"UPDATE public.limite_descuentos
                            SET id_marca=@id_marca, limite=@limite
                            WHERE id_limite_descuento=@id_limite_descuento;";

                var result = await db.ExecuteAsync(sql, new
                {
                    discountLimit.id_limite_descuento,
                    discountLimit.id_marca,
                    discountLimit.limite
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
