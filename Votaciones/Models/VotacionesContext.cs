using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Votaciones.Models
{
    public class VotacionesContext:DbContext
    {
        public VotacionesContext() : base("DefaultConnection")
        {
    }
        public DbSet<Estado> Estados { get; set; }
    }
}