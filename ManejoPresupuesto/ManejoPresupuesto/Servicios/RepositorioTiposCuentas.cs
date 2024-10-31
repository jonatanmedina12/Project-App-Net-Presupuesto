using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioTiposCuentas
    {
        Task crear(TipoCuenta tipoCuenta);
        Task<bool> Existe (string nombre, int usuarioId);
        Task<IEnumerable<TipoCuenta>> Obtener(string nombre, int usuarioId);
        Task Borrar(int id);
        Task Ordenar(IEnumerable<TipoCuenta> tipoCuentasOrdenes);
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
        public async Task<IEnumerable<TipoCuenta>>Obtener(string nombre,int usuarioId)
        {
            using var connection = new SqlConnection (connectionString);
            return await connection.QueryAsync<TipoCuenta>("select id,nombre,orden from TiposCuentas where nombre=@nombre and usuarioId=@usuarioId", new {nombre, usuarioId });
        }
        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE TiposCuentas Where Id = @Id",new {id});
        }
        public async Task Ordenar(IEnumerable<TipoCuenta> tipoCuentasOrdenes)
        {
            var query = "Update TiposCuentas set Orden = @Orden where Id=@Id";
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(query, tipoCuentasOrdenes);

        }
    }
}
