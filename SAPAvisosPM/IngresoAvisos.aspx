<%@ Page Title="" Language="C#" MasterPageFile="~/AvisosPM.Master" AutoEventWireup="true" CodeBehind="IngresoAvisos.aspx.cs" Inherits="SAPAvisosPM.Web.IngresoAvisos" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="css/gijgo.min.css" />

    <script type="text/javascript" src="js/gijgo.min.js"></script>

    <style type="text/css">
        .overlay {
            position: fixed;
            z-index: 999;
            top: 0px;
            left: 0px;
            right: 0px;
            bottom: 0px;
            background-color: #aaa;
            filter: alpha(opacity=80);
            opacity: 0.8;
        }

        .overlayContent {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 85px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }

            .overlayContent h2 {
                font-size: 18px;
                font-weight: bold;
                color: #000;
            }

            .overlayContent img {
                width: 64px;
                height: 64px;
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="updIngresoAviso" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="panel panel-default">
                    <div class="row">
                        <div class="col-sm-12">
                            <label class="control-label">Origen de Aviso</label>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtOrigenDescrp" runat="server" class="form-control input-sm" ReadOnly="true" placeholder="Click para buscar Origen"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <asp:ImageButton ID="ImgBuscarOrigenAviso" class="btn btn-default input-sm" runat="server" ImageUrl="~/img/search-3-24.png" UseSubmitBehavior="True" OnClick="ImgBuscarOrigenAviso_Click" data-target="#myModal" CausesValidation="False" />
                                                <asp:ImageButton ID="imgborrarOrigenAviso" class="btn btn-default input-sm" runat="server" ImageUrl="~/img/cancel.png" UseSubmitBehavior="True" OnClick="imgborrarOrigenAviso_Click" CausesValidation="False" />
                                                <script>
                                                    function OpenModalOrigenAviso() {
                                                        $('#ModalOrigenAviso').modal('show');
                                                        $('#ModalOrigenAviso').find('.modal-body').css({
                                                            'max-height': '100%',
                                                            'minHeight': '500'
                                                        });
                                                    }

                                                </script>
                                            </span>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtOrigenCodigo" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtOrigenDescripcion" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                        <asp:HiddenField ID="hdiOrigenGrupoCodigo" runat="server" />
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="row">
                        <div class="col-sm-12">
                            <label class="control-label">Grupo de Planificación</label>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <input id="txtGrupoPlanificacion" type="text" runat="server" class="form-control input-sm" placeholder="Buscar Grupo Planificación" maxlength="30" />
                                            <span class="input-group-btn">
                                                <asp:ImageButton ID="imgBusGrupoPlanifi" class="btn btn-default input-sm" runat="server" ImageUrl="~/img/search-3-24.png" UseSubmitBehavior="True" OnClick="imgBusGrupoPlanifi_Click" />
                                                <asp:ImageButton ID="imgBorrarGrupoPlanif" class="btn btn-default input-sm" runat="server" ImageUrl="~/img/cancel.png" UseSubmitBehavior="True" OnClick="imgBorrarGrupoPlanif_Click" CausesValidation="False" />
                                                <script>
                                                    function OpenModalGrupoPlanifi() {
                                                        $('#ModalGrupPlani').modal('show');
                                                        $('#ModalGrupPlani').find('.modal-body').css({
                                                            'max-height': '100%',
                                                            'minHeight': '500'
                                                        });
                                                    }

                                                </script>
                                            </span>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtGrupoPlanif" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtGrpplanifCtroCost" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtNombreGrupoPlanifi" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="row">
                        <div class="col-sm-12">
                            <label class="control-label">Puesto de Trabajo</label>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <input id="txtpuestoTraBuscar" type="text" runat="server" class="form-control input-sm" placeholder="Buscar Puesto de Trabajo" maxlength="30" />
                                            <span class="input-group-btn">
                                                <asp:ImageButton ID="imgBusPuestoTrabajo" class="btn btn-default input-sm" runat="server" ImageUrl="~/img/search-3-24.png" UseSubmitBehavior="True" OnClick="imgBusPuestoTrabajo_Click" />
                                                <asp:ImageButton ID="imgBorrarPuestoTrabajo" class="btn btn-default input-sm" runat="server" ImageUrl="~/img/cancel.png" UseSubmitBehavior="True" OnClick="imgBorrarPuestoTrabajo_Click" CausesValidation="False" />
                                                <script>
                                                    function OpenModalPuestoTrabajo() {
                                                        $('#ModalPuestoTrabajo').modal('show');
                                                        $('#ModalPuestoTrabajo').find('.modal-body').css({
                                                            'max-height': '100%',
                                                            'minHeight': '500'
                                                        });
                                                    }

                                                </script>
                                            </span>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtPuestoTrabaCod" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtPuestroTrabaCntrCst" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtPuestoTrabajoDescrp" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="row">
                        <div class="col-sm-12">
                            <label class="control-label">Autor</label>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtAutor" runat="server" class="form-control input-sm" MaxLength="12"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-sm-6">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="row">
                        <div class="col-sm-12">
                            <label class="control-label">Prioridad del Aviso</label>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlPrioridadAviso" runat="server" class="form-control input-sm" data-style="btn-primary"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-sm-6">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="row">
                        <div class="col-sm-12">
                            <label class="control-label">Texto Breve</label>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtTextoBreve" runat="server" class="form-control input-sm" placeholder="Ingrese texto Breve" MaxLength="40"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-12">
                            <label class="control-label">Texto Largo</label>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtTextoLargo" runat="server" class="form-control input-sm" placeholder="Ingrese texto Largo" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="row">
                        <div class="col-sm-12">
                            <label class="control-label">Ubicación Técnica</label>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtUbicaTecnicaCodigo" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtUbicaTecnicadescripcion" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="row">
                        <div class="col-sm-12">
                            <label class="control-label">Equipo</label>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtEquipocodigo" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtEquipoDescripcion" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="row">
                        <div class="col-sm-12">
                            <label class="control-label">Clase de Aviso</label>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlClaseAviso" runat="server" class="form-control input-sm" data-style="btn-primary"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-sm-6">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="row">
                        <div class="col-sm-12">
                            <label class="control-label">Equipo Parado</label>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <asp:CheckBox ID="chkparado" runat="server" class="form-control input-sm" />
                                    </div>
                                </div>

                                <div class="col-sm-6">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="row">
                        <div class="col-sm-12">
                            <label class="control-label">Repercusión</label>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlRepercusion" runat="server" class="form-control input-sm" data-style="btn-primary"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-sm-6">
                                    <div class="form-group">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="row">
                        <div class="col-sm-12">
                            <label class="control-label">Parte Objeto</label>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <input id="txtParteObjectoBuscar" type="text" runat="server" class="form-control input-sm" placeholder="Buscar parte Objeto" maxlength="30" />
                                            <span class="input-group-btn">
                                                <asp:ImageButton ID="imgBuscarParteObjecto" class="btn btn-default input-sm" runat="server" ImageUrl="~/img/search-3-24.png" UseSubmitBehavior="True" OnClick="imgBuscarParteObjecto_Click" />
                                                <asp:ImageButton ID="imgBorrarParteObjeto" class="btn btn-default input-sm" runat="server" ImageUrl="~/img/cancel.png" UseSubmitBehavior="True" OnClick="imgBorrarParteObjeto_Click" CausesValidation="False" />
                                                <script>
                                                    function OpenModalParteObjeto() {
                                                        $('#ModalParteObjeto').modal('show');
                                                        $('#ModalParteObjeto').find('.modal-body').css({
                                                            'max-height': '100%',
                                                            'minHeight': '500'
                                                        });
                                                    }

                                                </script>
                                            </span>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtParteObjGrp" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtParteObjCod" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtParteObjDescrp" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="row">
                        <div class="col-sm-12">
                            <label class="control-label">Sintomas</label>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <input id="txtsintomaBuscar" type="text" runat="server" class="form-control input-sm" placeholder="Buscar sintoma" />
                                            <span class="input-group-btn">
                                                <asp:ImageButton ID="imgBuscarSintoma" class="btn btn-default input-sm" runat="server" ImageUrl="~/img/search-3-24.png" UseSubmitBehavior="True" OnClick="imgBuscarSintoma_Click" />
                                                <asp:ImageButton ID="imgBorrarSintoma" class="btn btn-default input-sm" runat="server" ImageUrl="~/img/cancel.png" UseSubmitBehavior="True" OnClick="imgBorrarSintoma_Click" CausesValidation="False" />
                                                <script>
                                                    function OpenModalSintoma() {
                                                        $('#ModalSintoma').modal('show');
                                                        $('#ModalSintoma').find('.modal-body').css({
                                                            'max-height': '100%',
                                                            'minHeight': '500'
                                                        });
                                                    }

                                                </script>
                                            </span>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtsintomaCodGrp" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtsintomaCod" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtsintomaDescrp" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-12">
                            <label class="control-label">Texto del Sintoma</label>
                            <div class="row">
                                <div class="col-sm-9">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtTextosintoma" runat="server" class="form-control input-sm" placeholder="Ingrese texto del sintoma." MaxLength="40"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="row">
                        <div class="col-sm-12">
                            <label class="control-label">Causa</label>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <input id="txtcausaBuscar" type="text" runat="server" class="form-control input-sm" placeholder="Buscar causa" maxlength="30" />
                                            <span class="input-group-btn">
                                                <asp:ImageButton ID="imgBuscarCausa" class="btn btn-default input-sm" runat="server" ImageUrl="~/img/search-3-24.png" UseSubmitBehavior="True" OnClick="imgBuscarCausa_Click" />
                                                <asp:ImageButton ID="imgBorrarCausa" class="btn btn-default input-sm" runat="server" ImageUrl="~/img/cancel.png" UseSubmitBehavior="True" OnClick="imgBorrarCausa_Click" CausesValidation="False" />
                                                <script>
                                                    function OpenModalCausa() {
                                                        $('#ModalCausa').modal('show');
                                                        $('#ModalCausa').find('.modal-body').css({
                                                            'max-height': '100%',
                                                            'minHeight': '500'
                                                        });
                                                    }

                                                </script>
                                            </span>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtcausaCodGrp" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtcausaCod" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtcausaCodTexto" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-12">
                            <label class="control-label">Texto de la Causa</label>
                            <div class="row">
                                <div class="col-sm-9">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtTextoCausa" runat="server" class="form-control input-sm" placeholder="Ingrese texto de la causa." MaxLength="40"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="row">
                        <div class="col-sm-12">
                            <p></p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <div class="text-center">
                                    <asp:Button ID="btnGrabar" runat="server" Text="GRABAR" class="btn btn-primary" OnClick="btnGrabar_Click" OnClientClick="return confirm('¿Está seguro de Procesar el Aviso?');" ValidationGroup="Validacion" />
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-6">
                            <div class="form-group">
                                <div class="text-center">
                                    <asp:Button ID="btnCancelar" runat="server" Text="CANCELAR" class="btn btn-primary" OnClick="btnCancelar_Click" OnClientClick="return confirm('¿Está seguro de cancelar el ingreso del Aviso?');" />
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </ContentTemplate>

    </asp:UpdatePanel>

    <!-- Modal ORIGEN AVISO -->
    <div class="modal fade" id="ModalOrigenAviso" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <%--<img src="img/Logo_nuevo2.png" />--%>
                            <h4 class="modal-title" id="myModalLabel">Seleccionar Origen de Aviso</h4>
                        </div>
                        <div class="modal-body">
                            <asp:TreeView ID="tvOrigenAviso" runat="server" ImageSet="XPFileExplorer" class="table table-bordered" OnSelectedNodeChanged="tvOrigenAviso_SelectedNodeChanged">
                                <NodeStyle />
                            </asp:TreeView>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <!-- Modal GRUPO PLANIFICACION -->
    <div class="modal fade" id="ModalGrupPlani" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <%--<img src="img/Logo_nuevo2.png" />--%>
                            <h4 class="modal-title" id="LblGrupoTitle">Seleccionar Grupo de Planificación</h4>
                        </div>
                        <div class="modal-body">
                            <asp:GridView class='table table-bordered table-hover input-sm' HeaderStyle-BackColor="#e7e7e7" ID="gvResultadoGrupoPlanifica" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" OnRowDataBound="gvResultadoGrupoPlanifica_RowDataBound" OnSelectedIndexChanged="gvResultadoGrupoPlanifica_SelectedIndexChanged">
                                <Columns>

                                    <asp:CommandField ShowSelectButton="True" InsertImageUrl="~/img/btn_check.png" ButtonType="Image" SelectImageUrl="~/img/btn_check.png" SelectText="" />
                                    <asp:TemplateField HeaderText="Centro Planificación">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("centroPlanifiMante") %>'></asp:Label>
                                            <asp:HiddenField ID="hdiidResultadoGrpPlanif" runat="server" Value='<%# Eval("idResultado") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Grupo Planificación">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("grupoPlanificacion") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nombre Grupo Planificación">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("nombreGrupoPlanificacion") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <HeaderStyle BackColor="#E7E7E7" />
                                <PagerStyle CssClass="pagination-ys" />
                            </asp:GridView>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <!-- Modal PUESTO TRABAJO -->
    <div class="modal fade" id="ModalPuestoTrabajo" tabindex="-1" role="dialog" aria-labelledby="myModalLabelPuesto">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <%--<img src="img/Logo_nuevo2.png" />--%>
                            <h4 class="modal-title" id="LblPuestoTrabajoTitle">Seleccionar Puesto de Trabajo</h4>
                        </div>
                        <div class="modal-body">
                            <asp:GridView class='table table-bordered table-hover input-sm' HeaderStyle-BackColor="#e7e7e7" ID="GvResultadoBusPuestoTra" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnRowDataBound="GvResultadoBusPuestoTra_RowDataBound" OnSelectedIndexChanged="GvResultadoBusPuestoTra_SelectedIndexChanged" OnPageIndexChanging="GvResultadoBusPuestoTra_PageIndexChanging">
                                <Columns>

                                    <asp:CommandField ShowSelectButton="True" InsertImageUrl="~/img/btn_check.png" ButtonType="Image" SelectImageUrl="~/img/btn_check.png" SelectText="" />
                                    <asp:TemplateField HeaderText="Clase">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("clasePuestoTrabajo") %>'></asp:Label>
                                            <asp:HiddenField ID="hdiidResultadoPuestoTrabajo" runat="server" Value='<%# Eval("idResultado") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Centro">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("centro") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Puesto de Trabajo">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("puestoTrabajo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Denominación Breve">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("denominacionBreve") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <HeaderStyle BackColor="#E7E7E7" />
                                <PagerStyle CssClass="pagination-ys" />
                            </asp:GridView>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <!-- Modal PARTE OBJETO -->
    <div class="modal fade" id="ModalParteObjeto" tabindex="-1" role="dialog" aria-labelledby="myModalLabelPuesto">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <%--<img src="img/Logo_nuevo2.png" />--%>
                            <h4 class="modal-title" id="LblParteObjetoTitle">Seleccionar Parte Objecto</h4>
                        </div>
                        <div class="modal-body">
                            <asp:GridView class='table table-bordered table-hover input-sm' HeaderStyle-BackColor="#e7e7e7" ID="GvResultadoBusParteObjecto" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnRowDataBound="GvResultadoBusParteObjecto_RowDataBound" OnSelectedIndexChanged="GvResultadoBusParteObjecto_SelectedIndexChanged" OnPageIndexChanging="GvResultadoBusParteObjecto_PageIndexChanging">
                                <Columns>

                                    <asp:CommandField ShowSelectButton="True" InsertImageUrl="~/img/btn_check.png" ButtonType="Image" SelectImageUrl="~/img/btn_check.png" SelectText="" />
                                    <asp:TemplateField HeaderText="Grupo Codigo">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("grupoCodigo") %>'></asp:Label>
                                            <asp:HiddenField ID="hdiidResultadParteObjeto" runat="server" Value='<%# Eval("idResultado") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Grupo Descripción">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("grupoCodigoDescripcion") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Codigo">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("codigo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Codigo texto">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("codigoTexto") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <HeaderStyle BackColor="#E7E7E7" />
                                <PagerStyle CssClass="pagination-ys" />
                            </asp:GridView>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <!-- Modal SINTOMA -->
    <div class="modal fade" id="ModalSintoma" tabindex="-1" role="dialog" aria-labelledby="myModalLabelPuesto">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="Updsintoma" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <%--<img src="img/Logo_nuevo2.png" />--%>
                            <h4 class="modal-title" id="LblSintomaTitle">Seleccionar Sintoma</h4>
                        </div>
                        <div class="modal-body">
                            <asp:GridView class='table table-bordered table-hover input-sm' HeaderStyle-BackColor="#e7e7e7" ID="GvResultadoBusSintoma" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnRowDataBound="GvResultadoBusSintoma_RowDataBound" OnSelectedIndexChanged="GvResultadoBusSintoma_SelectedIndexChanged" OnPageIndexChanging="GvResultadoBusSintoma_PageIndexChanging">
                                <Columns>

                                    <asp:CommandField ShowSelectButton="True" InsertImageUrl="~/img/btn_check.png" ButtonType="Image" SelectImageUrl="~/img/btn_check.png" SelectText="" />
                                    <asp:TemplateField HeaderText="Grupo Codigo">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("grupoCodigo") %>'></asp:Label>
                                            <asp:HiddenField ID="hdiidResultadSintoma" runat="server" Value='<%# Eval("idResultado") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Grupo Descripción">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("grupoCodigoDescripcion") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Codigo">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("codigo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Codigo texto">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("codigoTexto") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <HeaderStyle BackColor="#E7E7E7" />
                                <PagerStyle CssClass="pagination-ys" />
                            </asp:GridView>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <!-- Modal CAUSA -->
    <div class="modal fade" id="ModalCausa" tabindex="-1" role="dialog" aria-labelledby="myModalLabelPuesto">
        <div class="modal-dialog modal-lg" role="document">
            <asp:UpdatePanel ID="updModalCausa" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <%--<img src="img/Logo_nuevo2.png" />--%>
                            <h4 class="modal-title" id="LblCausaTitle">Seleccionar Causa</h4>
                        </div>
                        <div class="modal-body">
                            <asp:GridView class='table table-bordered table-hover input-sm' HeaderStyle-BackColor="#e7e7e7" ID="GvResultadoBusCausa" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnRowDataBound="GvResultadoBusCausa_RowDataBound" OnSelectedIndexChanged="GvResultadoBusCausa_SelectedIndexChanged" OnPageIndexChanging="GvResultadoBusCausa_PageIndexChanging">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" InsertImageUrl="~/img/btn_check.png" ButtonType="Image" SelectImageUrl="~/img/btn_check.png" SelectText="" />
                                    <asp:TemplateField HeaderText="Grupo Codigo">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("grupoCodigo") %>'></asp:Label>
                                            <asp:HiddenField ID="hdiidResultadCausa" runat="server" Value='<%# Eval("idResultado") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Grupo Descripción">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("grupoCodigoDescripcion") %>'></asp:Label>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Codigo">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("codigo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Codigo texto">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("codigoTexto") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <HeaderStyle BackColor="#E7E7E7" />
                                <PagerStyle CssClass="pagination-ys" />
                            </asp:GridView>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>


    <script type="text/javascript">
        function openModalMensajes() {
            $('#ModalMensajes').modal('show');
        }
    </script>

    <!-- Modal mensajes -->
    <div class="modal fade" id="ModalMensajes" role="dialog">
        <div class="modal-dialog modal-lg">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title"></h4>
                            <img src="img/Logo_nuevo2.png" />
                        </div>
                        <div class="modal-body">
                            <p>
                                <asp:Label ID="lblmsgGenerado" runat="server" Text="Label"></asp:Label>
                            </p>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <script type="text/javascript">
        function openModalResultadook() {
            $('#ModalResultadoRegok').modal('show');
            $("#ModalResultadoRegok").on("hidden.bs.modal", function () {
                window.location = "Consulta.aspx";
            });
        }
    </script>

    <!-- Modal mensajes resultado error-->
    <div class="modal fade" id="ModalResultadoRegok" role="dialog">
        <div class="modal-dialog modal-lg">
            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                <ContentTemplate>
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title"></h4>
                            <img src="img/Logo_nuevo2.png" />
                        </div>
                        <div class="modal-body">
                            <div class="alert alert-success">
                                <strong>Aviso generado correctamente</strong>
                                <asp:Label ID="lblmsgresultadoregok" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>


    <script type="text/javascript">
        function openModalResultadoError() {
            $('#ModalResultadoRegError').modal('show');
        }

        // register jQuery extension
        jQuery.extend(jQuery.expr[':'], {
            focusable: function (el, index, selector) {
                return $(el).is('a, button, :input, [tabindex]');
            }
        });

        $(document).on('keypress', 'input,select', function (e) {
            if (e.which == 13) {
                e.preventDefault();
                // Get all focusable elements on the page
                var $canfocus = $(':focusable');
                var index = $canfocus.index(this) + 1;
                if (index >= $canfocus.length) index = 0;
                $canfocus.eq(index).focus();
                $canfocus.eq(index).trigger('click');
            }
        });

    </script>

    <!-- Modal mensajes resultado error-->
    <div class="modal fade" id="ModalResultadoRegError" role="dialog">
        <div class="modal-dialog modal-lg">
            <asp:UpdatePanel ID="updresultadoregError" runat="server">
                <ContentTemplate>
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title"></h4>
                            <img src="img/Logo_nuevo2.png" />
                        </div>
                        <div class="modal-body">
                            <div class="alert alert-warning">
                                <strong>Error en la creación del aviso PM</strong>
                                <asp:Label ID="lblResultadoRegError" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <script type="text/javascript">
        function openModalMensajesValidacion() {
            $('#ModalMensajesValidacion').modal('show');
        }
    </script>

    <!-- Modal mensajes Validacion -->
    <div class="modal fade" id="ModalMensajesValidacion" role="dialog">
        <div class="modal-dialog modal-lg">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title"></h4>
                            <img src="img/Logo_nuevo2.png" />
                        </div>
                        <div class="modal-body">
                            <div class="alert alert-danger" role="alert">
                                <asp:Label ID="lblAlertValidacion" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>


    <script type="text/javascript">
        function ShowPopupMsgErrorSAP(x) {
            $(".modal-body #filter").text(x);
            $('#ModalMensajeErrorSAP').modal('show');
        }
    </script>

    <div class="modal fade" id="ModalMensajeErrorSAP" role="dialog">
        <div class="modal-dialog modal-lg">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"></h4>
                    <img src="img/Logo_nuevo2.png" />
                </div>
                <div class="modal-body">
                    <label for="Logmsg">Mensaje de Servidor SAP:</label>
                    <br />
                    <h4><span class="label label-primary" id="filter"></span></h4>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                </div>
            </div>

        </div>
    </div>

    <asp:UpdateProgress ID="udpIngresoAviso" runat="server" AssociatedUpdatePanelID="updIngresoAviso">
        <ProgressTemplate>
            <div class="overlay" />
            <div class="overlayContent">
                <img src="img/image_1060480.gif" alt="Loading" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
