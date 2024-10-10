using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioTiposCuentas
    {
        Task crear(TipoCuenta tipoCuenta);
        Task<bool> Existe (string nombre, int usuarioId);
    }
    public class RepositorioTiposCuentas: IRepositorioTiposCuentas
    {
        private readonly string connectionString;
        public RepositorioTiposCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task crear(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connectionString);
            var id =  await connection.QuerySingleAsync<int>($@"iNSERT INTO TiposCuentas(Nombre,UsuarioId,Orden)values(@Nombre,@UsuarioId,@Orden)SELECT SCOPE_IDENTITY()",tipoCuenta);
            tipoCuenta.Id = id;
        }
        public async Task<bool> Existe (string nombre,int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM TiposCuentas where Nombre = @Nombre and UsuarioId=@UsuarioId",new {nombre,usuarioId});



            return existe == 1;
        }
    }
}
