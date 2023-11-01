using SAPAvisosPM.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAPAvisosPM.Web
{
    public partial class AvisosPM : System.Web.UI.MasterPage
    {
        string DESA_IDSIS = WebConfigurationManager.AppSettings["DESA_ID_SISTEMA"].ToString();
        string QA_IDSIS = WebConfigurationManager.AppSettings["QA_ID_SISTEMA"].ToString();
        string PROD_IDSIS = WebConfigurationManager.AppSettings["PROD_ID_SISTEMA"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[Constantes.SESION_AMBIENTE] != null && Session[Constantes.SESION_USUARIO] != null)
                {
                    ambconn.InnerText = Session[Constantes.SESION_AMBIENTE].ToString();
                    usrconn.InnerText = Session[Constantes.SESION_USUARIO].ToString();
                    mandan.InnerText = Session[Constantes.SESION_MANDANTE].ToString();
                    switch (ambconn.InnerText)
                    {
                        case "QAS":
                            {
                                idsis.InnerText = QA_IDSIS;
                                break;
                            }
                        case "DEV":
                            {
                                idsis.InnerText = DESA_IDSIS;
                                break;
                            }
                        case "PRD":
                            {
                                idsis.InnerText = PROD_IDSIS;
                                break;
                            }


                        default:
                            idsis.InnerText = "";
                            break;
                    }
                }
                else
                {
                     Response.Redirect(WebConfigurationManager.AppSettings[Constantes.DIRECCION_FRM_DEFAULT]);
                }
            }
        }
    }
}