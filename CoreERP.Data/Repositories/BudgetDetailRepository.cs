using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class BudgetDetailRepository : IBudgetDetailRepository
    {
        private SqlConfiguration _connectionString;

        public BudgetDetailRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteBudgetDetail(int id)
        {
            var db = dbConnection();

            var sql = @"DELETE FROM public.presupuesto_detalles
                        WHERE id_presupuesto_detalle=@Id;";

            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        public async Task<IEnumerable<BudgetDetails>> GetAllBudgetDetails()
        {
            var db = dbConnection();
            var sql = @"select pd.id_presupuesto, pd.id_producto, pd.id_descuento, pd.cantidad, pd.costo, pd.precio, p.codigo, p.producto , d.descuento, d.porcentaje 
                        from presupuesto_detalles pd
                        inner join productos p on p.id_producto = pd.id_producto
                        left outer join descuentos d on d.id_descuento = pd.id_descuento";

            return await db.QueryAsync<BudgetDetails>(sql, new { });
        }

        public async Task<IEnumerable<BudgetDetails>> GetBudgetDetails(int id)
        {
            var db = dbConnection();
            var sql = @"select pd.id_presupuesto_detalle, pd.id_presupuesto, pd.id_producto, pd.id_descuento, pd.cantidad, pd.costo, pd.precio, p.codigo, p.producto , d.descuento, pd.porcentaje , pd.total , pd.precio_unitario
                        from presupuesto_detalles pd
                        inner join productos p on p.id_producto = pd.id_producto
                        left outer join descuentos d on d.id_descuento = pd.id_descuento 
                        where pd.id_presupuesto = @Id
                        order by 1 desc";


            return await db.QueryAsync<BudgetDetails>(sql, new { Id = id });
        }

        public async Task<IEnumerable<BudgetDetails>> GetBudgetPDFDetails(int id)
        {
            var db = dbConnection();
            var sql = @"SELECT id_presupuesto, nro_presupuesto, fecha, estado, forma_pago, plazo_entrega, observaciones, codigo, producto, cantidad, precio, total, descuento, imagen, cliente, direccion, telefono, moneda, vendedor, porcentaje, cotizacion, ruc, contacto, direccion_entrega, condicion, monto_total, obra, motivo, precio_unitario, cliente_exento
                        FROM public.v_impresion_presupuestos
                        where id_presupuesto = @Id";


            return await db.QueryAsync<BudgetDetails>(sql, new { Id = id });
        }



        public async Task<bool> InsertBudgetDetail(BudgetDetails budgetDetail)
        {
            Discount discount;
            Product product;
            Currency currency_product;
            Currency currency_budget;
            Budget budget;

            try
            {
                var db = dbConnection();

                var sql_presupuesto = "select id_moneda from presupuestos p2  where id_presupuesto =@id_presupuesto";
                budget = await db.QueryFirstOrDefaultAsync<Budget>(sql_presupuesto, new
                {
                    budgetDetail.id_presupuesto
                });


                var sql_producto = "select costo, precio, id_moneda from productos d where d.id_producto = @id_producto";
                product = await db.QueryFirstOrDefaultAsync<Product>(sql_producto, new
                {
                    budgetDetail.id_producto
                }
                );

                budgetDetail.precio = product.precio;
                budgetDetail.costo = product.costo;



                var sql_descuento = "select porcentaje from descuentos d where d.id_descuento = @id_descuento";
                discount = await db.QueryFirstOrDefaultAsync<Discount>(sql_descuento, new
                {
                    budgetDetail.id_descuento

                }
                );

                //Obtengo las cotizacciones del tipo de moneda para el Producto
                var sql_moneda = "select cotizacion from  monedas m where m.id_moneda =@id_moneda";
                currency_product = await db.QueryFirstOrDefaultAsync<Currency>(sql_moneda, new
                {
                    product.id_moneda
                }
                );

                //Obtengo las cotizacciones del tipo de moneda para el Presupuesto
                currency_budget = await db.QueryFirstOrDefaultAsync<Currency>(sql_moneda, new
                {
                    budget.id_moneda
                }
                );

                if (product.id_moneda != budget.id_moneda)
                {
                    if (budget.id_moneda == 1)
                    {
                        budgetDetail.precio = budgetDetail.precio * currency_product.cotizacion;
                        budgetDetail.precio_unitario = product.precio * currency_product.cotizacion;
                    }
                    else 
                    {
                        budgetDetail.precio = budgetDetail.precio / currency_product.cotizacion;
                        budgetDetail.precio_unitario = product.precio / currency_product.cotizacion;
                    }
                }
                else
                {
                    budgetDetail.precio_unitario = product.precio;
                }


                budgetDetail.precio_unitario = (budgetDetail.precio - (budgetDetail.precio * (budgetDetail.porcentaje / 100)));
                budgetDetail.total = (budgetDetail.precio - (budgetDetail.precio * (discount.porcentaje / 100))) * budgetDetail.cantidad;

                var sql = @"INSERT INTO public.presupuesto_detalles
                        (id_presupuesto, id_producto, id_descuento, cantidad, costo, precio,total,porcentaje,precio_unitario)
                        VALUES(@id_presupuesto, @id_producto, @id_descuento, @cantidad, @costo, @precio, @total,@porcentaje,@precio_unitario);";

                var result = await db.ExecuteAsync(sql, new
                {
                    budgetDetail.id_presupuesto,
                    budgetDetail.id_producto,
                    budgetDetail.id_descuento,
                    budgetDetail.cantidad,
                    budgetDetail.costo,
                    budgetDetail.precio,
                    budgetDetail.total,
                    budgetDetail.porcentaje,
                    budgetDetail.precio_unitario
                }
                );

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateBudgetDetail(BudgetDetails budgetDetail)
        {
            Discount discount;
            //Product product;

            try
            {

                var db = dbConnection();

                //var sql_producto = "select costo, precio from productos d where d.id_producto = @id_producto";
                //product = await db.QueryFirstOrDefaultAsync<Product>(sql_producto, new
                //{
                //    budgetDetail.id_producto
                //}
                //);

                //budgetDetail.precio = product.precio;
                //budgetDetail.costo = product.costo;



                var sql_descuento = "select porcentaje from descuentos d where d.id_descuento = @id_descuento";
                discount = await db.QueryFirstOrDefaultAsync<Discount>(sql_descuento, new
                {
                    budgetDetail.id_descuento

                }
                );

                if (budgetDetail.id_descuento == 1)
                {
                    budgetDetail.total = (budgetDetail.precio - (budgetDetail.precio * (budgetDetail.porcentaje / 100))) * budgetDetail.cantidad;
                    budgetDetail.precio_unitario = (budgetDetail.precio - (budgetDetail.precio * (budgetDetail.porcentaje / 100)));
                }
                else
                {
                    budgetDetail.total = (budgetDetail.precio - (budgetDetail.precio * (discount.porcentaje / 100))) * budgetDetail.cantidad;
                    budgetDetail.precio_unitario = (budgetDetail.precio - (budgetDetail.precio * (discount.porcentaje / 100)));
                }



                var sql = @"UPDATE public.presupuesto_detalles
                        SET id_descuento=@id_descuento, cantidad=@cantidad, costo=@costo, precio=@precio, id_producto=@id_producto, total=@total, porcentaje=@porcentaje, precio_unitario =@precio_unitario 
                        WHERE id_presupuesto_detalle=@id_presupuesto_detalle;";

                var result = await db.ExecuteAsync(sql, new
                {
                    budgetDetail.id_presupuesto,
                    budgetDetail.id_producto,
                    budgetDetail.id_descuento,
                    budgetDetail.cantidad,
                    budgetDetail.costo,
                    budgetDetail.precio,
                    budgetDetail.total,
                    budgetDetail.id_presupuesto_detalle,
                    budgetDetail.porcentaje,
                    budgetDetail.precio_unitario
                }
                );

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> UpdateUnitaryPriceBudgetDetail(BudgetDetails budgetDetail)
        {
            Discount discount;
            //Product product;

            try
            {

                var db = dbConnection();

                //var sql_producto = "select costo, precio from productos d where d.id_producto = @id_producto";
                //product = await db.QueryFirstOrDefaultAsync<Product>(sql_producto, new
                //{
                //    budgetDetail.id_producto
                //}
                //);

                //budgetDetail.precio = product.precio;
                //budgetDetail.costo = product.costo;



                var sql_descuento = "select porcentaje from descuentos d where d.id_descuento = @id_descuento";
                discount = await db.QueryFirstOrDefaultAsync<Discount>(sql_descuento, new
                {
                    budgetDetail.id_descuento

                }
                );

                if (budgetDetail.id_descuento == 1)
                {
                    budgetDetail.total = budgetDetail.precio_unitario * budgetDetail.cantidad;
                    budgetDetail.porcentaje = 100 -( (budgetDetail.precio_unitario * 100) / budgetDetail.precio);
                }
                else
                {
                    budgetDetail.total = (budgetDetail.precio - (budgetDetail.precio * (discount.porcentaje / 100))) * budgetDetail.cantidad;
                    budgetDetail.precio_unitario = (budgetDetail.precio - (budgetDetail.precio * (discount.porcentaje / 100)));
                }



                var sql = @"UPDATE public.presupuesto_detalles
                        SET id_descuento=@id_descuento, cantidad=@cantidad, costo=@costo, precio=@precio, id_producto=@id_producto, total=@total, porcentaje=@porcentaje, precio_unitario =@precio_unitario 
                        WHERE id_presupuesto_detalle=@id_presupuesto_detalle;";

                var result = await db.ExecuteAsync(sql, new
                {
                    budgetDetail.id_presupuesto,
                    budgetDetail.id_producto,
                    budgetDetail.id_descuento,
                    budgetDetail.cantidad,
                    budgetDetail.costo,
                    budgetDetail.precio,
                    budgetDetail.total,
                    budgetDetail.id_presupuesto_detalle,
                    budgetDetail.porcentaje,
                    budgetDetail.precio_unitario
                }
                );

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
