<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="WIPFQAScanning.aspx.cs" Inherits="SGST.INE.WIP.WIPFQAScanning" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function PreventPostback(sender, eventArgs) {
            if (eventArgs.get_newValue() == "")
                eventArgs.set_cancel(true);
        }
    </script>
    <div class="content-wrapper">
        <section class="content-header">
            <h1>FQA TESTING</h1>
        </section>
        <!-- Main content -->
        <section class="content">
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
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Scan Machine Details</h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="box-body">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label>Scan Machine ID/Work Station ID : </label>
                            <asp:TextBox ID="txtScanMachineID" runat="server"
                                class="form-control" Style="width: 100%;" Height="35"
                                OnTextChanged="txtScanMachineID_TextChanged" autocomplete="off" AutoPostBack="true">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Machine Name/ Work Station Name : </label>
                            <asp:Label ID="lblMachineName" runat="server" Style="width: 100%;" Height="35" Font-Size="35px" ForeColor="Blue">
                            </asp:Label>
                        </div>
                        <div class="form-group">
                            <label>FG Item Code : </label>
                            <asp:DropDownList ID="drpFGItemCode"
                                runat="server" class="form-control select2" OnSelectedIndexChanged="drpFGItemCode_SelectedIndexChanged"
                                Style="width: 100%;" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Model Name/ Process Name : </label>
                            <asp:Label ID="lblModelNo" runat="server" Style="width: 100%;" Height="35" Font-Size="35px" ForeColor="Blue">
                            </asp:Label>
                        </div>
                        <div class="form-group">
                            <label>Sampling Rate(%) : </label>
                            <asp:Label ID="lblSamplingRate" runat="server" class="form-control"> </asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-default">
                <div class="box-body">
                    <div class="box-header with-border">
                        <h3 class="box-title">Create Lot</h3>
                    </div>
                    <div class="col-md-12">
                        <label>Please scan any one barcode of lot </label>
                        <div class="form-group">
                            <label>Scan PCB : </label>
                            <asp:TextBox ID="txtPCBID" runat="server" class="form-control" AutoPostBack="true"
                                placeholder="Scan PCB ID" autocomplete="off" MaxLength="100" OnTextChanged="txtPCBID_TextChanged"> </asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-default">
                <div class="box-body">
                    <div class="box-header with-border">
                        <h3 class="box-title">Scan PCB Details</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <label>Please scan all the pcb as per defined sampling rate </label>
                        <div class="form-group">
                            <label>Scan PCB from lot sampling : </label>
                            <asp:TextBox ID="txtPCBLotBarcode" runat="server" class="form-control" AutoPostBack="true"
                                placeholder="Scan PCB ID" autocomplete="off" MaxLength="100" OnTextChanged="txtPCBLotBarcode_TextChanged"> </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Work Order No. : </label>
                            <asp:Label ID="lblWorkOrderNo" runat="server" class="form-control"> </asp:Label>
                        </div>
                        <div class="form-group">
                            <label>Lot No : </label>
                            <asp:Label ID="lblRefNo" runat="server" class="form-control"> </asp:Label>
                        </div>
                        <div class="form-group">
                            <label>Total PCB Generated : </label>
                            <asp:Label ID="lblTotalPCB" runat="server" Style="width: 100%;" Height="35" Font-Size="35px"> </asp:Label>
                            <br />
                            <br />
                            <label>Left PCB Count(Last Scanned Stage) : </label>
                            <asp:Label ID="lblLeftPCB" runat="server" Style="width: 100%;" Height="35" Font-Size="35px"> </asp:Label>
                        </div>

                        <div class="form-group">
                            <label>Select Defect : </label>
                            <asp:DropDownList ID="drpDefect" class="form-control select2" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Scanned PCB Count : </label>
                            <asp:Label ID="lblScannedPCBCount" runat="server" Style="width: 100%;" Height="35" Font-Size="35px"> </asp:Label>
                        </div>
                        <div class="form-group">
                            <label>Enter Observation : </label>
                            <asp:TextBox ID="txtObservation" runat="server" class="form-control"
                                placeholder="Enter Observation" autocomplete="off" MaxLength="100"> </asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>PCB Status : </label>
                            <asp:DropDownList ID="drpStatus" class="form-control select2" runat="server"
                                AutoPostBack="true" OnSelectedIndexChanged="drpPCBStatus_SelectedIndexChanged">
                                <asp:ListItem>--Select--</asp:ListItem>
                                <asp:ListItem>OK</asp:ListItem>
                                <asp:ListItem>Reject</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <asp:GridView ID="gvPCBData" class="table table-bordered table-striped" runat="server"
                                AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField HeaderText="PCB" DataField="PART_BARCODE" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Pcb Master ID" DataField="PCB_MASTER_ID" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Lot No" DataField="REFNO" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Defect" DataField="DEFECT" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Remarks" DataField="REMARKS" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Status" DataField="STATUS" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-default">
                <div class="box-body">
                    <div class="box-header with-border">
                        <h3 class="box-title">Select Final Status</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Rework Station ID : </label>
                            <asp:DropDownList ID="drpstation" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-xs-4">
                            <asp:Button ID="btnOK" runat="server" class="btn btn-primary btn-block btn-flat"
                                Text="OK" OnClick="btnOK_Click" Visible="true" />
                        </div>
                        <div class="col-xs-4">
                            <asp:Button ID="btnReject" runat="server" class="btn btn-primary btn-block btn-flat"
                                Text="Reject" OnClick="btnReject_Click" Visible="true" />
                        </div>
                        <div class="col-xs-4">
                            <asp:Button ID="btnReset" runat="server" class="btn btn-primary btn-block btn-flat"
                                Text="Reset" OnClick="btnReset_Click" Visible="true" />
                        </div>
                    </div>
                    <asp:HiddenField ID="hidPartBarcode" runat="server" />
                    <asp:HiddenField ID="hidPCBmasterID" runat="server" />
                    <asp:HiddenField ID="hidRefNo" runat="server" />
                    <asp:HiddenField ID="hidWorkOrderNo" runat="server" />
                </div>
            </div>
        </section>
    </div>
</asp:Content>
