using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{   
    public interface IrepositorioCuentas
    {
        Task crear(Cuenta cuenta);
    }
    public class repositorioCuentas : IrepositorioCuentas
    {
        private readonly string connectionString;

        public repositorioCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");

        }
        public async Task crear(Cuenta cuenta)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync(@"INSERT INTO cuentas (Nombre,TipoCuentaId,Descripcion,Balance)Values (@Nombre,@TipoCuentaId,@Description,@Balance);
                Selectop SCOPE_IDENTITY();",cuenta);

        }
    }
}
