<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="QCLevelVerification.aspx.cs" Inherits="SGST.INE.WIP.QCLevelVerification" %>
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
            <h1>QC Verification</h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">QC Verification</h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="box-body">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>FG Item Code : </label>
                            <asp:DropDownList ID="ddlModel_Name" runat="server" class="form-control select2"
                                    Style="width: 100%;" AutoPostBack="true">
                                </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Select Defect : </label>
                            <asp:DropDownList ID="drpDefect" class="form-control select2" runat="server" AutoPostBack="true">
                            </asp:DropDownList>
                           
                        </div>
                        <div class="form-group">
                            <label>Rework Station ID : </label>
                            <asp:DropDownList ID="drpstation"
                                runat="server" class="form-control select2" Style="width: 100%;">
                              <asp:ListItem>PACKING REPAIR</asp:ListItem>
                                
                            </asp:DropDownList>
                           
                        </div>
                      
                        <div class="form-group">
                            <label>Scan PCB : </label>
                            <asp:TextBox ID="txtPCBID" runat="server" class="form-control" 
                                placeholder="Scan PCB ID" autocomplete="off" MaxLength="100"> </asp:TextBox>
                        </div>
                        <div class="col-xs-4">
                            <asp:Button ID="btnReject" runat="server" class="btn btn-primary btn-block btn-flat"
                                Text="Reject"  OnClick="btnQcVerification_Click" />
               
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
