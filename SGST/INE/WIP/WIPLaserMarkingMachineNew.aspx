<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="WIPLaserMarkingMachineNew.aspx.cs" Inherits="SGST.INE.WIP.WIPLaserFileGenerationNew"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function PreventPostback(sender, eventArgs) {
            if (eventArgs.get_newValue() == "")
                eventArgs.set_cancel(true);
        }
    </script>
    <style>
        div.scroll {
            width: auto;
            height: auto;
            overflow-x: auto;
            overflow-y: auto;
            text-align: center;
        }
    </style>
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
            <h1>PCB SN GENERATION</h1>

        </section>
        <!-- Main content -->
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Scan Details</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Scan Machine Barcode : </label>
                                <asp:TextBox ID="txtScanMachineBarcode" runat="server"
                                    class="form-control" placeholder="Scan Machine Barcode"
                                    autocomplete="off" OnTextChanged="txtScanMachineBarcode_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Type : </label>
                                <asp:DropDownList ID="drpType" runat="server"
                                    class="form-control select2" placeholder="Select Type"
                                    autocomplete="off" OnSelectedIndexChanged="drpType_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Packet Type : </label>
                                <asp:DropDownList ID="drpPacketType" runat="server"
                                    class="form-control select2" placeholder="Select Packet Type"
                                    autocomplete="off">
                                    <asp:ListItem Value="0" Text="Normal PCB"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Cross PCB"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Select Work Order No. : </label>
                                <asp:DropDownList ID="drpIssueSlipNo" runat="server"
                                    class="form-control select2" placeholder="Select Reservation slip no"
                                    autocomplete="off" AutoPostBack="true"
                                    OnSelectedIndexChanged="drpIssueSlipNo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Select FG Item Code : </label>
                                <asp:DropDownList ID="drpFGItemCodeRH" runat="server"
                                    class="form-control select2" placeholder="Select FG Item Code"
                                    OnSelectedIndexChanged="drpFGItemCodeRH_SelectedIndexChanged"
                                    autocomplete="off" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Select Customer Code : </label>
                                <asp:DropDownList ID="drpCustomerCode" runat="server"
                                    class="form-control select2" placeholder="Select Customer Code"
                                    autocomplete="off">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divTMO" runat="server" visible="false">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Model : </label>
                                <asp:Label ID="lblModel" runat="server"></asp:Label>
                            </div>
                            <div class="form-group">
                                <label>Available Qty For Printing : </label>
                                <asp:Label ID="lblAvailableQty" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label runat="server" id="lblScanRH">Scan Barcode(RH) : </label>
                                <asp:TextBox ID="txtBarcode" runat="server"
                                    class="form-control" placeholder="Scan RH Barcode"
                                    autocomplete="off" OnTextChanged="txtBarcode_TextChanged"
                                    AutoPostBack="true"></asp:TextBox>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </div>
                            <div class="col-md-12 scroll">
                                <div class="form-group">
                                    <asp:GridView ID="dvLaserFileData" runat="server" class="table table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" PageSize="10">
                                        <PagerStyle HorizontalAlign="Left" CssClass="pagination-ys" />
                                        <Columns>
                                            <asp:BoundField HeaderText="SN" DataField="R_D" HeaderStyle-HorizontalAlign="Left" Visible="false" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField HeaderText="Part Code" DataField="PART_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                            <asp:BoundField HeaderText="Quantity" DataField="REM_QTY" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                            <asp:BoundField HeaderText="Array Size" DataField="PCB_ARRAY_SIZE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="Batch" DataField="BATCHNO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="GRPO No." DataField="PONO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="Work Order No." DataField="ISSUE_SLIP_NO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="Supplier No." DataField="SUPPLIER_ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="Part Desc" DataField="PART_DESC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="Customer Part No." DataField="CUSTOMERCODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="Part Barcode" DataField="PARTBARCODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="col-md-6">
                        <div class="col-xs-4">
                            <asp:Button ID="btnPrint" runat="server" class="btn btn-primary btn-block btn-flat"
                                Text="Generate PCB SN" Visible="true" OnClick="btnPrint_Click" />
                        </div>
                        <div class="col-xs-4">
                            <asp:Button ID="btnReset" runat="server" class="btn btn-primary btn-block btn-flat"
                                Text="Reset" OnClick="btnReset_Click" Visible="true" />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
