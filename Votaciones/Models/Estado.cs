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
        public String descripcion { get; set; }

    }
}