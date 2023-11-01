using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPAvisosPM.BusinessData;
using System.Web.UI.HtmlControls;
using SAPAvisosPM.Helper;
using SAPAvisosPM.Entidades;
using System.Web.Configuration;
using System.Drawing;
namespace SAPAvisosPM.Web
{
    public partial class Consulta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.gvResultadoConsulta.PageSize = Convert.ToInt32(WebConfigurationManager.AppSettings[Constantes.CANTIDADREGPORPAGINA]);
            }
        }

        private void consultarDataBind()
        {
            ListaResultadoConsulta listResultadoConsulta = new ListaResultadoConsulta();
            ConsultaDatos objConsulta = new ConsultaDatos();
            if (this.Session[Constantes.SESION_CONSULTA_TIPOBUSQUEDASEL] != null && this.Session[Constantes.SESION_CONSULTA_VALORBUSQUEDA] != null)
            {
                string tipoBusqueda = Session[Constantes.SESION_CONSULTA_TIPOBUSQUEDASEL].ToString();
                string valorBuscado = Session[Constantes.SESION_CONSULTA_VALORBUSQUEDA].ToString();
                listResultadoConsulta = objConsulta.Consultar(Session[Constantes.SESION_AMBIENTE].ToString(), tipoBusqueda, valorBuscado);

                if (listResultadoConsulta.codResultado==0)
                { 

                this.Session.Add(Constantes.SESION_CONSULTA_RESULTADO, listResultadoConsulta.listaResultadoConsulta);

                if (listResultadoConsulta.listaResultadoConsulta.Count == 0)
                {
                    lblmsg.Text = WebConfigurationManager.AppSettings[Constantes.MSGCONSULTASINREGISTROS];
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSinRegistros();", true);
                }

                this.gvResultadoConsulta.PageIndex = 0;
                gvResultadoConsulta.DataSource = listResultadoConsulta.listaResultadoConsulta;
                gvResultadoConsulta.DataBind();
            }
            else{
                    //fallo llamado a consulta
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", string.Format("ShowPopupMsgErrorSAP('{0}');", listResultadoConsulta.DescrpResultado), true);
            }
                //System.Threading.Thread.Sleep(9000);
            }

            else
            {
                lblmsg.Text = WebConfigurationManager.AppSettings[Constantes.MSGCONSULTASINREGISTROS];
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSinRegistros();", true);
            }
        }


        protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgBuscar = sender as ImageButton;
            if (imgBuscar != null)
            {
                string tipobusqueda = imgBuscar.CommandArgument.ToString();
                string Busqueda;

                switch (tipobusqueda)
                {
                    case "1":
                        {
                            Busqueda = txtClasificacionUbicaTecnica.Value;
                            break;
                        }
                    case "2":
                        {                            
                            Busqueda = txtClasificacionEquipos.Value;
                            break;
                        }
                    case "3":
                        {
                            Busqueda = txtEquipo.Value;
                            break;
                        }
                    case "4":
                        {
                            Busqueda = txtubicafisica.Value;
                            break;
                        }
                    case "5":
                        {
                            Busqueda = txtfiltroclasequipo.Value;
                            break;
                        }
                    case "6":
                        {
                            Busqueda = txtfiltroclasubicatecnica.Value;
                            break;
                        }

                    default:
                        Busqueda = "";
                        break;
                }
                if (ValidacionBusqueda(tipobusqueda, Busqueda))
                {
                    this.Session.Add(Constantes.SESION_CONSULTA_TIPOBUSQUEDASEL, tipobusqueda);
                    this.Session.Add(Constantes.SESION_CONSULTA_VALORBUSQUEDA, Busqueda);

                    this.consultarDataBind();
                }
                else {
                    gvResultadoConsulta.DataSource = null;
                    gvResultadoConsulta.DataBind();
                }
            }
        }

        protected void gvResultadoConsulta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (this.Session[Constantes.SESION_CONSULTA_RESULTADO] != null)
            {
                this.gvResultadoConsulta.PageIndex = e.NewPageIndex;
                List<ResultadoConsulta> listResultadoConsulta = (List<ResultadoConsulta>)this.Session[Constantes.SESION_CONSULTA_RESULTADO];
                this.gvResultadoConsulta.DataSource = listResultadoConsulta;
                this.gvResultadoConsulta.DataBind();
            }
            else
            {
                this.consultarDataBind();
            }
        }

        private bool ValidacionBusqueda(string tipobusqueda, string valorBusqueda)
        {
            //VALIDACION PARA CAMPO VACIO
            if (valorBusqueda == null || valorBusqueda == string.Empty)
            {
                lblmsg.Text = WebConfigurationManager.AppSettings[Constantes.MSGCONSULTASCAMPOOBLIGAT];
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSinRegistros();", true);
                

                return false;
            }

            //VALIDACION DE BUSQUEDAS 1 Y 2
            if (tipobusqueda == Constantes.PI_BUSQ1 || tipobusqueda == Constantes.PI_BUSQ2)
            {
                if (Int32.Parse(valorBusqueda.Replace(Constantes.COMODINBUSUQEDA, "").Length.ToString()) < 4)
                {
                    lblmsg.Text = WebConfigurationManager.AppSettings[Constantes.MSGCONSULTASVALBUSQUEDAA];
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSinRegistros();", true);
                    return false;
                }
            }

            //VALIDACION DE BUSQUEDA 3 - EQUIPO
            if (tipobusqueda == Constantes.PI_BUSQ3)
            {
                if (valorBusqueda.IndexOf(Constantes.COMODINBUSUQEDA, 0) >= 0)
                {
                    lblmsg.Text = WebConfigurationManager.AppSettings[Constantes.MSGCONSULTASVALBUSQUEDAC];
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSinRegistros();", true);
                    return false;
                }
            }

            //VALIDACION DE BUSQUEDA 4 UBICACION TECNICA
            if (tipobusqueda == Constantes.PI_BUSQ4)
            {
                if (Int32.Parse(valorBusqueda.Replace(Constantes.COMODINBUSUQEDA, "").Length.ToString()) < 11)
                {
                    lblmsg.Text = WebConfigurationManager.AppSettings[Constantes.MSGCONSULTASVALBUSQUEDUBICATECNICA];
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSinRegistros();", true);
                    return false;
                }
            }

            //VALIDACION DE BUSQUEDA 5 y 6 TEXTO UBICACION TECNICA Y EQUIPO
            if (tipobusqueda == Constantes.PI_BUSQ5 || tipobusqueda == Constantes.PI_BUSQ6)
            {
                if (Int32.Parse(valorBusqueda.Replace(Constantes.COMODINBUSUQEDA, "").Length.ToString()) < 8) //CAMBIAR A 8 ESTAA 1 PARA PRUEBAS NOMAS
                {
                    lblmsg.Text = WebConfigurationManager.AppSettings[Constantes.MSGCONSULTASVALBUSQUETEXTUBEQUI];
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSinRegistros();", true);
                    return false;
                }
            }

            return true;
        }

        protected void gvResultadoConsulta_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvResultadoConsulta, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = WebConfigurationManager.AppSettings[Constantes.CONSULTASVALBUSQUETOOLTIP];
            }
        }

        protected void gvResultadoConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvResultadoConsulta.Rows)
            {
                if (row.RowIndex == gvResultadoConsulta.SelectedIndex)
                {
                    if (this.Session[Constantes.SESION_CONSULTA_RESULTADO] != null)
                    {
                        List<ResultadoConsulta> listResultadoConsulta = (List<ResultadoConsulta>)this.Session[Constantes.SESION_CONSULTA_RESULTADO];
                        List<ResultadoConsulta> listaBusquedaClass = listResultadoConsulta.FindAll(
                            delegate(ResultadoConsulta bk)
                            {
                                return bk.idResultado.Equals(row.DataItemIndex);
                            });

                        if (listaBusquedaClass.Count > 0)
                        {
                            this.Session.Remove(Constantes.SESION_RESULTADO_SELECT);
                            this.Session.Add(Constantes.SESION_RESULTADO_SELECT, listaBusquedaClass[0]);

                            Response.Redirect(WebConfigurationManager.AppSettings[Constantes.DIRECCION_FRM_REGISTRO], false);

                            return;
                        }
                    }
                }
            }
        }

    }
}