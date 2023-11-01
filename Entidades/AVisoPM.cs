using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPAvisosPM.Entidades
{
    public class AVisoPM
    {
        public ResultadoConsulta resultadoConsulta { get; set; }
        public string numEquipo { get; set; }
        public string ubicacionTecnica { get; set; }
        public ClaseAviso claseAviso { get; set; }
        public string textoBreve { get; set; }
        public Prioridad prioridad { get; set; }
        public GrupoPlanificacion grupoPlanificacion { get; set; }
        public string nombreAutor { get; set; }
        public string fechaAviso { get; set; }
        public string horaAviso { get; set; }
        public Origen Origen { get; set; }
        public Boolean equipoParado { get; set; }
        public Repercusion repercusion { get; set; }
        public PuestoTrabajo puestoTrabajo { get; set; }
        public Sintoma sintoma { get; set; }
        public string textoSintoma { get; set; }
        public ParteObjeto parteObjeto { get; set; }
        public Causa causa { get; set; }
        public string textoCausa { get; set; }
       
        public string textoLargo { get; set; }
        public IEnumerable<string> listatextoLargo { get; set; }
    }
}
