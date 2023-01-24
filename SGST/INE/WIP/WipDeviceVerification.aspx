<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WipDeviceVerification.aspx.cs" Inherits="SGST.INE.WIP.WipDeviceVerification" %>
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
            <h1>Device vs GB Comparison</h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Device vs GB Comparison </h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>FG Item Code : </label>
                                <asp:DropDownList ID="ddlModel_Name" runat="server" class="form-control select2"
                                    Style="width: 100%;" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                             <div class="form-group">
                                <label>Scan Pcb Barcode : </label>
                                <asp:TextBox ID="txtScanHere" runat="server" placeholder="Enter Barcode" autocomplete="off" class="form-control"
                                    Style="width: 100%;" MaxLength="100" OnTextChanged="txtScanHere_TextChanged" AutoPostBack="true">                                  
                                </asp:TextBox>
                            </div>
                           <div class="form-group">
                                <label>Scan Device QR Code: </label>
                                <asp:TextBox ID="txtDeviceQR" runat="server" placeholder=" Enter Device QR Code" autocomplete="off" class="form-control"
                                    Style="width: 100%;" MaxLength="1000" OnTextChanged="txtScanDeviceQR_TextChanged" AutoPostBack="true">                                  
                                </asp:TextBox>
                            </div>
                           
                            <div class="form-group">
                                <label>Scan GB QR Code : </label>
                                <asp:TextBox ID="txtGBQRCode" runat="server" placeholder="Enter GB QR Code" autocomplete="off" class="form-control"
                                    Style="width: 100%;" MaxLength="1000" OnTextChanged="txtScanGBQR_TextChanged" AutoPostBack="true">                                  
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                            <div class="row">
                        <div class="col-md-6">                           
                            <div class="col-xs-4">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" class="btn btn-primary btn-block btn-flat" />
                            </div>
                        </div>
                    </div>
                   
                </div>
            </div>
        </section>
    </div>
</asp:Content>
