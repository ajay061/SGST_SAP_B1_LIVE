<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master"
    AutoEventWireup="true" CodeBehind="PCBScanningNew.aspx.cs" EnableEventValidation="false"
    Inherits="SGST.INE.WIP.PCBScanningNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function PreventPostback(sender, eventArgs) {
            if (eventArgs.get_newValue() == "")
                eventArgs.set_cancel(true);
        }
    </script>
    <div class="content-wrapper">
        <section class="content-header">
            <h1>PCB SCANNING</h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Scan Details</h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="box-body">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Line Code : </label>
                            <asp:Label ID="lblLineCode" runat="server" Style="width: 100%;" Height="35" Font-Size="35px" ForeColor="Blue">
                            </asp:Label>
                        </div>
                        <div class="form-group">
                            <label>Scan Machine ID/ Work Station ID : </label>
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
                            <label>Select Loading Area : </label>
                            <asp:DropDownList ID="drpTOPBottom"
                                runat="server" class="form-control select2" Style="width: 100%;">
                                <asp:ListItem>TOP</asp:ListItem>
                                <asp:ListItem>BOT</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>FG Item Code : </label>
                            <asp:DropDownList ID="drpFGItemCode"
                                runat="server" class="form-control select2" AutoPostBack="true"
                                OnSelectedIndexChanged="drpFGItemCode_SelectedIndexChanged" Style="width: 100%;">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Process : </label>
                            <asp:DropDownList ID="drpType" runat="server"
                                class="form-control" placeholder="Select Type" AutoPostBack="true"
                                autocomplete="off" OnSelectedIndexChanged="drpType_SelectedIndexChanged">
                                <asp:ListItem Value="In" Text="In"></asp:ListItem>
                                <asp:ListItem Value="Out" Text="Out"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <asp:CheckBox ID="chkRepairRequired" runat="server" Text="User decision to send in repair" OnCheckedChanged="chkRepairRequired_CheckedChanged" AutoPostBack="true"
                                class="form-control" Visible="false" Checked="true"></asp:CheckBox>
                            <br />
                            <asp:CheckBox ID="chkPassedFailedBarcode" runat="server" Text="User decision to pass M/C rejection"
                                class="form-control"></asp:CheckBox>
                            <br />
                            <asp:CheckBox ID="chkSamplePCB" runat="server" Text=" Manual PCB Picked for Loader " AutoPostBack="true"
                                class="form-control"></asp:CheckBox>
                        </div>
                        <div class="form-group" id="diRework" runat="server">
                            <label>Rework Station ID : </label>
                            <asp:DropDownList ID="drpstation" runat="server" class="form-control">
                            </asp:DropDownList>
                            <asp:Label ID="lblAOIMessage" Text="Please scan only rejected slaves of pannel" runat="server"></asp:Label>
                            <br />
                            <label>Select Defect : </label>
                            <asp:DropDownList ID="drpDefect" class="form-control select2" runat="server" AutoPostBack="true">
                            </asp:DropDownList>
                            <label>Remarks : </label>
                            <asp:TextBox ID="txtRemarks" runat="server"
                                class="form-control" placeholder="Enter remarks" Style="width: 100%;" Height="35"
                                autocomplete="off">
                            </asp:TextBox>
                        </div>
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
                        <div class="form-group">
                            <label>Scan Ref Barcode :(if any) </label>
                            <asp:TextBox ID="txtRefBarcode" runat="server" class="form-control" AutoPostBack="true"
                                placeholder="Scan ref Barcode" autocomplete="off" MaxLength="100"
                                OnTextChanged="txtRefBarcode_TextChanged"> </asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Scan Tool Barcode(if any) : </label>
                            <asp:TextBox ID="txtToolBarcode" runat="server" class="form-control" AutoPostBack="true"
                                placeholder="Scan Tool ID" autocomplete="off" MaxLength="100"
                                OnTextChanged="txtToolBarcode_TextChanged"> </asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Scan PCB : </label>
                            <asp:TextBox ID="txtPCBID" runat="server" class="form-control" AutoPostBack="true"
                                placeholder="Scan PCB ID" autocomplete="off" MaxLength="100" OnTextChanged="txtPCBID_TextChanged"> </asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label>Scan Count : </label>
                            <asp:Label ID="lblCount" runat="server" class="form-control"> </asp:Label>
                        </div>
                        <div class="col-xs-4">
                            <asp:Button ID="btnOk" runat="server" class="btn btn-primary btn-block btn-flat"
                                Text="OK" Visible="true" OnClick="btnOk_Click" />
                        </div>
                          <div class="col-xs-4">
                            <asp:Button ID="btnReject" runat="server" class="btn btn-danger btn-block btn-flat"
                                Text="Reject" Visible="true" OnClick="btnReject_Click" />
                        </div>
                        <div class="col-xs-4">
                          
                            <asp:Button ID="btnReset" runat="server" class="btn btn-primary btn-block btn-flat"
                                Text="Reset" OnClick="btnReset_Click" Visible="true" />

                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
