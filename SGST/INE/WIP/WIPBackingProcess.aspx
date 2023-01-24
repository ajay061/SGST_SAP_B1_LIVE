<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="WIPBackingProcess.aspx.cs" Inherits="SGST.INE.WIP.WIPBackingProcess" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function PreventPostback(sender, eventArgs) {
            if (eventArgs.get_newValue() == "")
                eventArgs.set_cancel(true);
        }
    </script>
    <div class="content-wrapper">
        <div id="msgdiv" runat="server">
            <div id='msgerror' runat='server' style="display: none;" class="alert alert-danger alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4>
                    <i class="icon fa fa-ban"></i></h4>
            </div>
            <div id='msgwarning' runat='server' style="display: none;" class="alert alert-info alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4>
                    <i class="icon fa fa-info"></i></h4>
            </div>
            <div id='msginfo' runat='server' style="display: none;" class="alert alert-warning alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4>
                    <i class="icon fa fa-warning"></i></h4>
            </div>
            <div id='msgsuccess' runat='server' style="display: none;" class="alert alert-success alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4>
                    <i class="icon fa fa-check"></i></h4>
            </div>
        </div>
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>PCB BACKING PROCESS</h1>
        </section>
        <!-- Main content -->
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Baking Process</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Select FG Item Code :</label>
                                <asp:DropDownList ID="drpFGItemCode" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="drpFGItemCode_SelectedIndexChanged" Style="width: 100%;" class="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Select Work Order No. :</label>
                                <asp:DropDownList ID="drpWorkOrderNo" runat="server" 
                                    Style="width: 100%;" class="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Select Baking Process : </label>
                                <asp:DropDownList ID="drpProcessID" runat="server" class="form-control Select2"
                                    AutoPostBack="true" OnSelectedIndexChanged="drpProcessID_SelectedIndexChanged"
                                    >
                                    <asp:ListItem>In</asp:ListItem>
                                    <asp:ListItem>Out</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group" id="divIN" runat="server">
                                <label>Scan Barcode : </label>
                                <asp:TextBox ID="txtBarcode" runat="server" class="form-control"
                                    AutoPostBack="true" OnTextChanged="txtBarcode_TextChanged" AutoComplete="Off">                                    
                                </asp:TextBox>
                            </div>
                            <div class="form-group" id="divOut" runat="server" visible ="false">
                                <label>Select PCB PKT Barcode :</label>
                                <asp:DropDownList ID="drpPendingBarcode" runat="server" Style="width: 100%;" 
                                    class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged ="drpPendingBarcode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <div class="col-md-6">
                                    <div class="col-xs-4">
                                        <asp:Button ID="btnReset" runat="server" UseSubmitBehavior="false" Text="Reset"
                                            class="btn btn-primary btn-block btn-flat" OnClick="btnReset_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
