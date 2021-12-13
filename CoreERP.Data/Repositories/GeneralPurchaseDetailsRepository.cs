using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class GeneralPurchaseDetailsRepository : IGeneralPurchaseDetailsRepository
    {
        private SqlConfiguration _connectionString;

        public GeneralPurchaseDetailsRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeletePurchaseDetail(int id)
        {
            var db = dbConnection();

            var sql = @"DELETE FROM public.compra_general_detalles
                        WHERE id_compra_general_detalle=@Id;";

            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        public async Task<IEnumerable<GeneralPurchaseDetails>> GetAllGeneralPurchaseDetails()
        {
            var db = dbConnection();
            var sql = @"SELECT *
                        FROM public.v_impresion_compras_generales";

            return await db.QueryAsync<GeneralPurchaseDetails>(sql, new { });
        }

        public async Task<IEnumerable<GeneralPurchaseDetails>> GetGeneralPurchaseDetails(int id)
        {
            try
            {
                var db = dbConnection();
                var sql = @"SELECT *
                        FROM public.v_impresion_compras_generales
                        where id_compra_general = @Id";


                return await db.QueryAsync<GeneralPurchaseDetails>(sql, new { Id = id });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public async Task<bool> InsertPurchaseDetail(GeneralPurchaseDetails purchaseDetail)
        {

          
         
            GeneralPurchase purchase;

            try
            {
                var db = dbConnection();

                var sql_compra = "select * from compras_general c  where id_compra_general =@id_compra_general";
                purchase = await db.QueryFirstOrDefaultAsync<GeneralPurchase>(sql_compra, new
                {
                    purchaseDetail.id_compra_general
                });


               


                purchaseDetail.total = purchaseDetail.cantidad * purchaseDetail.monto;



                


                var sql = @"INSERT INTO public.compra_general_detalles
                            (id_compra_general, cantidad, descripcion, monto, total)
                            VALUES(@id_compra_general, @cantidad, @descripcion, @monto, @total);";

                var result = await db.ExecuteAsync(sql, new
                {
                    purchaseDetail.id_compra_general,
                    purchaseDetail.cantidad,
                    purchaseDetail.descripcion,
                    purchaseDetail.monto,
                    
                    purchaseDetail.total
                }
                );

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdatePurchaseDetail(GeneralPurchaseDetails purchaseDetail)
        {
            try
            {
                var db = dbConnection();

              

                purchaseDetail.total = purchaseDetail.monto * purchaseDetail.cantidad;

                var sql = @"UPDATE public.compra_general_detalles
                            SET id_compra_general=@id_compra_general, cantidad=@cantidad, descripcion=@descripcion, monto=@monto, total=@total
                            WHERE id_compra_general_detalle=@id_compra_general_detalle;";

                var result = await db.ExecuteAsync(sql, new
                {
                    purchaseDetail.id_compra_general,
                    purchaseDetail.cantidad,
                    purchaseDetail.descripcion,
                    purchaseDetail.monto,

                    purchaseDetail.total,
                    purchaseDetail.id_compra_general_detalle
                }
                );

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}
