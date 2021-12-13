using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class GeneralPurchaseRepository : IGeneralPurchaseRepository
    {
        private SqlConfiguration _connectionString;

        public GeneralPurchaseRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteGeneralPurchase(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"DELETE from compras_general
                        WHERE id_compra_general = @Id ";

                var result = await db.ExecuteAsync(sql, new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<GeneralPurchase>> GetAllGeneralPurchases()
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT c.id_compra_general ,c.id_proveedor, c.id_moneda, c.id_deposito, c.estado, c.fecha, c.nro_orden_compra, c.id_funcionario,
                            p.proveedor, m.moneda , d.deposito, f.usuario as funcionario
                            FROM public.compras_general c
                            inner join public.proveedores p on c.id_proveedor = p.id_proveedor
                            inner join public.monedas m  on c.id_moneda = m.id_moneda
                            inner join public.depositos d on c.id_deposito  = d.id_deposito
                            inner join public.funcionarios f on c.id_funcionario = f.id_funcionario
                            order by c.id_compra_general desc;";

                var result = await db.QueryAsync<GeneralPurchase>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GeneralPurchase> GetGeneralPurchaseDetails(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT c.id_compra_general , c.id_proveedor, c.id_moneda, c.id_deposito, c.estado,  c.fecha, c.nro_orden_compra, c.id_funcionario,
                            p.proveedor, m.moneda , d.deposito, f.usuario as funcionario
                            FROM public.compras_general  c
                            inner join public.proveedores p on c.id_proveedor = p.id_proveedor
                            inner join public.monedas m  on c.id_moneda = m.id_moneda
                            inner join public.depositos d on c.id_deposito  = d.id_deposito
                            inner join public.funcionarios f on c.id_funcionario = f.id_funcionario 
                            where c.id_compra_general = @Id";


                return await db.QueryFirstOrDefaultAsync<GeneralPurchase>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertGeneralPurchase(GeneralPurchase compra)
        {
            try
            {
                var db = dbConnection();

                var sql = @"INSERT INTO public.compras_general
                            (fecha, id_funcionario, id_proveedor, id_moneda, estado, nro_orden_compra, id_deposito)
                            VALUES(@fecha, @id_funcionario, @id_proveedor, @id_moneda, @estado, @nro_orden_compra, @id_deposito);";

                var result = await db.ExecuteAsync(sql, new
                {
                    compra.fecha,
                    compra.id_funcionario,
                    compra.id_proveedor,
                    compra.id_moneda,
                    compra.estado,
                    compra.nro_orden_compra,
                    compra.id_deposito
                }
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateGeneralPurchase(GeneralPurchase compra)
        {
            try
            {
                var db = dbConnection();

                var sql = @"UPDATE public.compras_general
                            SET fecha=@fecha, id_funcionario=@id_funcionario, id_proveedor=@id_proveedor, id_moneda=@id_moneda, estado=@estado, nro_orden_compra=@nro_orden_compra, id_deposito=@id_deposito
                            WHERE id_compra_general=@id_compra_general;";

                var result = await db.ExecuteAsync(sql, new
                {
                    compra.fecha,
                    compra.id_funcionario,
                    compra.id_proveedor,
                    compra.id_moneda,
                    compra.estado,
                    compra.nro_orden_compra,
                    compra.id_deposito,
                    compra.id_compra_general
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
