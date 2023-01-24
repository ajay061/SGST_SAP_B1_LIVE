<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master"
    CodeBehind="MaterialToMaterial.aspx.cs"
    Inherits="SGST.INE.RM.MaterialToMaterial" %>

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
            <h1>Label Printing</h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Material to Material Transfer</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Select Material Transfer Ref No : </label>
                                <asp:DropDownList ID="drpMatRefNo" runat="server" class="form-control select2" Style="width: 100%;"
                                    AutoPostBack="true" OnSelectedIndexChanged="drpMatRefNo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Select New Part Code : </label>
                                <asp:DropDownList ID="drpItemCode" runat="server" autocomplete="off"
                                    class="form-control select2"
                                    Style="width: 100%;" AutoPostBack="true"
                                    OnSelectedIndexChanged="drpItemCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="dvData" runat="server" class="table table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" PageSize="10">
                                    <PagerStyle HorizontalAlign="Left" CssClass="pagination-ys" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Qty" DataField="IN_QTY" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Out WHS" DataField="IN_WHS" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                        <asp:BoundField HeaderText="Out Batch" DataField="IN_BATCH" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                        <asp:BoundField HeaderText="Input Item Code" DataField="OUT_ITEM_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Input Total Quantity" DataField="OUT_QTY" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Input Scanned Quantity" DataField="OUT_SCAN_QTY" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Input Rem Quantity" DataField="OUT_REM_QTY" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Input WHS" DataField="OUT_WHS" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Input Batch" DataField="OUT_BATCH" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                    </Columns>
                                </asp:GridView>
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
                            <div class="form-group">
                                <label>Scan Part Barcode To Transfer : </label>
                                <asp:TextBox ID="txtReelBarcode" runat="server" class="form-control" Style="width: 100%;" placeholder="Enter Part Barcode"
                                    OnTextChanged="txtReelBarcode_TextChanged" autocomplete="off" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Invoice No. : </label>
                                <asp:TextBox ID="txtInvoiceNo" runat="server" ReadOnly="true" class="form-control" autocomplete="off" placeholder="Enter Invoice No" Style="width: 100%;" MaxLength="10"></asp:TextBox>

                            </div>
                            <div class="form-group">
                                <label>MFR Date : </label>
                                <asp:TextBox ID="txtMFGDate" runat="server" ReadOnly="true" class="form-control" onkeydown="return (event.keyCode!=13)" data-inputmask="'alias': 'yyyy-mm-dd'" data-mask Style="width: 100%;" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Batch No. : </label>
                                <asp:TextBox ID="txtBatchNo" runat="server" ReadOnly="true" class="form-control" placeholder="Enter Batch No" Style="width: 100%;" autocomplete="off"></asp:TextBox>
                            </div>
                            <!-- /.form-group -->
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Existing Quantity : </label>
                                <asp:TextBox ID="txtQty" runat="server" class="form-control" disabled="disabled" Style="width: 100%;"></asp:TextBox>
                            </div>
                            <!-- /.form-group -->
                            <div class="form-group">
                                <label>Enter New Quantity : </label>
                                <asp:TextBox ID="txtQuantity" runat="server" autocomplete="off" class="form-control" placeholder="Enter Quantity" Style="width: 100%;" MaxLength="8" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Invoice Date : </label>
                                <asp:TextBox ID="txtInvoiceDate" runat="server" ReadOnly="true" class="form-control" onkeydown="return (event.keyCode!=13)"
                                    data-inputmask="'alias': 'yyyy-mm-dd'" data-mask Style="width: 100%;" autocomplete="off"></asp:TextBox>

                            </div>
                            <div class="form-group">
                                <label>EXP Date : </label>
                                <asp:TextBox ID="txtEXPDate" ReadOnly="true" runat="server" class="form-control" onkeydown="return (event.keyCode!=13)"
                                    data-inputmask="'alias': 'yyyy-mm-dd'" data-mask Style="width: 100%;" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="form-group" id="dvPrintergrup" runat="server">
                                <label>Printer Name : </label>
                                <asp:DropDownList ID="drpPrinterName" runat="server" class="form-control select2" Style="width: 100%;"></asp:DropDownList>
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnPrint" runat="server" UseSubmitBehavior="false" Text="Transfer" class="btn btn-primary btn-block btn-flat" OnClick="btnPrint_Click" />
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnReset" runat="server" UseSubmitBehavior="false" Text="Reset" class="btn btn-primary btn-block btn-flat" OnClick="btnReset_Click" />
                            </div>
                        </div>
                        <!-- /.col -->
                    </div>
                </div>
            </div>
            <br />
            <br />
            <asp:HiddenField ID="hidRNO" runat="server" />
            <asp:HiddenField ID="hidqty" runat="server" />
            <asp:HiddenField ID="hidEXPDATE" runat="server" />
        </section>
    </div>
</asp:Content>
