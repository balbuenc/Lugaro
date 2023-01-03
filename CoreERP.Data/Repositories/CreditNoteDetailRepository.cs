using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class CreditNoteDetailRepository : ICreditNoteDetailRepository
    {
        private SqlConfiguration _connectionString;

        public CreditNoteDetailRepository (SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteCreditNoteDetails(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"DELETE from nota_credito_detalles
                        WHERE id_nota_credito_detalle = @Id ";

                var result = await db.ExecuteAsync(sql, new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CreditNoteDetails>> GetAllCreditNoteDetails(int CreditNoteID)
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT nc.id_nota_credito_detalle, nc.monto, nc.id_nota_credito, nc.concepto, nc.impuesto, nc.porcentaje_impuesto, nc2.nro_nota , nc.monto_impuesto
                            FROM public.nota_credito_detalles nc
                            inner join public.notas_credito nc2 on nc2.id_nota_credito = nc.id_nota_credito 
                            where nc.id_nota_credito = @Id";

                var result = await db.QueryAsync<CreditNoteDetails>(sql, new { Id = CreditNoteID });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      

        public async Task<bool> InsertCreditNoteDetails(CreditNoteDetails creditNoteDetail)
        {
            try
            {
                var db = dbConnection();

                var sql = @"INSERT INTO public.nota_credito_detalles
                            (monto, id_nota_credito, concepto, impuesto, porcentaje_impuesto, monto_impuesto)
                            VALUES(@monto, @id_nota_credito, @concepto, @impuesto, @porcentaje_impuesto, @monto_impuesto);
                            ";

                var result = await db.ExecuteAsync(sql, new
                {
                    creditNoteDetail.id_nota_credito,
                    creditNoteDetail.monto,
                    creditNoteDetail.concepto,
                    creditNoteDetail.impuesto,
                    creditNoteDetail.porcentaje_impuesto,
                    creditNoteDetail.monto_impuesto
                }
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateCreditNoteDetails(CreditNoteDetails creditNoteDetail)
        {
            try
            {
                var db = dbConnection();

                var sql = @"UPDATE public.nota_credito_detalles
                                    SET monto=@monto, id_nota_credito=@id_nota_credito, concepto=@concepto, impuesto=@impuesto, porcentaje_impuesto=@porcentaje_impuesto, monto_impuesto=@monto_impuesto
                                    WHERE id_nota_credito_detalle=@id_nota_credito_detalle;
                                    ";

                var result = await db.ExecuteAsync(sql, new
                {
                    creditNoteDetail.id_nota_credito,
                    creditNoteDetail.monto,
                    creditNoteDetail.concepto,
                    creditNoteDetail.impuesto,
                    creditNoteDetail.porcentaje_impuesto,
                    creditNoteDetail.id_nota_credito_detalle,
                    creditNoteDetail.monto_impuesto
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
