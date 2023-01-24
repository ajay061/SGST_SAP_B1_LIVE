<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="MasterReports.aspx.cs"
    Inherits="SGST.INE.Reports.MasterReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function ShowMessage() {
            setTimeout(function () {
                $("#ModalMsg").modal('show');
            }, 100);
        }
    </script>
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
            <h1>Master Data Report  
            </h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Master Data Report</h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Type</label>
                                <asp:DropDownList ID="drpType" runat="server" OnSelectedIndexChanged="drpType_SelectedIndexChanged" class="form-control select2" Style="width: 100%;" AutoPostBack="true">
                                    <asp:ListItem Text="--Select--"> </asp:ListItem>
                                    <asp:ListItem Text="Item BOM Master Report"> </asp:ListItem>
                                    <asp:ListItem Text="Item Master Data Report">  </asp:ListItem>
                                    <asp:ListItem Text="Item Quality Parameters Report">  </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="SearchHeader" runat="server">
                                    </asp:Label></label>
                                <asp:DropDownList ID="drpSearchData" runat="server"
                                    class="form-control select2" Style="width: 100%;" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnShow" runat="server" Text="Show Report" class="btn btn-primary btn-block btn-flat"
                        Width="20%" OnClick="btnShow_Click" />
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
                    <button type="button" class="btn-block btn-flat" style="height: auto; color: black; font-size: medium; background-color: ButtonFace"
                        data-dismiss="modal" aria-hidden="true">
                        Close</button>
                </div>
                <h1>
                    <p style="text-align: center;">
                        <asp:Label ID="lblHeader" runat="server"></asp:Label>
                    </p>
                </h1>
                <div class="box-body">
                    <div class="form-group">
                        <asp:Button ID="btnDownloadData" runat="server" Text="EXPORT TO EXCEL" 
                            class="btn btn-primary btn-block btn-flat" OnClick="btnDownloadData_Click" />
                    </div>
                </div>
                <div class="modal-body text-center">
                    <asp:GridView ID="gvData" CssClass="display compact"
                        runat="server">
                        <EmptyDataTemplate><b style="color: red">No Record Available</b></EmptyDataTemplate>
                        <EditRowStyle BackColor="#999999"></EditRowStyle>
                        <FooterStyle BackColor="#80ccff" Font-Bold="True" ForeColor="White"></FooterStyle>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
