using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPAvisosPM.Helper
{
    public static class Constantes
    {
        #region Constantes del Web.Config
        public static readonly string ID_APLICACION = "ID_APLICACION";
        public static readonly string DIRECCION_FRM_DEFAULT = "DireccionFrmDefault";
        public static readonly string DIRECCION_FRM_CONSULTA = "DireccionFrmConsulta";
        public static readonly string DIRECCION_FRM_REGISTRO = "DireccionFrmRegistro";

        public static readonly string CANTIDADREGPORPAGINA = "CantidaRegistrosporPagina";
        public static readonly string MSGCONSULTASINREGISTROS = "MsgConsultaSinResultados";
        public static readonly string MSGCONSULTASCAMPOOBLIGAT = "MsgConsultaCampoObligatorio";
        public static readonly string MSGCONSULTASVALBUSQUEDAA = "MsgConsultaValidacionBusquedaA";

        public static readonly string MSGCONSULTASVALBUSQUEDAC = "MsgConsultaValidacionBusquedaC";
        public static readonly string MSGCONSULTASVALBUSQUEDUBICATECNICA = "MsgConsultaValidacionBusquedaUbiTecnica";
        public static readonly string MSGCONSULTASVALBUSQUETEXTUBEQUI = "MsgConsultaValidacionBusquedaTextUbEqui";
        public static readonly string CONSULTASVALBUSQUETOOLTIP = "ConsultaValidacionTooltip";

        public static readonly string MSGSELECCIONARORIGENAVISO = "MsgSeleccionarOrigenAviso";
        public static readonly string MSGSINGRUPOENCONTRADO = "MsgSinGrupoEncontrado";
        public static readonly string MSGSINPUESTOENCONTRADO = "MsgSinPuestoTrabajoEncontrado";
        public static readonly string MSGSSINTOMAENCONTRADO = "MsgSinSintomaEncontrado";
        public static readonly string MSGCAUSAENCONTRADO = "MsgSinCausaEncontrado";
        public static readonly string MSGPARTEOBJETOENCONTRADO = "MsgSinParteObjectoEncontrado";
        public static readonly string RECURSO_RUTA_APLICACION = "RECURSO_RUTA_APLICACION";

        public static readonly string MSGSORIGENOBLIGATORIO = "MsgOrigenObligatorio";
        public static readonly string MSGSGRUPOPLANIOBLIGATORIO = "MsgGrupoPlaniObligatorio";
        public static readonly string MSGSPUESTOTRABAJOOBLIGATORIO = "MsgPuestoTrabajoObligatorio";
        public static readonly string MSGSAUTOROBLIGATORIO = "MsgAutorObligatorio";
        public static readonly string MSGSPRIORIDADOBLIGATORIO = "MsgPrioridadObligatorio";
        public static readonly string MSGSTEXTOBREVEBLIGATORIO = "MsgTextoBreveObligatorio";
        public static readonly string MSGSCLASEAVISONOBLIGATORIO = "MsgClaseAvisoObligatorio";
        public static readonly string MSGSREPERCUSIONOBLIGATORIO = "MsgRepercusionObligatorio";
        public static readonly string MSGSTEXTOCAUSAOBLIGATORIO = "MsgTextoCausaObligatorio";

        public static readonly string MSGSERRORREDSOCKS = "MsgErrorRedSocks";
        //DDL AMBIENTE
        public static readonly string DEFAULT_DDL_AMBIENTE = "DefaultDllAmbiente";

        #endregion FIN Constantes del Web.Config

        #region Constantes propias de la aplicación
        //SESSION --------------------------------------------------------------------------------------
        public static readonly string SESION_AMBIENTE = "AMBIENTE";
        public static readonly string SESION_USUARIO = "USUARIO";
        public static readonly string SESION_MANDANTE = "MANDANTE";
        public static readonly string SESION_CONSULTA_TIPOBUSQUEDASEL = "SESION_CONSULTA_TIPOBUSQUEDASEL";
        public static readonly string SESION_CONSULTA_VALORBUSQUEDA = "SESION_CONSULTA_VALORBUSQUEDA";
        public static readonly string SESION_CONSULTA_RESULTADO = "SESION_CONSULTA_RESULTADO";
        public static readonly string SESION_RESULTADO_SELECT = "SESION_RESULTADO_SELECT";
        public static readonly string SESION_RESULTADOMATCHCODE = "SESION_RESULTADOMATCHCODE";

        public static readonly string SESION_GRUPOPLANIFILTRO = "SESION_GRUPOPLANIFILTRO";
        public static readonly string SESION_PUESTOTRABAJOFILTRO = "SESION_PUESTOTRABAJOFILTRO";
        public static readonly string SESION_SINTOMAFILTRO = "SESION_SINTOMAFILTRO";
        public static readonly string SESION_CAUSAFILTRO = "SESION_CAUSAFILTRO";
        public static readonly string SESION_PARTEOBJETOFILTRO = "SESION_PARTEOBJETOFILTRO";

        public static readonly string SESION_ORIGEN_SEL = "SESION_ORIGEN_SEL";
        public static readonly string SESION_GRUPOPLANIFI_SEL = "SESION_GRUPOPLANIFI_SEL";
        public static readonly string SESION_PUESTOTRABAJO_SEL = "SESION_PUESTOTRABAJO_SEL";
        public static readonly string SESION_PARTEOBJETO_SEL = "SESION_PARTEOBJETO_SEL";
        public static readonly string SESION_SINTOMA_SEL = "SESION_SINTOMA_SEL";
        public static readonly string SESION_CAUSA_SEL = "SESION_CAUSA_SEL";
        //OTRAS VARIABLES
        public static readonly string AMBIENTE_DEV = "DEV";
        public static readonly string AMBIENTE_QAS = "QAS";
        public static readonly string AMBIENTE_PRD = "PRD";

        public static readonly string PI_BUSQ1 = "1";
        public static readonly string PI_BUSQ2 = "2";
        public static readonly string PI_BUSQ3 = "3";
        public static readonly string PI_BUSQ4 = "4";
        public static readonly string PI_BUSQ5 = "5";
        public static readonly string PI_BUSQ6 = "6";

        public static readonly string COMODINBUSUQEDA = "*";

        public static readonly string PI_BUSQ_MARCADO = "X";
        public static readonly string PI_BUSQ_VACIO = "";

        public static readonly string MSG_ENTER = "<br/>";
        public static readonly string LI_OPEN = "<li>";
        public static readonly string LI_CLOSE = "</li>";
        
        public static readonly Int32 NUM_REG_FOR_LINEA = 132;

        public static readonly string CLASE_AVISO_Z1 = "Z1";
        public static readonly string CLASE_AVISO_Z2 = "Z2";
        public static readonly string CLASE_AVISO_Z3 = "Z3";
        public static readonly string CLASE_AVISO_Z4 = "Z4";
        #endregion Fin de las constantes propias
    }
}
