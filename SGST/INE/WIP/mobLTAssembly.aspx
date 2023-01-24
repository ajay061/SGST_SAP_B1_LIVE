<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="mobLTAssembly.aspx.cs" Inherits="SGST.INE.MOB.mobLTAssembly" EnableEventValidation="false" %>

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
                <h4><i class="icon fa fa-ban"></i>Alert!</h4>
            </div>
            <div id='msgwarning' runat='server' style="display: none;" class="alert alert-info alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4><i class="icon fa fa-info"></i>Alert!</h4>
            </div>
            <div id='msginfo' runat='server' style="display: none;" class="alert alert-warning alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4><i class="icon fa fa-warning"></i>Alert!</h4>
            </div>
            <div id='msgsuccess' runat='server' style="display: none;" class="alert alert-success alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    &times;</button>
                <h4>
                    <i class="icon fa fa-check"></i>Alert!</h4>
            </div>
        </div>
        <section class="content-header">
            <h1>LIFE TESTING</h1>
        </section>
        <section class="content">
            <div class="box box-default">
                <div class="box-body">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label>Scan Machine ID/Work Station ID : </label>
                            <asp:TextBox ID="txtScanMachineID" runat="server"
                                class="form-control" Style="width: 100%;" Height="35"
                                OnTextChanged="txtScanMachineID_TextChanged"
                                autocomplete="off" AutoPostBack="true">
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
                                runat="server" class="form-control select2"
                                OnSelectedIndexChanged="drpFGItemCode_SelectedIndexChanged"
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
                            <label>Type : </label>
                            <asp:DropDownList ID="drpType" runat="server" class="form-control"
                                AutoPostBack="true" OnSelectedIndexChanged="drpType_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">IN</asp:ListItem>
                                <asp:ListItem Value="2">OUT</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Lot Size : </label>
                            <asp:Label ID="lblLotSize" Enabled="false" runat="server" autocomplete="off"
                                class="form-control" Style="width: 100%;" MaxLength="50">                                  
                            </asp:Label>
                        </div>
                        <div class="form-group">
                            <label>Lot Ref No : </label>
                            <asp:Label ID="lblLotRefNo" Enabled="false" runat="server" autocomplete="off"
                                class="form-control" Style="width: 100%;" MaxLength="50">                                  
                            </asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-default" runat="server" id="dvIn">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <label>Please scan PCB to create lot as per define data  </label>
                            <div class="form-group" id="divPCB" runat="server">
                                <label>Scan SN/RefNo : </label>
                                <asp:TextBox ID="txtPCBID" runat="server" class="form-control" AutoPostBack="true"
                                    placeholder="Scan sn" autocomplete="off" OnTextChanged="txtPCBID_TextChanged"> </asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="box box-default" runat="server" id="dvOut">
                <div class="box-body">
                    <div class="box-header with-border">
                        <h3 class="box-title">Scan PCB Details</h3>
                    </div>
                    <div class="col-md-12">
                        <label>Please scan any one barcode of lot </label>
                        <div class="form-group">
                            <label>Scan PCB : </label>
                            <asp:TextBox ID="txtOutPCBLotBarcode" runat="server" class="form-control" AutoPostBack="true"
                                placeholder="Scan PCB ID" autocomplete="off" MaxLength="100" OnTextChanged="txtOutPCBLotBarcode_TextChanged"> </asp:TextBox>
                        </div>
                    </div>
                    <div runat="server" visible="false">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Work Order No. : </label>
                                <asp:Label ID="lblWorkOrderNo" runat="server" class="form-control"> </asp:Label>
                            </div>
                            <div class="form-group">
                                <label>Lot No : </label>
                                <asp:Label ID="lblRefNo" runat="server" class="form-control"> </asp:Label>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Total PCB Generated : </label>
                                <asp:Label ID="lblTotalPCB" runat="server" Style="width: 100%;" Height="35" Font-Size="35px"> </asp:Label>
                                <br />
                                <label>Left PCB Count(Last Scanned Stage) : </label>
                                <asp:Label ID="lblLeftPCB" runat="server" Style="width: 100%;" Height="35" Font-Size="35px"> </asp:Label>
                                <br />
                                <label>Scanned PCB Count : </label>
                                <asp:Label ID="lblScannedPCBCount" runat="server" Style="width: 100%;" Height="35" Font-Size="35px"> </asp:Label>
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
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Rework Station ID : </label>
                                    <asp:DropDownList ID="drpstation" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Select Defect  : </label>
                                    <asp:DropDownList ID="drpDefect" runat="server" class="form-control select2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Observations : </label>
                                    <asp:TextBox ID="txtobservation" runat="server" class="form-control" AutoPostBack="false"
                                        onkeydown="return (event.keyCode!=13)" placeholder="Enter Observation"
                                        autocomplete="off" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>PCB Status : </label>
                                    <asp:DropDownList ID="drpStatus" class="form-control select2" runat="server"
                                        AutoPostBack="true" OnSelectedIndexChanged="drpStatus_SelectedIndexChanged">
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
                </div>

            </div>
            <div class="box box-default">
                <div class="box-body">
                    <div class="row">
                        <div class="form-group">
                            <div class="col-lg-3 col-xs-4">
                                <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary btn-block btn-flat"
                                    OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box-body" runat="server" visible="false">
                    <div class="row">
                        <div class="form-group">
                            <div class="col-lg-3 col-xs-4">
                                <asp:Button ID="btnOK" runat="server" class="btn btn-primary btn-block btn-flat"
                                    Text="OK" OnClick="btnSave_Click" />
                            </div>
                            <div class="col-lg-3 col-xs-4">
                                <asp:Button ID="btnReject" runat="server" class="btn btn-primary btn-block btn-flat"
                                    Text="Reject" OnClick="btnReject_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
