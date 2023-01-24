<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="RMPrinting.aspx.cs" Inherits="SGST.INE.RM.RMPrinting" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <script type="text/javascript">
        function onKeyDown(event) {
            event.preventDefault();
        }
    </script>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to continue printing?")) {
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
            <h1>RM Label Printing       
            </h1>
        </section>

        <!-- Main content -->
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Select Details</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Select GRPO Date : </label>
                                <asp:DropDownList ID="drpGRPODate" runat="server" class="form-control select2"
                                    Style="width: 100%;" OnSelectedIndexChanged="drpGRPODate_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Select GRPO No. : </label>
                                <asp:DropDownList ID="drpReceiptNo" runat="server" class="form-control select2"
                                    Style="width: 100%;" OnSelectedIndexChanged="drpReceiptNo_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>SAP Part Desc : </label>
                                <asp:DropDownList ID="drpItemCode" runat="server" class="form-control select2"
                                    Style="width: 100%;" OnSelectedIndexChanged="drpItemCode_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Item Line Code : </label>
                                <asp:DropDownList ID="drpLineNo" runat="server" class="form-control select2"
                                    Style="width: 100%;" AutoPostBack="true"
                                    OnSelectedIndexChanged="drpLineNo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>MFR Part Code : </label>
                                <asp:DropDownList ID="drpMFR" runat="server" class="form-control"
                                    Style="width: 100%;" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Make : </label>
                                <asp:TextBox ID="lblMake" runat="server" class="form-control"
                                    disabled="disabled" Style="width: 100%;" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>SAP Part Code : </label>
                                <asp:TextBox ID="txtComDescription" runat="server" class="form-control"
                                    placeholder="Enter Component Description" disabled="disabled"
                                    Style="width: 100%;" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group" id="dvPrintergrup" runat="server">
                                <label>Printer IP : </label>
                                <asp:DropDownList ID="drpPrinterName" runat="server"
                                    class="form-control select2" Style="width: 100%;" autocomplete="off"
                                    OnSelectedIndexChanged="drpPrinterName_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Part Details</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Bill of entry : </label>
                                <asp:TextBox ID="txtInvoiceNumber" runat="server"
                                    placeholder="Enter Invoice No"
                                    class="form-control" Style="width: 100%;" autocomplete="off" disabled="disabled"
                                    onkeydown="return (event.keyCode!=13)" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Bill of Date : </label>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtSupplierDate" runat="server" class="form-control"
                                        onkeydown="return (event.keyCode!=13)"
                                        data-inputmask="'alias': 'yyyy-mm-dd'" data-mask Style="width: 100%;" disabled="disabled" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Dealer Name : </label>
                                <asp:TextBox ID="txtDealerName" runat="server"
                                    placeholder="Enter Dealer Name"
                                    class="form-control" Style="width: 100%;" disabled="disabled" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Supplier Batch No. : </label>
                                <asp:TextBox ID="txtBatchNo" runat="server"
                                    placeholder="Enter Supplier Batch No."
                                    class="form-control" Style="width: 100%;" MaxLength="50" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>MFG Date : </label>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtMFGDate" runat="server" class="form-control" AutoPostBack="true"
                                        data-inputmask="'alias': 'yyyy-mm-dd'" OnTextChanged="txtMFGDate_TextChanged" data-mask Style="width: 100%;" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Expiry Date : </label>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtExpiryDate" runat="server" class="form-control" onkeydown="return (event.keyCode!=13)"
                                        data-inputmask="'alias': 'yyyy-mm-dd'" data-mask Style="width: 100%;" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>LF_RH : </label>
                                <asp:DropDownList ID="drpLF" runat="server" class="form-control select 2"
                                    Style="width: 100%;" autocomplete="off">
                                    <asp:ListItem>LF</asp:ListItem>
                                    <asp:ListItem>RoHS</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>MSL Value : </label>
                                <asp:TextBox ID="txtMSLValue" runat="server" class="form-control"
                                    placeholder="Enter MSL value"
                                    Style="width: 100%;" autocomplete="off"
                                    MaxLength="50" onkeydown="return (event.keyCode!=13)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Pack Size : </label>
                                <asp:TextBox ID="txtpackSize" runat="server" class="form-control" Style="width: 100%;"
                                    onkeydown="return (event.keyCode!=13)" onkeypress="javascript:return isNumber(event)"
                                    placeholder="Enter Pack Size"
                                    autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Print Barcode</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>GRPO Quantity : </label>
                                <asp:TextBox ID="txtQty" placeholder="Enter GRPO Qty" runat="server" class="form-control"
                                    Style="width: 100%;"
                                    disabled="disabled" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Remaining Quantity : </label>
                                <asp:TextBox ID="txtRemainingQty"
                                    runat="server" onKeyDown="javascript:return onKeyDown(event)"
                                    class="form-control" Style="width: 100%;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>No. of Print : </label>
                                <asp:TextBox ID="txtNoOfPrints" placeholder="Enter No Of Print"
                                    runat="server" MaxLength="10" class="form-control" Style="width: 100%;"
                                    onkeypress="javascript:return isNumber(event)" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="col-xs-4">
                                <asp:Button ID="btnPrint" runat="server" Text="Print" class="btn btn-primary btn-block btn-flat" OnClick="btnPrint_Click" 
                                    OnClientClick="Confirm()" />
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary btn-block btn-flat" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:GridView ID="dvDetails" runat="server" class="table table-striped table-bordered table-hover GridPager"
                                        AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="dvDetails_PageIndexChanging">
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                        <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                        <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                        <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                        <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                        <EditRowStyle BackColor="#999999"></EditRowStyle>
                                        <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                        <Columns>
                                            <asp:BoundField HeaderText="Part Code" DataField="PART_CODE" HeaderStyle-HorizontalAlign="Left" Visible="true" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="Part Barcode" DataField="PART_BARCODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                            <asp:BoundField HeaderText="Lot Quantity" DataField="LOT_QTY" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100" />
                                            <asp:BoundField HeaderText="Batch No." DataField="BatchNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.row -->
            </div>
            <!-- /.box-body -->
            <asp:HiddenField ID="hidRMID" runat="server" />
            <asp:HiddenField ID="hidRNO" runat="server" />
            <asp:HiddenField ID="hidEXPDATE" runat="server" />
            <asp:HiddenField ID="hidGRNdate" runat="server" />
            <asp:HiddenField ID="hidPartnumber" runat="server" />
            <asp:HiddenField ID="hidDealerName" runat="server" />
            <asp:HiddenField ID="hidInvoice" runat="server" />
            <asp:HiddenField ID="hidInvoiceDate" runat="server" />
            <asp:HiddenField ID="hidQty" runat="server" />
            <asp:HiddenField ID="hidRemQty" runat="server" />
            <asp:HiddenField ID="hidExpiryDate" runat="server" />
            <asp:HiddenField ID="hidbatchno" runat="server" />
            <asp:HiddenField ID="hiddiscription" runat="server" />
            <asp:HiddenField ID="hidMFGDate" runat="server" />
            <asp:HiddenField ID="hidPK" runat="server" />
            <asp:HiddenField ID="hidppo" runat="server" />
            <asp:HiddenField ID="hidSupplierCode" runat="server" />
            <asp:HiddenField ID="hidUOM" runat="server" />
            <asp:HiddenField ID="hidSupplierName" runat="server" />
            <asp:HiddenField ID="hidDeliveryNo" runat="server" />
            <asp:HiddenField ID="hidMSLComponent" runat="server" />
            <asp:HiddenField ID="hidNoODays" runat="server" />
            <asp:HiddenField ID="hidLHRH" runat="server" />
            <asp:HiddenField ID="hidMSLValue" runat="server" />
            <asp:HiddenField ID="hidMake" runat="server" />
        </section>
        <!-- /.content -->
    </div>

</asp:Content>
