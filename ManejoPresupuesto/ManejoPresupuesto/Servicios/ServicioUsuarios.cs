namespace ManejoPresupuesto.Servicios
{
    public interface IservicioUsuarios
    {
        int ObtenerUsuariosId();
        
    }
    public class ServicioUsuarios : IservicioUsuarios
    {
        public int ObtenerUsuariosId()
        {
            return 1;
        }
    }
}
