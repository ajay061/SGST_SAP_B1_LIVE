<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="WIPBoxPacking.aspx.cs" Inherits="SGST.INE.WIP.WIPBoxPacking" %>

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

        <section class="content-header">
            <h1>Primary Box Packing</h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Enter Details</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Scan Type : </label>
                                <asp:DropDownList ID="drpScanType" runat="server" class="form-control select2"
                                    AutoPostBack="true" OnSelectedIndexChanged="drpScanType_SelectedIndexChanged"
                                    Style="width: 100%;">
                                    <asp:ListItem>PCB</asp:ListItem>
                                    <asp:ListItem>IMEI</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Select FG Item Code : </label>
                                <asp:DropDownList ID="drpFGItemCode" runat="server" class="form-control select2" Style="width: 100%;"
                                    OnSelectedIndexChanged="drpFGItemCode_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group" id="dvModelName" runat="server">
                                <label>Model Name : </label>
                                <asp:Label ID="lblModelName" Enabled="false" runat="server" autocomplete="off"
                                    class="form-control" Style="width: 100%;" MaxLength="50">                                  
                                </asp:Label>
                                <br />
                                <label>Weight : </label>
                                <asp:Label ID="lblWT" Enabled="false" runat="server" autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="50">                                  
                                </asp:Label>
                                <br />
                                <label>Tolrence(+) : </label>
                                <asp:Label ID="lblTP" Enabled="false" runat="server" autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="50">                                  
                                </asp:Label>
                                <br />
                                <label>Tolrence(-)  : </label>
                                <asp:Label ID="lblTM" Enabled="false" runat="server" autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="50">                                  
                                </asp:Label>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Part Desc    :  </label>
                                <asp:Label ID="lblPartDesc" runat="server" Style="width: 100%;" Font-Bold="true" Font-Size="Large">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Select Work Order No. : </label>
                                <asp:DropDownList ID="drpWorkOrderNo" runat="server" class="form-control select2" Style="width: 100%;"
                                    OnSelectedIndexChanged="drpWorkOrderNo_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>FG Work Order Qty    :  </label>
                                <asp:Label ID="lblWOTotalQty" runat="server" Style="width: 100%;" Font-Bold="true" Font-Size="Large">
                                </asp:Label>
                            </div>
                            <div class="form-group">
                                <label>FG Work Order Scan Qty    :  </label>
                                <asp:Label ID="lblWOScanQty" runat="server" Style="width: 100%;" Font-Bold="true" Font-Size="Large">
                                </asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Select Details</h3>
                </div>
                <div class="box-body">
                    <div class="row">
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
                                <label>Customer Name : </label>
                                <asp:TextBox ID="txtCustomerName" runat="server" MaxLength="50" onkeydown="return (event.keyCode!=13)" ReadOnly="true" class="form-control" autocomplete="off"
                                    placeholder="Enter customer name">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Customer Part Number : </label>
                                <asp:TextBox ID="txtCustomerPartNo" runat="server" MaxLength="50" onkeydown="return (event.keyCode!=13)" class="form-control" ReadOnly="true"
                                    placeholder="Enter customer part no" autocomplete="off">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Purchase Order No. : </label>
                                <asp:DropDownList ID="ddlPurchaseOrder" runat="server" class="form-control select2"
                                    Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="box box-default">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Enter MSN : </label>
                                <asp:TextBox ID="txtMSN" runat="server" onkeydown="return (event.keyCode!=13)" placeholder="Enter MSN Here" class="form-control"
                                    Style="width: 100%;" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Enter Batch No. : </label>
                                <asp:TextBox ID="txtBatchNo" runat="server" onkeydown="return (event.keyCode!=13)"
                                    placeholder="Enter Batch No." class="form-control"
                                    Style="width: 100%;" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Packing Qty  :  </label>
                                <asp:Label ID="lblPackingQty" runat="server" Style="width: 100%;" Font-Bold="true" Font-Size="Large">
                                </asp:Label>
                                <div></div>
                                <label>Scan Qty     :  </label>
                                <asp:Label ID="lblScanQty" runat="server" Style="width: 100%;" Font-Bold="true" Font-Size="Large">
                                </asp:Label>
                                <div></div>
                            </div>
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
                            <div class="form-group">
                                <label>Scan Barcode : </label>
                                <asp:TextBox ID="txtPCBID" runat="server" class="form-control" AutoPostBack="true"
                                    placeholder="Scan PCB ID" autocomplete="off" OnTextChanged="txtPCBID_TextChanged">
                                </asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label>Last Scanned : </label>
                                <asp:Label ID="lblLastScanned" Enabled="false" runat="server" class="form-control" Style="width: 100%;"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group" style="width: 100%; overflow: scroll">
                                            <asp:GridView ID="gvCartonLabelPrint" runat="server" ShowHeader="true"
                                                class="table table-striped table-bordered table-hover GridPager"
                                                AutoGenerateColumns="false" PageSize="100">
                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                                <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                                <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                                <Columns>
                                                    <asp:BoundField HeaderText="SITE CODE" DataField="SITECODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                                    <asp:BoundField HeaderText="MODEL CODE" DataField="MODEL_NAME" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                                    <asp:BoundField HeaderText="MODEL NAME" DataField="MODEL_NAME" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                                    <asp:BoundField HeaderText="IMEI 1" DataField="IMEI1" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                                    <asp:BoundField HeaderText="IMEI 2" DataField="IMEI2" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                                    <asp:BoundField HeaderText="IMEI 3" DataField="IMEI3" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                                    <asp:BoundField HeaderText="IMEI 4" DataField="IMEI4" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                                    <asp:BoundField HeaderText="SR_NO" DataField="SR_NO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                                    <asp:BoundField HeaderText="BT MAC" DataField="BT_MAC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                                    <asp:BoundField HeaderText="WI FI" DataField="WIFI_MAC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                                    <asp:BoundField HeaderText="PCB SN" DataField="PCB_BARCODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                                    <asp:BoundField HeaderText="Work Order No." DataField="WORK_ORDER_NO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                                    <asp:BoundField HeaderText="PO No." DataField="PONO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                                </Columns>
                                                <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                            </asp:GridView>
                                            <asp:GridView ID="gvPCBPrinting" runat="server" ShowHeader="true"
                                                class="table table-striped table-bordered table-hover GridPager"
                                                AutoGenerateColumns="false" PageSize="100">
                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                                <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                                <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                                <Columns>
                                                    <asp:BoundField HeaderText="SITE CODE" DataField="SITECODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                                    <asp:BoundField HeaderText="FG Item Code" DataField="SN_MODEL" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                                    <asp:BoundField HeaderText="PCB SN" DataField="PCB_BARCODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                                    <asp:BoundField HeaderText="Work Order No." DataField="WORK_ORDER_NO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                                    <asp:BoundField HeaderText="PO No." DataField="PONO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                                </Columns>
                                                <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group" runat="server" id="dvMessage" visible="false">
                                <label>Please put weight on weighing machine and Press validate button : </label>
                                <br />
                                <br />
                                <div class="col-xs-4">
                                    <asp:Button ID="btnValidate" OnClick="btnValidate_Click" runat="server" Text="Validate"
                                        class="btn btn-primary btn-block btn-flat" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Enter Gross Weight : </label>
                                <asp:TextBox ID="txtCapWT" runat="server" Enabled="false" autocomplete="off" class="form-control"
                                    Style="width: 100%;">                                  
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="col-xs-4">
                                <asp:Button ID="btnComplete" runat="server" class="btn btn-primary btn-block btn-flat"
                                    Text="Complete Box" OnClick="btnComplete_Click" Visible="true"
                                    OnClientClick="Confirm()" />
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnReset" runat="server" class="btn btn-primary btn-block btn-flat"
                                    Text="Reset" OnClick="btnReset_Click" Visible="true" />
                            </div>
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
