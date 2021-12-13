using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private SqlConfiguration _connectionString;

        public PaymentRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeletePayment(int id)
        {
            try
            {
                var db = dbConnection();

                var sql = @"DELETE from pagos
                        WHERE id_pago = @Id ";

                var result = await db.ExecuteAsync(sql, new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT p.id_pago, p.nro_orden_pago, p.fecha_orden, p.id_compra, p.id_compra_general, p.id_funcionario, p.aprobado_por, p.fecha_pago, p.monto_pagado, p.nro_comprobante,
		                            f.usuario  as responsable,
		                            f.usuario  as aprobador,
		                            mp.id_medio_pago,
		                            mp.medio as medio_pago,
		                            m.id_moneda,
		                            m.moneda 
                            FROM public.pagos p
                            left outer join funcionarios f on f.id_funcionario = p.id_funcionario 
                            left outer join funcionarios aprobadores on aprobadores.id_funcionario = p.aprobado_por
                            left outer join medios_pago mp on mp.id_medio_pago = p.id_medio_pago
                            left outer join monedas m on m.id_moneda = p.id_moneda   
                            ;";

                var result = await db.QueryAsync<Payment>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Payment> GetPaymentDetails(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT p.id_pago, p.nro_orden_pago, p.fecha_orden, p.id_compra, p.id_compra_general, p.id_funcionario, p.aprobado_por, p.fecha_pago, p.monto_pagado, p.nro_comprobante,
		                            f.usuario  as responsable,
		                            f.usuario  as aprobador,
		                            mp.id_medio_pago,
		                            mp.medio as medio_pago,
		                            m.id_moneda,
		                            m.moneda 
                            FROM public.pagos p
                            left outer join funcionarios f on f.id_funcionario = p.id_funcionario 
                            left outer join funcionarios aprobadores on aprobadores.id_funcionario = p.aprobado_por
                            left outer join medios_pago mp on mp.id_medio_pago = p.id_medio_pago
                            left outer join monedas m on m.id_moneda = p.id_moneda 
                            where p.id_pago = @Id
                            ;";


                return await db.QueryFirstOrDefaultAsync<Payment>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertPayment(Payment payment)
        {
            try
            {
                var db = dbConnection();

                var sql = @"INSERT INTO public.pagos
                            (nro_orden_pago, fecha_orden, id_compra, id_compra_general, id_funcionario, aprobado_por, fecha_pago, monto_pagado, nro_comprobante,estado,id_medio_pago, id_moneda)
                            VALUES(@nro_orden_pago, @fecha_orden, @id_compra, @id_compra_general, @id_funcionario, @aprobado_por, @fecha_pago, @monto_pagado, @nro_comprobante, @estado,@id_medio_pago, @id_moneda);
                            ";

                var result = await db.ExecuteAsync(sql, new
                {
                  
                    payment.nro_orden_pago,
                    payment.fecha_orden,
                    payment.id_compra,
                    payment.id_compra_general,
                    payment.id_funcionario,
                    payment.aprobado_por,
                    payment.fecha_pago,
                    payment.monto_pagado,
                    payment.nro_comprobante,
                    payment.estado,
                    payment.id_medio_pago,
                    payment.id_moneda

                }
                );

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdatePayment(Payment payment)
        {
            try
            {
                var db = dbConnection();

                var sql = @"UPDATE public.pagos
                            SET nro_orden_pago=@nro_orden_pago, fecha_orden=@fecha_orden, id_compra=@id_compra, id_compra_general=@id_compra_general, id_funcionario=@id_funcionario, aprobado_por=@aprobado_por, 
                            fecha_pago=@fecha_pago, monto_pagado=@monto_pagado, nro_comprobante=@nro_comprobante, estado=@estado, id_medio_pago=@id_medio_pago,id_moneda=@id_moneda
                            WHERE id_cuenta=@id_cuenta;";

                var result = await db.ExecuteAsync(sql, new
                {
                    payment.id_pago,
                    payment.nro_orden_pago,
                    payment.fecha_orden,
                    payment.id_compra,
                    payment.id_compra_general,
                    payment.id_funcionario,
                    payment.aprobado_por,
                    payment.fecha_pago,
                    payment.monto_pagado,
                    payment.nro_comprobante,
                    payment.estado,
                    payment.id_medio_pago,
                    payment.id_moneda,
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
