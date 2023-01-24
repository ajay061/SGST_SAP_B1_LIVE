<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Quality_MarketReturn.aspx.cs" Inherits="SGST.INE.FG.Quality_MarketReturn"
    EnableEventValidation="false" %>

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
            <h1>FG </h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    MARKET RETURN QUALITY
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Scan Box Barcode : </label>
                                <asp:TextBox ID="txtBoxID" runat="server" class="form-control" placeholder="Scan Barcode"
                                    autocomplete="off"
                                    AutoPostBack="true" OnTextChanged="txtBoxID_TextChanged"></asp:TextBox>
                            </div>
                             <div class="form-group">
                                <label>Remarks : </label>
                                <asp:TextBox ID="txtRemarks" runat="server" class="form-control" placeholder="Enter Remarks"
                                    autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Observation : </label>
                                <asp:TextBox ID="txtObservation" runat="server" class="form-control" placeholder="Enter Observation"
                                    autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnOK" runat="server" class="btn btn-primary btn-block btn-flat" Text="OK" OnClick="btnOK_Click" />
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnReject" runat="server" class="btn btn-primary btn-block btn-flat" Text="Reject" OnClick="btnReject_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
