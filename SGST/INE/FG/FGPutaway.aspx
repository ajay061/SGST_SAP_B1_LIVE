<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="FGPutaway.aspx.cs"
    EnableEventValidation="false" Inherits="SGST.INE.Operation.Putaway" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
        <div id="msgdiv" runat="server">
            <div id='msgerror' runat='server' style="display: none;" class="alert alert-danger alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4>
                    <i class="icon fa fa-ban"></i>Alert!</h4>
            </div>
            <div id='msgwarning' runat='server' style="display: none;" class="alert alert-info alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4>
                    <i class="icon fa fa-info"></i>Alert!</h4>
            </div>
            <div id='msginfo' runat='server' style="display: none;" class="alert alert-warning alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4>
                    <i class="icon fa fa-warning"></i>Alert!</h4>
            </div>
            <div id='msgsuccess' runat='server' style="display: none;" class="alert alert-success alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4>
                    <i class="icon fa fa-check"></i>Alert!</h4>
            </div>
        </div>
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>FG Putaway</h1>
        </section>
        <!-- Main content -->
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Scan Barcode</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Select scan type : </label>
                                <asp:DropDownList ID="drpType" runat="server" class="form-control select2" autocomplete="off" EnableViewState="true">
                                    <asp:ListItem>Primary Box</asp:ListItem>
                                    <asp:ListItem>Pallet</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Scan Box Barcode : </label>
                                <asp:TextBox ID="txtBarcode" runat="server" class="form-control" placeholder="Scan Box Barcode" AutoPostBack="true" autocomplete="off" OnTextChanged="txtBarcode_TextChanged" EnableViewState="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Scan Location : </label>
                                <asp:TextBox ID="txtLocation" runat="server" class="form-control" AutoPostBack="true" placeholder="Scan Location barcode" autocomplete="off" OnTextChanged="txtLocation_TextChanged" EnableViewState="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2 col-xs-4">
                            <asp:Button ID="btnReset" runat="server" class="btn btn-primary btn-block btn-flat" Text="RESET" OnClick="btnReset_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
