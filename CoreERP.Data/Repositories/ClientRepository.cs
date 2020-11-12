﻿using CoreERP.Model;
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
            var db = dbConnection();
            var sql = @"select *
                        from clientes c  
                        left outer join barrios b on b.id_barrio = c.id_barrio
                        left outer join estados_civiles ec  on ec.id_estado_civil  = c.id_estado_civil 
                        left outer join nacionalidades n on n.id_nacionalidad = c.id_nacionalidad 
                        left outer join tipos_clientes tc  on tc.id_tipo_cliente  = c.id_tipo_cliente 
                        order by c.id_cliente asc";


            return await db.QueryAsync<Client, Neighborhood, CivilStatus, Nationality, ClientType, Client>(sql,
                (client, neighborhood, civilstatus, nationality, clienttype) =>
                { client.Neighborhood = neighborhood; client.CivilStatus = civilstatus; client.Nationality = nationality; client.ClientType = clienttype; return client; },
                splitOn: "id_barrio,id_estado_civil,id_nacionalidad,id_tipo_cliente");
        }

        public async Task<Client> GetClientDetails(int id)
        {
            var db = dbConnection();
            var sql = "select * from Clientes where id_cliente = @Id";


            return await db.QueryFirstOrDefaultAsync<Client>(sql, new { Id = id });
        }

        public async Task<bool> InsertClient(Client client)
        {
            var db = dbConnection();

            var sql = @" INSERT INTO public.clientes
                        (nombres, apellidos, sexo, fecha_nacimiento, ci, ruc, direccion, telefono, email, observaciones, fecha_alta, razon_social, codigo, es_cliente_fiel, id_estado_civil, tipo_vivienda, id_nacionalidad, direccion_envio, id_barrio, id_tipo_cliente)

                        VALUES(@nombres,@apellidos,@sexo,@fecha_nacimiento,@ci,@ruc,@direccion,@telefono,@email,@observaciones,@fecha_alta,@razon_social,@codigo,@es_cliente_fiel,@id_estado_civil,@tipo_vivienda,@id_nacionalidad,@direccion_envio,@id_barrio,@id_tipo_cliente);";

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
                client.id_estado_civil,
                client.tipo_vivienda,
                client.id_nacionalidad,
                client.direccion_envio,
                client.id_barrio,
                client.id_tipo_cliente

            });

            return result > 0;
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
                                    id_tipo_cliente= @id_tipo_cliente
                        where id_cliente = @id_cliente;";

            if (client.Neighborhood != null)
            {
                client.id_barrio = client.Neighborhood.id_barrio;
            }

            if (client.CivilStatus != null)
            {
                client.id_estado_civil = client.CivilStatus.id_estado_civil;
            }

            if (client.Nationality != null)
            {
                client.id_nacionalidad = client.Nationality.id_nacionalidad;
            }




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
                client.id_estado_civil,
                client.tipo_vivienda,
                client.id_nacionalidad,
                client.direccion_envio,
                client.id_barrio,
                client.id_cliente
            });

            return result > 0;
        }
    }
}
