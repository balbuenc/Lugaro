using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class LittleBoxDetailRepository : ILittleBoxDetailRepository
    {
        private SqlConfiguration _connectionString;

        public LittleBoxDetailRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteLittleBoxDetail(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"DELETE from caja_chica_detalle
                        WHERE id_caja_chica_detalle = @Id ";

                var result = await db.ExecuteAsync(sql, new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<LittleBoxDetails>> GetAllLittleBoxDetails(int LittleBoxID)
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT ccd.id_caja_chica, ccd.nro_comprobante, ccd.fecha, ccd.beneficiario, ccd.concepto, ccd.id_caja_chica_detalle, ccd.monto, 
                            (select sum(monto) from public.caja_chica_detalle ccd1 where ccd1.id_caja_chica = @Id) as total_gasto,
                            (cc.monto_apertura + cc.saldo_inicial)   - (select sum(monto) from public.caja_chica_detalle ccd2 where ccd2.id_caja_chica = @Id) as saldo
                            FROM public.caja_chica_detalle ccd
                            inner join public.caja_chica cc on cc.id_caja_chica = ccd.id_caja_chica 
                            where ccd.id_caja_chica = @Id
                            order by ccd.fecha desc";

                var result = await db.QueryAsync<LittleBoxDetails>(sql, new { Id = LittleBoxID });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LittleBoxDetails> GetLittleBoxDetailsDetail(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT id_caja_chica, nro_comprobante, fecha, beneficiario, concepto, id_caja_chica_detalle. monto
                            FROM public.caja_chica_detalle ccd
                            where cdd.id_caja_chica_detalle = @Id;";


                return await db.QueryFirstOrDefaultAsync<LittleBoxDetails>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertLittleBoxDetail(LittleBoxDetails littleBoxDetail)
        {
            try
            {
                var db = dbConnection();

                var sql = @"INSERT INTO public.caja_chica_detalle
                        (id_caja_chica, nro_comprobante, fecha, beneficiario, concepto, monto)
                        VALUES(@id_caja_chica, @nro_comprobante, @fecha, @beneficiario, @concepto, @monto);                        ";

                var result = await db.ExecuteAsync(sql, new
                {
                    littleBoxDetail.id_caja_chica,
                    littleBoxDetail.id_caja_chica_detalle,
                    littleBoxDetail.concepto,
                    littleBoxDetail.fecha,
                    littleBoxDetail.nro_comprobante,
                    littleBoxDetail.beneficiario,
                    littleBoxDetail.monto
                }
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateLittleBoxDetail(LittleBoxDetails littleBoxDetail)
        {
            try
            {
                var db = dbConnection();

                var sql = @"UPDATE public.caja_chica_detalle
                            SET id_caja_chica=@id_caja_chica, nro_comprobante=@nro_comprobante, fecha=@fecha, beneficiario=@beneficiario, concepto=@concepto, monto=@monto
                            WHERE id_caja_chica_detalle=@id_caja_chica_detalle;";

                var result = await db.ExecuteAsync(sql, new
                {
                    littleBoxDetail.id_caja_chica,
                    littleBoxDetail.id_caja_chica_detalle,
                    littleBoxDetail.concepto,
                    littleBoxDetail.fecha,
                    littleBoxDetail.nro_comprobante,
                    littleBoxDetail.beneficiario,
                    littleBoxDetail.monto
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
