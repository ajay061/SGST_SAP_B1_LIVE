<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="WIPSecondaryBoxPrinting.aspx.cs" Inherits="SGST.INE.WIP.WIPSecondaryBoxPrinting" %>

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
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode == 46 && evt.srcElement.value.split('.').length > 1) {
                return false;
            }
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to complete box?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to complete box?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <div class="content-wrapper">
        <div id="msgdiv" runat="server">
            <div id='msgerror' runat='server' style="display: none;" class="alert alert-danger alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4><i class="icon fa fa-ban"></i></h4>
            </div>
            <div id='msgwarning' runat='server' style="display: none;" class="alert alert-info alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4><i class="icon fa fa-info"></i></h4>
            </div>
            <div id='msginfo' runat='server' style="display: none;" class="alert alert-warning alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4><i class="icon fa fa-warning"></i></h4>
            </div>
            <div id='msgsuccess' runat='server' style="display: none;" class="alert alert-success alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4><i class="icon fa fa-check"></i></h4>
            </div>
        </div>
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Pallet Packing </h1>

        </section>
        <!-- Main content -->
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Scan Details</h3>

                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>FG Item Code : </label>
                                <asp:DropDownList ID="drpFGItemCode" runat="server" class="form-control select2" Style="width: 100%;"
                                    OnSelectedIndexChanged="drpFGItemCode_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Work Order No. : </label>
                                <asp:DropDownList ID="drpWorkOrderNo" runat="server" class="form-control select2" Style="width: 100%;"
                                    AutoPostBack="true" OnSelectedIndexChanged="drpWorkOrderNo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Select Customer Code : </label>
                                <asp:DropDownList ID="drpCustomerCode" runat="server"
                                    class="form-control select2" AutoPostBack="true"
                                    OnSelectedIndexChanged="drpCustomerCode_SelectedIndexChanged" Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Packing Quantity  :  </label>
                                <asp:Label ID="lblPackingQty" runat="server" Style="width: 100%;" Font-Size="Large">
                                </asp:Label>
                                <div></div>
                                <label>Scan Quantity :  </label>
                                <asp:Label ID="lblScanQty" runat="server" Style="width: 100%;" Font-Size="Large">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group" id="dvPrintergrup" runat="server">
                                <label>Printer Name : </label>
                                <asp:DropDownList ID="drpPrinterName" runat="server"
                                    class="form-control select2" Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Customer Name : </label>
                                <asp:TextBox ID="txtCustomerName" ReadOnly="true" runat="server" MaxLength="50" class="form-control" autocomplete="off"
                                    placeholder="Enter customer name">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Customer Part Number : </label>
                                <asp:TextBox ID="txtCustomerPartNo" ReadOnly="true" runat="server" MaxLength="50" class="form-control"
                                    placeholder="Enter customer part no" onkeydown="return (event.keyCode!=13)" autocomplete="off">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Customer Location : </label>
                                <asp:TextBox ID="txtCustomerLocation" runat="server" MaxLength="50" onkeydown="return (event.keyCode!=13)" class="form-control"
                                    placeholder="Enter customer location" autocomplete="off">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Purchase Order No. : </label>
                                <asp:DropDownList ID="ddlPurchaseOrder" runat="server" class="form-control select2" OnSelectedIndexChanged="ddlPurchaseOrder_SelectedIndexChanged"
                                    AutoPostBack="true"
                                    Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6" runat="server">
                            <div class="form-group">
                                <label>Invoice No : </label>
                                <asp:DropDownList ID="drpInvoiceNo" runat="server" OnSelectedIndexChanged="drpInvoiceNo_SelectedIndexChanged"
                                    AutoPostBack="true"
                                    class="form-control" Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6" runat="server">
                            <div class="form-group">
                                <label>Invoice Date : </label>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtInvoiceData" runat="server" class="form-control" onkeydown="return (event.keyCode!=13)"
                                        tyle="width: 100%;" ReadOnly="true"
                                        autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Enter Gross Weight : </label>
                                <asp:TextBox ID="txtWeight" runat="server" class="form-control" MaxLength="10"
                                    autocomplete="off" onkeydown="return (event.keyCode!=13)" onkeypress="javascript:return isNumber(event)"
                                    Style="width: 100%;">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Enter Net Weight : </label>
                                <asp:TextBox ID="txtNetWeight" runat="server" class="form-control" MaxLength="10"
                                    autocomplete="off" onkeydown="return (event.keyCode!=13)" onkeypress="javascript:return isNumber(event)"
                                    Style="width: 100%;" Text="0">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Location Type : </label>
                                <asp:DropDownList ID="drpLocatioType" runat="server" class="form-control">
                                    <asp:ListItem>OEM</asp:ListItem>
                                    <asp:ListItem>SPARE PART</asp:ListItem>
                                    <asp:ListItem>AFTER MARKET</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Scan Barcode : </label>
                                <asp:TextBox ID="txtBoxID" runat="server" class="form-control" AutoPostBack="true"
                                    placeholder="Scan box id" autocomplete="off" OnTextChanged="txtBoxID_TextChanged">
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Last Scanned : </label>
                                <asp:Label ID="lblLastScanned" Enabled="false" runat="server" class="form-control" Style="width: 100%;"></asp:Label>
                            </div>
                        </div>
                        <div class="col-xs-4">
                            <asp:Button ID="btnComplete" runat="server" class="btn btn-primary btn-block btn-flat"
                                Text="Complete Box" OnClick="btnComplete_Click" OnClientClick="Confirm()" Visible="true" />
                        </div>
                        <div class="col-xs-4">
                            <asp:Button ID="btnReset" runat="server" class="btn btn-primary btn-block btn-flat"
                                Text="Reset" OnClick="btnReset_Click" Visible="true" />
                        </div>
                        <div class="col-md-12" id="dvCompletePannel" runat="server" visible="false">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>User ID : </label>
                                    <asp:TextBox ID="txtID" runat="server" class="form-control"
                                        autocomplete="off">
                                    </asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Password : </label>
                                    <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" class="form-control"
                                        autocomplete="off">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-xs-4">
                                    <asp:Button ID="btnVerify" runat="server" class="btn btn-primary btn-block btn-flat"
                                        Text="Verify" OnClick="btnVerify_Click" Visible="true" />
                                </div>
                                <div class="col-xs-4">
                                    <asp:Button ID="btnClose" runat="server" class="btn btn-primary btn-block btn-flat"
                                        Text="Close" OnClick="btnClose_Click" Visible="true" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
