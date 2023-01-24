<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="RMSupplierReturn.aspx.cs" Inherits="SGST.INE.RM.RMSupplierReturn" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function PreventPostback(sender, eventArgs) {
            if (eventArgs.get_newValue() == "")
                eventArgs.set_cancel(true);
        }
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
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1 id="head1" runat="server">Supplier Return    
            </h1>
        </section>
        <!-- Main content -->
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Enter Details</h3>

                </div>
                <div class="box-header with-border">
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <label for="orderNo">Material Document No. : </label>
                            <div>
                                <asp:DropDownList ID="drpOrderNumber" runat="server"
                                    class="form-control select2" Style="width: 100%;"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="drpOrderNumber_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group col-md-6">
                            <label for="ItemCode">Item Code : </label>
                            <div>
                                <asp:DropDownList ID="drpItemCode" runat="server" AutoPostBack="true"
                                    class="form-control select2" Style="width: 100%;"
                                    OnSelectedIndexChanged="drpItemCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group col-md-6">
                            <label for="ItemCode">Item Line No : </label>
                            <div>
                                <asp:DropDownList ID="drpItemLineNo" runat="server"
                                    AutoPostBack="true" class="form-control select2" Style="width: 100%;"
                                    OnSelectedIndexChanged="drpItemLineNo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-default">
                <div class="box-header with-border">
                    <div class="row">
                        <hr />
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="gvSupplierReturn" runat="server" class="table table-striped table-bordered table-hover GridPager" AutoGenerateColumns="false"
                                    AllowPaging="true" PageSize="10" OnPageIndexChanging="gvSupplierReturn_PageIndexChanging" AllowSorting="true">
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                    <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                    <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                    <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                    <EditRowStyle BackColor="#999999"></EditRowStyle>
                                    <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                    <Columns>
                                        <asp:BoundField HeaderText="SR_ID" DataField="SR_ID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100" Visible="false" />
                                        <asp:BoundField HeaderText="Quantity" DataField="QTY" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100" />
                                        <asp:BoundField HeaderText="Return Qty" DataField="JOB_ISSUED_QTY" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100" />
                                        <asp:BoundField HeaderText="Description" DataField="PART_DESC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-default">
                <div class="box-header with-border">
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <label for="Location">Scan Location : </label>
                            <asp:TextBox ID="txtLocation" runat="server" class="form-control"
                                AutoPostBack="true" placeholder="Scan Location" AutoCompleteType="Disabled"
                                OnTextChanged="txtLocation_TextChanged"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-6">
                            <label for="Barcode">Scan Barcode : </label>
                            <asp:TextBox ID="txtBarcode" runat="server" class="form-control"
                                AutoPostBack="true" placeholder="Scan Barcode"
                                AutoCompleteType="Disabled"
                                OnTextChanged="txtBarcode_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-md-4">
                            <div class="col-xs-4">
                                <asp:Button ID="btnReset" runat="server"
                                    class="btn btn-primary btn-block btn-flat"
                                    Text="Reset" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
