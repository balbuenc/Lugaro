using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private SqlConfiguration _connectionString;

        public ProductRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var db = dbConnection();
                var sql1 = @"delete from public.stock where id_producto =@Id";
                var sql = @"DELETE FROM public.productos WHERE id_producto= @Id";

                await db.ExecuteAsync(sql1, new { Id = id });
                await db.ExecuteAsync(sql, new { Id = id });


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                var db = dbConnection();
                var sql = @"select *, o.origen, m.marca, pro.proveedor , m2.moneda 
                        from productos p
                        left outer join origenes o on o.id_origen = p.id_origen
                        left outer join marcas m on m.id_marca = p.id_marca
                        left outer join proveedores pro on pro.id_proveedor = p.id_proveedor
                        left outer join monedas m2 on m2.id_moneda = p.id_moneda ";

                var result = await db.QueryAsync<Product>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public async Task<IEnumerable<Product>> GetProductsDefinitions()
        {
            try
            {
                var db = dbConnection();
                var sql = @"select p.id_producto, p.codigo, p.producto, p.precio from productos p ";

                var result = await db.QueryAsync<Product>(sql, new { });

                return result;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

     

        public async Task<Product> GetProductDetails(int id)
        {
            var db = dbConnection();
            var sql = @"select p.*, o.origen, m.marca, pro.proveedor 
                        from productos p
                        left outer join origenes o on o.id_origen = p.id_origen
                        left outer join marcas m on m.id_marca = p.id_marca
                        left outer join proveedores pro on pro.id_proveedor = p.id_proveedor 
                        where id_producto = @Id";


            return await db.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
        }

        public async Task<bool> InsertProduct(Product product)
        {
            var db = dbConnection();

            var sql = @"INSERT INTO public.productos
                        (id_origen, producto, codigo, id_marca, descripcion, id_proveedor, costo, precio, dias_garantia, id_moneda, imagen)
                        values(@id_origen, @producto, @codigo, @id_marca, @descripcion, @id_proveedor, @costo, @precio, @dias_garantia, @id_moneda, @imagen)";

            var result = await db.ExecuteAsync(sql, new
            {
                product.id_origen,
                product.producto,
                product.codigo,
                product.id_marca,
                product.descripcion,
                product.id_proveedor,
                product.costo,
                product.precio,
                product.dias_garantia,
                product.id_moneda,
                product.imagen
            }
            );

            return result > 0;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var db = dbConnection();

            try
            {
                var sql = @"UPDATE public.productos
                        SET id_origen=@id_origen, producto=@producto, codigo=@codigo, id_marca=@id_marca, descripcion=@descripcion, id_proveedor=@id_proveedor, costo=@costo, precio=@precio, dias_garantia=@dias_garantia, id_moneda=@id_moneda,imagen=@imagen
                        WHERE id_producto=@id_producto;
                        ;";

                var result = await db.ExecuteAsync(sql, new
                {
                    product.id_producto,
                    product.id_origen,
                    product.producto,
                    product.codigo,
                    product.id_marca,
                    product.descripcion,
                    product.id_proveedor,
                    product.costo,
                    product.precio,
                    product.dias_garantia,
                    product.id_moneda,
                    product.imagen
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
