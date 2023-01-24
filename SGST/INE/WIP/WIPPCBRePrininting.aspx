<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="WIPPCBRePrininting.aspx.cs" Inherits="SGST.INE.WIP.WIPPCBRePrininting"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function PreventPostback(sender, eventArgs) {
            if (eventArgs.get_newValue() == "")
                eventArgs.set_cancel(true);
        }
    </script>


    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
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
            <h1>Label Printing</h1>
        </section>
        <!-- Main content -->
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">PCB SN Re-PRINTING</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>FG Item Code : </label>
                                <asp:DropDownList ID="drpFGItemCode" runat="server" class="form-control select2" AutoPostBack="true"
                                    OnSelectedIndexChanged="drpFGItemCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Work Order No. : </label>
                                <asp:DropDownList ID="drpWorkOrderNo" runat="server" class="form-control select2" AutoPostBack="true"
                                    OnSelectedIndexChanged="drpWorkOrderNo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Select PCB PKT Barcode :</label>
                                <asp:DropDownList ID="drpPendingBarcode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpPendingBarcode_SelectedIndexChanged" Style="width: 100%;" class="form-control select2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:GridView ID="dvLaserFileData" runat="server" class="table table-striped table-bordered table-hover GridPager"
                                        AutoGenerateColumns="false">
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                        <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                        <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                        <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                        <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                        <EditRowStyle BackColor="#999999"></EditRowStyle>
                                        <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select" ItemStyle-Width="5px" HeaderStyle-Width="5px"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Part Code" DataField="PART_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                            <asp:BoundField HeaderText="Batch" DataField="BATCHNO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="GRPONo" DataField="PONO" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="1000" ItemStyle-Wrap="false" />
                                            <asp:BoundField HeaderText="CustomerPartNo" DataField="CUSTOMER_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="PCB ID" DataField="PCB_ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300"
                                                ItemStyle-Wrap="false" />
                                            <asp:BoundField HeaderText="PCB Master ID" DataField="PCB_MASTER_ID" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" ItemStyle-Wrap="false" />
                                            <asp:BoundField HeaderText="FG Item Code" DataField="FG_ITEM_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                            <asp:BoundField HeaderText="Product No" DataField="TMO_PRODUCT_NO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="form-group" id="dvPrintergrup" runat="server">
                                <label>Printer Name</label>
                                <asp:DropDownList ID="drpPrinterName" runat="server" class="form-control select2" Style="width: 100%;"></asp:DropDownList>
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
                                        <asp:Button ID="btnPrint" runat="server" UseSubmitBehavior="false"
                                            Text="Print" class="btn btn-primary btn-block btn-flat" OnClick="btnPrint_Click" />
                                    </div>
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
