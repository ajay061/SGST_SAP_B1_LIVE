<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PhysicalStock.aspx.cs" Inherits="SGST.INE.Operation.PhysicalStock" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function IsValid(args) {
            if (args.value.length == 0) {
                return false;
            }
            else {
                return true;
            }
        }
    </script>
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
            <h1>Physical Item Stock     
            </h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Stock Area</label>
                                <asp:DropDownList ID="drpStockArea" runat="server" class="form-control" Style="width: 100%;" AutoPostBack="true" OnSelectedIndexChanged="drpStockArea_SelectedIndexChanged">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>RM</asp:ListItem>
                                    <asp:ListItem>WIP</asp:ListItem>
                                    <asp:ListItem>FG</asp:ListItem>
                                </asp:DropDownList>
                                <div class="form-group">
                                    <label>Scan Item Barcode</label>
                                    <asp:TextBox ID="txtScanBarcode" runat="server" class="form-control" placeholder="Scan Barcode" AutoPostBack="true" autocomplete="off" OnTextChanged="txtScanBarcode_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Quantity</label>
                                <asp:TextBox ID="txtQuantity" runat="server" class="form-control" placeholder="Quantity" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)" AutoPostBack="false" autocomplete="off" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                        <!-- /.col -->
                        <div class="col-md-4">
                            <div>
                                <label>
                                    <asp:CheckBox ID="chkManual" runat="server" OnCheckedChanged="chkManual_CheckedChanged" AutoPostBack="true" />
                                    Manual Add
                                </label>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-4">
                                    <input id="btnsubmit" type="button" name="Update " value="Submit" runat="server" visible="false" class="btn btn-primary btn-block btn-flat" onserverclick="btnInsert" />
                                </div>
                                <div class="col-xs-4">
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary btn-block btn-flat" OnClick="btnReset_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Counter : </label>
                                <asp:TextBox ID="txtCounter" runat="server" Text="0" class="form-control" autocomplete="off" ReadOnly="True"></asp:TextBox>
                            </div>                           
                        </div>
                    </div>
                </div>
                <!-- /.box-body -->
                <asp:HiddenField ID="hidUID" runat="server" />
                <asp:HiddenField ID="hidUpdate" runat="server" />
            </div>
        </section>
    </div>
</asp:Content>
