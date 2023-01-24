<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="mobQualityRework.aspx.cs" Inherits="SGST.INE.MOB.mobQualityRework" EnableEventValidation="false" %>

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
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Model Name : </label>
                                <asp:DropDownList ID="ddlModel_Name" runat="server" class="form-control select2"
                                    Style="width: 100%;" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlModel_Name_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Model Type :  </label>
                                <asp:Label ID="lblModelType" Enabled="false" runat="server" autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="50">                                  
                                </asp:Label>
                            </div>
                            <div class="form-group">
                                <label>Color : </label>
                                <asp:DropDownList ID="ddlColor" runat="server" class="form-control select2"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlColor_SelectedIndexChanged"
                                    Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>FG Item Code : </label>
                                <asp:Label ID="lblFGItemCode" Enabled="false" runat="server" class="form-control">                                  
                                </asp:Label>
                            </div>
                            <div class="form-group">
                                <label>Type : </label>
                                <asp:DropDownList ID="drpType" runat="server" class="form-control"
                                    AutoPostBack="true" OnSelectedIndexChanged="drpType_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="Select">Select</asp:ListItem>
                                    <asp:ListItem Value="IN" Text="IN">IN</asp:ListItem>
                                    <asp:ListItem Value="OUT" Text ="OUT">OUT</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-default">
                <div id="divOut" class="box-body" runat="server" visible="false">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Select Defect : </label>
                                <asp:DropDownList ID="drpDefect" runat="server" class="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Observation : </label>
                                <asp:TextBox ID="txtobservation" runat="server" class="form-control" AutoPostBack="false" onkeydown="return (event.keyCode!=13)"
                                    placeholder="Enter Observation" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Remarks : </label>
                                <asp:TextBox ID="txtRemarks" runat="server" class="form-control" AutoPostBack="false" onkeydown="return (event.keyCode!=13)" placeholder="Enter Observation" autocomplete="off" Rows="2" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group" id="divPCB" runat="server">
                                <label>Scan barcode : </label>
                                <asp:TextBox ID="txtPCBID" runat="server" class="form-control" AutoPostBack="true"
                                    placeholder="Scan barcode" autocomplete="off" MaxLength="200"
                                    OnTextChanged="txtPCBID_TextChanged"> </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Last Scanned</label>
                                <asp:Label ID="lbllastscanned" runat="server" placeholder="Enter Last Scanned" autocomplete="off" class="form-control" Style="width: 100%;" MaxLength="50">                                  
                                </asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="col-lg-6 col-xs-4">
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary btn-block btn-flat" OnClick="btnReset_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
