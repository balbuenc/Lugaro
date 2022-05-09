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
                var sql = @"select nc.id_nota_credito , nc.nro_nota, ncd.id_nota_credito_detalle, ncd.id_venta , v.factura, p.producto , ncd.monto 
                            from notas_credito nc 
                            inner join nota_credito_detalles ncd on ncd.id_nota_credito = nc.id_nota_credito 
                            inner join ventas v on v.id_venta = ncd.id_venta 
                            inner join presupuesto_detalles pd on pd.id_presupuesto_detalle  = ncd.id_presupuesto_detalle 
                            inner join productos p on p.id_producto = pd.id_producto 
                            where nc.id_nota_credito = @Id
                            order by pd.id_presupuesto_detalle desc";

                var result = await db.QueryAsync<CreditNoteDetails>(sql, new { Id = CreditNoteID });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CreditNoteDetails> GetCreditNoteDetailsDetails(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT id_caja_chica, nro_comprobante, fecha, beneficiario, concepto, id_nota_credito_detalle. monto
                            FROM public.nota_credito_detalles ccd
                            where cdd.id_nota_credito_detalle = @Id;";


                return await db.QueryFirstOrDefaultAsync<CreditNoteDetails>(sql, new { Id = id });
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
                            (id_venta, id_presupuesto_detalle, monto, id_nota_credito)
                            VALUES(@id_venta, @id_presupuesto_detalle, @monto, @id_nota_credito);
                            ";

                var result = await db.ExecuteAsync(sql, new
                {
                    creditNoteDetail.id_venta,
                    creditNoteDetail.id_presupuesto_detalle,
                    creditNoteDetail.monto,
                    creditNoteDetail.id_nota_credito
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
                            SET id_venta=@id_venta, id_presupuesto_detalle=@id_presupuesto_detalle, monto=@monto, id_nota_credito=@id_nota_credito
                            WHERE id_nota_credito_detalle=@id_nota_credito_detalle;
                            ";

                var result = await db.ExecuteAsync(sql, new
                {
                    creditNoteDetail.id_venta,
                    creditNoteDetail.id_presupuesto_detalle,
                    creditNoteDetail.monto,
                    creditNoteDetail.id_nota_credito,
                    creditNoteDetail.id_nota_credito_detalle
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
