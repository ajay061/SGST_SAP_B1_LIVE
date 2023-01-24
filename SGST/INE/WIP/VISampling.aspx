<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" 
    CodeBehind="VISampling.aspx.cs" Inherits="SGST.INE.WIP.VISampling" %>
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
            <h1>VISUAL INSPECTION QUALITY</h1>
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
                                <label>Scan Machine ID/Work Station ID : </label>
                                <asp:TextBox ID="txtScanMachineID" runat="server"
                                    class="form-control" Style="width: 100%;" Height="35"
                                    OnTextChanged="txtScanMachineID_TextChanged" autocomplete="off" AutoPostBack="true">
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Machine Name/ Work Station Name : </label>
                                <asp:Label ID="lblMachineName" runat="server" Style="width: 100%;" Height="35" Font-Size="35px" ForeColor="Blue">
                                </asp:Label>
                                <label>Model Name/ Process Name : </label>
                                <asp:Label ID="lblModelNo" runat="server" Style="width: 100%;" Height="35" Font-Size="35px" ForeColor="Blue">
                                </asp:Label>
                            </div>
                           
                            <div class="form-group">
                                <label>FG Item Code : </label>
                                <asp:DropDownList ID="drpFGItemCode"
                                    runat="server" class="form-control select2" 
                                    OnSelectedIndexChanged="drpFGItemCode_SelectedIndexChanged" AutoPostBack="true"
                                    Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group" style ="display :none">
                                <label>PDI Insepction box </label>
                                <asp:CheckBox ID="chkPDIInspection"
                                    runat="server" class="form-control" Text ="PDI Inspection Box"
                                    Style="width: 100%;">
                                </asp:CheckBox>
                            </div>
                            <div class="col-md-6"> 
                            <div class="form-group">
                               <label>Total PCB : </label>
                                <asp:Label ID="lblTotalPCB" runat="server" Style="width: 100%;" Height="35" Font-Size="35px" ForeColor="Blue">
                                </asp:Label>
                                </div>
                        </div> 
                            <div class="col-md-6">
                            <div class="form-group">
                                <label>Sampling pcb quantity : </label>
                                <asp:Label ID="lblPDquantityPCB" runat="server" Style="width: 100%;" Height="35" Font-Size="35px" ForeColor="Blue">
                                </asp:Label>
                              </div>
                        </div>
                            <div class="col-md-6">
                            <div class="form-group">
                              <label>Sampling PCB : </label>
                                <asp:Label ID="lblSamplingPCB" runat="server" Style="width: 100%;" Height="35" Font-Size="35px" ForeColor="Blue">
                                </asp:Label>
                            </div>
                           </div>
                            <div class="col-md-6">
                            <div class="form-group">
                                 <label>Sampling PDI : </label>
                                <asp:Label ID="lblSamplingPDIPCB" runat="server" Style="width: 100%;" Height="35" Font-Size="35px" ForeColor="Blue">
                                </asp:Label>
                            </div>
                        </div>
                            <div class="form-group">
                                <label>Scan Barcode : </label>
                                <asp:TextBox ID="txtReelID" runat="server" class="form-control"
                                    placeholder="Scan Barcode" autocomplete="off" OnTextChanged="txtReelID_TextChanged"
                                    AutoPostBack="true">
                                </asp:TextBox>
                            </div>                                                        
                            <div class="col-xs-3">
                                <asp:Button ID="btnReset" runat="server" class="btn btn-primary btn-block btn-flat"
                                     Text="Reset" OnClick="btnReset_Click" />
                            </div>
                        </div>                       
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
