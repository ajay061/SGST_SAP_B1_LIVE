<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="DryProcess.aspx.cs" Inherits="SGST.INE.WIP.DryProcess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function PreventPostback(sender, eventArgs) {
            if (eventArgs.get_newValue() == "")
                eventArgs.set_cancel(true);
        }
    </script>
    <script>
        // WRITE THE VALIDATION SCRIPT.
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
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
        <section class="content-header">
            <h1>Dry In/Out Process For Expired Items</h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Dry In/Out Process</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Process Inventory : </label>
                                <asp:DropDownList ID="drpModule" runat="server" class="form-control select2"
                                    Style="width: 100%;">
                                    <asp:ListItem>RM</asp:ListItem>
                                    <asp:ListItem>WIP</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Select Process : </label>
                                <asp:DropDownList ID="drpProcess" runat="server" autocomplete="off"
                                    class="form-control select2" AutoPostBack="true"
                                    Style="width: 100%;" OnSelectedIndexChanged="drpProcess_SelectedIndexChanged">
                                    <asp:ListItem>IN</asp:ListItem>
                                    <asp:ListItem>OUT</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group" id="dvOutProcess" runat="server">
                                <label>Extended Expiry (In Months) : </label>
                                <asp:TextBox ID="txtDays" runat="server" class="form-control"
                                    Style="width: 100%;" MaxLength="8" Text ="1" 
                                    autocomplete="off"
                                    onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                            </div>
                            <div class="form-group" id="dvPrintergrup" runat="server" visible="false">
                                <label>Select Printer : </label>
                                <asp:DropDownList ID="drpPrinterName" runat="server" class="form-control select2"
                                    Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Scan PartBarcode : </label>
                                <asp:TextBox ID="txtReelBarcode" runat="server" class="form-control" Style="width: 100%;" placeholder="Enter Part Barcode"
                                    OnTextChanged="txtReelBarcode_TextChanged" autocomplete="off" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title"></h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-xs-4">
                                <asp:Button ID="btnReset" runat="server" UseSubmitBehavior="false"
                                    Text="Reset" class="btn btn-primary btn-block btn-flat" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
