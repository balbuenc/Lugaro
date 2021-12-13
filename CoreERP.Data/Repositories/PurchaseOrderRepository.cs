using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private SqlConfiguration _connectionString;

        public PurchaseOrderRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeletePurchaseOrder(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"DELETE from ordenes_compra
                        WHERE id_orden_compra = @Id ";

                var result = await db.ExecuteAsync(sql, new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<PurchaseOrder>> GetAllPurchaseOrders()
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT oc.id_orden_compra, oc.fecha, oc.id_funcionario, oc.estado, oc.id_compra_general, oc.id_compra, oc.tipo_compra, oc.aprobado_por, oc.fecha_aprobacion,
		                    f.usuario as propietario, f2.usuario  as aprobador
                            FROM public.ordenes_compra oc 
                            left outer join public.funcionarios f on f.id_funcionario = oc.id_funcionario 
                            left outer join public.funcionarios f2 on f2.id_funcionario = oc.aprobado_por 
                            order by oc.id_orden_compra desc";

                var result = await db.QueryAsync<PurchaseOrder>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PurchaseOrder> GetPurchaseOrderByPurchaseID(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT oc.id_orden_compra, oc.fecha, oc.id_funcionario, oc.estado, oc.id_compra_general, oc.id_compra, oc.tipo_compra, oc.aprobado_por, oc.fecha_aprobacion,
                            f.usuario as propietario, f2.usuario as aprobador
                            FROM public.ordenes_compra oc
                            left outer join public.funcionarios f on f.id_funcionario = oc.id_funcionario
                            left outer join public.funcionarios f2 on f2.id_funcionario = oc.aprobado_por 
                            where oc.id_compra = @Id";


                return await db.QueryFirstOrDefaultAsync<PurchaseOrder>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PurchaseOrder> GetPurchaseOrderBygeneralPurchaseID(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT oc.id_orden_compra, oc.fecha, oc.id_funcionario, oc.estado, oc.id_compra_general, oc.id_compra, oc.tipo_compra, oc.aprobado_por, oc.fecha_aprobacion,
                            f.usuario as propietario, f2.usuario as aprobador
                            FROM public.ordenes_compra oc
                            left outer join public.funcionarios f on f.id_funcionario = oc.id_funcionario
                            left outer join public.funcionarios f2 on f2.id_funcionario = oc.aprobado_por 
                            where oc.id_compra_general = @Id";


                return await db.QueryFirstOrDefaultAsync<PurchaseOrder>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PurchaseOrder> GetPurchaseOrderDetails(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT oc.id_orden_compra, oc.fecha, oc.id_funcionario, oc.estado, oc.id_compra_general, oc.id_compra, oc.tipo_compra, oc.aprobado_por, oc.fecha_aprobacion,
                            f.usuario as propietario, f2.usuario as aprobador
                            FROM public.ordenes_compra oc
                            left outer join public.funcionarios f on f.id_funcionario = oc.id_funcionario
                            left outer join public.funcionarios f2 on f2.id_funcionario = oc.aprobado_por 
                            where id_orden_compra = @Id";


                return await db.QueryFirstOrDefaultAsync<PurchaseOrder>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            try
            {
                var db = dbConnection();

                var sql = @"INSERT INTO public.ordenes_compra
                            (fecha, id_funcionario, estado, id_compra_general, id_compra, tipo_compra, aprobado_por, fecha_aprobacion)
                            VALUES(@fecha, @id_funcionario, @estado, @id_compra_general, @id_compra, @tipo_compra, @aprobado_por, @fecha_aprobacion);
                            ";

                var result = await db.ExecuteAsync(sql, new
                {
                    purchaseOrder.fecha,
                    purchaseOrder.id_funcionario,
                    purchaseOrder.estado,
                    purchaseOrder.id_compra_general,
                    purchaseOrder.id_compra,
                    purchaseOrder.tipo_compra,
                    purchaseOrder.aprobado_por,
                    purchaseOrder.fecha_aprobacion
                }
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            try
            {
                var db = dbConnection();

                var sql = @"UPDATE public.ordenes_compra
                            SET fecha=@fecha, estado=@estado, id_compra_general=@id_compra_general, id_compra=@id_compra, tipo_compra=@tipo_compra, aprobado_por=@aprobado_por, fecha_aprobacion=@fecha_aprobacion
                            WHERE id_orden_compra=@id_orden_compra;";

                var result = await db.ExecuteAsync(sql, new
                {
                    purchaseOrder.id_orden_compra,
                    purchaseOrder.fecha,
                    purchaseOrder.id_funcionario,
                    purchaseOrder.estado,
                    purchaseOrder.id_compra_general,
                    purchaseOrder.id_compra,
                    purchaseOrder.tipo_compra,
                    purchaseOrder.aprobado_por,
                    purchaseOrder.fecha_aprobacion
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
