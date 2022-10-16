using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class StampRepository : IStampRepository
    {
        private SqlConfiguration _connectionString;

        public StampRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteStamp(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"DELETE FROM public.timbrados WHERE id_timbrado= @Id";


                await db.ExecuteAsync(sql, new { Id = id });


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public async Task<IEnumerable<Stamp>> GetAllStamps()
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT id_timbrado, timbrado, fecha_emision, fecha_vencimiento, punto_emision, serie_inicio, serie_final, nro_factura
                            FROM public.timbrados;";

                var result = await db.QueryAsync<Stamp>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                return null;

            }
        }





        public async Task<Stamp> GetStampDetails(int id)
        {
            var db = dbConnection();
            var sql = @"SELECT id_timbrado, timbrado, fecha_emision, fecha_vencimiento, punto_emision, serie_inicio, serie_final, nro_factura
                        FROM public.timbrados where id_timbrado = @Id";


            return await db.QueryFirstOrDefaultAsync<Stamp>(sql, new { Id = id });
        }

        public async Task<Stamp> GetNextInvoiceNumber(int id)
        {
            var db = dbConnection();
            var sql = @"update public.timbrados 
                        set nro_factura  = nextval('nro_factura_seq')
                        where id_timbrado  = @Id;";

            await db.ExecuteAsync(sql, new { Id = id });

            sql = @"select * from public.timbrados
                        where id_timbrado  = @Id;";

            return await db.QueryFirstOrDefaultAsync<Stamp>(sql, new { Id = id });
        }

        public async Task<bool> InsertStamp(Stamp stamp)
        {
            var db = dbConnection();

            var sql = @"INSERT INTO public.timbrados
                            (timbrado, fecha_emision, fecha_vencimiento, punto_emision, serie_inicio, serie_final)
                            VALUES(@timbrado, @fecha_emision, @fecha_vencimiento, @punto_emision, @serie_inicio, @serie_final);
                            ";

            var result = await db.ExecuteAsync(sql, new
            {
                stamp.timbrado,
                stamp.fecha_emision,
                stamp.fecha_vencimiento,
                stamp.punto_emision,
                stamp.serie_inicio,
                stamp.serie_final
            }
            );

            return result > 0;
        }

        public async Task<bool> UpdateStamp(Stamp stamp)
        {
            var db = dbConnection();

            try
            {
                var sql = @"UPDATE public.timbrados
                            SET timbrado=@timbrado, fecha_emision=@fecha_emision, fecha_vencimiento=@fecha_vencimiento, punto_emision=@punto_emision, serie_inicio=@serie_inicio, serie_final=@serie_final
                            WHERE id_timbrado=@id_timbrado;
                            ";

                var result = await db.ExecuteAsync(sql, new
                {
                    stamp.id_timbrado,
                    stamp.timbrado,
                    stamp.fecha_emision,
                    stamp.fecha_vencimiento,
                    stamp.punto_emision,
                    stamp.serie_inicio,
                    stamp.serie_final
                }
                );

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }

}
