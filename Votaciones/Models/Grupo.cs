using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Votaciones.Models
{
    public class Grupo
    {
        [Key]

        public int idGrupo { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(50, ErrorMessage = "El campo {0} debe tener minimo {2} caracteres.", MinimumLength = 3)]
        public String descripcion { get; set; }
        public virtual ICollection<MiembroDeGrupo> miembrosDeGrupo { get; set; }
    }
}