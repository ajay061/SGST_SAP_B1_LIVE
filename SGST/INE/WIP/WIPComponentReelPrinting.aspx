<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="WIPComponentReelPrinting.aspx.cs" Inherits="SGST.INE.WIP.WIPComponentReelPrinting"
    EnableEventValidation="false" %>

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
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>COMPONENT FORMING</h1>
        </section>
        <!-- Main content -->
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Scan Details</h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group" id="dvPrintergrup" runat="server">
                                <label>Printer Name</label>
                                <asp:DropDownList ID="drpPrinterName" runat="server" class="form-control select2" Style="width: 100%;"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>SFG Item Code : </label>
                                <asp:DropDownList ID="drpSFGItemCode" runat="server" class="form-control select2" Style="width: 100%;"></asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label>Scan Barcode : </label>
                                <asp:TextBox ID="txtBarcode" runat="server"
                                    class="form-control" placeholder="Scan Barcode"
                                    autocomplete="off" OnTextChanged="txtBarcode_TextChanged" AutoPostBack="true">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Scan Forming Tool ID : </label>
                                <asp:TextBox ID="txtFormingToolID" runat="server" class="form-control" OnTextChanged="txtFormingToolID_TextChanged"
                                    placeholder="Scan Forming Tool ID" autocomplete="off"
                                    AutoPostBack="true"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Tool Desc : </label>
                                <asp:Label ID="lblToolDesc" runat="server" class="form-control" disabled="disabled" Style="width: 100%;"></asp:Label>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:GridView ID="divComponentReelPrinting" runat="server" class="table table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" PageSize="10">
                                            <PagerStyle HorizontalAlign="Left" CssClass="pagination-ys" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Part Code" DataField="PART_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                                <asp:BoundField HeaderText="Part Desc" DataField="PART_DESC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                                <asp:BoundField HeaderText="Total Qty" DataField="QTY" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100" />
                                                <asp:BoundField HeaderText="Rem Qty" DataField="REM_QTY" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100" />
                                                <asp:BoundField HeaderText="MFG Data" DataField="MFG_DATE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                                <asp:BoundField HeaderText="EXP Date" DataField="EXP_DATE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                                <asp:BoundField HeaderText="Reservation No" DataField="ISSUE_SLIPNO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                                <asp:BoundField HeaderText="Batch No" DataField="BATCHNO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100" />
                                                <asp:BoundField HeaderText="PO NO" DataField="PONO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Lot Size(Packet Size) : </label>
                                <asp:TextBox ID="txtLotSize" placeholder="Enter lot size"
                                    runat="server" class="form-control"
                                    Style="width: 100%;" MaxLength="8" onkeypress="javascript:return isNumber(event)" onkeydown="return (event.keyCode!=13)" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>No Of Labels : </label>
                                <asp:TextBox ID="txtNoOfLabels" placeholder="Enter lot size"
                                    runat="server" class="form-control"
                                    Style="width: 100%;" MaxLength="8" onkeypress="javascript:return isNumber(event)" onkeydown="return (event.keyCode!=13)" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-4">
                                    <asp:Button ID="btnPrint" runat="server" UseSubmitBehavior="false" Text="Print" class="btn btn-primary btn-block btn-flat" OnClick="btnPrint_Click" />
                                </div>
                                <div class="col-xs-4">
                                    <asp:Button ID="btnReset" runat="server" UseSubmitBehavior="false" Text="Reset" class="btn btn-primary btn-block btn-flat" OnClick="btnReset_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box-footer">
                </div>
            </div>
        </section>
    </div>
</asp:Content>
