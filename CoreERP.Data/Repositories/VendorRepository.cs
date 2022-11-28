using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class VendorRepository : IVendorRepository
    {
        private SqlConfiguration _connectionString;

        public VendorRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteVendor(int id)
        {
            var db = dbConnection();

            var sql = @"DELETE from proveedores
                        WHERE id_pais = @Id ";

            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        public async Task<IEnumerable<Vendor>> GetAllVendors()
        {
            var db = dbConnection();
            var sql = @"select p.* , p2.pais, pc.cuenta as cuenta_contable
                        from proveedores p
                        left outer join paises p2 on p2.id_pais = p.id_pais 
                        left outer join plan_cuentas pc on pc.id_plan_cuenta = p.id_plan_cuenta 
                        order by p.proveedor";


            return await db.QueryAsync<Vendor>(sql, new { });
        }

        public async Task<Vendor> GetVendorDetails(int id)
        {
            var db = dbConnection();
            var sql = "select * from proveedores where id_proveedor = @Id";


            return await db.QueryFirstOrDefaultAsync<Vendor>(sql, new { Id = id });
        }

        public async Task<bool> InsertVendor(Vendor vendor)
        {
            var db = dbConnection();

            var sql = @"INSERT INTO public.proveedores
                        (proveedor, descripcion, id_pais, ruc, direccion, id_plan_cuenta, telefono, email, contacto)
                        VALUES(@proveedor, @descripcion, @id_pais, @ruc, @direccion, @id_plan_cuenta, @telefono, @email, @contacto);";

            var result = await db.ExecuteAsync(sql, new
            {
                vendor.proveedor,
                vendor.descripcion,
                vendor.id_pais,
                vendor.ruc,
                vendor.direccion,
                vendor.id_plan_cuenta,
                vendor.telefono,
                vendor.email,
                vendor.contacto
            }
            );

            return result > 0;
        }

        public async Task<bool> UpdateVendor(Vendor vendor)
        {
            var db = dbConnection();

            var sql = @"UPDATE public.proveedores 
                        SET proveedor=@proveedor, descripcion=@descripcion, id_pais=@id_pais, ruc=@ruc, direccion=@direccion, id_plan_cuenta=@id_plan_cuenta,telefono=@telefono, email=@email, contacto=@contacto
                        WHERE id_proveedor=@id_proveedor;";

            var result = await db.ExecuteAsync(sql, new
            {
                vendor.id_proveedor,
                vendor.proveedor,
                vendor.id_pais,
                vendor.descripcion,
                vendor.ruc,
                vendor.direccion,
                vendor.id_plan_cuenta,
                vendor.telefono,
                vendor.email,
                vendor.contacto
            }
            );

            return result > 0;
        }
    }
}
