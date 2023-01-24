<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Issuetoshopfloor.aspx.cs" Inherits="SGST.INE.RM.Issuetoshopfloor" EnableEventValidation="false" %>

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
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to post data to SAP?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Issue to Shop Floor     
            </h1>
        </section>
        <!-- Main content -->
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Select Issue Details</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Work Order No. : </label>
                                <asp:DropDownList ID="drpIssueSlip" runat="server" class="form-control select2" Style="width: 100%;" AutoPostBack="true" OnSelectedIndexChanged="drpIssueSlip_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>FG Item Code. : </label>
                                <asp:Label ID="lblFGItemCode" runat="server" class="form-control" Style="width: 100%;"></asp:Label>
                            </div>                            
                        </div>
                        <!-- /.col -->
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:DropDownList ID="drpItemCode" runat="server" class="form-control select2" Style="width: 100%;" AutoPostBack="true" Visible="false"></asp:DropDownList>
                            </div>
                            <asp:HiddenField ID="hidPartialQty" runat="server" />
                            <asp:HiddenField ID="hidrm" runat="server" />
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <hr />
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:GridView ID="dvIssue" runat="server" class="table table-striped table-bordered table-hover GridPager"
                                            AutoGenerateColumns="false" AllowPaging="true" PageSize="200" OnRowDataBound="OnRowDataBound"
                                            OnPageIndexChanging="dvIssue_PageIndexChanging" OnRowCommand="dvIssue_RowCommand">
                                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                            <PagerStyle HorizontalAlign="Left" CssClass="pgr" />
                                            <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                            <RowStyle HorizontalAlign="Center" Wrap="true"></RowStyle>
                                            <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                                            <EditRowStyle BackColor="#999999"></EditRowStyle>
                                            <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                                            <Columns>
                                                <asp:BoundField HeaderText="SN" DataField="R_D" HeaderStyle-HorizontalAlign="Left" Visible="false" ItemStyle-HorizontalAlign="Left" />
                                                <asp:BoundField HeaderText="Item Code" DataField="PART_CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                                <asp:BoundField HeaderText="Item Description" DataField="PART_DESC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                                <asp:BoundField HeaderText="Line No" DataField="ITEM_LINE_NO" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                                <asp:BoundField HeaderText="Quantity" DataField="QTY" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                                <asp:BoundField HeaderText="Issued Qty" DataField="ISSUED_QTY" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                                <asp:BoundField HeaderText="Remaining Qty" DataField="REM_QTY" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                                <asp:BoundField HeaderText="Location" DataField="Location" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="300" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button Text="View Location" HeaderText="Location" runat="server" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Scan Location : </label>
                                <asp:TextBox ID="txtLocation" runat="server" class="form-control"
                                    placeholder="Scan Location" AutoPostBack="true" autocomplete="off" MaxLength="50" OnTextChanged="txtLocation_TextChanged"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Scan Material Barcode : </label>
                                <asp:TextBox ID="txtBarcode" runat="server" class="form-control"
                                    placeholder="Scan Barcode" autocomplete="off" OnTextChanged="txtBarcode_TextChanged"
                                    AutoPostBack="true"></asp:TextBox>
                            </div>                            
                        </div>                       
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Button ID="btnReset" runat="server" class="btn btn-primary btn-block btn-flat"
                                    Text="Reset" OnClick="btnReset_Click" />
                                <br />
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnResets" runat="server" class="btn btn-primary btn-block btn-flat"
                                    Text="Clear" Visible="true" OnClick="sReset_Click" />
                            </div>
                            <br />
                            <div class="form-group">
                                <asp:Button ID="btnCompleteSlip" runat="server"
                                    class="btn btn-primary btn-block btn-flat"
                                    Text="Transfer To SAP" OnClick="btnCompleteSlip_Click"
                                    OnClientClick="Confirm()" />

                            </div>
                        </div>
                        <div class="col-md-6">                           
                            <div class="col-xs-4">
                                <asp:Button ID="btnViewLocation" runat="server" class="btn btn-primary btn-block btn-flat"
                                    Text="View" Visible="false" OnClick="btnViewLocation_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
