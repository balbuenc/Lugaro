using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class CollectionRepository : ICollectionRepository
    {
        private SqlConfiguration _connectionString;

        public CollectionRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteCollection(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"DELETE from cobranzas
                        WHERE id_cobranza = @Id ";

                var result = await db.ExecuteAsync(sql, new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Collection>> GetAllCollections()
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT c.id_cobranza, c.fecha, c.id_cuota, c.id_funcionario, c.monto_cobrado, c.id_medio_pago, c.nro_recibo, c.nro_comprobante, c.nro_transaccion,
                                   cli.razon_social as cliente, cli.ruc, f.usuario as funcionario, cuo.cuota || '/' || cuo.cant_cuotas as cuota, mp.medio as medio_pago,
                                   ven.factura 
                            FROM public.cobranzas c
                            left outer join cuotas cuo on cuo.id_cuota = c.id_cuota
                            left outer join ventas ven on ven.id_venta  = cuo.id_venta
                            left outer join presupuestos pven on pven.id_presupuesto = ven.id_presupuesto
                            left outer join clientes cli on cli.id_cliente = pven.id_cliente
                            left outer join funcionarios f on f.id_funcionario = c.id_funcionario
                            left outer join medios_pago mp on mp.id_medio_pago = c.id_medio_pago
                            order by c.id_cobranza desc;";

                var result = await db.QueryAsync<Collection>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Collection> GetCollectionDetails(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = "select * from cobranzas  where id_cobranza = @Id";


                return await db.QueryFirstOrDefaultAsync<Collection>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertCollection(Collection collection)
        {
            try
            {
                var db = dbConnection();

                var sql = @"INSERT INTO public.cobranzas
                            (fecha, id_cuota, id_funcionario, monto_cobrado, id_medio_pago, nro_recibo, nro_comprobante, nro_transaccion)
                            VALUES(@fecha,  @id_cuota, @id_funcionario, @monto_cobrado, @id_medio_pago, @nro_recibo, @nro_comprobante, @nro_transaccion);";

                var result = await db.ExecuteAsync(sql, new
                {
                    collection.fecha,
                    collection.id_cuota,
                    collection.id_funcionario,
                    collection.monto_cobrado,
                    collection.id_medio_pago,
                    collection.nro_recibo,
                    collection.nro_comprobante,
                    collection.nro_transaccion

                }
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateCollection(Collection collection)
        {
            try
            {
                var db = dbConnection();

                var sql = @"UPDATE public.cobranzas
                            SET fecha=@fecha, id_cuota=@id_cuota, id_funcionario=@id_funcionario, monto_cobrado=@monto_cobrado, id_medio_pago=@id_medio_pago, nro_recibo=@nro_recibo, nro_comprobante=@nro_comprobante,
                            nro_transaccion=@nro_transaccion
                            WHERE id_cobranza=@id_cobranza;";

                var result = await db.ExecuteAsync(sql, new
                {
                    collection.fecha,
                    collection.id_cuota,
                    collection.id_funcionario,
                    collection.monto_cobrado,
                    collection.id_medio_pago,
                    collection.nro_recibo,
                    collection.nro_comprobante,
                    collection.nro_transaccion,
                    collection.id_cobranza
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
