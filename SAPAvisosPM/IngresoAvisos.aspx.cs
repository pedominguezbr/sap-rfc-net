using SAPAvisosPM.BusinessData;
using SAPAvisosPM.Entidades;
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
    public partial class IngresoAvisos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDatosConsulta();
            }
        }

        private bool cargarDatosConsulta()
        {
            if (this.Session[Constantes.SESION_RESULTADO_SELECT] != null)
            {
                ResultadoConsulta resultadoConsulta = (ResultadoConsulta)this.Session[Constantes.SESION_RESULTADO_SELECT];
                txtUbicaTecnicaCodigo.Text = resultadoConsulta.ubicaTecnica;
                txtUbicaTecnicadescripcion.Text = resultadoConsulta.denominaUbicaTecnica;
                txtEquipocodigo.Text = resultadoConsulta.numEquipo;
                txtEquipoDescripcion.Text = resultadoConsulta.denominaEquipo;
                txtAutor.Text = Session[Constantes.SESION_USUARIO].ToString();

                cargarListaMachcode(resultadoConsulta.perfildeCatalogo);
            }
            return false;
        }

        private bool cargarListaMachcode(String perfilCatalogo)
        {
            ConsultaDatos objConsulta = new ConsultaDatos();
            ResultadoMachCode resultadoMachCode;
            resultadoMachCode = objConsulta.ConsultarMatchCode(Session[Constantes.SESION_AMBIENTE].ToString(), perfilCatalogo);

            //validar que no se genero erro
            if (resultadoMachCode.codResultado == 0)
            {
                this.Session.Add(Constantes.SESION_RESULTADOMATCHCODE, resultadoMachCode);

                //CARGAR LISTAS
                Herramienta.CargarDropDownListItemNinguno(ddlRepercusion, resultadoMachCode.listRepercusion, "repercusion", "repercusionTextoConcat");
                Herramienta.CargarDropDownListSeleccione(ddlPrioridadAviso, resultadoMachCode.listPrioridad, "prioridad", "textoPrioridad");
                Herramienta.CargarDropDownListSeleccione(ddlClaseAviso, resultadoMachCode.listClaseAviso, "claseAviso", "claseAvisoTextoConcat");

                return true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopMsgErrorSAP", string.Format("ShowPopupMsgErrorSAP('{0}');", resultadoMachCode.DescrpResultado), true);
                return false;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Session.Remove(Constantes.SESION_RESULTADO_SELECT);
            this.Session.Remove(Constantes.SESION_RESULTADOMATCHCODE);
            this.Session.Remove(Constantes.SESION_ORIGEN_SEL);
            this.Session.Remove(Constantes.SESION_GRUPOPLANIFI_SEL);
            this.Session.Remove(Constantes.SESION_PUESTOTRABAJO_SEL);
            this.Session.Remove(Constantes.SESION_PARTEOBJETO_SEL);
            this.Session.Remove(Constantes.SESION_SINTOMA_SEL);
            this.Session.Remove(Constantes.SESION_CAUSA_SEL);
            Response.Redirect(WebConfigurationManager.AppSettings[Constantes.DIRECCION_FRM_CONSULTA], false);
        }

        #region Busqueda de Origen de Aviso
        protected void ImgBuscarOrigenAviso_Click(object sender, ImageClickEventArgs e)
        {
            CargarTreeViewOrigenAviso();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenModalOrigenAviso();", true);
        }

        private void CargarTreeViewOrigenAviso()
        {
            if (this.Session[Constantes.SESION_RESULTADOMATCHCODE] != null)
            {
                List<Origen> listOrigentreeview;
                ResultadoMachCode resultadoMachCode = (ResultadoMachCode)this.Session[Constantes.SESION_RESULTADOMATCHCODE];
                tvOrigenAviso.Nodes.Clear();

                //si se desea filtrar usar esta variable
                listOrigentreeview = resultadoMachCode.listOrigen;

                TreeNode nodo = null;
                TreeNode nodoHijo = null;

                var listaOrigenGrupo = listOrigentreeview.Select(m => new { m.GrupoCodigo, m.DescripcionGrupoCodigo }).Distinct(); ;

                foreach (var item in listaOrigenGrupo)
                {
                    nodo = new TreeNode();
                    nodo.Value = item.GrupoCodigo;
                    nodo.Text = string.Format("{0} - {1}", item.GrupoCodigo, item.DescripcionGrupoCodigo);
                    //nodo.ImageToolTip = item.DescripcionGrupoCodigo;
                    nodo.SelectAction = TreeNodeSelectAction.SelectExpand;
                    nodo.ImageUrl = WebConfigurationManager.AppSettings[Constantes.RECURSO_RUTA_APLICACION];

                    tvOrigenAviso.Nodes.Add(nodo);

                    foreach (Origen origen in listOrigentreeview)
                    {
                        nodoHijo = new TreeNode();
                        nodoHijo.Value = origen.idResultado.ToString();
                        nodoHijo.Text = string.Format("{0} - {1}", origen.Codigo, origen.TextoCodigo);
                        nodoHijo.ImageToolTip = origen.TextoCodigo;
                        //nodoHijo.Text = origen.TextoCodigo;
                        nodoHijo.SelectAction = TreeNodeSelectAction.SelectExpand;
                        nodoHijo.ImageUrl = WebConfigurationManager.AppSettings[Constantes.RECURSO_RUTA_APLICACION];

                        nodo.ChildNodes.Add(nodoHijo);
                    }
                }

                tvOrigenAviso.ExpandAll();
                tvOrigenAviso.Attributes["onexpand"] = "";
            }
        }

        protected void tvOrigenAviso_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeNode nodoPadre;
            Int32 idResultado;
            nodoPadre = null;

            if (Funciones.IsNumeric(tvOrigenAviso.SelectedNode.Value))
            {
                idResultado = Convert.ToInt32(tvOrigenAviso.SelectedNode.Value);
                //this.txtOrigenDescripcion.Text = tvOrigenAviso.SelectedNode.ImageToolTip.ToString();
                //this.txtOrigenCodigo.Text = tvOrigenAviso.SelectedNode.Value.ToString();
                nodoPadre = tvOrigenAviso.SelectedNode.Parent;

                ResultadoMachCode resultadoMachCode = (ResultadoMachCode)this.Session[Constantes.SESION_RESULTADOMATCHCODE];

                List<Origen> listOrigenFiltro = (List<Origen>)resultadoMachCode.listOrigen;
                Origen OrigenFiltroSel = listOrigenFiltro.First(
                    delegate(Origen bk)
                    {
                        return bk.idResultado.Equals(idResultado);
                    });

                if (OrigenFiltroSel != null)
                {
                    this.txtOrigenDescripcion.Text = OrigenFiltroSel.TextoCodigo;
                    this.txtOrigenCodigo.Text = OrigenFiltroSel.Codigo;

                    this.Session.Add(Constantes.SESION_ORIGEN_SEL, OrigenFiltroSel);
                }
            }

            if (nodoPadre != null)
            {
                this.txtOrigenDescrp.Text = nodoPadre.Value.ToString();
                this.hdiOrigenGrupoCodigo.Value = nodoPadre.Value.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#ModalOrigenAviso').modal('hide');", true);
            }
            else
            {
                this.txtOrigenDescripcion.Text = string.Empty;
                this.txtOrigenCodigo.Text = string.Empty;
                this.txtOrigenDescrp.Text = string.Empty;
                this.hdiOrigenGrupoCodigo.Value = string.Empty;
                lblmsgGenerado.Text = WebConfigurationManager.AppSettings[Constantes.MSGSELECCIONARORIGENAVISO];
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalMensajes();", true);
            }
        }

        protected void imgborrarOrigenAviso_Click(object sender, ImageClickEventArgs e)
        {
            this.txtOrigenDescripcion.Text = string.Empty;
            this.txtOrigenCodigo.Text = string.Empty;
            this.txtOrigenDescrp.Text = string.Empty;
            this.hdiOrigenGrupoCodigo.Value = string.Empty;
            this.Session.Remove(Constantes.SESION_ORIGEN_SEL);
        }
        #endregion Fin Busqueda de Origen de Aviso

        #region Busqueda de Grupo planificacion
        private bool buscarGrupoPlanificacion(string textoBusqueda)
        {
            if (this.Session[Constantes.SESION_RESULTADOMATCHCODE] != null)
            {
                ResultadoMachCode resultadoMachCode = (ResultadoMachCode)this.Session[Constantes.SESION_RESULTADOMATCHCODE];

                List<GrupoPlanificacion> listgrupoPlanificacionFiltro;

                listgrupoPlanificacionFiltro = resultadoMachCode.listGrupoPlanificacion.FindAll(
                            delegate(GrupoPlanificacion bk)
                            {
                                return bk.grupoPlanificacion.ToUpper().Contains(textoBusqueda.ToUpper()) || bk.nombreGrupoPlanificacion.ToUpper().Contains(textoBusqueda.ToUpper());
                            });

                if (listgrupoPlanificacionFiltro.Count == 0)
                {
                    lblmsgGenerado.Text = WebConfigurationManager.AppSettings[Constantes.MSGSINGRUPOENCONTRADO];
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopGrupoPla", "openModalMensajes();", true);

                    txtGrupoPlanif.Text = string.Empty;
                    txtNombreGrupoPlanifi.Text = string.Empty;
                    txtGrpplanifCtroCost.Text = string.Empty;
                    return false;
                }
                else
                {
                    this.Session.Add(Constantes.SESION_GRUPOPLANIFILTRO, listgrupoPlanificacionFiltro);
                    this.gvResultadoGrupoPlanifica.PageIndex = 0;
                    gvResultadoGrupoPlanifica.DataSource = listgrupoPlanificacionFiltro;
                    gvResultadoGrupoPlanifica.DataBind();
                    return true;
                }
            }
            return true;
        }

        protected void imgBusGrupoPlanifi_Click(object sender, ImageClickEventArgs e)
        {
            if (buscarGrupoPlanificacion(txtGrupoPlanificacion.Value))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenModalGrupoPlanifi();", true);
            }
            //limpiar campo luego de buscar
            txtGrupoPlanificacion.Value = string.Empty;
        }

        protected void gvResultadoGrupoPlanifica_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';";
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvResultadoGrupoPlanifica, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = WebConfigurationManager.AppSettings[Constantes.CONSULTASVALBUSQUETOOLTIP];
            }
        }

        protected void gvResultadoGrupoPlanifica_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idResultado = Convert.ToInt32((gvResultadoGrupoPlanifica.SelectedRow.FindControl("hdiidResultadoGrpPlanif") as HiddenField).Value);

            if (idResultado >= 0 || this.Session[Constantes.SESION_GRUPOPLANIFILTRO] != null)
            {
                List<GrupoPlanificacion> listgrupoPlanificacionFiltro = (List<GrupoPlanificacion>)this.Session[Constantes.SESION_GRUPOPLANIFILTRO];
                GrupoPlanificacion PlanificacionFiltroSel = listgrupoPlanificacionFiltro.First(
                    delegate(GrupoPlanificacion bk)
                    {
                        return bk.idResultado.Equals(idResultado);
                    });

                if (PlanificacionFiltroSel != null)
                {
                    txtGrupoPlanif.Text = PlanificacionFiltroSel.grupoPlanificacion;
                    txtNombreGrupoPlanifi.Text = PlanificacionFiltroSel.nombreGrupoPlanificacion;
                    txtGrpplanifCtroCost.Text = PlanificacionFiltroSel.centroPlanifiMante;

                    this.Session.Add(Constantes.SESION_GRUPOPLANIFI_SEL, PlanificacionFiltroSel);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#ModalGrupPlani').modal('hide');", true);
                    return;
                }
            }
        }

        protected void imgBorrarGrupoPlanif_Click(object sender, ImageClickEventArgs e)
        {
            txtGrupoPlanif.Text = string.Empty;
            txtNombreGrupoPlanifi.Text = string.Empty;
            txtGrpplanifCtroCost.Text = string.Empty;
            this.Session.Remove(Constantes.SESION_GRUPOPLANIFI_SEL);
        }
        #endregion Fin Busqueda de Planificacion

        #region Busqueda de Puesto de Trabajo
        protected void GvResultadoBusPuestoTra_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';";
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GvResultadoBusPuestoTra, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = WebConfigurationManager.AppSettings[Constantes.CONSULTASVALBUSQUETOOLTIP];
            }
        }

        protected void GvResultadoBusPuestoTra_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idResultado = Convert.ToInt32((GvResultadoBusPuestoTra.SelectedRow.FindControl("hdiidResultadoPuestoTrabajo") as HiddenField).Value);

            if (idResultado >= 0 || this.Session[Constantes.SESION_PUESTOTRABAJOFILTRO] != null)
            {
                List<PuestoTrabajo> listPuestoTrabajoFiltro = (List<PuestoTrabajo>)this.Session[Constantes.SESION_PUESTOTRABAJOFILTRO];
                PuestoTrabajo PuestoTrabaFiltroSel = listPuestoTrabajoFiltro.First(
                    delegate(PuestoTrabajo bk)
                    {
                        return bk.idResultado.Equals(idResultado);
                    });

                if (PuestoTrabaFiltroSel != null)
                {
                    txtPuestoTrabaCod.Text = PuestoTrabaFiltroSel.puestoTrabajo;
                    txtPuestoTrabajoDescrp.Text = PuestoTrabaFiltroSel.denominacionBreve;
                    txtPuestroTrabaCntrCst.Text = PuestoTrabaFiltroSel.centro;
                    this.Session.Add(Constantes.SESION_PUESTOTRABAJO_SEL, PuestoTrabaFiltroSel);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#ModalPuestoTrabajo').modal('hide');", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSinRegistros();", true);
                    return;
                }
            }
        }

        private bool buscarPuestoTrabajo(string textoBusqueda)
        {
            if (this.Session[Constantes.SESION_RESULTADOMATCHCODE] != null)
            {
                ResultadoMachCode resultadoMachCode = (ResultadoMachCode)this.Session[Constantes.SESION_RESULTADOMATCHCODE];

                List<PuestoTrabajo> listPuestoTrabajoFiltro;

                listPuestoTrabajoFiltro = resultadoMachCode.listPuestoTrabajo.FindAll(
                            delegate(PuestoTrabajo bk)
                            {
                                return bk.puestoTrabajo.ToUpper().Contains(textoBusqueda.ToUpper()) || bk.denominacionBreve.ToUpper().Contains(textoBusqueda.ToUpper());
                            });

                if (listPuestoTrabajoFiltro.Count == 0)
                {
                    lblmsgGenerado.Text = WebConfigurationManager.AppSettings[Constantes.MSGSINPUESTOENCONTRADO];
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopGrupoPla", "openModalMensajes();", true);

                    txtPuestoTrabaCod.Text = string.Empty;
                    txtPuestoTrabajoDescrp.Text = string.Empty;
                    txtPuestroTrabaCntrCst.Text = string.Empty;
                    return false;
                }
                else
                {
                    this.Session.Add(Constantes.SESION_PUESTOTRABAJOFILTRO, listPuestoTrabajoFiltro);
                    this.GvResultadoBusPuestoTra.PageIndex = 0;
                    GvResultadoBusPuestoTra.DataSource = listPuestoTrabajoFiltro;
                    GvResultadoBusPuestoTra.DataBind();
                    return true;
                }
            }
            return true;
        }

        protected void imgBusPuestoTrabajo_Click(object sender, ImageClickEventArgs e)
        {
            if (buscarPuestoTrabajo(txtpuestoTraBuscar.Value))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenModalPuestoTrabajo();", true);
            }
            //limpiar campo luego de buscar
            txtpuestoTraBuscar.Value = string.Empty;
        }

        protected void GvResultadoBusPuestoTra_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (this.Session[Constantes.SESION_PUESTOTRABAJOFILTRO] != null)
            {
                this.GvResultadoBusPuestoTra.PageIndex = e.NewPageIndex;
                List<PuestoTrabajo> listResultadoConsulta = (List<PuestoTrabajo>)this.Session[Constantes.SESION_PUESTOTRABAJOFILTRO];
                this.GvResultadoBusPuestoTra.DataSource = listResultadoConsulta;
                this.GvResultadoBusPuestoTra.DataBind();
            }
        }

        protected void imgBorrarPuestoTrabajo_Click(object sender, ImageClickEventArgs e)
        {
            txtPuestoTrabaCod.Text = string.Empty;
            txtPuestoTrabajoDescrp.Text = string.Empty;
            txtPuestroTrabaCntrCst.Text = string.Empty;
            this.Session.Remove(Constantes.SESION_PUESTOTRABAJO_SEL);
        }
        #endregion Fin Busqueda de Puesto de Trabajo

        #region Busqueda de Parte Objecto
        private bool buscarParteObjeto(string textoBusqueda)
        {
            if (this.Session[Constantes.SESION_RESULTADOMATCHCODE] != null)
            {
                ResultadoMachCode resultadoMachCode = (ResultadoMachCode)this.Session[Constantes.SESION_RESULTADOMATCHCODE];

                List<ParteObjeto> listParteObjetoFiltro;

                listParteObjetoFiltro = resultadoMachCode.listParteObjeto.FindAll(
                            delegate(ParteObjeto bk)
                            {
                                return bk.codigo.ToUpper().Contains(textoBusqueda.ToUpper()) || bk.codigoTexto.ToUpper().Contains(textoBusqueda.ToUpper());
                            });

                if (listParteObjetoFiltro.Count == 0)
                {
                    lblmsgGenerado.Text = WebConfigurationManager.AppSettings[Constantes.MSGPARTEOBJETOENCONTRADO];
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ParteObjectMsg", "openModalMensajes();", true);

                    txtParteObjCod.Text = string.Empty;
                    txtParteObjGrp.Text = string.Empty;
                    txtParteObjDescrp.Text = string.Empty;

                    return false;
                }
                else
                {
                    this.Session.Add(Constantes.SESION_PARTEOBJETOFILTRO, listParteObjetoFiltro);
                    this.GvResultadoBusParteObjecto.PageIndex = 0;
                    GvResultadoBusParteObjecto.DataSource = listParteObjetoFiltro;
                    GvResultadoBusParteObjecto.DataBind();
                    return true;
                }
            }
            return true;
        }
        protected void imgBuscarParteObjecto_Click(object sender, ImageClickEventArgs e)
        {
            if (buscarParteObjeto(txtParteObjectoBuscar.Value))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenModalParteObjeto();", true);
            }
            //limpiar campo luego de buscar
            txtParteObjectoBuscar.Value = string.Empty;
        }

        protected void GvResultadoBusParteObjecto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idResultado = Convert.ToInt32((GvResultadoBusParteObjecto.SelectedRow.FindControl("hdiidResultadParteObjeto") as HiddenField).Value);

            if (idResultado >= 0 || this.Session[Constantes.SESION_PARTEOBJETOFILTRO] != null)
            {
                List<ParteObjeto> listParteObjetoFiltro = (List<ParteObjeto>)this.Session[Constantes.SESION_PARTEOBJETOFILTRO];
                ParteObjeto ParteObjectoFiltroSel = listParteObjetoFiltro.First(
                    delegate(ParteObjeto bk)
                    {
                        return bk.idResultado.Equals(idResultado);
                    });

                if (ParteObjectoFiltroSel != null)
                {
                    txtParteObjCod.Text = ParteObjectoFiltroSel.codigo;
                    txtParteObjGrp.Text = ParteObjectoFiltroSel.grupoCodigo;
                    txtParteObjDescrp.Text = ParteObjectoFiltroSel.codigoTexto;

                    this.Session.Add(Constantes.SESION_PARTEOBJETO_SEL, ParteObjectoFiltroSel);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#ModalParteObjeto').modal('hide');", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSinRegistros();", true);
                    return;
                }
            }
        }

        protected void GvResultadoBusParteObjecto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (this.Session[Constantes.SESION_PARTEOBJETOFILTRO] != null)
            {
                this.GvResultadoBusParteObjecto.PageIndex = e.NewPageIndex;
                List<ParteObjeto> listParteObjetoFiltro = (List<ParteObjeto>)this.Session[Constantes.SESION_PARTEOBJETOFILTRO];
                this.GvResultadoBusParteObjecto.DataSource = listParteObjetoFiltro;
                this.GvResultadoBusParteObjecto.DataBind();
            }
        }
        protected void GvResultadoBusParteObjecto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';";
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GvResultadoBusParteObjecto, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = WebConfigurationManager.AppSettings[Constantes.CONSULTASVALBUSQUETOOLTIP];
            }
        }

        protected void imgBorrarParteObjeto_Click(object sender, ImageClickEventArgs e)
        {
            txtParteObjCod.Text = string.Empty;
            txtParteObjGrp.Text = string.Empty;
            txtParteObjDescrp.Text = string.Empty;
            this.Session.Remove(Constantes.SESION_PARTEOBJETO_SEL);
        }
        #endregion Fin Busqueda de Parte Objecto

        #region Busqueda de Sintoma
        private bool buscarSintoma(string textoBusqueda)
        {
            if (this.Session[Constantes.SESION_RESULTADOMATCHCODE] != null)
            {
                ResultadoMachCode resultadoMachCode = (ResultadoMachCode)this.Session[Constantes.SESION_RESULTADOMATCHCODE];

                List<Sintoma> listSintomaFiltro;

                listSintomaFiltro = resultadoMachCode.listSintoma.FindAll(
                            delegate(Sintoma bk)
                            {
                                return bk.codigo.ToUpper().Contains(textoBusqueda.ToUpper()) || bk.codigoTexto.ToUpper().Contains(textoBusqueda.ToUpper());
                            });

                if (listSintomaFiltro.Count == 0)
                {
                    lblmsgGenerado.Text = WebConfigurationManager.AppSettings[Constantes.MSGSSINTOMAENCONTRADO];
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PoSintomaMsg", "openModalMensajes();", true);

                    txtsintomaCod.Text = string.Empty;
                    txtsintomaCodGrp.Text = string.Empty;
                    txtsintomaDescrp.Text = string.Empty;

                    return false;
                }
                else
                {
                    this.Session.Add(Constantes.SESION_SINTOMAFILTRO, listSintomaFiltro);
                    this.GvResultadoBusSintoma.PageIndex = 0;
                    GvResultadoBusSintoma.DataSource = listSintomaFiltro;
                    GvResultadoBusSintoma.DataBind();
                    return true;
                }
            }
            return true;
        }

        protected void imgBuscarSintoma_Click(object sender, ImageClickEventArgs e)
        {
            if (buscarSintoma(txtsintomaBuscar.Value))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenModalSintoma();", true);
            }
            //limpiar campo luego de buscar
            txtsintomaBuscar.Value = string.Empty;
        }

        protected void GvResultadoBusSintoma_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';";
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GvResultadoBusSintoma, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = WebConfigurationManager.AppSettings[Constantes.CONSULTASVALBUSQUETOOLTIP];
            }
        }

        protected void GvResultadoBusSintoma_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idResultado = Convert.ToInt32((GvResultadoBusSintoma.SelectedRow.FindControl("hdiidResultadSintoma") as HiddenField).Value);

            if (idResultado >= 0 || this.Session[Constantes.SESION_SINTOMAFILTRO] != null)
            {
                List<Sintoma> listSintomaFiltro = (List<Sintoma>)this.Session[Constantes.SESION_SINTOMAFILTRO];
                Sintoma sintomaFiltroSel = listSintomaFiltro.First(
                    delegate(Sintoma bk)
                    {
                        return bk.idResultado.Equals(idResultado);
                    });

                if (sintomaFiltroSel != null)
                {
                    txtsintomaCod.Text = sintomaFiltroSel.codigo;
                    txtsintomaCodGrp.Text = sintomaFiltroSel.grupoCodigo;
                    txtsintomaDescrp.Text = sintomaFiltroSel.codigoTexto;

                    this.Session.Add(Constantes.SESION_SINTOMA_SEL, sintomaFiltroSel);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#ModalSintoma').modal('hide');", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSinRegistros();", true);
                    return;
                }
            }
        }

        protected void GvResultadoBusSintoma_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (this.Session[Constantes.SESION_SINTOMAFILTRO] != null)
            {
                this.GvResultadoBusSintoma.PageIndex = e.NewPageIndex;
                List<Sintoma> listSintomaFiltro = (List<Sintoma>)this.Session[Constantes.SESION_SINTOMAFILTRO];
                this.GvResultadoBusSintoma.DataSource = listSintomaFiltro;
                this.GvResultadoBusSintoma.DataBind();
            }
        }

        protected void imgBorrarSintoma_Click(object sender, ImageClickEventArgs e)
        {
            txtsintomaCod.Text = string.Empty;
            txtsintomaCodGrp.Text = string.Empty;
            txtsintomaDescrp.Text = string.Empty;
            this.Session.Remove(Constantes.SESION_SINTOMA_SEL);
        }
        #endregion Fin Busqueda de Sintoma

        #region Busqueda de Causa
        private bool buscarCausa(string textoBusqueda)
        {
            if (this.Session[Constantes.SESION_RESULTADOMATCHCODE] != null)
            {
                ResultadoMachCode resultadoMachCode = (ResultadoMachCode)this.Session[Constantes.SESION_RESULTADOMATCHCODE];

                List<Causa> listCausaFiltro;

                listCausaFiltro = resultadoMachCode.listCausa.FindAll(
                            delegate(Causa bk)
                            {
                                return bk.codigo.ToUpper().Contains(textoBusqueda.ToUpper()) || bk.codigoTexto.ToUpper().Contains(textoBusqueda.ToUpper());
                            });

                if (listCausaFiltro.Count == 0)
                {
                    lblmsgGenerado.Text = WebConfigurationManager.AppSettings[Constantes.MSGCAUSAENCONTRADO];
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PoSintomaMsg", "openModalMensajes();", true);

                    txtcausaCod.Text = string.Empty;
                    txtcausaCodGrp.Text = string.Empty;
                    txtcausaCodTexto.Text = string.Empty;

                    return false;
                }
                else
                {
                    this.Session.Add(Constantes.SESION_CAUSAFILTRO, listCausaFiltro);
                    this.GvResultadoBusCausa.PageIndex = 0;
                    GvResultadoBusCausa.DataSource = listCausaFiltro;
                    GvResultadoBusCausa.DataBind();
                    return true;
                }
            }
            return true;
        }

        protected void GvResultadoBusCausa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';";
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GvResultadoBusCausa, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = WebConfigurationManager.AppSettings[Constantes.CONSULTASVALBUSQUETOOLTIP];
            }
        }

        protected void GvResultadoBusCausa_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idResultado = Convert.ToInt32((GvResultadoBusCausa.SelectedRow.FindControl("hdiidResultadCausa") as HiddenField).Value);

            if (idResultado >= 0 || this.Session[Constantes.SESION_CAUSAFILTRO] != null)
            {
                List<Causa> listCausaFiltro = (List<Causa>)this.Session[Constantes.SESION_CAUSAFILTRO];
                Causa causaFiltroSel = listCausaFiltro.First(
                    delegate(Causa bk)
                    {
                        return bk.idResultado.Equals(idResultado);
                    });

                if (causaFiltroSel != null)
                {
                    txtcausaCod.Text = causaFiltroSel.codigo;
                    txtcausaCodGrp.Text = causaFiltroSel.grupoCodigo;
                    txtcausaCodTexto.Text = causaFiltroSel.codigoTexto;

                    this.Session.Add(Constantes.SESION_CAUSA_SEL, causaFiltroSel);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#ModalCausa').modal('hide');", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSinRegistros();", true);
                    return;
                }
            }
        }

        protected void GvResultadoBusCausa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (this.Session[Constantes.SESION_CAUSAFILTRO] != null)
            {
                this.GvResultadoBusCausa.PageIndex = e.NewPageIndex;
                List<Causa> listCausaFiltro = (List<Causa>)this.Session[Constantes.SESION_CAUSAFILTRO];
                this.GvResultadoBusCausa.DataSource = listCausaFiltro;
                this.GvResultadoBusCausa.DataBind();
            }
        }

        protected void imgBuscarCausa_Click(object sender, ImageClickEventArgs e)
        {
            if (buscarCausa(txtcausaBuscar.Value))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenModalCausa();", true);
            }
            //limpiar campo luego de buscar
            txtcausaBuscar.Value = string.Empty;
        }

        protected void imgBorrarCausa_Click(object sender, ImageClickEventArgs e)
        {
            txtcausaCod.Text = string.Empty;
            txtcausaCodGrp.Text = string.Empty;
            txtcausaCodTexto.Text = string.Empty;
            this.Session.Remove(Constantes.SESION_CAUSA_SEL);
        }
        #endregion Fin Busqueda de Causa

        protected void btnGrabar_Click(object sender, EventArgs e)
        {

            //System.Threading.Thread.Sleep(9000);
            AvisosPMProcess objAvisos = new AvisosPMProcess();
            listResultadoNuevoAvisoPM listresultadoNuevoAvisoPM;
            AVisoPM avisoPM = new AVisoPM();
            if (this.Session[Constantes.SESION_RESULTADOMATCHCODE] != null)
            {

                ResultadoConsulta resultadoConsulta = (ResultadoConsulta)this.Session[Constantes.SESION_RESULTADO_SELECT];
                Origen origenSel = (Origen)this.Session[Constantes.SESION_ORIGEN_SEL];
                GrupoPlanificacion grupoplanificacionSel = (GrupoPlanificacion)this.Session[Constantes.SESION_GRUPOPLANIFI_SEL];
                PuestoTrabajo puestoTrabajoSel = (PuestoTrabajo)this.Session[Constantes.SESION_PUESTOTRABAJO_SEL];
                ParteObjeto parteobjetoSel = (ParteObjeto)this.Session[Constantes.SESION_PARTEOBJETO_SEL];
                Sintoma sintomaSel = (Sintoma)this.Session[Constantes.SESION_SINTOMA_SEL];
                Causa causaSel = (Causa)this.Session[Constantes.SESION_CAUSA_SEL];

                if (txtTextoLargo.Text != string.Empty)
                {
                    avisoPM.listatextoLargo = Funciones.GetNextChars(txtTextoLargo.Text, Constantes.NUM_REG_FOR_LINEA);
                }

                avisoPM.Origen = origenSel;
                avisoPM.grupoPlanificacion = grupoplanificacionSel;
                avisoPM.puestoTrabajo = puestoTrabajoSel;
                avisoPM.parteObjeto = parteobjetoSel;
                avisoPM.sintoma = sintomaSel;
                avisoPM.causa = causaSel;
                avisoPM.resultadoConsulta = resultadoConsulta;

                avisoPM.nombreAutor = txtAutor.Text;

                avisoPM.prioridad = new Prioridad();
                avisoPM.prioridad.prioridad = ddlPrioridadAviso.SelectedItem.Value.ToString();
                avisoPM.prioridad.textoPrioridad = ddlPrioridadAviso.SelectedItem.Text.ToString();

                avisoPM.textoBreve = txtTextoBreve.Text;
                avisoPM.textoLargo = txtTextoLargo.Text;

                avisoPM.claseAviso = new ClaseAviso();
                avisoPM.claseAviso.claseAviso = ddlClaseAviso.SelectedItem.Value.ToString();
                avisoPM.claseAviso.claseAvisoTexto = ddlClaseAviso.SelectedItem.Text.ToString();
                avisoPM.equipoParado = chkparado.Checked;

                if (ddlRepercusion.SelectedItem.Value.ToString() != "-1")
                {
                    avisoPM.repercusion = new Repercusion();
                    avisoPM.repercusion.repercusion = ddlRepercusion.SelectedItem.Value.ToString();
                    avisoPM.repercusion.textoRepercusion = ddlRepercusion.SelectedItem.Text.ToString();
                }

                avisoPM.textoSintoma = txtTextosintoma.Text;
                avisoPM.textoCausa = txtTextoCausa.Text;

                if (ValidarDatos(avisoPM))
                {
                    listresultadoNuevoAvisoPM = objAvisos.registrarNuevoAviso(Session[Constantes.SESION_AMBIENTE].ToString(), avisoPM);

                    if (listresultadoNuevoAvisoPM.codResultado == 0)
                    {
                        if (listresultadoNuevoAvisoPM.lstresultadoNuevoAvisoPM[0].numberresultado == "000")
                        {
                            //EJECUCION OK
                            this.Session.Remove(Constantes.SESION_RESULTADO_SELECT);
                            this.Session.Remove(Constantes.SESION_RESULTADOMATCHCODE);
                            this.Session.Remove(Constantes.SESION_ORIGEN_SEL);
                            this.Session.Remove(Constantes.SESION_GRUPOPLANIFI_SEL);
                            this.Session.Remove(Constantes.SESION_PUESTOTRABAJO_SEL);
                            this.Session.Remove(Constantes.SESION_PARTEOBJETO_SEL);
                            this.Session.Remove(Constantes.SESION_SINTOMA_SEL);
                            this.Session.Remove(Constantes.SESION_CAUSA_SEL);

                            lblmsgresultadoregok.Text = string.Format("<br/>Numero resultado: {0}<br/>Descripcion: {1}", listresultadoNuevoAvisoPM.lstresultadoNuevoAvisoPM[0].numberresultado, listresultadoNuevoAvisoPM.lstresultadoNuevoAvisoPM[0].message);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Poresultadook", "openModalResultadook();", true);
                        }
                        else
                        { //FALLO REGISTRO
                            lblResultadoRegError.Text = string.Format("<br/>Numero Error: {0} <br/>Descripción Error: {1}", listresultadoNuevoAvisoPM.lstresultadoNuevoAvisoPM[0].numberresultado, listresultadoNuevoAvisoPM.lstresultadoNuevoAvisoPM[0].message);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Poresultadoerror", "openModalResultadoError();", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopMsgErrorSAP", string.Format("ShowPopupMsgErrorSAP('{0}');", listresultadoNuevoAvisoPM.DescrpResultado), true);
                    }
                }
            }
            else
            {
                lblResultadoRegError.Text = "<br/>Numero Error: -1  <br/>Descripción Error: Error en las Sessiones del Servidor";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Poresultadoerror", "openModalResultadoError();", true);

            }
        }

        private bool ValidarDatos(AVisoPM avisoPM)
        {
            bool validacion = true;
            lblAlertValidacion.Text = string.Empty;
            //if (avisoPM.Origen == null)
            //{
            //    lblAlertValidacion.Text += Constantes.LI_OPEN + WebConfigurationManager.AppSettings[Constantes.MSGSORIGENOBLIGATORIO] + Constantes.LI_CLOSE;
            //    validacion = false;
            //}

            //if (avisoPM.grupoPlanificacion == null)
            //{
            //    lblAlertValidacion.Text += Constantes.LI_OPEN + WebConfigurationManager.AppSettings[Constantes.MSGSGRUPOPLANIOBLIGATORIO] + Constantes.LI_CLOSE;
            //    validacion = false;
            //}

            //if (avisoPM.puestoTrabajo == null)
            //{
            //    lblAlertValidacion.Text += Constantes.LI_OPEN + WebConfigurationManager.AppSettings[Constantes.MSGSPUESTOTRABAJOOBLIGATORIO] + Constantes.LI_CLOSE;
            //    validacion = false;
            //}

            if (avisoPM.claseAviso.claseAviso == "-1")
            {
                lblAlertValidacion.Text += Constantes.LI_OPEN + WebConfigurationManager.AppSettings[Constantes.MSGSCLASEAVISONOBLIGATORIO] + Constantes.LI_CLOSE;
                validacion = false;
            }

            if (avisoPM.nombreAutor == string.Empty)
            {
                lblAlertValidacion.Text += Constantes.LI_OPEN + WebConfigurationManager.AppSettings[Constantes.MSGSAUTOROBLIGATORIO] + Constantes.LI_CLOSE;
                validacion = false;
            }

            if (avisoPM.prioridad.prioridad == "-1" && avisoPM.claseAviso.claseAviso!=Constantes.CLASE_AVISO_Z2)
            {
                lblAlertValidacion.Text += Constantes.LI_OPEN + WebConfigurationManager.AppSettings[Constantes.MSGSPRIORIDADOBLIGATORIO] + Constantes.LI_CLOSE;
                validacion = false;
            }


            if (avisoPM.textoBreve == string.Empty)
            {
                lblAlertValidacion.Text += Constantes.LI_OPEN + WebConfigurationManager.AppSettings[Constantes.MSGSTEXTOBREVEBLIGATORIO] + Constantes.LI_CLOSE;
                validacion = false;
            }

           

            //if (avisoPM.repercusion.repercusion == "-1")
            //{
            //    lblAlertValidacion.Text += Constantes.LI_OPEN + WebConfigurationManager.AppSettings[Constantes.MSGSREPERCUSIONOBLIGATORIO] + Constantes.LI_CLOSE;
            //    validacion = false;
            //}

            //if (avisoPM.textoCausa == string.Empty)
            //{
            //    lblAlertValidacion.Text += Constantes.LI_OPEN + WebConfigurationManager.AppSettings[Constantes.MSGSTEXTOCAUSAOBLIGATORIO] + Constantes.LI_CLOSE;
            //    validacion = false;
            //}

            if (!validacion)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "poMSGValidacion", "openModalMensajesValidacion();", true);
            }

            return validacion;
        }

        
    }
}