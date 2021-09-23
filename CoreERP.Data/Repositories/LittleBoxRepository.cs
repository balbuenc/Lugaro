using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class LittleBoxRepository : ILittleBoxRepository
    {
        private SqlConfiguration _connectionString;

        public LittleBoxRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteLittleBox(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"DELETE from public.caja_chica
                        WHERE id_caja_chica = @Id ";

                var result = await db.ExecuteAsync(sql, new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<LittleBox>> GetAllLittleBox()
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT cc.id_caja_chica, cc.fecha_apertura, cc.fecha_cierre, cc.id_funcionario, cc.monto_apertura, cc.estado, f.usuario, saldo_inicial, cc.nro_comprobante,
		                    cc.monto_apertura  - (select coalesce(sum(ccd.monto),0) from public.caja_chica_detalle ccd where ccd.id_caja_chica = cc.id_caja_chica) + cc.saldo_inicial as saldo
                            FROM public.caja_chica cc
                            left outer join funcionarios f on f.id_funcionario  = cc.id_funcionario 
                                order by cc.id_caja_chica desc   ";

                var result = await db.QueryAsync<LittleBox>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LittleBox> GetLittleBoxDetails(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT cc.id_caja_chica, cc.fecha_apertura, cc.fecha_cierre, cc.id_funcionario, cc.monto_apertura, cc.estado, f.usuario 
                            FROM public.caja_chica cc
                            left outer join funcionarios f on f.id_funcionario  = cc.id_funcionario 
                            order by cc.id_caja_chica desc 
                            where cc.id_caja_chica = @Id";


                return await db.QueryFirstOrDefaultAsync<LittleBox>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertLittleBox(LittleBox littleBox)
        {
            try
            {
                var db = dbConnection();

                var sql = @"INSERT INTO public.caja_chica
                            (fecha_apertura, fecha_cierre, id_funcionario, monto_apertura, estado, saldo_inicial, nro_comprobante)
                            VALUES(@fecha_apertura, null, @id_funcionario, @monto_apertura, 'ABIERTO', @saldo_inicial,@nro_comprobante);";

                var result = await db.ExecuteAsync(sql, new
                {
                    littleBox.fecha_apertura,
                    littleBox.fecha_cierre,
                    littleBox.id_funcionario,
                    littleBox.monto_apertura,
                    littleBox.estado,
                    littleBox.saldo_inicial,
                    littleBox.nro_comprobante
                }
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateLittleBox(LittleBox littleBox)
        {
            try
            {
                var db = dbConnection();

                var sql = @"UPDATE public.caja_chica
                                SET fecha_apertura=@fecha_apertura, fecha_cierre=@fecha_cierre, monto_apertura=@monto_apertura, estado=@estado, nro_comprobante=@nro_comprobante
                                WHERE id_caja_chica=@id_caja_chica;
                                ";

                var result = await db.ExecuteAsync(sql, new
                {
                    littleBox.fecha_apertura,
                    littleBox.fecha_cierre,
                    littleBox.id_funcionario,
                    littleBox.monto_apertura,
                    littleBox.estado,
                    littleBox.id_caja_chica,
                    littleBox.nro_comprobante
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
