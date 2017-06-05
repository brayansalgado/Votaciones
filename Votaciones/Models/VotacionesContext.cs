using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Votaciones.Models
{
    public class VotacionesContext:DbContext
    {
        internal IEnumerable Estado;

        public VotacionesContext() : base("DefaultConnection")
        {
    }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        public DbSet<Estado> Estados { get; set; }

        public DbSet<Grupo> Grupos { get; set; }

        public DbSet<Censo> Censos { get; set; }

        public DbSet<Persona> Personas { get; set; }

        public DbSet<MiembroDeGrupo> MiembrosDeGrupo { get; set; }
    }
}