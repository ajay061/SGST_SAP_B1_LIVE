<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Putaway.aspx.cs"
    Inherits="SGST.INE.RM.Putaway" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function PreventPostback(sender, eventArgs) {
            if (eventArgs.get_newValue() == "")
                eventArgs.set_cancel(true);
        }
    </script>
    <div class="content-wrapper">

        <section class="content-header">
            <h1>Putaway To RM       
            </h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Scan Received Items(For OK/Reject WH LOC)</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Scan Barcode To Get Location : </label>
                                <asp:TextBox ID="txtGetLocation" runat="server"
                                    class="form-control" placeholder="Scan Barcode To Get Location"
                                    autocomplete="off" OnTextChanged="txtGetLocation_TextChanged"
                                    AutoPostBack="true"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Suggested Location : </label>
                                <asp:Label ID="txtSuggestedLocation" runat="server"></asp:Label>
                                <br />
                                <label>Location Type : </label>
                                <asp:Label ID="lblLocationType" runat="server"></asp:Label>
                            </div>
                            <asp:HiddenField runat="server" ID="hidLocationType" />
                            <%--<div class="form-group">
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-default">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Scan Location : </label>
                                <asp:TextBox ID="txtLocation" runat="server" class="form-control"
                                    OnTextChanged="txtLocation_TextChanged"
                                    placeholder="Scan Location" autocomplete="off"
                                    AutoPostBack="true"></asp:TextBox>
                            </div>
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
                            <div class="form-group">
                                <label>Scan Barcode : </label>
                                <asp:TextBox ID="txtBarcode" runat="server"
                                    class="form-control" placeholder="Scan Barcode"
                                    autocomplete="off" OnTextChanged="txtBarcode_TextChanged"
                                    AutoPostBack="true"></asp:TextBox>
                                <br />
                                <label>Last Scanned : </label>
                                <asp:Label ID="lblLastScanned" runat="server"
                                    TextMode="MultiLine" Wrap="True"></asp:Label>
                                <br />
                                <label>Batch No : </label>
                                <asp:Label ID="lblBatchNo" runat="server"></asp:Label>
                                <br />
                                <label>Remaining Item for Putaway : </label>
                                <asp:Label ID="lblPendingCount" runat="server"></asp:Label>
                                <br />
                                <label>Scan Counter : </label>
                                <asp:Label ID="lblCounter" Text="0" runat="server"></asp:Label>
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
