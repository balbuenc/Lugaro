﻿using CoreERP.Model;
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
            var db = dbConnection();

            var sql = @"DELETE FROM public.productos
                        WHERE id_producto= @Id);";

            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var db = dbConnection();
            var sql = @"select *, o.origen, m.marca, pro.proveedor 
                        from productos p
                        left outer join origenes o on o.id_origen = p.id_origen
                        left outer join marcas m on m.id_marca = p.id_marca
                        left outer join proveedores pro on pro.id_proveedor = p.id_proveedor";

            return await db.QueryAsync<Product>(sql, new { });
        }

        public async Task<Product> GetProductDetails(int id)
        {
            var db = dbConnection();
            var sql = @"select *, o.origen, m.marca, pro.proveedor 
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
                        (id_origen, producto, codigo, id_marca, descripcion, id_proveedor, costo, precio, dias_garantia)
                        values(@id_origen, @producto, @codigo, @id_marca, @descripcion, @id_proveedor, @costo, @precio, @dias_garantia)";

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
                product.dias_garantia
            }
            );

            return result > 0;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var db = dbConnection();

            var sql = @"UPDATE public.productos
                        SET id_origen=@id_origen, producto=@producto, codigo=@codigo, id_marca=@id_marca, descripcion=@descripcion, id_proveedor=@id_proveedor, costo=@costo, precio=@precio, dias_garantia=@dias_garantia
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
                product.dias_garantia
            }
            );

            return result > 0;
        }
    }
}
