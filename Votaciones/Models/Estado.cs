using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Votaciones.Models
{
    public class Estado
    {
        [Key]
        
        public int idEstado { get; set; }
        [Display(Name = "Estado Votación")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(50, ErrorMessage = "El campo {0} debe tener minimo {2} caracteres.", MinimumLength = 3)]
        public String descripcion { get; set; }
        public virtual ICollection<Censo>censos { get; set; }
    }
}