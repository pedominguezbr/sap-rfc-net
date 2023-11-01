<%@ Page Title="" Language="C#" MasterPageFile="~/AvisosPM.Master" AutoEventWireup="true" CodeBehind="frmIngresodiseno.aspx.cs" Inherits="SAPAvisosPM.Web.frmIngresodiseño" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="panel panel-default">
            <div class="row">
                <div class="col-sm-9">
                    <label class="control-label">Origen del aviso</label>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <div class="input-group">
                                    <input id="txtClasificacionEquipos" type="text" runat="server" class="form-control" placeholder="Ingresar Clasificación de Equipos" maxlength="30" />
                                    <span class="input-group-btn">
                                        <asp:ImageButton ID="imgBuscarClasifiEquipo" class="btn btn-default" runat="server" ImageUrl="~/img/search-3-24.png" UseSubmitBehavior="True"  CommandArgument="1" />

                                    </span>

                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <div class="input-group">
                                    <input id="txtClasificacionUbicaTecnica" type="text" runat="server" class="form-control" placeholder="Ingresar Clasificación de ubicaciones técnicas" />
                                    <span class="input-group-btn">
                                        <asp:ImageButton ID="imgBusClasifiUbica" class="btn btn-default" runat="server" ImageUrl="~/img/search-3-24.png" UseSubmitBehavior="False"  CommandArgument="2" />
                                    </span>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <fieldset>
    <legend>Origen del Aviso</legend>
    <div class='row'>
        <div class='col-sm-4'>    
            <div class='form-group'>
                <label for="user_title">Title</label>
                <input class="form-control" id="user_title" name="user[title]" size="30" type="text" />
            </div>
        </div>
        <div class='col-sm-4'>
            <div class='form-group'>
                <label for="user_firstname">First name</label>
                <input class="form-control" id="user_firstname" name="user[firstname]" required="true" size="30" type="text" />
            </div>
        </div>
        <div class='col-sm-4'>
            <div class='form-group'>
                <label for="user_lastname">Last name</label>
                <input class="form-control" id="user_lastname" name="user[lastname]" required="true" size="30" type="text" />
            </div>
        </div>
    </div>
    <div class='row'>
        <div class='col-sm-12'>
            <div class='form-group'>

                <label for="user_email">Email</label>
                <input class="form-control required email" id="user_email" name="user[email]" required="true" size="30" type="text" />
            </div>
        </div>
    </div>
</fieldset>
        </div>
    </div>
</asp:Content>
