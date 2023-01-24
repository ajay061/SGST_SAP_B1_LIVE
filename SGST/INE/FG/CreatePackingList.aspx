<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="CreatePackingList.aspx.cs"
    Inherits="SGST.INE.FG.PackingList" EnableEventValidation="false" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            <h1>Items Picking     
            </h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Select Details</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Outbound Delivery No. : </label>
                                <asp:DropDownList ID="drpOutboundDelivery" runat="server" class="form-control select2" Style="width: 100%;" AutoPostBack="true" OnSelectedIndexChanged="drpOutboundDelivery_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Sales Order No. : </label>
                                <asp:DropDownList ID="ddlsalesOrder" runat="server" class="form-control select2" Style="width: 100%;" AutoPostBack="true" OnSelectedIndexChanged="ddlsalesOrder_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Pick List : </label>
                                <asp:DropDownList ID="ddlPicklist" runat="server" class="form-control select2" Style="width: 100%;" AutoPostBack="true" OnSelectedIndexChanged="ddlPackinglist_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4" runat="server" visible="false">
                            <div class="form-group">
                                <label>Item Code : </label>
                                <asp:DropDownList ID="ddlItemCode" runat="server" class="form-control select2"
                                    Style="width: 100%;" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlItemCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="row scroll">
                        <asp:GridView ID="GV_Packinglist" runat="server" CellPadding="4" ForeColor="#333333"
                            GridLines="None" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false"
                            Width="100%" class="table table-bordered table-hover">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="20px" HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsrno" runat="server" Text='<%# Container.DataItemIndex + 1%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Item Code" DataField='ITEM_CODE' ItemStyle-Width="20px" HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                <asp:BoundField HeaderText="Item Line ID" DataField='LINE_ID' ItemStyle-Width="20px" HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                <asp:BoundField HeaderText="Picklist Quantity" DataField='PICKLIST_QTY' ItemStyle-Width="20px" HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                <asp:BoundField HeaderText="Rem Quantity" DataField='REM_QTY' ItemStyle-Width="20px" HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                <asp:BoundField HeaderText="Scan Quantity" DataField='SCAN_QTY' ItemStyle-Width="20px" HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Scan Here</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Scan Location : </label>
                                <span>
                                    <asp:TextBox ID="txtScanLocation" runat="server" class="form-control" placeholder="Scan Location" AutoPostBack="true" OnTextChanged="txtScanLocation_TextChanged" action="#" autocomplete="off"></asp:TextBox></span>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Scan Box Barcode : </label>
                                <span>
                                    <asp:TextBox ID="txtScanbar" runat="server" class="form-control" placeholder="Scan Secondary box barcode" AutoPostBack="true" OnTextChanged="txtScanbar_TextChanged" action="#" autocomplete="off"></asp:TextBox></span>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <span>
                                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label></span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="col-xs-4">
                                <asp:Button ID="btnPicklist" runat="server" Text="Save" class="btn btn-primary btn-block btn-flat" OnClick="btnPicklist_Click" />
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary btn-block btn-flat" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
