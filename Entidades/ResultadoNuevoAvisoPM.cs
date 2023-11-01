using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPAvisosPM.Entidades
{
    public class ResultadoNuevoAvisoPM
    {
        public string type { get; set; }
        public string idResultado { get; set; }
        public string message { get; set; }
        public string numberresultado { get; set; }
    }

    public class listResultadoNuevoAvisoPM
    {
        public List<ResultadoNuevoAvisoPM> lstresultadoNuevoAvisoPM { get; set; }
        public Int32 codResultado { get; set; }
        public string DescrpResultado { get; set; }
    }
}
