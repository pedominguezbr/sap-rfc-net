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
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session[Constantes.SESION_AMBIENTE] = null;
                Session[Constantes.SESION_USUARIO] = null;
                Response.Redirect(WebConfigurationManager.AppSettings[Constantes.DIRECCION_FRM_DEFAULT]);
            }
        }
    }
}