using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class TransferRepository : ITransferRepository
    {
        private SqlConfiguration _connectionString;

        public TransferRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteTransfer(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"DELETE from transferencias
                        WHERE id_trasnferencia = @Id ";

                var result = await db.ExecuteAsync(sql, new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Transfer>> GetAllTransfers()
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT t.id_transferencia, t.fecha, t.id_producto, t.id_deposito_origen, t.id_deposito_destino, t.id_funcionario , t.cantidad
                            ,f.usuario , origenes.deposito as origen, destinos.deposito as destino
                            ,t.nro_transferencia , t.retirado_por , t.observaciones 
                            FROM public.transferencias t
                            left outer join funcionarios f on f.id_funcionario = t.id_funcionario
                            left outer join depositos origenes on origenes.id_deposito = t.id_deposito_origen 
                            left outer join depositos destinos on destinos.id_deposito = t.id_deposito_destino
                            order by t.id_transferencia  desc 
                            ";

                var result = await db.QueryAsync<Transfer>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Transfer> GetTransferDetails(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT t.id_transferencia, t.fecha, t.id_producto, t.id_deposito_origen, t.id_deposito_destino, t.id_funcionario , t.cantidad
                            ,f.usuario , origenes.deposito as origen, destinos.deposito as destino
                            ,t.nro_transferencia , t.retirado_por , t.observaciones 
                            FROM public.transferencias t
                            left outer join funcionarios f on f.id_funcionario = t.id_funcionario
                            left outer join depositos origenes on origenes.id_deposito = t.id_deposito_origen 
                            left outer join depositos destinos on destinos.id_deposito = t.id_deposito_destino 
                            where t.id_producto = @id_producto
                            order by t.id_transferencia  desc;";


                return await db.QueryFirstOrDefaultAsync<Transfer>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertTransfer(Transfer transfer)
        {
            try
            {
                var db = dbConnection();

                var sql = @"INSERT INTO public.transferencias
                            (fecha, id_producto, id_deposito_origen, id_deposito_destino, id_funcionario, cantidad, nro_transferencia, retirado_por, observaciones)
                            VALUES(@fecha, @id_producto, @id_deposito_origen, @id_deposito_destino, @id_funcionario, @cantidad, @nro_transferencia, @retirado_por, @observaciones);
                            ";

                var result = await db.ExecuteAsync(sql, new
                {
                    transfer.fecha,
                    transfer.id_producto,
                    transfer.id_deposito_origen,
                    transfer.id_deposito_destino,
                    transfer.id_funcionario,
                    transfer.cantidad,
                    transfer.nro_transferencia,
                    transfer.retirado_por,
                    transfer.observaciones
                }
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateTransfer(Transfer transfer)
        {
            try
            {
                var db = dbConnection();

                var sql = @"UPDATE public.transferencias
                            SET fecha=@fecha, id_producto=@id_producto, id_deposito_origen=@id_deposito_origen, id_deposito_destino=@id_deposito_destino, id_funcionario=@id_funcionario, cantidad=@cantidad, nro_transferencia=@nro_transferencia, retirado_por=@retirado_por, observaciones=@observaciones
                            WHERE id_transferencia=@id_transferencia;";

                var result = await db.ExecuteAsync(sql, new
                {
                    transfer.id_transferencia,
                    transfer.fecha,
                    transfer.id_producto,
                    transfer.id_deposito_origen,
                    transfer.id_deposito_destino,
                    transfer.id_funcionario,
                    transfer.cantidad,
                    transfer.nro_transferencia,
                    transfer.retirado_por,
                    transfer.observaciones
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
