using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAP.Middleware.Connector;

using System.Configuration;
namespace SAPAvisosPM.BusinessData
{
    public class DestinationConfig : IDestinationConfiguration
    {

        public bool ChangeEventsSupported()
        { return false; }

        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;

        public RfcConfigParameters GetParameters(string destinationName)
        {
            // EL MANDANTE QUE SEA PARAMETRO DE SESIÓN
            string DESA_IP = ConfigurationManager.AppSettings["DESA_IP"].ToString();
            string DESA_INSTANCIA = ConfigurationManager.AppSettings["DESA_INSTANCIA"].ToString();
            string DESA_ID_SISTEMA = ConfigurationManager.AppSettings["DESA_ID_SISTEMA"].ToString();
            string DESA_USUARIO = ConfigurationManager.AppSettings["DESA_USUARIO"].ToString();
            string DESA_PASSWORD = ConfigurationManager.AppSettings["DESA_PASSWORD"].ToString();
            //string DESA_MANDANTE = ConfigurationManager.AppSettings["DESA_MANDANTE"].ToString();
            string DESA_LENGUAJE = ConfigurationManager.AppSettings["DESA_LENGUAJE"].ToString();
            string DESA_POOLSIZE = ConfigurationManager.AppSettings["DESA_POOLSIZE"].ToString();

            string QA_IP = ConfigurationManager.AppSettings["QA_IP"].ToString();
            string QA_INSTANCIA = ConfigurationManager.AppSettings["QA_INSTANCIA"].ToString();
            string QA_ID_SISTEMA = ConfigurationManager.AppSettings["QA_ID_SISTEMA"].ToString();
            string QA_USUARIO = ConfigurationManager.AppSettings["QA_USUARIO"].ToString();
            string QA_PASSWORD = ConfigurationManager.AppSettings["QA_PASSWORD"].ToString();
            //string QA_MANDANTE = ConfigurationManager.AppSettings["QA_MANDANTE"].ToString();
            string QA_LENGUAJE = ConfigurationManager.AppSettings["QA_LENGUAJE"].ToString();
            string QA_POOLSIZE = ConfigurationManager.AppSettings["QA_POOLSIZE"].ToString();

            string PROD_IP = ConfigurationManager.AppSettings["PROD_IP"].ToString();
            string PROD_INSTANCIA = ConfigurationManager.AppSettings["PROD_INSTANCIA"].ToString();
            string PROD_ID_SISTEMA = ConfigurationManager.AppSettings["PROD_ID_SISTEMA"].ToString();
            string PROD_USUARIO = ConfigurationManager.AppSettings["PROD_USUARIO"].ToString();
            string PROD_PASSWORD = ConfigurationManager.AppSettings["PROD_PASSWORD"].ToString();
            //string PROD_MANDANTE = ConfigurationManager.AppSettings["PROD_MANDANTE"].ToString();
            string PROD_LENGUAJE = ConfigurationManager.AppSettings["PROD_LENGUAJE"].ToString();
            string PROD_POOLSIZE = ConfigurationManager.AppSettings["PROD_POOLSIZE"].ToString();

            RfcConfigParameters parms = new RfcConfigParameters();

            if (destinationName.Equals("DEV"))
            {
                /*
                 parms.Add(RfcConfigParameters.AppServerHost, "10.94.1.8");
                 parms.Add(RfcConfigParameters.SystemNumber, "10"); //ultimos digitos del puerto  //se indica en el SAP
                 parms.Add(RfcConfigParameters.SystemID, "EDV");
                 parms.Add(RfcConfigParameters.User, "APASTOR");//Variables.Usuario);//"APASTOR");
                 parms.Add(RfcConfigParameters.Password, "Apastor2!");// Variables.Password);//"Apastor2!");
                 parms.Add(RfcConfigParameters.Client, "110");       //mandante
                 parms.Add(RfcConfigParameters.Language, "ES");
                 parms.Add(RfcConfigParameters.PoolSize, "5");  //tamaño de caracteres  cada cliente que puede concectarse cola
                 */

                parms.Add(RfcConfigParameters.AppServerHost, DESA_IP);
                parms.Add(RfcConfigParameters.SystemNumber, DESA_INSTANCIA);
                parms.Add(RfcConfigParameters.SystemID, DESA_ID_SISTEMA);
                parms.Add(RfcConfigParameters.User, DESA_USUARIO);
                parms.Add(RfcConfigParameters.Password, DESA_PASSWORD);
                parms.Add(RfcConfigParameters.Client, GlobalMandante.Mandante);// Session["MANDANTE"]);//DESA_MANDANTE);    
                parms.Add(RfcConfigParameters.Language, DESA_LENGUAJE);
                parms.Add(RfcConfigParameters.PoolSize, DESA_POOLSIZE);
            }
            else if (destinationName.Equals("QAS"))
            {
                parms.Add(RfcConfigParameters.AppServerHost, QA_IP);
                parms.Add(RfcConfigParameters.SystemNumber, QA_INSTANCIA);
                parms.Add(RfcConfigParameters.SystemID, QA_ID_SISTEMA);
                parms.Add(RfcConfigParameters.User, QA_USUARIO);
                parms.Add(RfcConfigParameters.Password, QA_PASSWORD);
                parms.Add(RfcConfigParameters.Client, GlobalMandante.Mandante);
                parms.Add(RfcConfigParameters.Language, QA_LENGUAJE);
                parms.Add(RfcConfigParameters.PoolSize, QA_POOLSIZE);

            }
            else if (destinationName.Equals("PRD"))
            {
                parms.Add(RfcConfigParameters.AppServerHost, PROD_IP);
                parms.Add(RfcConfigParameters.SystemNumber, PROD_INSTANCIA);
                parms.Add(RfcConfigParameters.SystemID, PROD_ID_SISTEMA);
                parms.Add(RfcConfigParameters.User, PROD_USUARIO);
                parms.Add(RfcConfigParameters.Password, PROD_PASSWORD);
                parms.Add(RfcConfigParameters.Client, GlobalMandante.Mandante);
                parms.Add(RfcConfigParameters.Language, PROD_LENGUAJE);
                parms.Add(RfcConfigParameters.PoolSize, PROD_POOLSIZE);
            }

            return parms;
        }
    }
}