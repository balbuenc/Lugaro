using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class QuotaRepository : IQuotaRepository
    {
        private SqlConfiguration _connectionString;

        public QuotaRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteQuota(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"DELETE from cuotas
                        WHERE id_cuota = @Id ";

                var result = await db.ExecuteAsync(sql, new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteQuotaBySaleId(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"DELETE from cuotas
                        WHERE id_venta = @Id ";

                var result = await db.ExecuteAsync(sql, new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Quota>> GetAllQuotas()
        {
            try
            {
                var db = dbConnection();
                var sql = @"select v.factura , v.condicion , c2.razon_social as cliente, c2.ruc , m.moneda,
                            c.id_cuota, c.id_credito ,c.id_venta, c.cuota, c.monto_capital ,c.monto_interes ,c.multa , c.mora, c.vencimiento ,c.fecha_pago , c.estado, c.total,
                            c.cuota || '/' || c.cant_cuotas as cuota_str, c.id_moneda,
                            c.monto_capital - (select coalesce (sum(c3.monto_cobrado),0) from cobranzas c3 where c3.id_cuota = c.id_cuota) as saldo
                            from cuotas c 
                            left outer join ventas v on v.id_venta = c.id_venta 
                            left outer join presupuestos p on p.id_presupuesto = v.id_presupuesto 
                            left outer join clientes c2 on c2.id_cliente  = p.id_cliente 
                            left outer join monedas m on m.id_moneda = c.id_moneda 
                            order by c.id_cuota desc";

                var result = await db.QueryAsync<Quota>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Quota> GetQuotaDetails(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = "select * from cuotas  where id_cuota = @Id";


                return await db.QueryFirstOrDefaultAsync<Quota>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertQuota(Quota cuota)
        {
            try
            {
                var db = dbConnection();

                var sql = @"INSERT INTO public.cuotas
                            (id_credito, cuota, monto_capital, monto_interes, vencimiento, fecha_pago, multa, mora, id_venta)
                            VALUES(@id_credito, @cuota, @monto_capital, @monto_interes, @vencimiento, @fecha_pago, @multa, @mora, @id_venta);";

                var result = await db.ExecuteAsync(sql, new
                {
                    cuota.id_credito,
                    cuota.id_venta,
                    cuota.monto_capital,
                    cuota.monto_interes,
                    cuota.vencimiento,
                    cuota.fecha_pago,
                    cuota.multa,
                    cuota.mora
                }
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> GenerateSalesQuote(Sale sale)
        {
            try
            {
                var db = dbConnection();

                var sql = @"CALL public.sp_generar_cuotas(@p_id_venta,@p_id_credito,@p_id_usuario,@p_cant_cuotas,@p_primer_vencimiento);";

                var p = new DynamicParameters();
                p.Add("@p_id_venta", sale.id_venta, System.Data.DbType.Int32);
                p.Add("@p_id_credito", null, System.Data.DbType.Int32);
                p.Add("@p_id_usuario", null, System.Data.DbType.Int32);
                p.Add("@p_cant_cuotas", 1,  System.Data.DbType.Int32);
                p.Add("@p_primer_vencimiento", sale.fecha, System.Data.DbType.Date);

                var result = await db.ExecuteAsync(sql, p);

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateQuota(Quota cuota)
        {
            int? IdCredito = null;
            int? IdVenta = null;

            try
            {
                var db = dbConnection();

                var sql = @"UPDATE public.cuotas
                            SET id_credito=@id_credito, 
                            cuota=@cuota, 
                            monto_capital=@monto_capital, 
                            monto_interes=@monto_interes, 
                            vencimiento=@vencimiento, 
                            fecha_pago=fecha_pago, 
                            multa=@multa, 
                            mora=@mora, 
                            id_venta=@id_venta, 
                            id_moneda=@id_moneda, 
                            estado=@estado
                            where id_cuota = @id_cuota;";

                if (cuota.id_credito != 0)
                    IdCredito = cuota.id_credito;
                if (cuota.id_venta != 0)
                    IdVenta = cuota.id_venta;

                cuota.id_venta = IdVenta;
                cuota.id_credito = IdCredito;

                var result = await db.ExecuteAsync(sql, new
                {
                    cuota.id_credito,
                    cuota.cuota,
                    cuota.id_venta,
                    cuota.monto_capital,
                    cuota.monto_interes,
                    cuota.vencimiento,
                    cuota.fecha_pago,
                    cuota.multa,
                    cuota.mora,
                    cuota.id_cuota,
                    cuota.estado,
                    cuota.id_moneda
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
