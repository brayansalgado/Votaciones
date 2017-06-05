using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Votaciones.Models
{
    public class AdicionarMiembroVista
    {
        public int idGrupo { get; set; }

        [Required(ErrorMessage = "Usted debe ingresar Persona")]
        public int idPersona { get; set; }
    }
}