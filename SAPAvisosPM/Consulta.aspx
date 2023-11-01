<%@ Page Title="" Language="C#" MasterPageFile="~/AvisosPM.Master" AutoEventWireup="true" CodeBehind="Consulta.aspx.cs" Inherits="SAPAvisosPM.Web.Consulta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- FormValidation CSS file -->
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



    <asp:UpdatePanel ID="updConsultar" runat="server">
        <ContentTemplate>

            <div class="container">
                <div class="panel panel-default">
                    <div class="row">
                        <div class="col-sm-12">
                            <label class="control-label">Buscar por Clasificacion/Tag:</label>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <input id="txtClasificacionUbicaTecnica" type="text" runat="server" class="form-control input-sm" placeholder="Ingresar Clasificación de ubicaciones técnicas" />
                                            <span class="input-group-btn">
                                                <asp:ImageButton ID="imgBusClasifiUbica" class="btn btn-default input-sm" runat="server" ImageUrl="~/img/search-3-24.png" UseSubmitBehavior="False" OnClick="imgBuscar_Click" CommandArgument="1" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <input id="txtClasificacionEquipos" type="text" runat="server" class="form-control input-sm" placeholder="Ingresar Clasificación de Equipos" maxlength="30" />
                                            <span class="input-group-btn">
                                                <asp:ImageButton ID="imgBuscarClasifiEquipo" class="btn btn-default input-sm" runat="server" ImageUrl="~/img/search-3-24.png" UseSubmitBehavior="True" OnClick="imgBuscar_Click" CommandArgument="2" />
                                            </span>

                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="row">
                        <div class="col-sm-12">
                            <label class="control-label">Equipo PM:</label>
                            <div class="row">
                                <div class="col-sm-9">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <input id="txtEquipo" type="text" runat="server" class="form-control input-sm" placeholder="Ingresar equipos" maxlength="18" />
                                            <span class="input-group-btn">
                                                <asp:ImageButton ID="ImageButton1" class="btn btn-default input-sm" runat="server" ImageUrl="~/img/search-3-24.png" UseSubmitBehavior="False" OnClick="imgBuscar_Click" CommandArgument="3" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="row">
                        <div class="col-sm-12">
                            <label class="control-label">Ubicación Tecnica:</label>
                            <div class="row">
                                <div class="col-sm-9">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <input id="txtubicafisica" type="text" runat="server" class="form-control input-sm" placeholder="Ingresar Ubicacion Tecnica" />
                                            <span class="input-group-btn">
                                                <asp:ImageButton ID="ImageButton5" class="btn btn-default input-sm" runat="server" ImageUrl="~/img/search-3-24.png" UseSubmitBehavior="False" OnClick="imgBuscar_Click" CommandArgument="4" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="panel panel-default">
                    <div class="row">
                        <div class="col-sm-12">
                            <label class="control-label">Buscar por Filtro de Textos:</label>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <input id="txtfiltroclasequipo" type="text" runat="server" class="form-control input-sm" placeholder="Ingresar Texto de Equipos" />
                                            <span class="input-group-btn">
                                                <asp:ImageButton ID="ImageButton2" class="btn btn-default input-sm" runat="server" ImageUrl="~/img/search-3-24.png" UseSubmitBehavior="False" OnClick="imgBuscar_Click" CommandArgument="5" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <input id="txtfiltroclasubicatecnica" type="text" runat="server" class="form-control input-sm" placeholder="Ingresar Texto de ubicaciones técnicas" />
                                            <span class="input-group-btn">
                                                <asp:ImageButton ID="imgtextoclasubitec" class="btn btn-default input-sm" runat="server" ImageUrl="~/img/search-3-24.png" UseSubmitBehavior="False" CommandArgument="6" OnClick="imgBuscar_Click" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="container">
                <asp:GridView class='table table-bordered table-hover input-sm' HeaderStyle-BackColor="#e7e7e7" ID="gvResultadoConsulta" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" OnPageIndexChanging="gvResultadoConsulta_PageIndexChanging" OnRowDataBound="gvResultadoConsulta_RowDataBound" OnSelectedIndexChanged="gvResultadoConsulta_SelectedIndexChanged">
                    <Columns>

                        <asp:CommandField ShowSelectButton="True" InsertImageUrl="~/img/btn_check.png" ButtonType="Image" SelectImageUrl="~/img/btn_check.png" SelectText="" />
                        <asp:TemplateField HeaderText="Codigo Ubicacion Tecnica">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("UbicaTecnica") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Codigo Equipo">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("NumEquipo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Texto Ubicacion">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("DenominaUbicaTecnica") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Texto Equipo">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("DenominaEquipo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Clasificacion">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("Clasificacion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Grupo Planificacion">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("GrupoPlanificador") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <HeaderStyle BackColor="#E7E7E7" />
                    <PagerStyle CssClass="pagination-ys" />
                </asp:GridView>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>

    <script type="text/javascript">
        function openModalSinRegistros() {
            $('#ModalSinregistros').modal('show');
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

    <!-- Modal -->
    <div class="modal fade" id="ModalSinregistros" role="dialog">
        <div class="modal-dialog modal-sm">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                                <asp:Label ID="lblmsg" runat="server" Text="Label"></asp:Label>
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

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updConsultar">
        <ProgressTemplate>
            <div class="overlay" />
            <div class="overlayContent">

                <img src="img/image_1060480.gif" alt="Loading" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
