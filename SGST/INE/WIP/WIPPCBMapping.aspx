<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="WIPPCBMapping.aspx.cs"
    Inherits="SGST.INE.WIP.WIPPCBMapping" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function PreventPostback(sender, eventArgs) {
            if (eventArgs.get_newValue() == "")
                eventArgs.set_cancel(true);
        }
    </script>

    <script>
        function funCheck() {
            var flag = confirm('Do you want to continue');
            var hdnfld = document.getElementById('<%= HiddenField1.ClientID %>');
            hdnfld.value = flag ? '1' : '0';
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
            <h1>PCB BARCODE - MASTER ID MAPPING</h1>

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
                                <label>Select Work Order No. : </label>
                                <asp:DropDownList ID="drpIssueSlipNo" runat="server"
                                    class="form-control" placeholder="Select Reservation slip no"
                                    autocomplete="off" AutoPostBack="true"
                                    OnSelectedIndexChanged="drpIssueSlipNo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Scan Master ID : </label>
                                <asp:TextBox ID="txtBarcode" runat="server"
                                    class="form-control" placeholder="Scan Master Barcode"
                                    autocomplete="off" OnTextChanged="txtBarcode_TextChanged"
                                    AutoPostBack="true"></asp:TextBox>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </div>
                            <div class="form-group">
                                <label>Scan Child Barcode : </label>
                                <asp:TextBox ID="txtScanChildBarcode" runat="server"
                                    class="form-control" placeholder="Scan RH Barcode"
                                    autocomplete="off" OnTextChanged="txtScanChildBarcode_TextChanged"
                                    AutoPostBack="true"></asp:TextBox>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:GridView ID="gvScannedBarcodeData" runat="server" class="table table-bordered table-hover"
                                        AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <ItemStyle Width="2%" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Part Barcode" DataField="PART_BARCODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="500" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="col-md-6">
                            <div class="col-xs-4">
                                <asp:Button ID="btnPrint" runat="server" class="btn btn-primary btn-block btn-flat"
                                    Text="Mapped PCB SN" Visible="true" OnClick="btnPrint_Click" />
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnReset" runat="server" class="btn btn-primary btn-block btn-flat"
                                    Text="Reset" OnClick="btnReset_Click" Visible="true" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>

