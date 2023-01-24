<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="RMReturnNew.aspx.cs" Inherits="SGST.INE.RM.RMReturnNew" %>

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
            else if (iKeyCode == 46) {
                if ((elementRef.value) && (elementRef.value.indexOf('.') >= 0))
                    return false;
            }
            else {
                return true;
            }
            return true;
        }
    </script>
    <!-- Control Sidebar -->
    <!-- Content Wrapper. Contains page content -->
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
            <h1>MRN-Shop Floor To RM</h1>

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
                                <label>Type. : </label>
                                <asp:DropDownList ID="drpType" runat="server" class="form-control"
                                    autocomplete="off" >
                                    <asp:ListItem>RM</asp:ListItem>
                                    <asp:ListItem>SFG</asp:ListItem>
                                </asp:DropDownList>
                                
                            </div>
                            <div class="form-group">
                                <label>Scan RM Barcode : </label>
                                <asp:TextBox ID="txtBarcode" runat="server" class="form-control" placeholder="Scan Barcode" autocomplete="off" AutoPostBack="true" OnTextChanged="txtBarcode_TextChanged"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Available Quantity :</label>
                                <asp:TextBox ID="txtQuantity" runat="server" class="form-control" MaxLength="10" ReadOnly="true"
                                    autocomplete="off" Visible="true"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Enter Remarks : </label>
                                <asp:TextBox ID="txtRemarks" runat="server" class="form-control" placeholder="Enter Remarks" 
                                    autocomplete="off" MaxLength="100" ></asp:TextBox>
                            </div>

                        </div>
                        <!-- /.col -->
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Work Order No. : </label>
                                <asp:TextBox ID="txtWorkOrderNo" runat="server" class="form-control" MaxLength="10"
                                    autocomplete="off" Visible="true" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Return Quantity : </label>
                                <asp:TextBox ID="txtQnt" runat="server" class="form-control" MaxLength="10"
                                    autocomplete="off" Visible="true"
                                    onkeypress="javascript:return isNumber(event)"
                                    onpaste="return false;"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <br />
                                <div class="col-xs-4">
                                    <asp:Button ID="btnAdd" runat="server" class="btn btn-primary btn-block btn-flat" Text="Add" OnClick="btnAdd_Click" />
                                </div>
                                <div class="col-xs-4">
                                    <asp:Button ID="btnReset" runat="server" class="btn btn-primary btn-block btn-flat" Text="Reset" OnClick="btnReset_Click" />
                                </div>
                            </div>
                            <asp:HiddenField ID="hidRRID" runat="server" />
                            <asp:HiddenField ID="hidInvQty" runat="server" />
                            <asp:HiddenField ID="hidQty" runat="server" />
                            <asp:HiddenField ID="hidScanQty" runat="server" />
                            <asp:HiddenField ID="hidPartCode" runat="server" />
                            <asp:HiddenField ID="hidWorkOrderno" runat="server" />
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:GridView ID="gvReturnBarcode" class="table table-bordered table-striped" runat="server"
                                            AutoGenerateColumns="false" AllowPaging="true" PageSize="10">
                                            <PagerStyle HorizontalAlign="Left" CssClass="pagination-ys" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Part barCode" DataField="itemcode" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField HeaderText="Available Quantity" DataField="availableqty" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField HeaderText="Return Quantity" DataField="qty" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField HeaderText="Part Code" DataField="PartCode" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField HeaderText="Work Order No" DataField="WORKORDERNO" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.box -->
            <div class="box box-default">
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6" id="dvPrintergrup" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblPrinter" runat="server" Text="Printer"></asp:Label>
                                <asp:DropDownList ID="drpPrinterName" runat="server" class="form-control select2" Style="width: 100%;"></asp:DropDownList>
                            </div>
                        </div>
                        <!-- /.col -->
                        <div class="col-md-6">
                            <div class="col-xs-4">
                                <asp:Button ID="btnPrint" runat="server" class="btn btn-primary btn-block btn-flat" Text="Return & Print" OnClick="btnPrint_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
