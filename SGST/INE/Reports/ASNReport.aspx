<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ASNReport.aspx.cs" Inherits="SGST.INE.Reports.ASNReport" %>

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
            <h1>
                <asp:Label ID="lblHeader" runat="server"></asp:Label>
            </h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Enter Invoice No : </label>
                                <asp:TextBox ID="txtPONO" runat="server"
                                    class="form-control" Style="width: 100%;">                                    
                                </asp:TextBox>
                            </div>
                            <div class="form-group" id="LotNo" runat="server">
                                <label>Enter Lot No. : </label>
                                <asp:TextBox ID="txtEnterLotNo" runat="server"
                                    class="form-control" Style="width: 100%;">                                    
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Download Mode : </label>
                                <asp:DropDownList ID="drpDownloadMode" runat="server"
                                    class="form-control" Style="width: 100%;">
                                    <asp:ListItem>XLS</asp:ListItem>
                                    <asp:ListItem>CSV</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnShow" runat="server" Text="Show Report" class="btn btn-primary btn-block btn-flat"
                        Width="20%" OnClick="btnShow_Click" />
                </div>
                <div class="form-group">
                    <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary btn-block btn-flat"
                        Width="20%" OnClick="btnReset_Click" />
                </div>
            </div>
        </section>
    </div>
</asp:Content>
