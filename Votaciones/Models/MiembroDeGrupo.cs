using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Votaciones.Models
{
    public class MiembroDeGrupo
    {
        [Key]
        public int idMiembroGrupo { get; set; }
        public int idGrupo { get; set; }
        public int idPersona { get; set; }
        public virtual Grupo grupo { get; set; }
        public virtual Persona persona { get; set; }
    }
}