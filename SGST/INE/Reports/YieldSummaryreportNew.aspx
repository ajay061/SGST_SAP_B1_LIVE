<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="YieldSummaryreportNew.aspx.cs"
    Inherits="SGST.INE.Reports.YieldSummaryreportNew" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        function ShowMessage() {
            setTimeout(function () {
                $("#ModalMsg").modal('show');
            }, 100);

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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <section class="content-header">
            <h1>Yield Summary Report      
            </h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">DPR/YIELD REPORT (SUMMARY)</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Report Type:</label>
                                <asp:DropDownList ID="drpReportType" runat="server" class="form-control select2"
                                    Style="width: 100%;">
                                    <asp:ListItem>Summary</asp:ListItem>
                                    <asp:ListItem>Details</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>FG Item Code:</label>
                                <asp:DropDownList ID="drpFGItemCode" runat="server" class="form-control select2"
                                    Style="width: 100%;" AutoPostBack="true" OnSelectedIndexChanged="drpFGItemCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Machine ID:</label>
                                <asp:DropDownList ID="drpMachine" runat="server" class="form-control select2"
                                    Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>From Date:</label>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtFromDate" runat="server" class="form-control"
                                        data-inputmask="'alias': 'yyyy-mm-dd'" data-mask Style="width: 100%;" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>To Date:</label>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtToDate" runat="server" class="form-control" data-inputmask="'alias': 'yyyy-mm-dd'" data-mask Style="width: 100%;" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Enter Work Order No:</label>
                                <asp:TextBox ID="txtWorkOrderNo" runat="server" class="form-control"                                    
                                    autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnShow" runat="server" Text="Show Report"
                            class="btn btn-primary btn-block btn-flat" Width="20%" OnClick="btnShow_Click" />
                    </div>
                </div>
            </div>
        </section>

    </div>
    <div id="ModalMsg" class="modal fade">
        <div class="modal-dialog modal-confirm">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="icon-box">
                        <i class="la la-check"></i>
                    </div>
                    <button type="button" class="btn-block btn-flat" style="height: 40px; width: 100px; color: black; font-size: medium; background-color: ButtonFace"
                        data-dismiss="modal" aria-hidden="true">
                        << Back</button>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" WaitMessageFont-Size="14pt" Width="100%" Height="100%" AsyncRendering="False" SizeToReportContent="True"></rsweb:ReportViewer>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


