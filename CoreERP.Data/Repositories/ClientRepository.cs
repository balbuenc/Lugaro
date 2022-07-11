using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private SqlConfiguration _connectionString;

        public ClientRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteClient(int id)
        {
            var db = dbConnection();

            var sql = @"DELETE from clientes
                        WHERE id_cliente = @Id ";

            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            try
            {
                var db = dbConnection();

                var sql = @"SELECT c.id_cliente, c.nombres, c.apellidos, c.sexo, c.fecha_nacimiento, c.ci, c.ruc, c.direccion, c.telefono, c.email, c.observaciones, c.fecha_alta, c.razon_social, c.codigo, c.es_cliente_fiel,  c.tipo_vivienda, c.direccion_envio, c.id_funcionario, c.cliente_exento
                        ,b.id_barrio
                        ,b.barrio
                        ,ec.id_estado_civil
                        ,ec.estado_civil
                        ,n.id_nacionalidad
                        ,n.nacionalidad
                        ,tc.id_tipo_cliente
                        ,tc.tipo
                        ,v.usuario as vendedor
                        from clientes c  
                        left outer join barrios b on b.id_barrio = c.id_barrio
                        left outer join estados_civiles ec  on ec.id_estado_civil  = c.id_estado_civil 
                        left outer join nacionalidades n on n.id_nacionalidad = c.id_nacionalidad 
                        left outer join tipos_clientes tc  on tc.id_tipo_cliente  = c.id_tipo_cliente
                        left outer join funcionarios v on v.id_funcionario = c.id_funcionario 
                        order by c.nombres asc";

                return await db.QueryAsync<Client>(sql, new { });
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Client>> GetAllClientsByUserName(string userName)
        {
            try
            {
                var db = dbConnection();

                var sql = @"SELECT c.id_cliente, c.nombres, c.apellidos, c.razon_social 
                        ,v.usuario as vendedor
                        from clientes c  
                        left outer join barrios b on b.id_barrio = c.id_barrio
                        left outer join estados_civiles ec  on ec.id_estado_civil  = c.id_estado_civil 
                        left outer join nacionalidades n on n.id_nacionalidad = c.id_nacionalidad 
                        left outer join tipos_clientes tc  on tc.id_tipo_cliente  = c.id_tipo_cliente
                        left outer join funcionarios v on v.id_funcionario = c.id_funcionario 
                        where v.usuario = @usuario
                        order by c.nombres asc";

                return await db.QueryAsync<Client>(sql, new { @usuario = userName });
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Client> GetClientDetails(int id)
        {
            var db = dbConnection();
            var sql = "select * from Clientes where id_cliente = @Id";


            return await db.QueryFirstOrDefaultAsync<Client>(sql, new { Id = id });
        }

        public async Task<Client> GetDefaultClientDetails()
        {
            var db = dbConnection();
            var sql = @"select * from Clientes where por_defecto = true limit 1";


            return await db.QueryFirstOrDefaultAsync<Client>(sql, new { });
        }

        public async Task<Int32> InsertClient(Client client)
        {

            var db = dbConnection();

            var sql = @" INSERT INTO public.clientes
                        (nombres, apellidos, sexo, fecha_nacimiento, ci, ruc, direccion, telefono, email, observaciones, fecha_alta, razon_social, codigo, es_cliente_fiel, id_estado_civil, tipo_vivienda, id_nacionalidad, direccion_envio, id_barrio, id_tipo_cliente, cliente_exento)

                        VALUES(@nombres,@apellidos,@sexo,@fecha_nacimiento,@ci,@ruc,@direccion,@telefono,@email,@observaciones,@fecha_alta,@razon_social,@codigo,@es_cliente_fiel,@id_estado_civil,@tipo_vivienda,@id_nacionalidad,@direccion_envio,@id_barrio,@id_tipo_cliente, @cliente_exento)
                        RETURNING id_cliente;";
            try
            {


                Object result = await db.ExecuteScalarAsync(sql, new
                {
                    client.nombres,
                    client.apellidos,
                    client.sexo,
                    client.fecha_nacimiento,
                    client.ci,
                    client.ruc,
                    client.direccion,
                    client.telefono,
                    client.email,
                    client.observaciones,
                    client.fecha_alta,
                    client.razon_social,
                    client.codigo,
                    client.es_cliente_fiel,
                    client.id_estado_civil,
                    client.tipo_vivienda,
                    client.id_nacionalidad,
                    client.direccion_envio,
                    client.id_barrio,
                    client.id_tipo_cliente,
                    client.cliente_exento

                });

                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<bool> UpdateClient(Client client)
        {

            var db = dbConnection();

            var sql = @"UPDATE public.clientes
                                SET nombres= @nombres, 
                                    apellidos= @apellidos, 
                                    sexo= @sexo, 
                                    fecha_nacimiento= @fecha_nacimiento, 
                                    ci= @ci, 
                                    ruc= @ruc, 
                                    direccion= @direccion, 
                                    telefono= @telefono, 
                                    email= @email, 
                                    observaciones= @observaciones, 
                                    fecha_alta= @fecha_alta, 
                                    razon_social= @razon_social, 
                                    codigo= @codigo, 
                                    es_cliente_fiel= @es_cliente_fiel, 
                                    id_estado_civil= @id_estado_civil, 
                                    tipo_vivienda= @tipo_vivienda, 
                                    id_nacionalidad= @id_nacionalidad, 
                                    direccion_envio= @direccion_envio, 
                                    id_barrio= @id_barrio, 
                                    id_tipo_cliente= @id_tipo_cliente,
                                    cliente_exento= @cliente_exento
                        where id_cliente = @id_cliente;";

            //if (client.id_barrio == 0)
            //{
            //    client.id_barrio = new int();
            //}

            //if (client.id_estado_civil == 0)
            //{
            //    client.id_estado_civil = new int();
            //}

            //if (client.id_nacionalidad == 0)
            //{
            //    client.id_nacionalidad = new int();
            //}

            //if (client.id_tipo_cliente == 0)
            //{
            //    client.id_tipo_cliente = new int();
            //}



            var result = await db.ExecuteAsync(sql, new
            {
                client.nombres,
                client.apellidos,
                client.sexo,
                client.fecha_nacimiento,
                client.ci,
                client.ruc,
                client.direccion,
                client.telefono,
                client.email,
                client.observaciones,
                client.fecha_alta,
                client.razon_social,
                client.codigo,
                client.es_cliente_fiel,
                id_estado_civil = client.id_estado_civil == 0 ? (int?)null : client.id_estado_civil,
                client.tipo_vivienda,
                id_nacionalidad = client.id_nacionalidad == 0 ? (int?)null : client.id_nacionalidad,
                client.direccion_envio,
                id_barrio = client.id_barrio == 0 ? (int?)null : client.id_barrio,
                id_tipo_cliente = client.id_tipo_cliente == 0 ? (int?)null : client.id_tipo_cliente,
                client.id_cliente,
                client.cliente_exento
            });

            return result > 0;
        }
    }
}
