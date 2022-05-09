using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class CreditNoteRepository : ICreditNoteRepository
    {
        private SqlConfiguration _connectionString;

        public CreditNoteRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteCreditNote(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"DELETE from notas_credito
                        WHERE id_nota_credito = @Id ";

                var result = await db.ExecuteAsync(sql, new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CreditNote>> GetAllCreditNotes()
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT id_nota_credito, fecha, nro_nota, motivo
                            FROM public.notas_credito
                            order by id_nota_credito desc;";

                var result = await db.QueryAsync<CreditNote>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CreditNote> GetCreditNoteDetails(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = "select * from notas_credito  where id_nota_credito = @Id";


                return await db.QueryFirstOrDefaultAsync<CreditNote>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertCreditNote(CreditNote creditNote)
        {
            try
            {
                var db = dbConnection();

                var sql = @"INSERT INTO public.notas_credito
                            (fecha, nro_nota, motivo)
                            VALUES(@fecha, @nro_nota, @motivo);
                            ";

                var result = await db.ExecuteAsync(sql, new
                {
                    creditNote.fecha,
                    creditNote.nro_nota,
                    creditNote.motivo
                }
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateCreditNote(CreditNote creditNote)
        {
            try
            {
                var db = dbConnection();

                var sql = @"UPDATE public.notas_credito
                            SET fecha=@fecha, nro_nota=@nro_nota, motivo=@motivo
                            WHERE id_nota_credito=@id_nota_credito;";

                var result = await db.ExecuteAsync(sql, new
                {
                    creditNote.id_nota_credito,
                    creditNote.fecha,
                    creditNote.nro_nota,
                    creditNote.motivo
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
