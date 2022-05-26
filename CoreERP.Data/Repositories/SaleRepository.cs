﻿using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class SaleRepository : ISaleRepository
    {

        private SqlConfiguration _connectionString;

        public SaleRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteSale(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"DELETE from ventas
                        WHERE id_venta = @Id ";

                var result = await db.ExecuteAsync(sql, new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Sale>> GetAllSales()
        {
            try
            {
                var db = dbConnection();
                var sql = @"select * from ventas 
                            order by c.id_venta desc";

                var result = await db.QueryAsync<Sale>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Sale> GetSaleDetails(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = "select * from ventas  where id_presupuesto = @Id order by id_venta desc limit 1";


                return await db.QueryFirstOrDefaultAsync<Sale>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        public async Task<bool> InsertSale(Sale venta)
        {
            try
            {
                var db = dbConnection();

                //var sql = @"INSERT INTO public.ventas
                //            (id_credito, venta, monto_capital, monto_interes, vencimiento, fecha_pago, multa, mora, id_venta, estado)
                //            VALUES(@id_credito, @venta, @monto_capital, @monto_interes, @vencimiento, @fecha_pago, @multa, @mora, @id_venta, @estado);";

                var sql = @"INSERT INTO public.ventas
                            (id_credito, venta, monto_capital, monto_interes, vencimiento, fecha_pago, multa, mora, id_venta, estado)
                            VALUES(@id_credito, @venta, @monto_capital, @monto_interes, @vencimiento, @fecha_pago, @multa, @mora, @id_venta, @estado);";

                var result = await db.ExecuteAsync(sql, new
                {
                    venta.id_presupuesto,
                    venta.factura,
                    venta.fecha,
                    venta.importe,
                    venta.estado
                }
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Sale> GenerateSales(Budget budget)
        {
            try
            {
                var db = dbConnection();

                var sql = @"CALL public.sp_generar_venta_fecha(@p_id_presupuesto,@p_fecha_facturacion);";

                var p = new DynamicParameters();
                p.Add("@p_id_presupuesto", budget.id_presupuesto, System.Data.DbType.Int32);
                p.Add("@p_fecha_facturacion", budget.fecha_factura, System.Data.DbType.Date);

                var result = await db.ExecuteAsync(sql, p);

                var sql1 = "select * from ventas  where id_presupuesto = @Id";


                return await db.QueryFirstOrDefaultAsync<Sale>(sql1, new { Id = budget.id_presupuesto });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateSale(Sale venta)
        {
            try
            {
                var db = dbConnection();

                var sql = @"UPDATE public.ventas
                            SET id_presupuesto=@id_presupuesto, factura=@factura, fecha=@fecha, condicion=@condicion, importe=@importe, estado=@estado
                            where id_venta=@id_venta;";

                var result = await db.ExecuteAsync(sql, new
                {
                    venta.id_presupuesto,
                    venta.factura,
                    venta.fecha,
                    venta.importe,
                    venta.estado,
                    venta.condicion,
                    venta.id_venta
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
