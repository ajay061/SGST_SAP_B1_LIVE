<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="FGQuality.aspx.cs"
    Inherits="SGST.INE.Operation.Quality" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
        <section class="content-header">
            <h1>FG Operations</h1>
        </section>
        <!-- Main content -->
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">DOC Quality</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>FG Item Code : </label>
                                <asp:DropDownList ID="drpFGItemCode"
                                    runat="server" class="form-control select2" AutoPostBack="true" Style="width: 100%;">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Scan Secondary Box Barcode : </label>
                                <asp:TextBox ID="txtSecondaryBoxID" runat="server" class="form-control" placeholder="Scan SecondaryBox Barcode"
                                    autocomplete="off" AutoPostBack="true" OnTextChanged="txtSecondaryBoxID_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <asp:GridView ID="gvBoxData" class="table table-bordered table-striped" runat="server"
                                AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField HeaderText="SECONDAR BOX ID" DataField="SECONDAR_BOX_ID" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-xs-3">
                        <asp:Button ID="btnResetbarcode" runat="server" class="btn btn-primary btn-block btn-flat"
                            Text="Reset Secondary Barcode" OnClick="btnReset_Click" Width="100%" />
                    </div>
                    <div class="col-xs-3">
                        <asp:Button ID="btnCreateLot" runat="server" class="btn btn-primary btn-block btn-flat"
                            Text="Create Lot" OnClick="btnCreateLot_Click" Width="100%" />
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <b>Do you want to scan PCB ?</b>
                                <br />
                                <asp:RadioButton ID="rdPrimaryYes" GroupName="Group1" Text="Yes" Value="Yes" runat="server" Checked="true" OnCheckedChanged="Group1_CheckedChanged" AutoPostBack="true" />
                                <asp:RadioButton ID="rdPrimaryNo" GroupName="Group1" Text="No" Value="No" runat="server" OnCheckedChanged="Group1_CheckedChanged" AutoPostBack="true" />
                            </div>
                        </div>
                    </div>
                    <div class="row" id="dvPrimaryScan" visible="false" runat="server">                        
                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="form-group">
                                    <label>Scan PCB Barcode : </label>
                                    <asp:TextBox ID="txtPcb" runat="server" class="form-control" placeholder="Scan PCB Barcode" autocomplete="off" AutoPostBack="true" OnTextChanged="txtPcb_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Defects (If any) : </label>
                                <asp:DropDownList ID="drpDefect" runat="server" class="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Remarks : </label>
                                <asp:TextBox ID="txtRemarks" runat="server" class="form-control" autocomplete="off"
                                    AutoPostBack="false" onkeydown="return (event.keyCode!=13)"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label>Status : </label>
                                <asp:DropDownList ID="drpStatus"
                                    runat="server" class="form-control select2" AutoPostBack="true" Style="width: 100%;" OnSelectedIndexChanged="drpStatus_SelectedIndexChanged">
                                    <asp:ListItem>--Select--</asp:ListItem>
                                    <asp:ListItem>OK</asp:ListItem>
                                    <asp:ListItem>Rejected</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hidCode" runat="server" />
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Scanned Data : </h3>
                    <asp:Label ID="lblNumberofRecords" runat="server" Text="0"></asp:Label>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="col-xs-12">>
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:GridView ID="gvQualityData" class="table table-bordered table-striped" runat="server"
                                    AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField HeaderText="SECONDAR BOX ID" DataField="SECONDAR_BOX_ID" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                    <Columns>
                                        <asp:BoundField HeaderText="PRIMARY BOX ID" DataField="PRIMARY_BOX_ID" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                    <Columns>
                                        <asp:BoundField HeaderText="PCB ID" DataField="PCB_ID" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                    <Columns>
                                        <asp:BoundField HeaderText="REMARKS" DataField="REMARKS" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                    <Columns>
                                        <asp:BoundField HeaderText="DEFECT" DataField="DEFECT" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                    </Columns>

                                    <Columns>
                                        <asp:BoundField HeaderText="CODE" DataField="Code" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                                    </Columns>
                                    <Columns>
                                        <asp:BoundField HeaderText="STORAGE LOCATION" DataField="StorageLocation" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                                    </Columns>
                                    <Columns>
                                        <asp:BoundField HeaderText="TYPE" DataField="type" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" Visible="false" />
                                    </Columns>

                                    <Columns>
                                        <asp:BoundField HeaderText="STATUS" DataField="STATUS" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-3">
                                <asp:Button ID="btnOK" runat="server" class="btn btn-primary btn-block btn-flat"
                                    Text="OK" OnClick="btnOk_Click" Width="100%" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-3">
                                <asp:Button ID="btnReject" runat="server" class="btn btn-primary btn-block btn-flat"
                                    Text="Reject" OnClick="btnReject_Click" Width="100%" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-3">
                                <asp:Button ID="btnParialOK" runat="server" class="btn btn-primary btn-block btn-flat"
                                    Text="Partial OK" OnClick="btnParialOK_Click" Width="100%" />
                            </div>
                        </div>
                        <div class="col-xs-3">
                            <asp:Button ID="btnReset" runat="server" class="btn btn-primary btn-block btn-flat"
                                Text="Reset" OnClick="btnResetData_Click" Width="100%" />
                        </div>
                    </div>

                </div>
            </div>
        </section>
    </div>
</asp:Content>
