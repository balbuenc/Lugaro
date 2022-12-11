using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class DailyBookRepository : IDailyBookRepository
    {
        private SqlConfiguration _connectionString;

        public DailyBookRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteDailyBook(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"DELETE from libro_diario
                        WHERE id = @Id ";

                var result = await db.ExecuteAsync(sql, new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<DailyBook>> GetAllDailyBooks()
        {
            try
            {
                var db = dbConnection();
                var sql = @"select  ld.asiento_numero, pc.cuenta, pc.descripcion , ld.fecha, ld.debe, ld.haber 
                            from libro_diario ld 
                            inner join plan_cuentas pc on pc.id_plan_cuenta = ld.id_plan_cuenta 
                            order by 1 desc";

                var result = await db.QueryAsync<DailyBook>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DailyBook> GetDailyBookDetails(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = "select * from libro_diario  where id = @Id";


                return await db.QueryFirstOrDefaultAsync<DailyBook>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertDailyBook(DailyBook DailyBook)
        {
            try
            {
                var db = dbConnection();

                var sql = @"INSERT INTO public.libro_diario
                            (asiento_numero, fecha, id_plan_cuenta, debe, haber)
                            VALUES(@asiento_numero, @fecha, @id_plan_cuenta, @debe, @haber);
                            ";

                var result = await db.ExecuteAsync(sql, new
                {
                    DailyBook.asiento_numero,
                    DailyBook.fecha,
                    DailyBook.id_plan_cuenta,
                    DailyBook.debe,
                    DailyBook.haber
                }
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> GenerateDailyBook(string factura)
        {
            try
            {
                var db = dbConnection();

                var sql = @"CALL public.generarasientolibrodiario(@operacion,@factura);";

                var result = await db.ExecuteAsync(sql, new
                {
                   operacion= "FACTURA VENTA CREDITO",
                    factura=factura
                }
                );

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateDailyBook(DailyBook DailyBook)
        {
            try
            {
                var db = dbConnection();

                var sql = @"UPDATE public.libro_diario
                            SET  asiento_numero=@asiento_numero, fecha=@fecha, id_plan_cuenta=@id_plan_cuenta, debe=@debe, haber=@haber;
                            where id=@id
                            ";

                var result = await db.ExecuteAsync(sql, new
                {
                    DailyBook.asiento_numero,
                    DailyBook.fecha,
                    DailyBook.id_plan_cuenta,
                    DailyBook.debe,
                    DailyBook.haber,
                    DailyBook.id
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
