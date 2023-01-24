<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="FGCustomerReturn.aspx.cs"
    Inherits="SGST.INE.FG.FGCustomerReturn" EnableEventValidation="false" %>

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
        <section class="content-header">
            <h1>FG</h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Customer Return</h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Return Slip No : </label>
                                <asp:DropDownList ID="drpReturnSlipNo" runat="server" class="form-control select2" Style="width: 100%;" OnSelectedIndexChanged="drpReturnSlipNo_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:GridView ID="gvReturnSlip" runat="server" class="table table-bordered table-hover"
                                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10">
                                    <PagerStyle HorizontalAlign="Left" CssClass="pagination-ys" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Order No" DataField="SALES_ORDER_NO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="900" />
                                        <asp:BoundField HeaderText="Customer Code" DataField="CUSTOMER_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="900" />
                                        <asp:BoundField HeaderText="Item Code" DataField="ITEM_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="900" />
                                        <asp:BoundField HeaderText="Qty" DataField="NOOFBOXES" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        <asp:BoundField HeaderText="Scan Qty" DataField="SCAN_QTY" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="form-group">
                                <label>Scan Location : </label>
                                <asp:TextBox ID="txtScanLocation" runat="server" class="form-control" AutoPostBack="true"
                                    placeholder="Scan Location" autocomplete="off" MaxLength="50" OnTextChanged="txtScanLocation_TextChanged"> </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Scan Box : </label>
                                <asp:TextBox ID="txtBoxID" runat="server" class="form-control" AutoPostBack="true"
                                    placeholder="Scan Box" autocomplete="off" MaxLength="50" OnTextChanged="txtBoxID_TextChanged"> </asp:TextBox>
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary btn-block btn-flat"
                                    OnClick="btnReset_Click" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <asp:HiddenField ID="hidItemCode" runat="server" />
                            <asp:HiddenField ID="hidSPO" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
