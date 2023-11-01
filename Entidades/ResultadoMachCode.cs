using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPAvisosPM.Entidades
{
    public class ResultadoMachCode
    {
        public List<Origen> listOrigen { get; set; }
        public List<GrupoPlanificacion> listGrupoPlanificacion { get; set; }
        public List<PuestoTrabajo> listPuestoTrabajo { get; set; }
        public List<Prioridad> listPrioridad { get; set; }
        public List<Repercusion> listRepercusion { get; set; }
        public List<Sintoma> listSintoma { get; set; }
        public List<Causa> listCausa { get; set; }
        public List<ParteObjeto> listParteObjeto { get; set; }
        public List<ClaseAviso> listClaseAviso { get; set; }
        public Int32 codResultado { get; set; }
        public string DescrpResultado { get; set; }
    }
}
