using ManejoPresupuesto.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Models
{
    public class TipoCuenta:IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es requerido")]
        [StringLength(maximumLength:50,MinimumLength =3,ErrorMessage ="la longitud del campo {0} debe estar entre {2} y  {1}")]
        //[PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        public int UsuarioId { get; set; }

        public int Orden { get; set; }

        [Required(ErrorMessage ="El campo{0} es requerido ")]
        [EmailAddress(ErrorMessage ="El campo debe ser un correo electronico valido")]
        public string Email { get; set; }


        [Range(minimum:18,maximum:130,ErrorMessage ="El valor debe entre {1 y  {2}")]
       
        public int Edad {  get; set; }
        [Url(ErrorMessage ="El campo debe ser una Url validad")]
        public string Url { get; set; }
        [CreditCard(ErrorMessage ="la tarjeta de credito no es validad")]
        public string TarjetaDeCredito { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Nombre != null && Nombre.Length > 0)
            {
                var primeraLetra = Nombre[0].ToString();
                if(primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayuscula", new[] {nameof(Nombre)});
                }
            }

        }
    }
}
