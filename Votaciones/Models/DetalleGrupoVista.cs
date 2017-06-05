using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Votaciones.Models
{
    public class DetalleGrupoVista
    {
        public int idGrupo { get; set; }

        public String descripcion { get; set; }

        public List<MiembroDeGrupo> miembros { get; set; }
    }
}