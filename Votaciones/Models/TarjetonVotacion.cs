using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Votaciones.Models
{
    public class Censo
    {
        [Key]
        public int idVotacion { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(50, ErrorMessage = "El campo {0} debe tener minimo {2} caracteres.", MinimumLength = 3)]
        public string descripcion { get; set; }

        [Display(Name = "Estado Votación")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [StringLength(20, ErrorMessage = "El campo {0} debe tener minimo {2} caracteres.", MinimumLength = 3)]
        public int idEstado { get; set; }

        [Display(Name = "Comentarios")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [DataType(DataType.MultilineText)]
        public string comentarios { get; set; }

        [Display(Name = "Fecha y Hora de Inicio")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime fechaHoraInicio { get; set; }

        [Display(Name = "Fecha y Hora de Finalización ")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd hh:mm}",ApplyFormatInEditMode =true)]
        public DateTime fechaHoraFinal { get; set; }

        [Display(Name = "¿Es para todos los Usuarios?")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        public bool EsParaTodoUsuario { get; set; }

        [Display(Name = "¿Habilitar Votos en Blanco? ")]
        [Required(ErrorMessage = "Usted debe ingresar {0}")]
        public bool HabilitarVotosBlanco { get; set; }

        [Display(Name = "Cantidad de votos")]
        public int cantidadVotos { get; set; }

        [Display(Name = "Cantidad de votos en blanco")]
        public int cantVotosBlanco { get; set; }

        [Display(Name = "Candidato Ganador")]
        public int CandidatoGanadorId { get; set; }




    }
}