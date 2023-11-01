using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using SAP.Middleware.Connector;
using SAPAvisosPM.Helper;
using System.Web.Configuration;
namespace SAPAvisosPM.BusinessData
{
    public class Usuario
    {
        public String ValidaUsuario(String pDestino, String pUsuario, String pPassword,String vMandante)
        {
            DestinationConfig cfg = null;
            RfcRepository repo = null;
            RfcDestination dest = null;


            try
            {
                GlobalMandante.Mandante = vMandante;

                cfg = new DestinationConfig();

                RfcDestinationManager.RegisterDestinationConfiguration(cfg);
                dest = RfcDestinationManager.GetDestination(pDestino);

                try
                {
                    dest.Ping();
                    repo = dest.Repository;
                }
                catch (Exception e1)
                {
                    RfcDestinationManager.UnregisterDestinationConfiguration(cfg);
                    cfg = null;
                    NetLogger.WriteLog(ELogLevel.ERROR, e1.Message + Environment.NewLine + e1.StackTrace);
                    return WebConfigurationManager.AppSettings[Constantes.MSGSERRORREDSOCKS];
                }
                

                IRfcFunction Function__ZRFC_VALIDA_USUARIO = repo.CreateFunction("ZRFC_VALIDA_USUARIO");

                Function__ZRFC_VALIDA_USUARIO.SetValue("IM_USUARIO", pUsuario);
                Function__ZRFC_VALIDA_USUARIO.SetValue("IM_CLAVE", pPassword);

                Function__ZRFC_VALIDA_USUARIO.Invoke(dest);

                String retornoValor = Function__ZRFC_VALIDA_USUARIO.GetValue("EX_RESULTADO").ToString();
                String EsCaducado = Function__ZRFC_VALIDA_USUARIO.GetValue("EX_MESSAGE").ToString();
                //si es 1 caducado 


                if (EsCaducado.Equals("El usuario ha caducado"))
                {
                    retornoValor = "2";
                }

                RfcDestinationManager.UnregisterDestinationConfiguration(cfg);
                return retornoValor;
                // 0 es valor ok //1 error en usuario // 2 caducado
            }
            catch (Exception ex)
            {
                //El mandante BBB no existe en el sistema
                if (cfg != null)
                {
                    RfcDestinationManager.UnregisterDestinationConfiguration(cfg);
                }
                //  Response.Write(@"<script language='javascript'>alert('" + ex.Message + "');</script>");
                NetLogger.WriteLog(ELogLevel.ERROR, ex.Message + Environment.NewLine + ex.StackTrace);
                return ex.Message.ToString();
            }
        }

    }
}
