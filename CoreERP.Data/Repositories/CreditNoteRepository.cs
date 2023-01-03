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
                var sql = @"SELECT 	distinct 	nc.id_nota_credito, 
	                nc.nro_nota, 
	                nc.motivo, 
	                nc.id_funcionario,
	                v.factura,
	                v.estado as facturacion,
	                v.importe as importe_factura,
	                p.id_presupuesto,
	                p.fecha as fecha_presupuesto,
	                p.estado as estaso_prepuesto,
	                c.id_cliente,
	                c.ruc,
	                c.razon_social,
	                c.nombres,
	                c.apellidos,
	                (select sum (monto) from nota_credito_detalles ncd where ncd.id_nota_credito = nc.id_nota_credito)  as total,
	                nc.fecha 
	        FROM public.notas_credito nc
	        left outer join ventas v on v.factura = nc.factura 
	        left outer join presupuestos p on p.id_presupuesto = v.id_presupuesto 
	        left outer join clientes c on c.id_cliente = p.id_cliente
	        where v.estado = 'FACTURADO' ;";

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
                var sql = @"SELECT 	nc.id_nota_credito, 
                                    nc.fecha, 
                                    nc.nro_nota, 
                                    nc.motivo, 
                                    nc.id_funcionario,
                                    v.factura,
                                    v.fecha as fecha_factura,
                                    v.estado as facturacion,
                                    v.importe as importe_factura,
                                    p.id_presupuesto,
                                    p.fecha as fecha_presupuesto,
                                    p.estado as estaso_prepuesto,
                                    c.id_cliente,
                                    c.ruc,
                                    c.razon_social,
                                    c.nombres,
                                    c.apellidos,
                                    (select sum (monto) from nota_credito_detalles ncd where ncd.id_nota_credito = nc.id_nota_credito)  as total
                            FROM public.notas_credito nc
                            left outer join ventas v on v.factura = nc.factura 
                            left outer join presupuestos p on p.id_presupuesto = v.id_presupuesto 
                            left outer join clientes c on c.id_cliente = p.id_cliente 
                            where id_nota_credito = @Id";


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
                                    (fecha, nro_nota, motivo, id_funcionario, id_venta)
                                    VALUES(@fecha, @nro_nota, @motivo, @id_funcionario, @factura);";

                var result = await db.ExecuteAsync(sql, new
                {
                    creditNote.fecha,
                    creditNote.nro_nota,
                    creditNote.motivo,
                    creditNote.id_funcionario,
                    creditNote.factura
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
                            SET fecha=@fecha, nro_nota=@nro_nota, motivo=@motivo, id_funcionario=@id_funcionario, factura=@factura
                            WHERE id_nota_credito=@id_nota_credito;";

                var result = await db.ExecuteAsync(sql, new
                {
                    creditNote.id_nota_credito,
                    creditNote.fecha,
                    creditNote.nro_nota,
                    creditNote.motivo,
                    creditNote.id_funcionario,
                    creditNote.factura
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
