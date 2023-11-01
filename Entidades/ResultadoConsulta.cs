using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPAvisosPM.Entidades
{
    public class ResultadoConsulta
    {
        public string ubicaTecnica { get; set; }
        public string numEquipo { get; set; }
        public string denominaUbicaTecnica { get; set; }
        public string denominaEquipo { get; set; }
        public string clasificacion { get; set; }
        public string grupoPlanificador { get; set; }
        public string perfildeCatalogo { get; set; }
        public Int32 idResultado { get; set; }

    }

    public class ListaResultadoConsulta
    {
        public List<ResultadoConsulta> listaResultadoConsulta { get; set; }
        public Int32 codResultado { get; set; }
        public string DescrpResultado { get; set; }
    }
}
