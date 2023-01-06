using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class StockRepository : IStockRepository
    {
        private SqlConfiguration _connectionString;

        public StockRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }
        public async Task<bool> DeleteStock(int id)
        {
            var db = dbConnection();

            var sql = @"DELETE from public.stock
                        WHERE id_stock = @Id ";

            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        public async Task<IEnumerable<Stock>> GetAllStocks()
        {
            var db = dbConnection();
            var sql = @"select s.id_stock, s.id_deposito, s.id_producto, p.codigo, p.producto, d.deposito, s.cantidad 
                        from public.productos p 
                        left outer join public.stock s on p.id_producto = s.id_producto 
                        left outer join public.depositos d on d.id_deposito = s.id_deposito";


            return await db.QueryAsync<Stock>(sql, new { });
        }

        public async Task<IEnumerable<Stock>> GetProductStock(int id)
        {
            var db = dbConnection();
            var sql = @"select s.id_stock, s.id_deposito, s.id_producto, p.codigo, p.producto, d.deposito, s.cantidad 
                        from public.productos p 
                        left outer join public.stock s on p.id_producto = s.id_producto 
                        left outer join public.depositos d on d.id_deposito = s.id_deposito
                        where p.id_producto  = @Id
                        order by d.deposito asc";


            return await db.QueryAsync<Stock>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Stock>> GetTransferDestinations(int id)
        {
            var db = dbConnection();
            var sql = @"select s.id_stock, s.id_deposito, s.id_producto, p.codigo, p.producto, d.deposito, s.cantidad 
                        from public.productos p 
                        left outer join public.stock s on p.id_producto = s.id_producto 
                        left outer join public.depositos d on d.id_deposito = s.id_deposito
                        where p.id_producto =  (select id_producto from stock s2 where s2.id_stock = @Id)
                        and s.id_deposito <> (select id_deposito from stock s3 where s3.id_stock = @Id);";


            return await db.QueryAsync<Stock>(sql, new { Id = id });
        }

        public async Task<Stock> GetStockDetails(int id)
        {
            var db = dbConnection();
            var sql = @"select s.id_stock, s.id_deposito, s.id_producto, p.codigo, p.producto, d.deposito, s.cantidad 
                        from public.productos p 
                        left outer join public.stock s on p.id_producto = s.id_producto 
                        left outer join public.depositos d on d.id_deposito = s.id_deposito
                        where s.id_producto = @Id
                        order by d.prioridad asc";

            return await db.QueryFirstOrDefaultAsync<Stock>(sql, new { Id = id });
        }

        public async Task<Stock> GetStockDetailsByStore(int productId, int storeID)
        {
            Stock s;

            var db = dbConnection();
            var sql = @"select s.id_stock, s.id_deposito, s.id_producto, p.codigo, p.producto, d.deposito, s.cantidad 
                        from public.productos p 
                        left outer join public.stock s on p.id_producto = s.id_producto 
                        left outer join public.depositos d on d.id_deposito = s.id_deposito
                        where s.id_producto = @ProductId
                        and s.id_deposito = @StoreID
                        order by s.id_deposito asc";

            s = await db.QueryFirstOrDefaultAsync<Stock>(sql, new { ProductId = productId, StoreID = storeID });

            if (s == null)
            {
                Stock createdStock = new Stock();
                createdStock.id_deposito = storeID;
                createdStock.id_producto = productId;

                await InsertStock(createdStock);

                return await GetStockDetailsByStore(productId, storeID);
            }
            else
            {
                return s;
            }

        }


        public async Task<bool> InsertStock(Stock stock)
        {
            try
            {
                Stock s;

                var db = dbConnection();

                var sql_exist = @"select *
                                from stock s 
                                where s.id_producto = @id_producto
                                and s.id_deposito = @id_deposito";

                s = await db.QueryFirstOrDefaultAsync<Stock>(sql_exist, new { id_producto = stock.id_producto, id_deposito = stock.id_deposito });

                if (s == null)
                {
                    var sql = @"INSERT INTO public.stock (id_deposito, id_producto, cantidad) VALUES(@id_deposito, @id_producto, @cantidad);";

                    var result = await db.ExecuteAsync(sql, new
                    {
                        stock.id_deposito,
                        stock.id_producto,
                        stock.cantidad
                    }
                    );

                    return result > 0;
                }
                else
                {
                    
                    var sql = @"UPDATE public.stock
                                SET cantidad= cantidad + @cantidad
                                WHERE id_deposito=@id_deposito AND id_producto=@id_producto;
                                ";

                    var result = await db.ExecuteAsync(sql, new
                    {
                        stock.id_deposito,
                        stock.id_producto,
                        stock.cantidad
                    }
                    );

                    return result > 0;
                }


            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateStock(Stock stock)
        {
            var db = dbConnection();

            var sql = @"UPDATE public.stock
                        SET id_deposito=@id_deposito, id_producto=@id_producto, cantidad=@cantidad
                        WHERE id_stock=@id_stock;";

            var result = await db.ExecuteAsync(sql, new
            {
                stock.id_stock,
                stock.id_deposito,
                stock.id_producto,
                stock.cantidad
            }
            );

            return result > 0;
        }
    }
}
