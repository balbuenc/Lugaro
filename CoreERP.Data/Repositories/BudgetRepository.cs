using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private SqlConfiguration _connectionString;

        public BudgetRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteBudget(int id)
        {
            try
            {
                var db = dbConnection();

                //Delete Budget details before the Budget Header
                var sql_delete_details = @"delete from presupuesto_detalles where id_presupuesto = @Id";

                
                await db.ExecuteAsync(sql_delete_details, new { Id = id });

                //Delete Budget Header 
                var sql = @"DELETE from presupuestos
                        WHERE id_presupuesto = @Id ";

                
                var result = await db.ExecuteAsync(sql, new { Id = id });
             
                return result > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Budget>> GetAllBudgets()
        {
            var db = dbConnection();
            var sql = @"select p.id_presupuesto, p.nro_presupuesto , p.fecha, p.estado, p.id_cliente, p.id_moneda, p.cotizacion, f2.usuario as vendedor, c2.razon_social as cliente, m2.moneda, p.forma_pago, p.plazo_entrega, p.observaciones, p.contacto, p.direccion_entrega, cv.condicion, p.id_condicion_venta, p.obra, p.motivo, c2.cliente_exento
                        ,v.estado as estado_venta,
                        (select sum(pd.total)  from presupuesto_detalles pd where pd.id_presupuesto = p.id_presupuesto) as total
                        from presupuestos p
                        left outer join funcionarios f2 on f2.id_funcionario = p.id_funcionario
                        left outer join clientes c2 on c2.id_cliente = p.id_cliente
                        left outer join monedas m2 on m2.id_moneda = p.id_moneda 
                        left outer join condicion_venta cv on cv.id_condicion_venta  = p.id_condicion_venta 
                        left outer join ventas v on v.id_presupuesto = p.id_presupuesto and v.estado != 'ANULADO'
                        order by p.id_presupuesto desc";

            return await db.QueryAsync<Budget>(sql, new { });
        }

        public async Task<IEnumerable<Budget>> GetAllBudgetsByUserName(string userName, bool canViewOnlyOwned)
        {
            var db = dbConnection();
            string sql;

            if (!canViewOnlyOwned)
            {
                sql = @"select p.id_presupuesto, p.nro_presupuesto , p.fecha, p.estado, p.id_cliente, p.id_moneda, p.cotizacion, f2.usuario as vendedor, c2.razon_social as cliente, m2.moneda, p.forma_pago, p.plazo_entrega, p.observaciones, p.contacto, p.direccion_entrega, cv.condicion, p.id_condicion_venta, p.obra, p.motivo, c2.cliente_exento
                        ,v.estado as estado_venta
                        from presupuestos p
                        left outer join funcionarios f2 on f2.id_funcionario = p.id_funcionario
                        left outer join clientes c2 on c2.id_cliente = p.id_cliente
                        left outer join monedas m2 on m2.id_moneda = p.id_moneda 
                        left outer join condicion_venta cv on cv.id_condicion_venta  = p.id_condicion_venta 
                        left outer join ventas v on v.id_presupuesto = p.id_presupuesto and v.estado != 'ANULADO'
                        order by p.id_presupuesto desc";
            }
            else
            {
                sql = @"select p.id_presupuesto, p.nro_presupuesto , p.fecha, p.estado, p.id_cliente, p.id_moneda, p.cotizacion, f2.usuario as vendedor, c2.razon_social as cliente, m2.moneda, p.forma_pago, p.plazo_entrega, p.observaciones, p.contacto, p.direccion_entrega, cv.condicion, p.id_condicion_venta, p.obra, p.motivo, c2.cliente_exento
                        ,v.estado as estado_venta
                        from presupuestos p
                        left outer join funcionarios f2 on f2.id_funcionario = p.id_funcionario
                        left outer join clientes c2 on c2.id_cliente = p.id_cliente
                        left outer join monedas m2 on m2.id_moneda = p.id_moneda 
                        left outer join condicion_venta cv on cv.id_condicion_venta  = p.id_condicion_venta 
                        left outer join ventas v on v.id_presupuesto = p.id_presupuesto and v.estado != 'ANULADO'
                        where f2.usuario =  @user
                        order by p.id_presupuesto desc";
            }

            return await db.QueryAsync<Budget>(sql, new { user = userName });
        }

        public async Task<Budget> GetBudgetDetails(int id)
        {
            var db = dbConnection();
            var sql = @"select p.id_presupuesto, p.nro_presupuesto , p.fecha, p.estado, p.id_cliente, p.id_moneda, p.cotizacion, f2.usuario as vendedor, c2.razon_social as cliente, m2.moneda, p.forma_pago, p.plazo_entrega, p.observaciones, p.contacto, p.direccion_entrega, cv.condicion, p.id_condicion_venta,p.id_funcionario, p.obra, p.motivo, c2.cliente_exento
                        ,v.estado as estado_venta
                        from presupuestos p
                        left outer join funcionarios f2 on f2.id_funcionario = p.id_funcionario
                        left outer join clientes c2 on c2.id_cliente = p.id_cliente
                        left outer join monedas m2 on m2.id_moneda = p.id_moneda
                        left outer join condicion_venta cv on cv.id_condicion_venta  = p.id_condicion_venta 
                        left outer join ventas v on v.id_presupuesto = p.id_presupuesto
                        where p.id_presupuesto = @Id
                        order by v.id_venta desc 
                        limit 1";

            return await db.QueryFirstOrDefaultAsync<Budget>(sql, new { Id = id });
        }

        public async Task<Int32> InsertBudget(Budget budget)
        {
            var db = dbConnection();


            try
            {
                var sqlCotization = @"select * from monedas m where id_moneda = @id_moneda;";

                var resulCotizacion = await db.QueryFirstOrDefaultAsync<Currency>(sqlCotization, new
                {
                    budget.id_moneda
                }
                );

                budget.cotizacion = resulCotizacion.cotizacion;

                var sql = @"INSERT INTO public.presupuestos (id_cliente, id_funcionario, fecha, estado, nro_presupuesto,id_moneda,cotizacion,plazo_entrega,forma_pago,observaciones,contacto,direccion_entrega,id_condicion_venta,obra,motivo) 
                            VALUES(@id_cliente, @id_funcionario, @fecha, @estado, @nro_presupuesto,@id_moneda,@cotizacion,@plazo_entrega,@forma_pago,@observaciones,@contacto,@direccion_entrega,@id_condicion_venta,@obra,@motivo)
                            RETURNING id_presupuesto;";


                budget.estado = "GENERADO";

                Object result = await db.ExecuteScalarAsync(sql, new
                {
                    budget.id_cliente,
                    budget.id_funcionario,
                    budget.fecha,
                    budget.estado,
                    budget.nro_presupuesto,
                    budget.id_moneda,
                    budget.cotizacion,
                    budget.plazo_entrega,
                    budget.forma_pago,
                    budget.observaciones,
                    budget.contacto,
                    budget.direccion_entrega,
                    budget.id_condicion_venta,
                    budget.obra,
                    budget.motivo
                }
                );

                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<bool> UpdateBudget(Budget budget)
        {
            try
            {
                var db = dbConnection();

                var sql = @"UPDATE public.presupuestos
                        SET id_cliente=@id_cliente,  fecha=@fecha, estado=@estado, nro_presupuesto=@nro_presupuesto, id_moneda=@id_moneda,plazo_entrega=@plazo_entrega,forma_pago=@forma_pago,observaciones=@observaciones,contacto=@contacto,direccion_entrega=@direccion_entrega,id_condicion_venta=@id_condicion_venta,obra=@obra,motivo=@motivo
                        WHERE id_presupuesto=@id_presupuesto;
                        ";

                var result = await db.ExecuteAsync(sql, new
                {
                    budget.id_presupuesto,
                    budget.id_cliente,
                    budget.fecha,
                    budget.estado,
                    budget.nro_presupuesto,
                    budget.id_moneda,
                    budget.plazo_entrega,
                    budget.forma_pago,
                    budget.observaciones,
                    budget.contacto,
                    budget.direccion_entrega,
                    budget.id_condicion_venta,
                    budget.obra,
                    budget.motivo
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
