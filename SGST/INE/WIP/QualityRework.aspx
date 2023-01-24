<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="QualityRework.aspx.cs" Inherits="SGST.INE.WIP.QualityRework" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function PreventPostback(sender, eventArgs) {
            if (eventArgs.get_newValue() == "")
                eventArgs.set_cancel(true);
        }
    </script>
    <!-- Content Wrapper. Contains page content -->
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
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Quality Rework</h1>
        </section>

        <!-- Main content -->
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-default">
                <div class="box-header with-border">
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Type</label>
                                <asp:DropDownList ID="drpType" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpType_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="1">IN</asp:ListItem>
                                    <asp:ListItem Value="2">OUT</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div id="divOut" runat="server" visible="false">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Select Defect</label>
                                    <asp:DropDownList ID="drpDefect" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Observations</label>
                                    <asp:TextBox ID="txtobservation" runat="server" class="form-control" MaxLength="100"
                                        AutoPostBack="false" onkeydown="return (event.keyCode!=13)" placeholder="Enter Observation"
                                        autocomplete="off" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Remarks</label>
                                    <asp:TextBox ID="txtRemarks" runat="server" class="form-control" MaxLength="100"
                                        AutoPostBack="false" onkeydown="return (event.keyCode!=13)" placeholder="Enter Observation"
                                        autocomplete="off" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>                          
                             <div class="col-md-6">
                                <div class="form-group">
                                    <label>PCB Move</label>
                                    <asp:DropDownList ID="drpAction" runat="server" class="form-control">
                                        <asp:ListItem Value="0">Performence Testing</asp:ListItem>
                                        <asp:ListItem Value="1">Primary Packing</asp:ListItem>
                                        <asp:ListItem Value="2">Scraped</asp:ListItem>
                                        <asp:ListItem Value="3">PACK-IN</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group" id="divPCB" runat="server">
                        <label>Scan Barcode</label>
                        <asp:TextBox ID="txtPCBID" runat="server" class="form-control" AutoPostBack="true"
                            placeholder="Scan Barcode" autocomplete="off" MaxLength="100" OnTextChanged="txtPCBID_TextChanged"> </asp:TextBox>
                    </div>

                    <div class="form-group">
                        <div class="col-lg-3 col-xs-4">
                            <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary btn-block btn-flat" OnClick="btnReset_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
