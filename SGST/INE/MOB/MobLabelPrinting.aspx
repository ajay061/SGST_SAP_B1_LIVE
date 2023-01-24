<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="MobLabelPrinting.aspx.cs" Inherits="SGST.INE.MOB.MobLabelPrinting" EnableEventValidation="false" %>

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
        <section class="content-header">
            <h1>Printing</h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblHeader" runat="server"></asp:Label></h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Line Code : </label>
                                <asp:Label ID="lblLineCode" runat="server" Style="width: 100%;" 
                                    Height="35" Font-Size="35px" ForeColor="Blue">
                                </asp:Label>
                            </div>
                            <div class="form-group">
                                <label>FG Item Code :  </label>
                                <asp:DropDownList ID="drpFGItemCode" runat="server" AutoPostBack="true"
                                    autocomplete="off" class="form-control select2" Style="width: 100%;" MaxLength="50"
                                    OnSelectedIndexChanged="drpFGItemCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label>Model Name : </label>
                                <asp:Label ID="lblModelName" Enabled="false" runat="server" autocomplete="off"
                                    class="form-control" Style="width: 100%;" MaxLength="50">                                  
                                </asp:Label>
                                <br />
                                <label>Model Type : </label>
                                <asp:Label ID="lblModelType" Enabled="false" runat="server" autocomplete="off"
                                    class="form-control" Style="width: 100%;" MaxLength="50">                                  
                                </asp:Label>
                            </div>
                            <div class="form-group" id="dvPrintergrup" runat="server">
                                <label>Printer : </label>
                                <asp:DropDownList ID="ddlprinter" runat="server" class="form-control select2" Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Scan Pcb Barcode : </label>
                                <asp:TextBox ID="txtScanHere" runat="server" placeholder="Enter Barcode" autocomplete="off" class="form-control"
                                    Style="width: 100%;" MaxLength="100" OnTextChanged="txtScanHere_TextChanged" AutoPostBack="true">                                  
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Last Scanned : </label>
                                <asp:Label ID="lbllastscanned" runat="server" placeholder="Enter Last Scanned" autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="50">                                  
                                </asp:Label>
                            </div>
                            <div class="col-md-12" style="margin-top: 10%;">
                                <div class="form-group">
                                    <div class="col-xs-4">
                                        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" class="btn btn-primary btn-block btn-flat" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hidModelType" runat="server" />
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
