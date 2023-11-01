using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAPAvisosPM.BusinessData
{
    public  static class GlobalMandante
    {
        public static string _mandante;


        public static string Mandante
        {
            get
            {
                return _mandante;
            }
            set
            {
                _mandante = value;
            }
        }
    }
}