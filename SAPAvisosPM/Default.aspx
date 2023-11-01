<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SAPAvisosPM.Web.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Creación Avisos PM</title>

    <link rel="stylesheet" type="text/css" media="screen" href="css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="css/bootstrap-datetimepicker.css" />

    <script type="text/javascript" src="js/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/moment-with-locales.js"></script>
    <script type="text/javascript" src="js/bootstrap-datetimepicker.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <br />
        <br />
        <div>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-sm-3"></div>
                    <div class="col-sm-6">
                        <div class="panel panel-default" style="background-color: #f5f5f5">
                            <img id="logo-main2" src="img/Logo_nuevo2.png" width="200" alt="Logo Thing main logo" style="display: block; margin: 20px auto;" />

                            <!--    <h3><span runat="server"  id="ambconnX" class =" label"  style="font-weight:normal;display: block;margin: 20px auto;background-color: #e7e7e7;color:#777">Notificación de Órdenes de Mantenimiento</span></h3>
                            -->
                        </div>
                    </div>
                    <div class="col-sm-3"></div>
                </div>
                <div class="row">
                    <div class="col-sm-3"></div>
                    <div class="col-sm-6">
                        <div class="panel panel-default" style="background-color: #f5f5f5">
                            <br />
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                                <ContentTemplate>

                                    <div class="row">

                                        <div class="col-sm-3"></div>
                                        <div class="col-sm-6">

                                            <img id="logoADO" src="img/x2.jpg"
                                                style="display: block; margin: 20px auto;" />


                                            <img id="logo-sap" src="img/lo-sap2.png" width="100" alt="Logo Thing main logo"
                                                style="display: block; margin: 20px auto;" />
                                            <br />
                                        </div>
                                        <div class="col-sm-2"></div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-3"></div>
                                        <div class="col-sm-4">


                                            <label for="Ambiente1">Ambiente</label>
                                            <asp:DropDownList ID="lstGrupo" runat="server" class="form-control"
                                                OnSelectedIndexChanged="lstGrupo_SelectedIndexChanged1" AutoPostBack="true">
                                                <asp:ListItem Value="-- Seleccione --"></asp:ListItem>
                                                <asp:ListItem Value="DEV">DEV</asp:ListItem>
                                                <asp:ListItem Value="QAS">QAS</asp:ListItem>
                                                <asp:ListItem Value="PRD">PRD</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-sm-2">

                                            <label for="Mandante1">Mandante</label>

                                            <input runat="server" id="mandan" readonly="readonly" class="form-control" />
                                            <div class="col-sm-3"></div>


                                        </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="lstGrupo" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>


                        </div>
                        <br />

                        <div class="row">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-6">
                                <label for="Usuario">Usuario AssetCare/SAP</label>
                                <input runat="server" id="Usuario" type="text" class="form-control" />
                            </div>
                            <div class="col-sm-3"></div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-sm-3"></div>
                            <div class="col-sm-6">
                                <label for="Password">Password AssetCare/SAP</label>
                                <input runat="server" id="Password" type="password" class="form-control" />
                            </div>
                            <div class="col-sm-3"></div>
                        </div>
                        <br />

                        <div class="row">
                            <div class="col-sm-5"></div>
                            <div class="col-sm-2">
                                <asp:Button ID="btnLogin" runat="server" Text="Login" type="submit" class="btn btn-default" OnClick="LoginBTN_Click" />
                            </div>
                            <div class="col-sm-5"></div>
                        </div>
                        <br />
                    </div>
                    <div class="col-sm-3"></div>
                </div>
            </div>
        </div>
        <!--I caducado-->

        <script type="text/javascript">
            function openModalCaducado() {
                $('#myModalCaducado').modal('show');
            }
        </script>
        <!-- Modal -->
        <div class="modal fade" id="myModalCaducado" role="dialog">
            <div class="modal-dialog modal-sm">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title"></h4>
                        <img src="img/Logo_nuevo2.png" />
                    </div>
                    <div class="modal-body">
                        <p>El usuario ha expirado.</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>

            </div>
        </div>

        <!--F caducado-->
        <script type="text/javascript">
            function openModal() {
                $('#myModal').modal('show');
            }
        </script>
        <!-- Modal -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog modal-sm">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title"></h4>
                        <img src="img/Logo_nuevo2.png" />
                    </div>
                    <div class="modal-body">
                        <p>Por favor verifique su usuario o password</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>

        <script type="text/javascript">
            function ShowPopupLogin(x) {
                $(".modal-body #filter").text(x);
                $('#ModalLogin').modal('show');
            }
        </script>

        <div class="modal fade" id="ModalLogin" role="dialog">
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



    </form>
</body>
</html>
