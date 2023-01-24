<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" 
    CodeBehind="SolderPast_Scanning.aspx.cs" 
    Inherits="SGST.INE.WIP.SolderPast_Scanning" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>
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
            <h1 id="head1" runat="server">SOLDER PASTE STORAGE SCANNING
            </h1>
        </section>
        <!-- Main content -->
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Scan Details</h3>
                </div>
                <div class="box-body">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Scan Machine ID/Work Station ID : </label>
                            <asp:TextBox ID="txtMachineiD" runat="server" class="form-control" Style="width: 100%;"
                                autocomplete="off" AutoPostBack="true" OnTextChanged="txtProcessName_TextChanged"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Machine Name/ Work Station Name :</label>
                            <asp:Label ID="lblMachineName" runat="server" Text="" Style="display: inline-block; color: Blue; font-size: 35px; height: 35px; width: 100%;"></asp:Label>
                             <label>Model Name/ Process Name : </label>
                            <asp:Label ID="lblModelName" runat="server" Visible="true" Style="display: inline-block; color: Blue; font-size: 35px; height: 35px; width: 100%;"></asp:Label><br />
                            <asp:Label ID="lblMachineSeq" runat="server" Visible="false" ></asp:Label><br />
                        </div>

                        <div class="form-group">
                            <label>Process Type : </label>
                            <asp:DropDownList ID="ddlProcessType" runat="server" class="form-control">
                                <asp:ListItem Text="Select" Value="-1">--Select Process Type--</asp:ListItem>
                                <asp:ListItem Text="In" Value="0">In</asp:ListItem>
                                <asp:ListItem Text="Out" Value="1">Out</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>FG Item Code : </label>
                            <asp:DropDownList ID="drpFGItemCode"
                                runat="server" class="form-control select2" AutoPostBack="true"
                                Style="width: 100%;">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Scan Solder Paste Barcode  : </label>
                            <asp:TextBox ID="txtRELLID" runat="server" class="form-control" placeholder="PART BARCODE"
                                autocomplete="off" AutoPostBack="true" OnTextChanged="txtRELLID_TextChanged">
                            </asp:TextBox>
                        </div>
                        <div class="col-xs-4">
                            <asp:Button ID="btnReset" runat="server" class="btn btn-primary btn-block btn-flat"
                                Text="Reset" Visible="true" OnClick="btnReset_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-default">
            </div>
        </section>
    </div>
</asp:Content>
