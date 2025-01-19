using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioCuentas
    {
        Task Crear(Cuenta cuenta);
    }
    public class RepositorioCuenta: IRepositorioCuentas
    {

        private readonly string connectionString;

        public RepositorioCuenta(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");

        }

        public async Task Crear(Cuenta cuenta)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync(@"INSERT INTO cuentas(Nombre,TipoCuentaId,Descripcion,Balance)Values(@Nombre,@TipoCuentaId,@Description,@Balance);

                select scope_indentity();
            ",cuenta);

            cuenta.Id = id; 
        }
    }
}
