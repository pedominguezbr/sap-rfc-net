using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPAvisosPM.Entidades
{
    public class Causa
    {
        public string grupoCodigo { get; set; }
        public string grupoCodigoDescripcion { get; set; }
        public string codigo { get; set; }
        public string codigoTexto { get; set; }
        public Int32 idResultado { get; set; }
    }
}
