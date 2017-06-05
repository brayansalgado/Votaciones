using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Votaciones.Models
{
    public class PersonaVista
    {
        public int idPersona { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(50, ErrorMessage = "El campo {0} debe tener minimo {2} caracteres.", MinimumLength = 3)]
        public string nombrePersona { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(50, ErrorMessage = "El campo {0} debe tener minimo {2} caracteres.", MinimumLength = 3)]
        [Index("usuarioIndex", IsUnique = true)]
        public string usuario { get; set; }

        [Display(Name = "Correo electrónico")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        public string email { get; set; }

        [Display(Name = "Cargo")]

        public string cargo { get; set; }


        public string grupo { get; set; }

 
        public  HttpPostedFileBase foto { get; set; }

}
}