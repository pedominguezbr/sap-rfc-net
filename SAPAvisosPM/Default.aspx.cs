using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPAvisosPM.BusinessData;
using SAPAvisosPM.Helper;
using System.Web.Configuration;

namespace SAPAvisosPM.Web
{

    public partial class Default : System.Web.UI.Page
    {
        private Usuario objUsuario = new Usuario();

        string DESA_MANDANTE01 = WebConfigurationManager.AppSettings["DESA_MANDANTE01"].ToString();
        string QA_MANDANTE01 = WebConfigurationManager.AppSettings["QA_MANDANTE01"].ToString();
        string PROD_MANDANTE01 = WebConfigurationManager.AppSettings["PROD_MANDANTE01"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void LoginBTN_Click(object sender, EventArgs e)
        {
            String vAmbiente = null;
            String vUsuario = null;
            String vPassword = null;
            String vMandante = null;

            vAmbiente = lstGrupo.SelectedValue.ToString();
            vUsuario = Usuario.Value.ToString();
            vPassword = Password.Value.ToString();

            if (vAmbiente.Equals(WebConfigurationManager.AppSettings[Constantes.DEFAULT_DDL_AMBIENTE]))
            {

            }
            else
            {

                vMandante = mandan.Value.ToString();// lstMandante.SelectedValue.ToString();
                String resultado_ValidaUsuario = objUsuario.ValidaUsuario(vAmbiente, vUsuario, vPassword, vMandante);

                if (resultado_ValidaUsuario.Equals("0"))
                {
                    Session[Constantes.SESION_AMBIENTE] = vAmbiente;
                    Session[Constantes.SESION_USUARIO] = vUsuario;
                    Session[Constantes.SESION_MANDANTE] = vMandante;

                    Response.Redirect(WebConfigurationManager.AppSettings[Constantes.DIRECCION_FRM_CONSULTA], false);
                }
                else
                {
                    if (resultado_ValidaUsuario.Equals("1"))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    }
                    else
                    {
                        if (resultado_ValidaUsuario.Equals("2"))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalCaducado();", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", string.Format("ShowPopupLogin('{0}');", resultado_ValidaUsuario), true);
                        }

                    }
                }
            }
        }


        public static DateTime DateTimeAdo(DateTime dateTime, int hours, int minutes, int seconds)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                seconds,
                dateTime.Kind);
        }

        protected void lstGrupo_SelectedIndexChanged1(object sender, EventArgs e)
        {

            if (lstGrupo.SelectedValue == Constantes.AMBIENTE_DEV)
            {
                mandan.Value = DESA_MANDANTE01;
            }
            else if (lstGrupo.SelectedValue == Constantes.AMBIENTE_QAS)
            {
                mandan.Value = QA_MANDANTE01;
            }
            else if (lstGrupo.SelectedValue == Constantes.AMBIENTE_PRD)
            {
                mandan.Value = PROD_MANDANTE01;
            }
            else if (lstGrupo.SelectedValue == WebConfigurationManager.AppSettings[Constantes.DEFAULT_DDL_AMBIENTE])
            {
                mandan.Value = "";
            }
        }
    }
}