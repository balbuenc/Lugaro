﻿using CoreERP.Model;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreERP.Data.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private SqlConfiguration _connectionString;

        public BudgetRepository(SqlConfiguration connectionStringg)
        {
            _connectionString = connectionStringg;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteBudget(int id)
        {
            var db = dbConnection();

            var sql = @"DELETE from presupuestos
                        WHERE id_presupuesto = @Id ";

            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        public async Task<IEnumerable<Budget>> GetAllBudgets()
        {
            var db = dbConnection();
            var sql = @"select p.id_presupuesto, p.nro_presupuesto , p.fecha, p.estado, f2.usuario as vendedor, c2.razon_social as cliente
                        from presupuestos p
                        left outer join funcionarios f2 on f2.id_funcionario = p.id_funcionario
                        left outer join clientes c2 on c2.id_cliente = p.id_cliente
                        order by p.nro_presupuesto asc";

            return await db.QueryAsync<Budget>(sql, new { });
        }

        public async Task<Budget> GetBudgetDetails(int id)
        {
            var db = dbConnection();
            var sql = "select * from presupuestos  where id_presupuesto = @Id";

            return await db.QueryFirstOrDefaultAsync<Budget>(sql, new { Id = id });
        }

        public async Task<bool> InsertBudget(Budget budget)
        {
            var db = dbConnection();

            try
            {
                var sql = @"INSERT INTO public.presupuestos (id_cliente, id_funcionario, fecha, estado, nro_presupuesto) VALUES(@id_cliente, @id_funcionario, @fecha, @estado, @nro_presupuesto);";
                
                budget.id_funcionario = 6;
                budget.estado = "Generado";

                var result = await db.ExecuteAsync(sql, new
                {
                    budget.id_cliente,
                    budget.id_funcionario,
                    budget.fecha,
                    budget.estado,
                    budget.nro_presupuesto
                }
                );

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateBudget(Budget budget)
        {
            var db = dbConnection();

            var sql = @"UPDATE public.presupuestos
                        SET id_cliente=@id_cliente, id_funcionario=@id_funcionario, fecha=@fecha, estado=@estado
                        WHERE id_presupuesto=@id_presupuesto;
                        ";

            var result = await db.ExecuteAsync(sql, new
            {
                budget.id_presupuesto,
                budget.id_cliente,
                budget.id_funcionario,
                budget.fecha,
                budget.estado
            }
            );

            return result > 0;
        }
    }
}
