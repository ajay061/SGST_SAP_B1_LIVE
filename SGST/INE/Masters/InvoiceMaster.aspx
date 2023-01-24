<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="InvoiceMaster.aspx.cs" Inherits="SGST.INE.Masters.InvoiceMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        $(document).ready(function () {
            $(".allow_numeric").on("input", function (evt) {
                var self = $(this);
                self.val(self.val().replace(/[^\d].+/, ""));
                if ((evt.which < 48 || evt.which > 57)) {
                    evt.preventDefault();
                }
            });
        });
    </script>
    <div class="content-wrapper">
        <div id="msgdiv" runat="server">
            <div id='msgerror' runat='server' style="display: none;" class="alert alert-danger alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4>
                    <i class="icon fa fa-ban"></i>Alert!</h4>
            </div>
            <div id='msgwarning' runat='server' style="display: none;" class="alert alert-info alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4>
                    <i class="icon fa fa-info"></i>Alert!</h4>
            </div>
            <div id='msginfo' runat='server' style="display: none;" class="alert alert-warning alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4>
                    <i class="icon fa fa-warning"></i>Alert!</h4>
            </div>
            <div id='msgsuccess' runat='server' style="display: none;" class="alert alert-success alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4>
                    <i class="icon fa fa-check"></i>Alert!</h4>
            </div>
        </div>
        <section class="content-header">
            <h1>MASTERS</h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Invoice Master</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>FG Item Code :</label>
                                <asp:DropDownList ID="ddlFGItemCode" runat="server" class="form-control select2"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlFGItemCode_SelectedIndexChanged"
                                    Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Invoice No :</label>
                                <asp:TextBox ID="txtInvoiceNo" runat="server" class="form-control" MaxLength="50"
                                    Style="width: 100%;">
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Purchase Order No :</label>
                                <asp:DropDownList ID="drpPurchaseOrderNo" runat="server" class="form-control select2"
                                    Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Invoice Qty :</label>
                                <asp:TextBox ID="txtPOQty" runat="server" placeholder="Enter Invoice QTY"
                                    autocomplete="off" class="form-control allow_numeric" Style="width: 100%;" MaxLength="10">                                  
                                </asp:TextBox>
                            </div>

                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Model Code :</label>
                                <asp:TextBox ID="txtModel" runat="server" class="form-control" ReadOnly="true"
                                    Style="width: 100%;">
                                </asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label>Invoice Date :</label>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtpurchaseDate" runat="server" class="form-control" onkeydown="return (event.keyCode!=13)"
                                        data-inputmask="'alias': 'yyyy-mm-dd'" data-mask Style="width: 100%;" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Ship To Address :</label>
                                <asp:DropDownList ID="drpShipToAddress" runat="server" class="form-control select2"
                                    Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Invoice Box Size :</label>
                                <asp:TextBox ID="txtInvoiceBoxSize" runat="server" placeholder="Enter Invoice Box Size"
                                    autocomplete="off" class="form-control allow_numeric" Style="width: 100%;" MaxLength="10">                                  
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Stock Point Note :</label>
                                <asp:TextBox ID="txtStockPointNote" runat="server"
                                    autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="100">                                  
                                </asp:TextBox>
                            </div>

                        </div>
                         <div class="col-md-6">
                            <div class="form-group">
                                <label>Supplier Code :</label>
                                <asp:TextBox ID="txtSupplierCode" runat="server"
                                    autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="100">                                  
                                </asp:TextBox>
                            </div>

                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Shipment Date :</label>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtShipmentDate" runat="server" class="form-control" onkeydown="return (event.keyCode!=13)"
                                        data-inputmask="'alias': 'yyyy-mm-dd'" data-mask Style="width: 100%;" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="col-xs-4">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary btn-block btn-flat" OnClick="btnSave_Click" />
                                    </div>
                                    <div class="col-xs-4">
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary btn-block btn-flat" OnClick="btnReset_Click" />
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-default">
                <div class="box-header with-border">
                    <b>Total Records : 
                        <asp:Label ID="lblNumberofRecords" runat="server" Text="0"></asp:Label>
                    </b>
                </div>

                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="gvInvoiceMaster" runat="server"
                                    class="table table-striped table-bordered table-hover GridPager"
                                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                                    OnRowCommand="gvInvoiceMaster_RowCommand" OnPageIndexChanging="gvInvoiceMaster_PageIndexChanging">
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                    <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                    <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                    <Columns>
                                        <asp:BoundField HeaderText="Purchase Order" DataField="PURCHASE_ORDER_NO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Model Name" DataField="MODEL_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Invoice No" DataField="INVOICE_NO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Invoice Date" DataField="INVOICE_DATE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Invoice Qty" DataField="INVOICE_QTY"
                                            ItemStyle-Width="300px" HeaderStyle-Width="100px"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                        <asp:BoundField HeaderText="Invoice Box Size" DataField="INVOICE_BOX_SIZE"
                                            ItemStyle-Width="300px" HeaderStyle-Width="100px"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                        <asp:BoundField HeaderText="Shipping Address" DataField="ADDRESS" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Supplier Code" DataField="SUPPLIER_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Stock Point Note" DataField="STOCK_POINT_NOTE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Shipment Date" DataField="SHIPMENT_DATE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300px" />
                                        
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkEdit" Text=" <img src='../../Images/editGrid.png' alt='Edit this record' />" runat="server" CausesValidation="False" ToolTip="Edit this record" CommandName="EditRecords"
                                                    CommandArgument='<%#Eval("INVOICE_NO")%>' Visible="true">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkDelete" runat="server" Text="<img src='../../Images/deleteGrid.png' alt='Delete this record' />" ToolTip="Delete this record" CausesValidation="False" CommandName="DeleteRecords"
                                                    CommandArgument='<%#Eval("INVOICE_NO")%>' OnClientClick='return confirm("Are you sure, you want to delete this Record?")'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                    <EditRowStyle BackColor="#999999"></EditRowStyle>
                                    <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                </asp:GridView>
                                <asp:HiddenField ID="hidUID" runat="server" />
                                <asp:HiddenField ID="hidUpdate" runat="server" />
                                <asp:HiddenField ID="hidPoOrderID" runat="server" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
