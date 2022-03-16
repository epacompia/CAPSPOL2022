using CAPSPOL2022.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace CAPSPOL2022.Models
{
    public class Position: IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        [StringLength(maximumLength:50,MinimumLength =3, ErrorMessage ="La longitud del campo {0} debe estar entre {2} y {1}")]
        [Display(Name ="Nombre")]
        //[PrimeraLetraMayuscula]
        public string Name { get; set; }
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        public bool Flag { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var primeraLetra=Name[0].ToString();
            if (primeraLetra != primeraLetra.ToUpper())
            {
                yield return new ValidationResult("La primera letra debe ser mayuscula", new[] {nameof(Name)});
            }
        }
    }
}
